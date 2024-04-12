using diploma.Estimation.API.Controllers;
using diploma.Estimation.API.Dto;
using diploma.Estimation.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EstimationController : ControllerBase
{
    private readonly EstimationServices _estimationServices;
    private readonly EstimationServiceManager _estimationServiceManager;

    public EstimationController(EstimationServices estimationServices)
    {
        _estimationServices = estimationServices;
        _estimationServiceManager = estimationServices.estimationServiceManager;
    }

    [HttpGet("{id: guid}", Name = "GetEstimateById")]
    public async Task<IResult> GetEstimateById(Guid id)
    {
        var estimateDto = await _estimationServiceManager.Estimate.GetEstimateById(id, false);

        return TypedResults.Ok(estimateDto);
    }

    [HttpPost]
    public async Task<IResult> CreateEstimate([FromBody] EstimateDtoForCreate estimateForCreate)
    {
        var estimateDto = await _estimationServiceManager.Estimate.CreateEstimate(estimateForCreate);

        return Results.CreatedAtRoute("GetEstimateById", new { id = estimateDto.Id }, estimateDto);
    }

    [HttpPut("{id: guid}")]
    public async Task<IResult> UpdateEstimate(Guid id, [FromBody] EstimateDtoForUpdate estimateForUpdate)
    {
        await _estimationServiceManager.Estimate.UpdateEstimate(id, estimateForUpdate, false);

        return TypedResults.NoContent();
    }

    [HttpDelete("{id: guid}")]
    public async Task<IResult> DeleteEstimate(Guid id)
    {
        await _estimationServiceManager.Estimate.DeleteEstimate(id, false);

        return TypedResults.NoContent();
    }
}
