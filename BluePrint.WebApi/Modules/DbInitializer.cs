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

        try
        {
            logger.LogInformation("Beginning Migration");
            
            var cnl = new CancellationTokenSource();
            cnl.CancelAfter(20 * 1000);
            cnl.Token.ThrowIfCancellationRequested();
            
            await context.Database.BeginTransactionAsync(cnl.Token);
            await context.Database.MigrateAsync(cnl.Token);
            await context.Database.CommitTransactionAsync(cnl.Token);
            
            logger.LogInformation("End Migration");
        }
        catch (Exception e)
        {
            await context.Database.RollbackTransactionAsync();
            
            logger.LogError(e.Message, e);
            throw;
        }
    }
}