using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace diploma.Network;

public class NetworkContext(
    IServiceProvider serviceProvider, 
    IOptions<NetworkContextInfo> info) 
    : INetworkContext
{
    private readonly NetworkContextInfo _info = info.Value;

    public async Task<TOut> ExecuteStrategy<TIn, TOut, TStrategy>(string key, TIn model, CancellationToken cancellationToken) 
        where TStrategy : INetworkStrategy<TIn, TOut>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(model);


        if (!_info.NetworkTypes.TryGetValue(key, out var strategyType))
        {
            StrategyNotFoundException.Throw(key);
        }
       
        var strategy = serviceProvider.GetKeyedService<TStrategy>(strategyType);

        return await strategy!.Execute(model, cancellationToken);
    }
}
