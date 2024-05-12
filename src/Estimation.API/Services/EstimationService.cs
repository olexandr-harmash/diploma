using System.Text.Json;
using diploma.Estimation.API.Exceptions;
using diploma.Estimation.API.Services.Abstractions;

namespace diploma.Estimation.API.Services;

public enum Qualification
{
    Expert,
    Senior,
    Middle,
    Junior,
}

public class EstimationService : IEstimationService
{
    private string _setupFile = "pattern.json";
    private string _setupFolder = "Setup";

    private IStrategyService _strategyService;

    //TODO: Async initialize extension
    public EstimationService(IWebHostEnvironment env, ILogger<EstimationService> logger, IStrategyService strategyService)
    {
        _strategyService = strategyService;

        var contentRootPath = env.ContentRootPath;
        var sourcePath = Path.Combine(contentRootPath, _setupFolder, _setupFile);

        bool PatternsHaveSameLength(double[][] patterns) =>
            patterns.Length % patterns.Rank == 0;

        try
        {
            var sourceJson = File.ReadAllText(sourcePath);
            var patterns = JsonSerializer.Deserialize<double[][]>(sourceJson);

            if (patterns == null || !PatternsHaveSameLength(patterns))
            {
                throw new EstimationServiceBadPatternSetupException(contentRootPath);
            }

            _strategyService.SetPatterns(patterns);

            _strategyService.Train();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }

    public async Task<Qualification> TestPattern(double[] pattern)
    {
        if (pattern.Length != _strategyService.GetZeroLayerSize())
        {
            throw new EstimationServiceBadPatternSizeException();
        }

        return (Qualification) await _strategyService.TestPattern(pattern);
    }
}
