using diploma.Projects.API.Infrastructure;

namespace diploma.ProjectsApi.Infrastructure
{
    public class ProjectContextSeed : IDbSeeder<ProjectContext>
    {
        public Task SeedAsync(ProjectContext context)
        {
            return Task.CompletedTask;
        }
    }
}
