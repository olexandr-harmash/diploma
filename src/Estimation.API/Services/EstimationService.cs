using System.Text.Json;
using diploma.Estimation.API.Exceptions;
using diploma.Estimation.API.Services.Abstractions;

namespace diploma.Estimation.API.Services;

public class EstimationService : EstimationServiceBase, IEstimationService
{
    private string _setupFile = "pattern.json";
    private string _setupFolder = "Setup";

    public enum Qualification
    {
        Expert,
        Senior,
        Middle,
        Junior,
    }

    public EstimationService(IWebHostEnvironment env, ILogger<EstimationService> logger)
    {
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

            _patterns = patterns;

            Train();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }

    public Qualification MatchEstimateToPattern(Estimate estimate)
    {
        var criteriaValueCollection = estimate.GetCriterionValueCollection();

        if (criteriaValueCollection.Length != _sizeLayer0)
        {
            throw new EstimationServiceBadPatternSizeException(estimate.Id);
        }

        var patternIndex = TestPattern(criteriaValueCollection);

        return (Qualification)patternIndex;
    }
}
