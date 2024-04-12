using diploma.Estimation.API.Dto;

namespace diploma.Estimation.API.Services.Abstractions;

public interface ICriterionService
{
    Task<CriterionDto> GetCriterionById(Guid id, bool trackChanges);
}
