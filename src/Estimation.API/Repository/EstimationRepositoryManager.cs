using diploma.Estimation.API.Infrastructure;
using diploma.Estimation.API.Repository.Abstractions;

namespace diploma.Estimation.API.Repository;

public class EstimationRepositoryManager
{
    private readonly EstimationContext _estimationContext;
    private readonly IEstimateRepository _estimateRepository;
    private readonly ICriterionRepository _criterionRepository;
    private readonly IEstimateCriterionRepository _estimateCriterionRepository;

    public EstimationRepositoryManager(
        IEstimateRepository estimateRepository,
        ICriterionRepository criterionRepository,
        IEstimateCriterionRepository estimateCriterionRepository,
        EstimationContext estimationContext)
    {
        _estimateRepository = estimateRepository;
        _estimationContext = estimationContext;
        _criterionRepository = criterionRepository;
        _estimateCriterionRepository = estimateCriterionRepository;
    }

    public IEstimateRepository Estimate => _estimateRepository;
    public ICriterionRepository Criterion => _criterionRepository;
    public IEstimateCriterionRepository EstimateCriterion => _estimateCriterionRepository;

    public Task SaveChangesAsync() => _estimationContext.SaveChangesAsync();
}
