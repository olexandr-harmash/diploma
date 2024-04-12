namespace diploma.Projects.API.Repositories;

public interface IProjectsRepository
{
    Task<PaginatedItemsModel<ProjectModel>> FetchAsync(PaginationRequest parameters, bool trackChanges);

    Task<ProjectModel?> GetAsync(Guid id, bool trackChanges);

    void Create(ProjectModel project);

    void Delete(ProjectModel project);

    void Update(ProjectModel project);

    Task SaveAsync();

    Task<IEnumerable<ProjectModel>> GetByIdsAsync(IEnumerable<Guid> ids,
                                                 bool trackChanges);
}
