using diploma.Estimation.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace diploma.Estimation.API.Controllers;

[ApiController]
[Route("api/criterion/{criterionId:guid}")]
public class CrtiterionController
{
    private readonly EstimationServices _estimationServices;
    private readonly EstimationServiceManager _estimationServiceManager;

    public CrtiterionController(EstimationServices estimationServices)
    {
        _estimationServices = estimationServices;
        _estimationServiceManager = estimationServices.estimationServiceManager;
    }

    [HttpGet(Name = "GetCriterionById")]
    public async Task<IResult> GetCriterionById(Guid criterionId)
    {
        var criterionDto = await _estimationServiceManager.Criterion.GetCriterionById(criterionId, false);

        return TypedResults.Ok(criterionDto);
    }
}
