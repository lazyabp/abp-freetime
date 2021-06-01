using System;
using System.Collections.Generic;
using System.Text;
using Lazy.Abp.FreeTime.Localization;
using Volo.Abp.Application.Services;

namespace Lazy.Abp.FreeTime
{
    /* Inherit your application services from this class.
     */
    public abstract class FreeTimeAppService : ApplicationService
    {
        protected FreeTimeAppService()
        {
            LocalizationResource = typeof(FreeTimeResource);
        }
    }
}
