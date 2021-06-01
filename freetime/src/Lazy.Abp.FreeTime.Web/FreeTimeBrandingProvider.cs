using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Lazy.Abp.FreeTime.Web
{
    [Dependency(ReplaceServices = true)]
    public class FreeTimeBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "FreeTime";
    }
}
