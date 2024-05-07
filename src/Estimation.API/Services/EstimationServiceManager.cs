using diploma.Estimation.API.Services.Abstractions;

namespace diploma.Estimation.API.Services;

public class EstimationServiceManager
{
    private readonly IEstimateService _estimateService;
    private readonly ICriterionService _criterionService;
    private readonly IEstimateCriterionService _estimateCriterionService;

    public EstimationServiceManager(
        IEstimateService estimateService, 
        ICriterionService criterionService,
        IEstimateCriterionService estimateCriterionService)
    {
        _estimateService = estimateService;
        _criterionService = criterionService;
        _estimateCriterionService = estimateCriterionService;
    }

    public IEstimateService Estimate => _estimateService;
    public ICriterionService Criterion => _criterionService;
    public IEstimateCriterionService EstimateCriterion => _estimateCriterionService;
}
