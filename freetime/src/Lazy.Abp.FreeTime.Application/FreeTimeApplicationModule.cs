using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Lazy.Abp.FreeTime
{
    [DependsOn(
        typeof(FreeTimeDomainModule),
        typeof(FreeTimeApplicationContractsModule),
        typeof(LazyAbpApplicationModule)
        )]
    public class FreeTimeApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<FreeTimeApplicationModule>();
            });
        }
    }
}
