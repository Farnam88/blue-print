using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TestAssignment.WebApi.Helpers
{
    public static class StartupConfigurationBuilder
    {
        private const string AppSettingFileName = "appsettings.json";
        private const string AppSettingEnvFileName = "appsettings.{0}.json";

        public static IConfigurationRoot BuildConfigurationRoot(this IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(AppSettingFileName, optional: false, reloadOnChange: true)
                .AddJsonFile(string.Format(AppSettingEnvFileName, env.EnvironmentName), optional: false,
                    reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}