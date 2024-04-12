using Microsoft.EntityFrameworkCore;
using diploma.Estimation.API.Infrastructure;
using diploma.Estimation.API.Model;
using diploma.Estimation.API.Repository.Abstractions;

namespace diploma.Estimation.API.Repository;

public class EstimateRepository : RepositoryBase<Estimate, EstimationContext>, IEstimateRepository
{
    public EstimateRepository(EstimationContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateEstimate(Estimate estimate) => Create(estimate);

    public void DeleteEstimate(Estimate estimate) => Delete(estimate);

    public async Task<Estimate?> GetEstimateById(Guid id, bool trackChanges) =>
        await FindByCondition(x => id.Equals(x.Id), trackChanges).SingleOrDefaultAsync();
}
