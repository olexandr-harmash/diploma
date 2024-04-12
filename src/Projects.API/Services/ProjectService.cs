using AutoMapper;
using diploma.Projects.API.Dtos;
using diploma.Projects.API.Exceptions;
using diploma.Projects.API.Repositories;

namespace diploma.Projects.API.Services;
public class ProjectService : IProjectService
{
    private readonly IMapper _projectsMapper;
    private readonly IProjectsRepository _projectsRepository;

    public ProjectService(IProjectsRepository projectsRepository, IMapper projectsMapper)
    {
        _projectsMapper = projectsMapper;
        _projectsRepository = projectsRepository;
    }

    public async Task<(IEnumerable<ProjectDto>, string)> CreateProjectCollection(
        IEnumerable<ProjectDtoForCreate> projectCollection)
    {
        var projectEntities = _projectsMapper.Map<IEnumerable<ProjectModel>>(projectCollection);

        foreach (var project in projectEntities)
        {
           _projectsRepository.Create(project);
        }

        await _projectsRepository.SaveAsync();

        var projectDtosCollection = _projectsMapper.Map<IEnumerable<ProjectDto>>(projectEntities);

        var ids = string.Join(",", projectDtosCollection.Select(c => c.Id));

        return (projectDtosCollection, ids);
    }

    public async Task<ProjectDto> CreateProject(ProjectDtoForCreate project)
    {
        var projectEntity = _projectsMapper.Map<ProjectModel>(project);

        _projectsRepository.Create(projectEntity);

        await _projectsRepository.SaveAsync();

        var projectToReturn = _projectsMapper.Map<ProjectDto>(projectEntity);

        return projectToReturn;
    }

    public async Task DeleteProject(Guid id, bool trackChanges)
    {
        var project = await GetProjectOrTrowError(id, trackChanges);

        _projectsRepository.Delete(project);

        await _projectsRepository.SaveAsync();
    }

    public async Task<PaginatedItemsModel<ProjectDto>> FetchProjects(PaginationRequest pagination, bool trackChanges)
    {
        var paginatedItems = await _projectsRepository.FetchAsync(pagination, trackChanges);

        var projectDtos = _projectsMapper.Map<IEnumerable<ProjectDto>>(paginatedItems.Data);

        //Define automapper for this cast
        var paginatedDtos = new PaginatedItemsModel<ProjectDto>(
            pagination.PageIndex,
            pagination.PageSize,
            paginatedItems.Count,
            projectDtos);

        return paginatedDtos;
    }

    public async Task<IEnumerable<ProjectDto>> FetchProjectsByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        var projectCollection = await _projectsRepository.GetByIdsAsync(ids, trackChanges);

        if (projectCollection.Count() != ids.Count())
        {
            throw new ProjectNotFoundException(ids);
        }

        var projectDtoCollection = _projectsMapper.Map<IEnumerable<ProjectDto>>(projectCollection);

        return projectDtoCollection;
    }

    public async Task<ProjectDto> GetProject(Guid id, bool trackChanges)
    {
        var projectEntity = await GetProjectOrTrowError(id, trackChanges);

        var projectDto = _projectsMapper.Map<ProjectDto>(projectEntity);

        return projectDto;
    }

    public async Task UpdateProject(Guid id, ProjectDtoForUpdate projectForUpdate, bool trackChanges)
    {
        var projectEntity = await GetProjectOrTrowError(id, trackChanges);

        _projectsMapper.Map(projectForUpdate, projectEntity);

        await _projectsRepository.SaveAsync();
    }

    private async Task<ProjectModel> GetProjectOrTrowError(Guid id, bool trackChanges)
    {
        var projectEntity = await _projectsRepository.GetAsync(id, trackChanges);

        if (projectEntity is null)
        {
            throw new ProjectNotFoundException(id);
        }

        return projectEntity;
    }
}