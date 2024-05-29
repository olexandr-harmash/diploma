namespace diploma.Network.Abstractions;

public class NetworkContextData {
    public object StrategyState { get; set; }
    public object ExecutorState { get; set; }
    public DateTime ExecutoionStartedAt { get; set; }
}