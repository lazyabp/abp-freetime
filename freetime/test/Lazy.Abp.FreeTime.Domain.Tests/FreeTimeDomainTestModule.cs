using Lazy.Abp.FreeTime.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Lazy.Abp.FreeTime
{
    [DependsOn(
        typeof(FreeTimeEntityFrameworkCoreTestModule)
        )]
    public class FreeTimeDomainTestModule : AbpModule
    {

    }
}