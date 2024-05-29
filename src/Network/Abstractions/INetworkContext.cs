namespace diploma.Network.Abstractions;

public interface INetworkContext
{
    Task<TOut> ExecuteStrategy<TIn, TOut, TMd, ISt>(string key, TIn model, TMd metadata, CancellationToken cancellationToken)
        where TMd : class
        where ISt : INetworkStrategy<TIn, TOut>;
}
