using BluePrint.Application.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BluePrint.WebApi.Modules;

public static class DbInitializer
{
    public static async Task InitDataBase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        using var context = scope.ServiceProvider.GetRequiredService<IDbContext>();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<IDbContext>>();
        var cnl = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        cnl.Token.ThrowIfCancellationRequested();
        try
        {
            logger.LogInformation("Beginning Migration");
            if (await context.Database.CanConnectAsync(cnl.Token))
            {
                await context.Database.BeginTransactionAsync(cnl.Token);
                await context.Database.MigrateAsync(cnl.Token);
                await context.Database.CommitTransactionAsync(cnl.Token);
                logger.LogInformation("End Migration");
            }
            else
            {
                logger.LogError("Database does not exist!");
            }
        }
        catch (Exception e)
        {
            await context.Database.RollbackTransactionAsync(cnl.Token);

            logger.LogError(e.Message, e);
            throw;
        }
    }
}