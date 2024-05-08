using diploma.Estimation.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace diploma.Estimation.API.Controllers;

[ApiController]
[Route("api/criterion")]
public class CrtiterionController
{
    private readonly EstimationServices _estimationServices;
    private readonly EstimationServiceManager _estimationServiceManager;

    public CrtiterionController(EstimationServices estimationServices)
    {
        _estimationServices = estimationServices;
        _estimationServiceManager = estimationServices.estimationServiceManager;
    }

    [HttpGet("{criterionId:guid}", Name = "GetCriterionById")]
    public async Task<IResult> GetCriterionById(Guid criterionId)
    {
        var criterionDto = await _estimationServiceManager.Criterion.GetCriterionById(criterionId, false);

        return TypedResults.Ok(criterionDto);
    }

    [HttpGet("list")]
    public async Task<IResult> FetchAllCriterionCollection()
    {
        var criterionCollectionDto = await _estimationServiceManager.Criterion.FetchFullCriterionCollection(false);

        return TypedResults.Ok(criterionCollectionDto);
    }
}
