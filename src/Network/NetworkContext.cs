using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace diploma.Network;

public class NetworkContext(
    IServiceProvider serviceProvider, 
    IOptions<NetworkContextInfo> info) 
    : INetworkContext
{
    private readonly NetworkContextInfo _info = info.Value;

    public async Task<TOut> ExecuteStrategy<TIn, TOut, IStrategy>(string key, TIn model, CancellationToken cancellationToken) 
        where IStrategy : INetworkStrategy<TIn, TOut>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(model);

        //think about direct strategyType export 
        if (!_info.NetworkTypes.TryGetValue(key, out var strategyType))
        {
            StrategyNotFoundException.Throw(key);
        }
       
        var strategy = serviceProvider.GetKeyedService<IStrategy>(strategyType);

        StrategyNotFoundException.ThrowIfNull(strategy);

        return await strategy!.Execute(model, cancellationToken);
    }
}
