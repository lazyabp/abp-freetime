using Lazy.Abp.FreeTime.Localization;
using Localization.Resources.AbpUi;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Lazy.Abp.FreeTime
{
    [DependsOn(
        typeof(FreeTimeApplicationContractsModule),
        typeof(LazyAbpHttpApiModule)
        )]
    public class FreeTimeHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<FreeTimeResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
