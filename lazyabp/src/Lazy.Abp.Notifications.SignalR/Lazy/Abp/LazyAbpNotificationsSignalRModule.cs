using Lazy.Abp.AspNetCore.SignalR.JwtToken;
using Lazy.Abp.Notifications;
using Lazy.Abp.Notifications.SignalR;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Modularity;

namespace Lazy.Abp
{
    [DependsOn(
        typeof(LazyAbpNotificationsModule),
        typeof(AbpAspNetCoreSignalRModule),
        typeof(AbpAspNetCoreSignalRJwtTokenModule)
    )]
    public class LazyAbpNotificationsSignalRModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNotificationOptions>(options =>
            {
                options.PublishProviders.Add<SignalRNotificationPublishProvider>();
                options.NotificationDataMappings
                       .MappingDefault(SignalRNotificationPublishProvider.ProviderName,
                       data => data);
            });

            Configure<AbpAspNetCoreSignalRJwtTokenMapPathOptions>(options =>
            {
                options.MapPath("notifications");
            });
        }
    }
}
