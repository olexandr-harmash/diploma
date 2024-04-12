using System.Text.Json;
using diploma.Estimation.API.Exceptions;
using diploma.Estimation.API.Services.Abstractions;

namespace diploma.Estimation.API.Services;

public class EstimationService : EstimationServiceBase, IEstimationService
{
    private string _setupFile = "patterns.json";
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

        bool PatternsHaveSameLength(List<List<double>> patterns) =>
            patterns.All(innerList => innerList.Count == patterns.First().Count);

        try
        {
            var sourceJson = File.ReadAllText(sourcePath);
            var patterns = JsonSerializer.Deserialize<List<List<double>>>(sourceJson);

            if (patterns == null || !PatternsHaveSameLength(patterns))
            {
                throw new EstimationServiceBadPatternSetupException(contentRootPath);
            }

            _patterns = patterns;
            _sizeLayer1 = patterns.Count;
            _sizeLayer0 = patterns.First().Count;

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

        var patternNumber = TestPattern(criteriaValueCollection.ToList());

        return (Qualification)patternNumber;
    }
}
