using BluePrint.Domain.Common.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BluePrint.Application.Data.Contexts;

public interface IDbContext : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();

    DbSet<TEntity> EntitySet<TEntity>()
        where TEntity : BaseEntity;
    public DatabaseFacade Database { get; }
}