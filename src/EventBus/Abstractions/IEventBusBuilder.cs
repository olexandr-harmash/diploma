using Microsoft.Extensions.DependencyInjection;

namespace diploma.EventBus.Abstractions
{
    public interface IEventBusBuilder
    {
        public IServiceCollection Services { get; }
    }
}
