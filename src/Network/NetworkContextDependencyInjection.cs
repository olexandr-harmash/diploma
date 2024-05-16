using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace diploma.Network;

public static class NetworkContextDependencyInjection
{
    public static INetworkContextBuilder AddNetworkContext(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<INetworkContext, NetworkContext>();

        return new NetworkContextBuilder(builder.Services);
    }

    private class NetworkContextBuilder(IServiceCollection services) : INetworkContextBuilder
    {
        public IServiceCollection Services => services;
    }
}
