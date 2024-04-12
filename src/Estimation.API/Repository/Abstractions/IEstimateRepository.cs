namespace diploma.Estimation.API.Repository.Abstractions;

public interface IEstimateRepository
{
    Task<Estimate?> GetEstimateById(Guid id, bool trackChanges);
    void CreateEstimate(Estimate estimate);
    void DeleteEstimate(Estimate estimate);
}
