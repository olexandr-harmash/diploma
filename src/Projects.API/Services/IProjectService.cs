using diploma.Projects.API.Dtos;

namespace diploma.Projects.API.Services;

public interface IProjectService
{
    Task<PaginatedItemsModel<ProjectDto>> FetchProjects(PaginationRequest pagination, bool trackChanges);
    Task<ProjectDto> GetProject(Guid id, bool trackChanges);
    Task<ProjectDto> CreateProject(ProjectDtoForCreate project);
    Task UpdateProject(Guid id, ProjectDtoForUpdate projectForUpdate, bool trackChanges);
    Task DeleteProject(Guid id, bool trackChanges);
    Task<IEnumerable<ProjectDto>> FetchProjectsByIds(IEnumerable<Guid> ids, bool trackChanges);
    Task<(IEnumerable<ProjectDto> companies, string ids)> CreateProjectCollection(IEnumerable<ProjectDtoForCreate> projectCollection);
}