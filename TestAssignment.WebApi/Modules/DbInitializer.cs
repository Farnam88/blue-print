using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestAssignment.Core.DataLayer;

namespace TestAssignment.WebApi.Modules
{
    public static class DbInitializer
    {
        public static async Task InitDataBase(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<IDbContext>();

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IDbContext>>();

            logger.LogInformation("Beginning to recreating database");

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            logger.LogInformation("Database recreation ended");
        }
    }
}