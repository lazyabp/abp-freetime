using Lazy.Abp.FreeTime.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Lazy.Abp.FreeTime.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class FreeTimePageModel : AbpPageModel
    {
        protected FreeTimePageModel()
        {
            LocalizationResourceType = typeof(FreeTimeResource);
        }
    }
}