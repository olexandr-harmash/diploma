using diploma.Estimation.API.Services.Abstractions;

namespace diploma.Estimation.API.Services;

public class EstimationServiceManager
{
    private readonly IEstimationService _estimationService;
    private readonly IEstimateService _estimateService;
    private readonly ICriterionService _criterionService;
    private readonly IEstimateCriterionService _estimateCriterionService;

    public EstimationServiceManager(
        IEstimationService estimationService,
        IEstimateService estimateService, 
        ICriterionService criterionService,
        IEstimateCriterionService estimateCriterionService)
    {
        _estimationService = estimationService;
        _estimateService = estimateService;
        _criterionService = criterionService;
        _estimateCriterionService = estimateCriterionService;
    }

    public IEstimationService Estimation => _estimationService;
    public IEstimateService Estimate => _estimateService;
    public ICriterionService Criterion => _criterionService;
    public IEstimateCriterionService EstimateCriterion => _estimateCriterionService;
}
