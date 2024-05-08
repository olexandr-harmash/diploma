using diploma.Estimation.API.Dto;

namespace diploma.Estimation.API.Repository.Abstractions;

public interface ICriterionRepository
{
    Task<Criterion?> GetCriterionById(Guid id, bool trackChanges);
    Task<IEnumerable<Criterion>> FetchFullCriterionCollection(bool trackChanges);
    void CreateCriterionCollection(IEnumerable<Criterion> criterionCollection, bool trackChanges);
}
