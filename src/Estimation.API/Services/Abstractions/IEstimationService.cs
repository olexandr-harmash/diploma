using static diploma.Estimation.API.Services.EstimationService;

namespace diploma.Estimation.API.Services.Abstractions;

public interface IEstimationService
{
    public Qualification MatchEstimateToPattern(Estimate estimate);
}
