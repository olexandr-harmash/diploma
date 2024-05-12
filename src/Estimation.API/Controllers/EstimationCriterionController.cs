using diploma.Estimation.API.Dto;
using diploma.Estimation.API.Services;
using diploma.Projects.API.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace diploma.Estimation.API.Controllers;

[ApiController]
[Route("api/estimation/{estimationId:guid}")]
public class EstimationCriterionController
{
    private readonly EstimationServices _estimationServices;
    private readonly EstimationServiceManager _estimationServiceManager;

    public EstimationCriterionController(EstimationServices estimationServices)
    {
        _estimationServices = estimationServices;
        _estimationServiceManager = estimationServices.estimationServiceManager;
    }

    [HttpGet("criterion/{criterionId}", Name = "GetEstimateCriterion")]
    public async Task<IResult> GetEstimateCriterion(Guid estimationId, Guid criterionId)
    {
        var estimateDto = await _estimationServiceManager.EstimateCriterion.GetEstimateCriterion(estimationId, criterionId, false);

        return TypedResults.Ok(estimateDto);
    }

    [HttpPost("criterion/{criterionId:guid}")]
    [ServiceFilter(typeof(ValidationAttributeFilter))]
    public async Task<IResult> CreateEstimateCriterion(Guid estimationId, Guid criterionId, [FromBody] EstimateCriterionDtoForCreate estimateForCreate)
    {
        var estimateCriterionDto = await _estimationServiceManager.EstimateCriterion.CreateEstimateCriterion(estimationId, criterionId, estimateForCreate, false);

        return Results.CreatedAtRoute(
            "GetEstimateCriterion",
            new
            {
                estimationId = estimateCriterionDto.EstimateId,
                criterionId = estimateCriterionDto.CriterionId,
            },
            estimateCriterionDto);
    }

    [HttpPut("criterion/{criterionId:guid}")]
    [ServiceFilter(typeof(ValidationAttributeFilter))]
    public async Task<IResult> UpdateEstimateCriterion(Guid estimationId, Guid criterionId, [FromBody] EstimateCriterionDtoForUpdate estimateForUpdate)
    {
        await _estimationServiceManager.EstimateCriterion.UpdateEstimateCriterion(estimationId, criterionId, estimateForUpdate, false);

        return TypedResults.NoContent();
    }

    [HttpDelete("criterion/{criterionId:guid}")]
    public async Task<IResult> DeleteEstimateCriterion(Guid estimationId, Guid criterionId)
    {
        await _estimationServiceManager.EstimateCriterion.DeleteEstimateCriterion(estimationId, criterionId, false);

        return TypedResults.NoContent();
    }
}
