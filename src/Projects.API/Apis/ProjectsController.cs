using diploma.Projects.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using diploma.Projects.API.Infrastructure.Binders;
using diploma.Projects.API.Infrastructure.Filters;

namespace diploma.Projects.API.Apis;

[Route("[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly ProjectServices _projectsServices;

    public ProjectsController(ProjectServices projectsServices)
    {
        _projectsServices = projectsServices;
    }

    [HttpGet]
    public async Task<IResult> GetProjects([FromQuery] PaginationRequest paginationRequest)
    {
        var paginatedItems = await _projectsServices.ProjectService.FetchProjects(paginationRequest, trackChanges: false);

        return TypedResults.Ok(paginatedItems);
    }

    [HttpGet("{ids}", Name = "FetchProjectsByIds")]
    public async Task<IActionResult> FetchProjectsByIds([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var projects = await _projectsServices.ProjectService.FetchProjectsByIds(ids, trackChanges:
        false);
        return Ok(projects);
    }

    [HttpGet("{id:guid}", Name = "GetProjectById")]
    public async Task<IResult> GetProjectById(Guid id)
    {
        var project = await _projectsServices.ProjectService.GetProject(id, trackChanges: false);

        return TypedResults.Ok(project);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationAttributeFilter))]
    public async Task<IResult> CreateProject([FromBody] ProjectDtoForCreate project)
    {
        var createdProject = await _projectsServices.ProjectService.CreateProject(project);

        return Results.CreatedAtRoute("GetProjectById", new { id = createdProject.Id }, createdProject);
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationAttributeFilter))]
    public async Task<IResult> UpdateProject(Guid id, [FromBody] ProjectDtoForUpdate project)
    {
        await _projectsServices.ProjectService.UpdateProject(id, project, trackChanges: true);

        return TypedResults.NoContent();
    }
}
