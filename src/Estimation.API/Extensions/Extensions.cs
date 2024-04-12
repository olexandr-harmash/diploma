using Microsoft.AspNetCore.Mvc;
using diploma.Estimation.API.Infrastructure;
using diploma.Shared.Exceptions;

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

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddMigrations<EstimationContext, EstimationContextSeed>();
        }

        return builder;
    }
}

