using Lazy.Abp.FreeTime.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Lazy.Abp.FreeTime
{
    [DependsOn(
        typeof(LazyAbpDomainSharedModule)
        )]
    public class FreeTimeDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            FreeTimeGlobalFeatureConfigurator.Configure();
            FreeTimeModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<FreeTimeDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<FreeTimeResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/FreeTime");

                options.DefaultResourceType = typeof(FreeTimeResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("FreeTime", typeof(FreeTimeResource));
            });
        }
    }
}
