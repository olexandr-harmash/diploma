using Microsoft.EntityFrameworkCore;
using diploma.Estimation.API.Infrastructure.EntityConfiguration;

namespace diploma.Estimation.API.Infrastructure;

public class EstimationContext : DbContext
{
    public DbSet<Estimate> Estimations { get; set; }
    public DbSet<Criterion> Criterions { get; set; }
    public DbSet<EstimateCriterion> EstimateCriterions { get; set; }

    public EstimationContext(DbContextOptions<EstimationContext> options,
                               IConfiguration configuration)
             : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
       
        builder.ApplyConfiguration(new EstimateEntityConfiguration());
        builder.ApplyConfiguration(new CriterionEntityConfiguration());
        builder.ApplyConfiguration(new EstimateCriterionEntityConfiguration());
    }
}

