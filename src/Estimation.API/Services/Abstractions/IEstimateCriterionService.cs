using diploma.Estimation.API.Dto;

namespace diploma.Estimation.API.Services.Abstractions;

public interface IEstimateCriterionService
{
    Task<IEnumerable<EstimateCriterionDto>> FetchEstimateCriterion(Guid estimateId, IEnumerable<Guid> criterionIds, bool trackChanges);
    Task<EstimateCriterionDto> GetEstimateCriterion(Guid estimateId, Guid criterionId, bool trackChanges);
    Task<EstimateCriterionDto> CreateEstimateCriterion(Guid estimateId, Guid criterionId, EstimateCriterionDtoForCreate estimate, bool trackChanges);
    Task DeleteEstimateCriterion(Guid estimateId, Guid criterionId, bool trackChanges);
    Task UpdateEstimateCriterion(Guid estimateId, Guid criterionId, EstimateCriterionDtoForUpdate estimateCriterion, bool trackChanges);
}
