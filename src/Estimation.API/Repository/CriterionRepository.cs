using Microsoft.EntityFrameworkCore;
using diploma.Estimation.API.Infrastructure;
using diploma.Estimation.API.Repository.Abstractions;

namespace diploma.Estimation.API.Repository;

public class CriterionRepository : RepositoryBase<Criterion, EstimationContext>, ICriterionRepository
{
    public CriterionRepository(EstimationContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<Criterion?> GetCriterionById(Guid id, bool trackChanges) =>
        await FindByCondition(x => id.Equals(x.Id), trackChanges).SingleOrDefaultAsync();
}
