using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAssignment.Application.Data;
using TestAssignment.Application.Data.Contexts;
using TestAssignment.Application.Data.Repositories;
using TestAssignment.Domain.Entities;
using TestAssignment.Domain.Extensions;
using TestAssignment.Infrastructure.Data;
using TestAssignment.Infrastructure.Data.Contexts;
using TestAssignment.Infrastructure.Data.Repositories;

namespace TestAssignment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services,
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