using diploma.Estimation.API.Dto;
using static diploma.Estimation.API.Services.EstimationService;

namespace diploma.Estimation.API.Services.Abstractions;

public interface IEstimateService
{
    Task<EstimateDto> GetEstimateById(Guid id, bool trackChanges);
    Task<EstimateDto> CreateEstimate(EstimateDtoForCreate estimate);
    Task UpdateEstimate(Guid id, EstimateDtoForUpdate estimate, bool trackChanges);
    Task DeleteEstimate(Guid id, bool trackChanges);
    Task<Qualification> MatchEstimateToPattern(Guid estimateId, bool trackChanges);
}
