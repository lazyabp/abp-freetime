using Volo.Abp.Modularity;

namespace Lazy.Abp.FreeTime
{
    [DependsOn(
        typeof(FreeTimeDomainSharedModule),
        typeof(LazyAbpApplicationContractsModule)
    )]
    public class FreeTimeApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            FreeTimeDtoExtensions.Configure();
        }
    }
}
