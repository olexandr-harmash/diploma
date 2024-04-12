namespace diploma.Estimation.API.Infrastructure;

public class EstimationContextSeed : IDbSeeder<EstimationContext>
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<EstimationContextSeed> _logger;

    public EstimationContextSeed(
        IWebHostEnvironment env,
        ILogger<EstimationContextSeed> logger)
    {
        _env = env;
        _logger = logger;
    }

    public async Task SeedAsync(EstimationContext context)
    {
       
    }
}