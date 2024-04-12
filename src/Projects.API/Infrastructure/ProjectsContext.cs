using Microsoft.EntityFrameworkCore;
using diploma.Projects.API.Infrastructure.EntityConfigurations;

namespace diploma.Projects.API.Infrastructure;

public class ProjectContext : DbContext
{
    public ProjectContext(DbContextOptions<ProjectContext> options,
                          IConfiguration configuration)
        : base(options) { }

    public DbSet<ProjectModel> Projects
    {
        get;
        set;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ProjectRepositoryEntityConfiguration());
    }
}
