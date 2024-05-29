namespace diploma.HammingNetwork.Abstractions;

public interface IHammingNetworkStrategy : INetworkStrategy<HammingNetworkStrategyModel, int> {}

public class HammingNetworkState 
{
    public HammingNetworkState(int T, double braking, double[] layer0, double[] layer1)
    {
        T = T;
        Braking = braking;
        Layer0 = layer0;
        Layer1 = layer1;
    }

    public int T { get; set; }
    public double Braking { get; set; }
    public double[] Layer0 { get; set; }
    public double[] Layer1 { get; set; }
};
