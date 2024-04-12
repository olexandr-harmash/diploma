using diploma.Projects.API.Services;

namespace diploma.Projects.API.Apis;

public class ProjectServices(IProjectService projectService)
{
    public IProjectService ProjectService { get; } = projectService;
}

