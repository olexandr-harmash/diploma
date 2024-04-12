using diploma.Estimation.API.Services;
using Estimation.API.Controllers;

namespace diploma.Estimation.API.Controllers;

public class EstimationServices(
    ILogger<EstimationController> logger,
    EstimationServiceManager estimationServiceManager)
{
    public readonly ILogger<EstimationController>  logger = logger;
    public readonly EstimationServiceManager estimationServiceManager = estimationServiceManager;
}
