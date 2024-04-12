namespace diploma.Estimation.API.Repository.Abstractions;

public interface IEstimateCriterionRepository
{
    Task<IEnumerable<EstimateCriterion>> FetchEstimateCriterion(Guid estimateId, IEnumerable<Guid> criterionIds, bool trackChanges);
    Task<EstimateCriterion?> GetEstimateCriterion(Guid estimateId, Guid criterionId, bool trackChanges);
    void CreateEstimateCriterion(EstimateCriterion estimateCriterion);
    void DeleteEstimateCriterion(EstimateCriterion estimateCriterion);
}
