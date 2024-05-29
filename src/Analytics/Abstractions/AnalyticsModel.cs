namespace diploma.Analytics.Abstractions;

public class AnalyticsModel 
{
    public object StrategyContextData { get; set; }
    public DateTime StrategyExecutedAt { get; set; }
    public TimeSpan StrategyExecutionDuration { get; set; }
    public object StrategyResult { get; set; }
    public object StrategyState { get; set; }
}