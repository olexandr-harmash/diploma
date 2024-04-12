using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public abstract class RepositoryBase<T, C> : IRepositoryBase<T> where T
    : class where C : DbContext
{
    protected C RepositoryContext;

    public RepositoryBase(C repositoryContext) => RepositoryContext =
        repositoryContext;

    public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ? RepositoryContext.Set<T>()
             .AsNoTracking()
        : RepositoryContext.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
                                         bool trackChanges) =>
        !trackChanges ? RepositoryContext.Set<T>()
             .Where(expression)
             .AsNoTracking()
        : RepositoryContext.Set<T>()
             .Where(expression);

    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
}

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool trackChanges);

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
                                  bool trackChanges);

    void Create(T entity);

    void Update(T entity);

    void Delete(T entity);
}