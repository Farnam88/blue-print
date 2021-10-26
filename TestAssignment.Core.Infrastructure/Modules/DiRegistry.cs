using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAssignment.Core.DAL.Repositories;
using TestAssignment.Core.Infrastructure.DAL;
using TestAssignment.Core.Infrastructure.DataLayer;
using TestAssignment.Models;
using TestAssignment.Utilities.Extensions;

namespace TestAssignment.Core.Infrastructure.Modules
{
    public static class DiRegistry
    {
        public static IServiceCollection RegisterCoreDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            Preconditions.CheckNull(services, nameof(IServiceCollection));
            Preconditions.CheckNull(configuration, nameof(IConfiguration));

            services.AddDbContext<TestAssignmentDbContext>(optionBuilder =>
            {
                optionBuilder.UseInMemoryDatabase(configuration.GetSection("DataBase:Name").Value);
            });
            
            services.AddScoped<IAsyncRepository<TestAssignmentEntity>, AsyncRepository<TestAssignmentEntity>>();

            return services;
        }
    }
}