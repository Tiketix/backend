using System;
using System.Linq.Expressions;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext RepositoryContext;
    public RepositoryBase(RepositoryContext repositoryContext) => 
    RepositoryContext = repositoryContext;
 
    public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ?
    RepositoryContext.Set<T>().AsNoTracking() : RepositoryContext.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
    bool trackChanges) => !trackChanges ? RepositoryContext.Set<T>()
    .Where(expression).AsNoTracking() : RepositoryContext.Set<T>().Where(expression); 

    public async Task Create(T entity) => await RepositoryContext.Set<T>().AddAsync(entity);

    public async Task Update(T entity) => await Task.Run(() => RepositoryContext.Set<T>().Update(entity));

    public async Task Delete(T entity) => await Task.Run(() => RepositoryContext.Set<T>().Remove(entity));
}
