namespace BluePrint.WebApi.Helpers;

public static class StartupConfigurationBuilder
{
    private const string AppSettingFileName = "appsettings.json";
    private const string AppSettingEnvFileName = "appsettings.{0}.json";

    public static IConfigurationBuilder BuildConfigurationRoot(this IHostEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile(AppSettingFileName, optional: false, reloadOnChange: true)
            .AddJsonFile(string.Format(AppSettingEnvFileName, env.EnvironmentName), optional: false,
                reloadOnChange: true)
            .AddEnvironmentVariables();
        return builder;
    }
}