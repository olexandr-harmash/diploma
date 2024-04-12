using Polly;
using System.Diagnostics.CodeAnalysis;

namespace diploma.EventBusRabbitMQ
{
    public class RabbitMQEventBus(
        ILogger<RabbitMQEventBus> logger,
        IOptions<EventBusOptions> options,
        IOptions<EventBusSubscriptionInfo> subscriptionOptions,
        IServiceProvider serviceProvider 
        ) : IEventBus, IDisposable, IHostedService
    {
        private const string ExchangeName = "product_event_bus";

        private readonly int _retryCount = options.Value.RetryCount;
        private string _queueName = options.Value.SubscriptionClientName;
        private readonly EventBusSubscriptionInfo _subscriptionInfo = subscriptionOptions.Value;
        private IConnection _rabbitMQConnection;

        private IModel _consumerChannel;

        public Task PublishAsync(IntegrationEvent @event)
        {
            var policy = Policy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetryAsync(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            var routingKey = @event.GetType().Name;

            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, routingKey);
            }

            using var channel = _rabbitMQConnection?.CreateModel() ?? throw new InvalidOperationException("RabbitMQ connection is not open");

            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);
            }

            channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");

            var body = SerializeMessage(@event);

            return policy.ExecuteAsync(() =>
            {
                var properties = channel.CreateBasicProperties();
                // persistent
                properties.DeliveryMode = 2;

                if (logger.IsEnabled(LogLevel.Trace))
                {
                    logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);
                }

                try
                {
                    channel.BasicPublish(
                        exchange: ExchangeName,
                        routingKey: routingKey,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);

                    return Task.CompletedTask;
                }
                catch(Exception ex)
                {
                    logger.LogError("Publishing event to RabbitMQ error: {Error}", ex);

                    throw;
                }
            });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _ = Task.Factory.StartNew(() =>
            {
                try
                {
                    logger.LogInformation("Starting RabbitMQ connection on a background thread");

                    _rabbitMQConnection = serviceProvider.GetRequiredService<IConnection>();
                    if (!_rabbitMQConnection.IsOpen)
                    {
                        return;
                    }

                    _consumerChannel = _rabbitMQConnection.CreateModel();

                    _consumerChannel.CallbackException += (sender, ea) =>
                    {
                        logger.LogWarning(ea.Exception, "Error with RabbitMQ consumer channel");
                    };

                    _consumerChannel.ExchangeDeclare(exchange: ExchangeName, type: "direct");

                    _consumerChannel.QueueDeclare(
                                     queue: _queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                    var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                    consumer.Received += OnMessageReceived;

                    _consumerChannel.BasicConsume(
                        queue: _queueName,
                        autoAck: false,
                        consumer: consumer);

                    foreach (var (eventName, _) in _subscriptionInfo.EventTypes)
                    {
                        _consumerChannel.QueueBind(
                            queue: _queueName,
                            exchange: ExchangeName,
                            routingKey: eventName);
                    }
                } catch(Exception ex)
                {
                    logger.LogError(ex, "Error starting RabbitMQ connection");
                }
            }, TaskCreationOptions.LongRunning);

            return Task.CompletedTask;
        }

        private async Task OnMessageReceived(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Error Processing message \"{Message}\"", message);
            }

            // Even on exception we take the message off the queue.
            // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
            // For more information see: https://www.rabbitmq.com/dlx.html
            _consumerChannel?.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);
            }

            await using var scope = serviceProvider.CreateAsyncScope();

            if (!_subscriptionInfo.EventTypes.TryGetValue(eventName, out var eventType))
            {
                logger.LogWarning("Unable to resolve event type for event name {EventName}", eventName);
                return;
            }

            foreach (var handler in scope.ServiceProvider.GetKeyedServices<IIntegrationEventHandler>(eventType))
            {
                var integrationEvent = DeserializeMessage(message, eventType);

                await handler.Handle(integrationEvent);
            }
        }

        [UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode",
        Justification = "The 'JsonSerializer.IsReflectionEnabledByDefault' feature switch, which is set to false by default for trimmed .NET apps, ensures the JsonSerializer doesn't use Reflection.")]
        [UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = "See above.")]
        private IntegrationEvent? DeserializeMessage(string message, Type eventType)
        {
            return JsonSerializer.Deserialize(message, eventType, _subscriptionInfo.JsonSerializerOptions) as IntegrationEvent;
        }

        [UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode",
            Justification = "The 'JsonSerializer.IsReflectionEnabledByDefault' feature switch, which is set to false by default for trimmed .NET apps, ensures the JsonSerializer doesn't use Reflection.")]
        [UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = "See above.")]
        private byte[] SerializeMessage(IntegrationEvent @event)
        {
            return JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), _subscriptionInfo.JsonSerializerOptions);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();
        }
    }
}
