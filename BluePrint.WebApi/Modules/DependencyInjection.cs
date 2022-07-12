using BluePrint.WebApi.Helpers.Attributes;
using Microsoft.OpenApi.Models;
using BluePrint.Application;
using BluePrint.Application.Data.Contexts;
using BluePrint.Domain.Extensions;
using BluePrint.Infrastructure;
using BluePrint.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BluePrint.WebApi.Modules;

public static class DependencyInjection
{
    public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterInfrastructureDependencies()
            .RegisterApplicationDependencies()
            .RegisterApiDependencies()
            .RegisterDbContext(configuration);
    }

    private static IServiceCollection RegisterApiDependencies(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionHandlerFilterAttribute>();
            options.Filters.Add<ApiValidationFilterAttribute>();
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "MapiFesto WebApi", Version = "v1"});
            c.EnableAnnotations();
        });

        return services;
    }

    private static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IDbContext, MapiFestoDbContext>(optionBuilder =>
        {
            
            var databaseName = configuration.GetSection("DbConnection:Default").Value;

            Preconditions.CheckNull(databaseName, "Connection string");
            optionBuilder.UseSqlServer(databaseName
             , x => x.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
        });
        return services;
    }
}