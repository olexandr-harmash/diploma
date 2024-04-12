using AutoMapper;
using diploma.Estimation.API.Dto;
using diploma.Estimation.API.Exceptions;
using diploma.Estimation.API.Repository;
using diploma.Estimation.API.Services.Abstractions;

namespace diploma.Estimation.API.Services;

public class EstimateCriterionService : IEstimateCriterionService
{
    private readonly IMapper _estimationMapper;
    private readonly EstimationRepositoryManager _estimationRepositoryManager;

    public EstimateCriterionService(EstimationRepositoryManager estimationRepositoryManager, IMapper estimationMapper)
    {
        _estimationRepositoryManager = estimationRepositoryManager;
        _estimationMapper = estimationMapper;
    }

    public async Task<EstimateCriterionDto> CreateEstimateCriterion(Guid estimateId, Guid criterionId, EstimateCriterionDtoForCreate estimate)
    {
        var estimateCriterionEntity = _estimationMapper.Map<EstimateCriterion>(estimate);

        _estimationRepositoryManager.EstimateCriterion.CreateEstimateCriterion(estimateCriterionEntity);

        await _estimationRepositoryManager.SaveChangesAsync();

        var estimateCriterionToReturn = _estimationMapper.Map<EstimateCriterionDto>(estimateCriterionEntity);

        return estimateCriterionToReturn;
    }

    public async Task DeleteEstimateCriterion(Guid estimateId, Guid criterionId, bool trackChanges)
    {
        await GetEstimateAndCheckIfExists(estimateId, trackChanges);

        await GetCriterionAndCheckIfExists(criterionId, trackChanges);

        var estimateCriterionEntity = await GetEstimateCriterionAndCheckIfExists(estimateId, criterionId, trackChanges);

        _estimationRepositoryManager.EstimateCriterion.DeleteEstimateCriterion(estimateCriterionEntity);

        await _estimationRepositoryManager.SaveChangesAsync();
    }

    public async Task<IEnumerable<EstimateCriterionDto>> FetchEstimateCriterion(Guid estimateId, IEnumerable<Guid> criterionIds, bool trackChanges)
    {
        await GetEstimateAndCheckIfExists(estimateId, trackChanges);

        var estimateCriterionCollectionEntity = await _estimationRepositoryManager.EstimateCriterion.FetchEstimateCriterion(estimateId, criterionIds, trackChanges);

        if (estimateCriterionCollectionEntity.Count() != criterionIds.Count())
        {
            throw new EstimateCriterionNotFoundException(estimateId, criterionIds);
        }

        var estimateCriterionCollectionToReturn = _estimationMapper.Map<IEnumerable<EstimateCriterionDto>>(estimateCriterionCollectionEntity);

        return estimateCriterionCollectionToReturn;
    }

    public async Task<EstimateCriterionDto> GetEstimateCriterion(Guid estimateId, Guid criterionId, bool trackChanges)
    {
        await GetEstimateAndCheckIfExists(estimateId, trackChanges);

        await GetCriterionAndCheckIfExists(criterionId, trackChanges);

        var estimateCriterionEntity = await GetEstimateCriterionAndCheckIfExists(estimateId, criterionId, trackChanges);

        var estimateCriterionForReturn = _estimationMapper.Map<EstimateCriterionDto>(estimateCriterionEntity);

        return estimateCriterionForReturn;
    }

    public async Task UpdateEstimateCriterion(Guid estimateId, Guid criterionId, EstimateCriterionDtoForUpdate estimateCriterion, bool trackChanges)
    {
        await GetEstimateAndCheckIfExists(estimateId, trackChanges);

        await GetCriterionAndCheckIfExists(criterionId, trackChanges);

        var estimateCriterionEntity = await GetEstimateCriterionAndCheckIfExists(estimateId, criterionId, trackChanges);

        _estimationMapper.Map(estimateCriterion, estimateCriterionEntity);

        await _estimationRepositoryManager.SaveChangesAsync();
    }

    private async Task<EstimateCriterion> GetEstimateCriterionAndCheckIfExists(Guid estimateId, Guid criterionId, bool trackChanges)
    {
        var estimateCriterionEntity = await _estimationRepositoryManager.EstimateCriterion.GetEstimateCriterion(estimateId, criterionId, trackChanges);

        if (estimateCriterionEntity == null)
        {
            throw new EstimateCriterionNotFoundException(estimateId, criterionId);
        }

        return estimateCriterionEntity;
    }

    private async Task<Estimate> GetEstimateAndCheckIfExists(Guid estimateId, bool trackChanges)
    {
        var estimateEntity = await _estimationRepositoryManager.Estimate.GetEstimateById(estimateId, trackChanges);

        if (estimateEntity == null)
        {
            throw new EstimateNotFoundException(estimateId);
        }

        return estimateEntity;
    }

    private async Task<Criterion> GetCriterionAndCheckIfExists(Guid criterionId, bool trackChanges)
    {
        var criterionEntity = await _estimationRepositoryManager.Criterion.GetCriterionById(criterionId, trackChanges);

        if (criterionEntity == null)
        {
            throw new CriterionNotFoundException(criterionId);
        }

        return criterionEntity;
    }
}
