using diploma.Estimation.API.Controllers;
using diploma.Estimation.API.Dto;
using diploma.Estimation.API.Services;
using diploma.Projects.API.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.API.Controllers;

[ApiController]
[Route("/api/estimation")]
public class EstimationController : ControllerBase
{
    private readonly EstimationServices _estimationServices;
    private readonly EstimationServiceManager _estimationServiceManager;

    public EstimationController(EstimationServices estimationServices)
    {
        _estimationServices = estimationServices;
        _estimationServiceManager = estimationServices.estimationServiceManager;
    }

    [HttpGet("{estimationId:guid}/qualification/{strategy}")]
    public async Task<IResult> GetEstimation(Guid estimationId, string strategy)
    {
        var qualification = await _estimationServiceManager.Estimate.MatchEstimateToPattern(estimationId, strategy, false);

        return TypedResults.Ok(qualification.ToString());
    }

    [HttpGet("{estimationId:guid}", Name = "GetEstimateById")]
    public async Task<IResult> GetEstimateById(Guid estimationId)
    {
        var estimateDto = await _estimationServiceManager.Estimate.GetEstimateById(estimationId, false);

        return TypedResults.Ok(estimateDto);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationAttributeFilter))]
    public async Task<IResult> CreateEstimate([FromBody] EstimateDtoForCreate estimateForCreate)
    {
        var estimateDto = await _estimationServiceManager.Estimate.CreateEstimate(estimateForCreate);

        return Results.CreatedAtRoute("GetEstimateById", new { estimationId = estimateDto.Id }, estimateDto);
    }

    [HttpPut]
    [ServiceFilter(typeof(ValidationAttributeFilter))]
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
