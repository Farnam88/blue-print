using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAssignment.Core.DAL.Repositories;
using TestAssignment.Core.DataLayer;
using TestAssignment.Core.Infrastructure.DAL;
using TestAssignment.Core.Infrastructure.DataLayer;
using TestAssignment.Models;
using TestAssignment.Utilities.Extensions;

namespace TestAssignment.Core.Infrastructure.Modules
{
    public static class CoreDiModule
    {
        public static IServiceCollection RegisterCoreDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            Preconditions.CheckNull(services, nameof(IServiceCollection));
            Preconditions.CheckNull(configuration, nameof(IConfiguration));

            services.AddDbContext<IDbContext, TestAssignmentDbContext>(optionBuilder =>
            {
                var databaseName = configuration.GetSection("DataBase:Name").Value;

                Preconditions.CheckNull(databaseName, "database name");

                optionBuilder.UseInMemoryDatabase(databaseName);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAsyncRepository<TestAssignmentEntity>, AsyncRepository<TestAssignmentEntity>>();

            return services;
        }
    }
}