using AutoMapper;
using diploma.Estimation.API.Dto;
using diploma.Estimation.API.Exceptions;
using diploma.Estimation.API.Repository;
using diploma.Estimation.API.Services.Abstractions;

namespace diploma.Estimation.API.Services;

public class EstimateService : IEstimateService
{
    private readonly IMapper _estimationMapper;
    private readonly EstimationRepositoryManager _estimationRepositoryManager;

    public EstimateService(EstimationRepositoryManager estimationRepositoryManager, IMapper estimationMapper)
    {
        _estimationRepositoryManager = estimationRepositoryManager;
        _estimationMapper = estimationMapper;
    }

    public async Task<EstimateDto> CreateEstimate(EstimateDtoForCreate estimate)
    {
        var estimateEntity = _estimationMapper.Map<Estimate>(estimate);
            
        _estimationRepositoryManager.Estimate.CreateEstimate(estimateEntity);

        await _estimationRepositoryManager.SaveChangesAsync();

        var estimateToReturn = _estimationMapper.Map<EstimateDto>(estimateEntity);

        return estimateToReturn;
    }

    public async Task DeleteEstimate(Guid id, bool trackChanges)
    {
        var estimateEntity = await GetEstimateAndCheckIfExists(id, trackChanges);

        _estimationRepositoryManager.Estimate.DeleteEstimate(estimateEntity);

        await _estimationRepositoryManager.SaveChangesAsync();
    }

    public async Task<EstimateDto> GetEstimateById(Guid id, bool trackChanges)
    {
        var estimateEntity = await GetEstimateAndCheckIfExists(id, trackChanges);

        var estimateForReturn = _estimationMapper.Map<EstimateDto>(estimateEntity);

        return estimateForReturn;
    }

    public async Task UpdateEstimate(Guid id, EstimateDtoForUpdate estimate, bool trackChanges)
    {
        var estimateEntity = await GetEstimateAndCheckIfExists(id, trackChanges);

        _estimationMapper.Map(estimate, estimateEntity);

        await _estimationRepositoryManager.SaveChangesAsync();
    }

    private async Task<Estimate> GetEstimateAndCheckIfExists(Guid id, bool trackChanges)
    {
        var estimateEntity = await _estimationRepositoryManager.Estimate.GetEstimateById(id, trackChanges);

        if (estimateEntity is null)
        {
            throw new EstimateNotFoundException(id);
        }

        return estimateEntity;
    }
}
