using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestAssignment.Application.Data.Contexts;
using TestAssignment.Application.Data.Repositories;
using TestAssignment.Domain.Common.BaseEntities;
using TestAssignment.Domain.Extensions;

[assembly: InternalsVisibleTo("TestAssignment.InfrastructureTests")]

namespace TestAssignment.Infrastructure.Data.Repositories
{
    internal class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        public AsyncRepository(IDbContext dbContext)
        {
            Preconditions.CheckNull(dbContext, nameof(IDbContext));

            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> FindAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(keyValues: new object[] {id}, cancellationToken: cancellationToken);
        }

        public async Task<TResult> FirstOrDefaultAsync<TResult>(ISpecification<TEntity, TResult> spec,
            CancellationToken cancellationToken = default)
        {
            Preconditions.CheckNull(spec, "Specification");
            return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default)
        {
            Preconditions.CheckNull(spec, "Specification");
            return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TResult> FirstAsync<TResult>(ISpecification<TEntity, TResult> spec,
            CancellationToken cancellationToken = default)
        {
            Preconditions.CheckNull(spec, "Specification");
            return await ApplySpecification(spec).FirstAsync(cancellationToken);
        }

        public async Task<TEntity> FirstAsync(ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default)
        {
            Preconditions.CheckNull(spec, "Specification");
            return await ApplySpecification(spec).FirstAsync(cancellationToken);
        }

        public async Task<IList<TResult>> ToListAsync<TResult>(ISpecification<TEntity, TResult> spec,
            CancellationToken cancellationToken = default)
        {
            Preconditions.CheckNull(spec, "Specification");
            return await ApplySpecification(spec).ToListAsync(cancellationToken);
        }

        public async Task<IList<TEntity>> ToListAsync(ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default)
        {
            Preconditions.CheckNull(spec, "Specification");
            return await ApplySpecification(spec).ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            Preconditions.CheckNull(spec, "Specification");
            return await ApplySpecification(spec).CountAsync(cancellationToken);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Preconditions.CheckNull(entity, "Input entity");
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
        {
            Preconditions.CheckNull(entities, "Input entities");
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, cancellationToken);
            _dbSet.Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            Preconditions.CheckNull(entity, "Input entity");
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
}