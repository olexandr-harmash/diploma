namespace diploma.Estimation.API.Services.Abstractions;

public interface IEstimationService
{
    Task<Qualification> TestPattern(double[] pattern);
}
