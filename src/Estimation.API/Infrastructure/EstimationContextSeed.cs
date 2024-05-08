using AutoMapper;
using diploma.Estimation.API.Dto;
using diploma.Estimation.API.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text.Json;

namespace diploma.Estimation.API.Infrastructure;

public class EstimationContextSeed : IDbSeeder<EstimationContext>
{
    private string _setupFolder = "Setup";
    private string _setupFile = "criterion.json";

    private readonly IWebHostEnvironment _env;
    private readonly IMapper _estimationMapper;
    private readonly ICriterionService _criterionService;
    private readonly ILogger<EstimationContextSeed> _logger;

    public EstimationContextSeed(
        IWebHostEnvironment env,
        IMapper estimationMapper,
        ILogger<EstimationContextSeed> logger,
        ICriterionService criterionService)
    {
        _env = env;
        _logger = logger;
        _criterionService = criterionService;
        _estimationMapper = estimationMapper;
    }

    public async Task SeedAsync(EstimationContext context)
    {
        ((NpgsqlConnection)context.Database.GetDbConnection()).ReloadTypes();

        if (!context.Criterions.Any())
        {
            var contentRootPath = _env.ContentRootPath;
            var sourcePath = Path.Combine(contentRootPath, _setupFolder, _setupFile);

            var sourceJson = File.ReadAllText(sourcePath);
            var criterions = JsonSerializer.Deserialize<IEnumerable<CriterionDtoForCreate>>(sourceJson);

            if (criterions == null)
            {
                throw new Exception($"Failed to load or validate criterions from {contentRootPath}");
            }

            await _criterionService.CreateCriterionCollection(criterions, false);
        }
    }
}