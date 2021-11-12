using Microsoft.EntityFrameworkCore;
using TestAssignment.Application.Data.Contexts;
using TestAssignment.Domain.Extensions;
using TestAssignment.Infrastructure.Data.EntitiesConfigurations;

namespace TestAssignment.Infrastructure.Data.Contexts
{
    internal class TestAssignmentDbContext : DbContext, IDbContext
    {
        public TestAssignmentDbContext(DbContextOptions options) : base(options)
        {
            Preconditions.CheckNull(options, nameof(DbContextOptions));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Preconditions.CheckNull(optionsBuilder, nameof(DbContextOptionsBuilder));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Preconditions.CheckNull(modelBuilder, nameof(ModelBuilder));

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestAssignmentEntityConfig).Assembly);
        }
    }
}