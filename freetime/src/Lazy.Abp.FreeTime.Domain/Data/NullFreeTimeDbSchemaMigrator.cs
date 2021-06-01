using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Lazy.Abp.FreeTime.Data
{
    /* This is used if database provider does't define
     * IFreeTimeDbSchemaMigrator implementation.
     */
    public class NullFreeTimeDbSchemaMigrator : IFreeTimeDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}