using Lazy.Abp.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Lazy.Abp.FreeTime.EntityFrameworkCore
{
    [DependsOn(
        typeof(FreeTimeDomainModule),
        typeof(LazyAbpEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule)
        )]
    public class FreeTimeEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            FreeTimeEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<FreeTimeDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also FreeTimeMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
            });
        }
    }
}
