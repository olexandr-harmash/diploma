namespace diploma.Estimation.API.Services.Abstractions;

public interface IEstimationService
{
    int TestPattern(double[] pattern);
    int GetZeroLayerSize();
    int GetFirstLayerSize();
    int GetCountOfPatterns();
}
