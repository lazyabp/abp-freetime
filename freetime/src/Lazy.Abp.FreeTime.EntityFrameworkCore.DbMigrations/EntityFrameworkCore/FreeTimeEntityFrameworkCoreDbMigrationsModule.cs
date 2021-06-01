using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Lazy.Abp.FreeTime.EntityFrameworkCore
{
    [DependsOn(
        typeof(FreeTimeEntityFrameworkCoreModule)
        )]
    public class FreeTimeEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<FreeTimeMigrationsDbContext>();
        }
    }
}
