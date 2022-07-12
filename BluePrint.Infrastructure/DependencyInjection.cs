using BluePrint.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using BluePrint.Application.Data;

namespace BluePrint.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}