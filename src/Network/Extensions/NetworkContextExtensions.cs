using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace diploam.Network.Extensions;

public static class NetworkContextExtensions
{
    public static INetworkContextBuilder AddNetworkStrategy<I, T>(this INetworkContextBuilder builder)
            where I : class
            where T : class, I
    {
        builder.Services.AddKeyedSingleton<I, T>(typeof(T));

        builder.Services.Configure<NetworkContextInfo>(o => {
            o.NetworkTypes[typeof(T).Name] = typeof(T);       
        });

        return builder;
    }
}
