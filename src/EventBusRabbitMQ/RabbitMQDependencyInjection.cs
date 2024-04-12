
namespace diploma.EventBusRabbitMQ
{
    public static class RabbitMQDependencyInjection
    {
        private const string SectionName = "EventBus";

        public static IEventBusBuilder AddRabbitMqEventBus(this IHostApplicationBuilder builder)
        {
            builder.AddRabbitMQ(SectionName, configureConnectionFactory: factory =>
            {
                ((ConnectionFactory)factory).DispatchConsumersAsync = true;
            });

            builder.Services.Configure<EventBusOptions>(builder.Configuration.GetSection(SectionName));

            builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();
            builder.Services.AddSingleton<IHostedService>(sp => (RabbitMQEventBus)sp.GetRequiredService<IEventBus>());

            return new EventBusBuilder(builder.Services);
        }

        private class EventBusBuilder(IServiceCollection services) : IEventBusBuilder
        {
            public IServiceCollection Services => services;
        }
    }
}
