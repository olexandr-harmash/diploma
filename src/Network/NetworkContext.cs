using System.Text.Json;
using diploma.Analytics.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace diploma.Network;

public class NetworkContext(
    IServiceProvider serviceProvider, 
    IOptions<NetworkContextInfo> info) 
    : INetworkContext
{
    private readonly NetworkContextInfo _info = info.Value;

    //TODO: create analyticsModel in extensions method by handling context live-time events
    public async Task<TOut> ExecuteStrategy<TIn, TOut, TMd, ISt>(string key, TIn model, TMd metadata, CancellationToken cancellationToken) 
        where TMd : class
        where ISt : INetworkStrategy<TIn, TOut>
    { 
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(metadata);
        
        var strategy = _getStrategy<TIn, TOut, ISt>(key);

        NetworkContextData contextData = new NetworkContextData
        {    
            ExecutorState = metadata,
            StrategyState = strategy.GetState(),
            ExecutoionStartedAt = DateTime.Now,
        };

        TOut strategyResult = await strategy!.Execute(model, cancellationToken);

        AnalyticsModel analysticsModel = new AnalyticsModel
        {
            StrategyContextData = contextData.ExecutorState,
            StrategyExecutedAt = contextData.ExecutoionStartedAt,
            StrategyExecutionDuration = DateTime.Now - contextData.ExecutoionStartedAt,
            StrategyResult = strategyResult,
            StrategyState = strategy.GetState()
        };

        Console.WriteLine(JsonSerializer.Serialize(analysticsModel));

        return strategyResult;
    }


    private ISt _getStrategy<TIn, TOut, ISt>(string key) 
        where ISt : INetworkStrategy<TIn, TOut>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        if (!_info.NetworkTypes.TryGetValue(key, out var strategyType))
        {
            StrategyNotFoundException.Throw(key);
        }
       
        var strategy = serviceProvider.GetKeyedService<ISt>(strategyType);

        StrategyNotFoundException.ThrowIfNull(strategy);

        return  strategy;
    }
}