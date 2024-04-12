using diploma.Estimation.API.Services.Abstractions;

namespace diploma.Estimation.API.Services;

public class EstimationServiceManager
{
    private readonly IEstimateCriterionService _estimateCriterionService;
    private readonly IEstimateService _estimateService;

    public EstimationServiceManager(IEstimateService estimateService, IEstimateCriterionService estimateCriterionService)
    {
        _estimateCriterionService = estimateCriterionService;
        _estimateService = estimateService;
    }

    public IEstimateService Estimate => _estimateService;

    public IEstimateCriterionService EstimateCriterion => _estimateCriterionService;
}
