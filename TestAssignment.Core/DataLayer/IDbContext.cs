using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TestAssignment.Core.DataLayer
{
    public interface IDbContext : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity)
            where TEntity : class;

        public DatabaseFacade Database { get; }
    }
}