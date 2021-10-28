using Microsoft.EntityFrameworkCore;
using TestAssignment.Core.DataLayer;
using TestAssignment.Core.Infrastructure.DataLayer.EntitiesConfigurations;
using TestAssignment.Models;
using TestAssignment.Utilities.Extensions;

namespace TestAssignment.Core.Infrastructure.DataLayer
{
    public class TestAssignmentDbContext : DbContext, IDbContext
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