namespace diploma.EventBusRabbitMQ.Abstractions
{
    public class RabbitMQClientSettings
    {
        public bool HealthChecks { get; set; }
        public string ConnectionString { get; set; }
        public int MaxConnectRetryCount { get; set; } = 10;
    }
}
