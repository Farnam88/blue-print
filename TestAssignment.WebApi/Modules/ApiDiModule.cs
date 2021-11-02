using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TestAssignment.Core.Infrastructure.Modules;
using TestAssignment.Services.Modules;
using TestAssignment.WebApi.Helpers.Attributes;

namespace TestAssignment.WebApi.Modules
{
    public static class ApiDiModule
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterCoreDependencies(configuration)
                .RegisterServicesDependencies()
                .RegisterApiDependencies();
        }

        private static IServiceCollection RegisterApiDependencies(this IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.Filters.Add<ExceptionHandlerFilterAttribute>();
                    options.Filters.Add<ApiValidationFilterAttribute>();
                })
                .AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TestAssignment WebApi", Version = "v1"});
                c.EnableAnnotations();
            });

            return services;
        }
    }
}