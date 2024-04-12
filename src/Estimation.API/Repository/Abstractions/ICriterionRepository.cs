namespace diploma.Estimation.API.Repository.Abstractions;

public interface ICriterionRepository
{
    Task<Criterion?> GetCriterionById(Guid id, bool trackChanges);
}
