using AutoMapper;
using diploma.Estimation.API.Controllers;
using diploma.Estimation.API.Dto;
using diploma.Estimation.API.Repository;
using diploma.Estimation.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.API.Controllers;

[ApiController]
[Route("/api/estimation")]
public class EstimationController : ControllerBase
{
    private readonly IMapper _estimationMapper;
    private readonly EstimationRepositoryManager _repositoryManager;
    private readonly EstimationServices _estimationServices;
    private readonly EstimationServiceManager _estimationServiceManager;

    public EstimationController(EstimationServices estimationServices, EstimationRepositoryManager repositoryManager, IMapper estimationMapper)
    {
        _repositoryManager = repositoryManager;
        _estimationMapper = estimationMapper;
        _estimationServices = estimationServices;
        _estimationServiceManager = estimationServices.estimationServiceManager;
    }

    [HttpGet("{estimationId:guid}/predict")]
    public async Task<IResult> GetEstimation(Guid estimationId)
    {
        var ent = await _repositoryManager.Estimate.GetEstimateById(estimationId, false);

        var res = _estimationServiceManager.Estimation.MatchEstimateToPattern(ent);

        return TypedResults.Ok(res.ToString());
    }

    [HttpGet("{estimationId:guid}", Name = "GetEstimateById")]
    public async Task<IResult> GetEstimateById(Guid estimationId)
    {
        var estimateDto = await _estimationServiceManager.Estimate.GetEstimateById(estimationId, false);

        return TypedResults.Ok(estimateDto);
    }

    [HttpPost]
    public async Task<IResult> CreateEstimate([FromBody] EstimateDtoForCreate estimateForCreate)
    {
        var estimateDto = await _estimationServiceManager.Estimate.CreateEstimate(estimateForCreate);

        return Results.CreatedAtRoute("GetEstimateById", new { estimationId = estimateDto.Id }, estimateDto);
    }

    [HttpPut]
    public async Task<IResult> UpdateEstimate(Guid estimationId, [FromBody] EstimateDtoForUpdate estimateForUpdate)
    {
        await _estimationServiceManager.Estimate.UpdateEstimate(estimationId, estimateForUpdate, false);

        return TypedResults.NoContent();
    }

    [HttpDelete("{estimationId:guid}")]
    public async Task<IResult> DeleteEstimate(Guid estimationId)
    {
        await _estimationServiceManager.Estimate.DeleteEstimate(estimationId, false);

        return TypedResults.NoContent();
    }
}
