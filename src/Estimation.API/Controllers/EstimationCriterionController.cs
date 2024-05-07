using diploma.Estimation.API.Dto;
using diploma.Estimation.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace diploma.Estimation.API.Controllers;

[ApiController]
[Route("api/estimation/{estimationId:guid}/criterion/{criterionId:guid}")]
public class EstimationCriterionController
{
    private readonly EstimationServices _estimationServices;
    private readonly EstimationServiceManager _estimationServiceManager;

    public EstimationCriterionController(EstimationServices estimationServices)
    {
        _estimationServices = estimationServices;
        _estimationServiceManager = estimationServices.estimationServiceManager;
    }

    [HttpGet(Name = "GetEstimateCriterion")]
    public async Task<IResult> GetEstimateCriterion(Guid estimateId, Guid criterionId)
    {
        var estimateDto = await _estimationServiceManager.EstimateCriterion.GetEstimateCriterion(estimateId, criterionId, false);

        return TypedResults.Ok(estimateDto);
    }

    [HttpPost]
    public async Task<IResult> CreateEstimateCriterion(Guid estimateId, Guid criterionId, [FromBody] EstimateCriterionDtoForCreate estimateForCreate)
    {
        var estimateCriterionDto = await _estimationServiceManager.EstimateCriterion.CreateEstimateCriterion(estimateId, criterionId, estimateForCreate);

        return Results.CreatedAtRoute(
            "GetEstimateCriterion", 
            new { 
                estimateId = estimateCriterionDto.EstimateId,
                criterionId = estimateCriterionDto.CriterionId,
            },
            estimateCriterionDto);
    }

    [HttpPut]
    public async Task<IResult> UpdateEstimateCriterion(Guid estimateId, Guid criterionId, [FromBody] EstimateCriterionDtoForUpdate estimateForUpdate)
    {
        await _estimationServiceManager.EstimateCriterion.UpdateEstimateCriterion(estimateId, criterionId, estimateForUpdate, false);

        return TypedResults.NoContent();
    }

    [HttpDelete]
    public async Task<IResult> DeleteEstimateCriterion(Guid estimateId, Guid criterionId)
    {
        await _estimationServiceManager.EstimateCriterion.DeleteEstimateCriterion(estimateId, criterionId, false);

        return TypedResults.NoContent();
    }
}
