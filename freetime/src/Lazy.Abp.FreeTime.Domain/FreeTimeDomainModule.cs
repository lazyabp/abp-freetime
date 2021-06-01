using Lazy.Abp.FreeTime.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Emailing;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Lazy.Abp.FreeTime
{
    [DependsOn(
        typeof(FreeTimeDomainSharedModule),
        typeof(LazyAbpDomainModule)
    )]
    public class FreeTimeDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

#if DEBUG
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
        }
    }
}
