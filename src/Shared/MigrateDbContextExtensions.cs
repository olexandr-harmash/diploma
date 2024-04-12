using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.Hosting;
/// TODO: make internal and connect compiled file to nessasary projects...
internal static class MigrationDbContextExtensions
{
    public static IServiceCollection AddMigrations<TContext, TDbSeeder>(this IServiceCollection services)
        where TContext : DbContext
        where TDbSeeder : class, IDbSeeder<TContext>
    {
        services.AddScoped<IDbSeeder<TContext>, TDbSeeder>();
        return services.AddMigrations<TContext>((context, sp) => sp.GetRequiredService<IDbSeeder<TContext>>().SeedAsync(context));
    }

    public static IServiceCollection AddMigrations<TContext>(
        this IServiceCollection services,
        Func<TContext, IServiceProvider, Task> seeder)
        where TContext : DbContext
    {
        return services.AddHostedService(sp => new MigrationSeederHostedService<TContext>(sp, seeder));
    }

    public static async Task MigrateSeederAsync<TContext>(
        this IServiceProvider serviceProvider,
        Func<TContext, IServiceProvider, Task> seeder)
        where TContext : DbContext
    {
        IServiceScope scope = serviceProvider.CreateScope();
        var scopeServices = scope.ServiceProvider;
        var logger = scopeServices.GetRequiredService<ILogger<TContext>>();
        var context = scopeServices.GetRequiredService<TContext>();

        try
        {
            logger.LogInformation("{DbContextName}: Migrating database started", typeof(TContext).Name);

            var executionStrategy = context.Database.CreateExecutionStrategy();

            await executionStrategy.Execute(() => InvokeSeeder(scopeServices, context, seeder));
        }
        catch (Exception ex)
        {
            logger.LogError("{DbContextName}: Migrating database failed.\n{ex}", typeof(TContext).Name, ex);
        }
    }

    private static async Task InvokeSeeder<TContext>(
        IServiceProvider serviceProvider,
        TContext context, Func<TContext, IServiceProvider, Task> seeder)
        where TContext : DbContext
    {
        try
        {
            await context.Database.MigrateAsync();
            await seeder(context, serviceProvider);
        }
        catch
        {
            throw;
        }
    }

    public class MigrationSeederHostedService<TContext>(
        IServiceProvider serviceProvider,
        Func<TContext, IServiceProvider, Task> seeder) : BackgroundService
        where TContext : DbContext
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return serviceProvider.MigrateSeederAsync(seeder);
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
public interface IDbSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(TContext context);
}