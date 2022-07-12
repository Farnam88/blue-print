using BluePrint.Infrastructure.Data.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;
using BluePrint.Application.Data.Contexts;
using BluePrint.Domain.Common.BaseEntities;

namespace BluePrint.Infrastructure.Data.Contexts;

public class MapiFestoDbContext : DbContext, IDbContext
{
    public MapiFestoDbContext(DbContextOptions<MapiFestoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestTodoEntityConfig).Assembly);
    }

    public DbSet<TEntity> EntitySet<TEntity>() where TEntity : BaseEntity
    {
        return base.Set<TEntity>();
    }
}