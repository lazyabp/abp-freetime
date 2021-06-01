using Volo.Abp.Modularity;

namespace Lazy.Abp.FreeTime
{
    [DependsOn(
        typeof(FreeTimeApplicationModule),
        typeof(FreeTimeDomainTestModule)
        )]
    public class FreeTimeApplicationTestModule : AbpModule
    {

    }
}