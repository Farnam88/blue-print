using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BluePrint.Application.Data.Contexts;
using BluePrint.Application.Data.Repositories;
using BluePrint.Domain.Common.BaseEntities;


namespace BluePrint.Infrastructure.Data.Repositories;

internal class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;

    public AsyncRepository(IDbContext dbContext)
    {
        _dbSet = dbContext.EntitySet<TEntity>();
    }

    public async Task<TEntity> FindAsync(int id, CancellationToken cancellationToken = default)
    {
        return (await _dbSet.FindAsync(keyValues: new object[] {id}, cancellationToken: cancellationToken))!;
    }

    public async Task<TResult> FirstOrDefaultAsync<TResult>(ISpecification<TEntity, TResult> spec,
        CancellationToken cancellationToken = default)
    {
        return (await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken))!;
    }

    public async Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
    {
        return (await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken))!;
    }

    public async Task<TResult> FirstAsync<TResult>(ISpecification<TEntity, TResult> spec,
        CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).FirstAsync(cancellationToken);
    }

    public async Task<TEntity> FirstAsync(ISpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).FirstAsync(cancellationToken);
    }

    public async Task<IList<TResult>> ToListAsync<TResult>(ISpecification<TEntity, TResult> spec,
        CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public async Task<IList<TEntity>> ToListAsync(ISpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).CountAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await FindAsync(id, cancellationToken);
        _dbSet.Remove(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        var evaluator = new SpecificationEvaluator();
        return evaluator.GetQuery(_dbSet.AsQueryable(), spec);
    }

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TEntity, TResult> spec)
    {
        var evaluator = new SpecificationEvaluator();
        return evaluator.GetQuery(_dbSet.AsQueryable(), spec);
    }
}