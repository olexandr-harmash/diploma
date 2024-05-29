namespace diploma.Network.Abstractions;

public interface INetworkStrategy<TIn, TOut>
{
    //TODO: think about best practies with special types or based on some base class
    object GetState();
    Task<TOut> Execute(TIn input, CancellationToken cancellationToken);
}