using System.Threading.Tasks;

namespace Lazy.Abp.FreeTime.Data
{
    public interface IFreeTimeDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
