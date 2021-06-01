using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lazy.Abp.FreeTime.Data;
using Volo.Abp.DependencyInjection;

namespace Lazy.Abp.FreeTime.EntityFrameworkCore
{
    public class EntityFrameworkCoreFreeTimeDbSchemaMigrator
        : IFreeTimeDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreFreeTimeDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the FreeTimeMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<FreeTimeMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}