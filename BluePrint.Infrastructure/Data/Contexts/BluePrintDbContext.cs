using BluePrint.Infrastructure.Data.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;
using BluePrint.Application.Data.Contexts;
using BluePrint.Domain.Common.BaseEntities;

namespace BluePrint.Infrastructure.Data.Contexts;

public class BluePrintDbContext : DbContext, IDbContext
{
    public BluePrintDbContext(DbContextOptions<BluePrintDbContext> options) : base(options)
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