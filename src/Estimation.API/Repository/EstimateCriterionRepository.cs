using diploma.Estimation.API.Infrastructure;
using diploma.Estimation.API.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace diploma.Estimation.API.Repository;

public class EstimateCriterionRepository : RepositoryBase<EstimateCriterion, EstimationContext>, IEstimateCriterionRepository
{
    public EstimateCriterionRepository(EstimationContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateEstimateCriterion(EstimateCriterion estimateCriterion) => Create(estimateCriterion);

    public void DeleteEstimateCriterion(EstimateCriterion estimateCriterion) => Delete(estimateCriterion);

    public async Task<IEnumerable<EstimateCriterion>> FetchEstimateCriterion(Guid estimateId, IEnumerable<Guid> criterionIds, bool trackChanges) =>
        await FindByCondition(x => estimateId.Equals(x.Id) && criterionIds.Contains(x.CriterionId), trackChanges).ToListAsync();

    public async Task<EstimateCriterion?> GetEstimateCriterion(Guid estimateId, Guid criterionId, bool trackChanges) =>
        await FindByCondition(x => estimateId.Equals(x.EstimateId) && criterionId.Equals(x.CriterionId), trackChanges).SingleOrDefaultAsync();
}
