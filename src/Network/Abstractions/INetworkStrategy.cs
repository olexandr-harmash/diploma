namespace diploma.Network.Abstractions;

public interface INetworkStrategy<TIn, TOut>
{
    Task<TOut> Execute(TIn input, CancellationToken cancellationToken);
}
