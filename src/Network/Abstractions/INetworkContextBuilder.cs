using Microsoft.Extensions.DependencyInjection;

namespace diploma.Network.Abstractions;

public interface INetworkContextBuilder
{
    public IServiceCollection Services { get; }
}
