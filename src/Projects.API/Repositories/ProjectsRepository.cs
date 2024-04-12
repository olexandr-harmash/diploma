using diploma.Projects.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using diploma.Projects.API.Repositories;

public class ProjectRepository : RepositoryBase<ProjectModel, ProjectContext>,
  IProjectsRepository
{
    private readonly ProjectContext _projectContext;
    public ProjectRepository(ProjectContext projectContext) : base(projectContext)
    {
        _projectContext = projectContext;
    }

    public async Task<IEnumerable<ProjectModel>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id),
        trackChanges)
      .ToListAsync();

    public async Task<ProjectModel?> GetAsync(Guid projectId,
        bool trackChanges) =>
      await FindByCondition(c => c.Id.Equals(projectId), trackChanges)
      .SingleOrDefaultAsync();

    public async Task<PaginatedItemsModel<ProjectModel>> FetchAsync(
      PaginationRequest pagination, bool trackChanges)
    {
        var pageSize = pagination.PageSize;
        var pageIndex = pagination.PageIndex;

        var baseQuery = FindByCondition(p => true, trackChanges);

        var totalItems = await baseQuery.LongCountAsync();

        var itemsOnPage = await baseQuery
          .OrderBy(e => e.Name)
          .Skip(pageIndex * pageSize)
          .Take(pageSize)
          .ToListAsync();

        return new PaginatedItemsModel<ProjectModel>(
          pageIndex, pageSize, totalItems, itemsOnPage);
    }

    public async Task SaveAsync() => await _projectContext.SaveChangesAsync();
}