namespace diploma.Network.Abstractions;

public interface INetworkContext
{
    Task<TOut> ExecuteStrategy<TIn, TOut, TStrategy>(string key, TIn model, CancellationToken cancellationToken)
        where TStrategy : INetworkStrategy<TIn, TOut>;
}
