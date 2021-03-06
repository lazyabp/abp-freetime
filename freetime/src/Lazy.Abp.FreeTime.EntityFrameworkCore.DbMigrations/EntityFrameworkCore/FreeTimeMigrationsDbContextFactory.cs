using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Lazy.Abp.FreeTime.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class FreeTimeMigrationsDbContextFactory : IDesignTimeDbContextFactory<FreeTimeMigrationsDbContext>
    {
        public FreeTimeMigrationsDbContext CreateDbContext(string[] args)
        {
            FreeTimeEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<FreeTimeMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new FreeTimeMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Lazy.Abp.FreeTime.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
