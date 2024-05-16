namespace diploma.Network.Abstractions;

public interface INetworkContext
{
    Task<TOut> ExecuteStrategy<TIn, TOut, IStrategy>(string key, TIn model, CancellationToken cancellationToken)
        where IStrategy : INetworkStrategy<TIn, TOut>;
}
