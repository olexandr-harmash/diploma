using AutoMapper;
using diploma.Estimation.API.Dto;
using diploma.Estimation.API.Exceptions;
using diploma.Estimation.API.Repository;
using diploma.Estimation.API.Services.Abstractions;
using diploma.HammingNetwork;
using diploma.HammingNetwork.Abstractions;
using diploma.Network.Abstractions;

namespace diploma.Estimation.API.Services;

public enum Qualification
{
    EXPERT,
    SENIOR,
    MIDDLE,
    JUNIOR
}

public class EstimateService : IEstimateService
{
    private readonly IMapper _estimationMapper;
    private readonly INetworkContext _networkContext;
    private readonly EstimationRepositoryManager _estimationRepositoryManager;

    public EstimateService(EstimationRepositoryManager estimationRepositoryManager, IMapper estimationMapper, INetworkContext networkContext)
    {
        _estimationRepositoryManager = estimationRepositoryManager;
        _estimationMapper = estimationMapper;
        _networkContext = networkContext;
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

    public async Task<Qualification> MatchEstimateToPattern(Guid estimateId, bool trackChanges)
    {
        var estimateEntity = await GetEstimateAndCheckIfExists(estimateId, trackChanges);

        var criteriaValueCollection = estimateEntity.GetCriterionValueCollection();

        return await HammingNetworkAdapter.ExecuteStrategy(_networkContext, criteriaValueCollection);
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

    // TODO: adapter classes for strategies
    private class HammingNetworkAdapter()
    {
        public static async Task<Qualification> ExecuteStrategy(INetworkContext context, double[] pattern)
        {
            CancellationTokenSource source = new CancellationTokenSource();

            var index = await context.ExecuteStrategy<double[], int, IHammingNetworkStrategy>("HammingNetworkStrategy", pattern, source.Token);

            return (Qualification)index;
        }
    }
}
