using Microsoft.AspNetCore.Mvc;
using diploma.Estimation.API.Infrastructure;
using diploma.Shared.Exceptions;
using diploma.Estimation.API.Services;
using diploma.Estimation.API.Repository.Abstractions;
using diploma.Estimation.API.Repository;
using diploma.Estimation.API.Services.Abstractions;

namespace diploma.Estimation.API.Extensions;

public static class Extensions
{

    public static IHostApplicationBuilder ConfigureServices(this IHostApplicationBuilder builder)
    {

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        builder.Services.AddCors(options => {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        builder.Services.Configure<IISOptions>(options =>
        {
        });

        return builder;
    }

    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddExceptionHandler<ExceptionHandlerService>();

        builder.AddNpgsqlDbContext<EstimationContext>("estimationdb", configureDbContextOptions: dbContextOptionsBuilder =>
        {
   
        });

        builder.Services.AddScoped<ICriterionRepository, CriterionRepository>();
        builder.Services.AddScoped<IEstimateRepository, EstimateRepository>();
        builder.Services.AddScoped<IEstimateCriterionRepository, EstimateCriterionRepository>();

        builder.Services.AddScoped<ICriterionService, CriterionService>();
        builder.Services.AddScoped<IEstimateService, EstimateService>();
        builder.Services.AddScoped<IEstimateCriterionService, EstimateCriterionService>();

        builder.Services.AddSingleton<IEstimationService, EstimationService>();

        builder.Services.AddScoped<EstimationRepositoryManager>();
        builder.Services.AddScoped<EstimationServiceManager>();

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddMigrations<EstimationContext, EstimationContextSeed>();
        }

        return builder;
    }
}

