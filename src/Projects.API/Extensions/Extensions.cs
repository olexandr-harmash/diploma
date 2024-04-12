using diploma.Projects.API.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using diploma.Projects.API.Services;
using diploma.Projects.API.Repositories;
using diploma.Projects.API.Infrastructure;
using diploma.ProjectsApi.Infrastructure;
using diploma.Projects.API.Apis;
using diploma.Projects.API.Infrastructure.Filters;

namespace diploma.Projects.API.Extensions;

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

        builder.AddNpgsqlDbContext<ProjectContext>("projectsdb", configureDbContextOptions: dbContextOptionsBuilder =>
        {
   
        });

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddMigrations<ProjectContext, ProjectContextSeed>();
        }

        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<IProjectsRepository, ProjectRepository>();

        builder.Services.AddScoped<ProjectServices>();
        builder.Services.AddScoped<ValidationAttributeFilter>();

        return builder;
    }
}

