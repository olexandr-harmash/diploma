using AutoMapper;
using diploma.Estimation.API.Dto;
using diploma.Estimation.API.Exceptions;
using diploma.Estimation.API.Repository;
using diploma.Estimation.API.Services.Abstractions;

namespace diploma.Estimation.API.Services;

public class CriterionService : ICriterionService
{
    private readonly IMapper _estimationMapper;
    private readonly EstimationRepositoryManager _estimationRepositoryManager;

    public CriterionService(EstimationRepositoryManager estimationRepositoryManager, IMapper estimationMapper)
    {
        _estimationRepositoryManager = estimationRepositoryManager;
        _estimationMapper = estimationMapper;
    }

    public async Task<CriterionDto> GetCriterionById(Guid id, bool trackChanges)
    {
        var criterionEntity = _estimationRepositoryManager.Criterion.GetCriterionById(id, trackChanges);

        if (criterionEntity == null)
        {
            throw new CriterionNotFoundException(id);
        }

        var criterionToReturn = _estimationMapper.Map<CriterionDto>(criterionEntity);

        return criterionToReturn;
    }
}
