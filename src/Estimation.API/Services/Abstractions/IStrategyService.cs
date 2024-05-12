namespace diploma.Estimation.API.Services.Abstractions;

public interface IStrategyService
{
    Task Train();
    Task<int> TestPattern(double[] pattern);
    int GetZeroLayerSize();
    int GetFirstLayerSize();
    int GetCountOfPatterns();
    void SetPatterns(double[][] patterns);
}
