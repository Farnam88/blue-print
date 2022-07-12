// using BluePrintBluePrint.Domain.Extensions;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.Extensions.Configuration;
//
// namespace BluePrint.Infrastructure.Data.Contexts;
//
// public class MapiFestoContextFactory : IDesignTimeDbContextFactory<MapiFestoDbContext>
// {
//     private readonly IConfiguration _configuration;
//
//     public MapiFestoContextFactory(IConfiguration configuration)
//     {
//         
//         _configuration = configuration;
//     }
//
//     public MapiFestoDbContext CreateDbContext(string[] args)
//     {
//         var optionsBuilder = new DbContextOptionsBuilder<MapiFestoDbContext>();
//         var databaseName = _configuration.GetSection("DbConnection:Default").Value;
//
//         Preconditions.CheckNull(databaseName, "Connection string");
//
//         optionsBuilder.UseSqlServer(databaseName,
//             x => x.MigrationsAssembly(typeof(MapiFestoDbContext).Assembly.GetName().Name));
//         
//         return new MapiFestoDbContext(optionsBuilder.Options);
//     }
// }