using Lazy.Abp.FreeTime.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Lazy.Abp.FreeTime.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class FreeTimeController : AbpController
    {
        protected FreeTimeController()
        {
            LocalizationResource = typeof(FreeTimeResource);
        }
    }
}