using Lazy.Abp.FreeTime.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Lazy.Abp.FreeTime.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(FreeTimeEntityFrameworkCoreDbMigrationsModule),
        typeof(FreeTimeApplicationContractsModule)
        )]
    public class FreeTimeDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
