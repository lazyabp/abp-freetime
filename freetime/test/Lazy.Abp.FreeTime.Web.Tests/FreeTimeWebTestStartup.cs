using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Lazy.Abp.FreeTime
{
    public class FreeTimeWebTestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<FreeTimeWebTestModule>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}