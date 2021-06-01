using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Lazy.Abp.FreeTime.EntityFrameworkCore
{
    public static class FreeTimeDbContextModelCreatingExtensions
    {
        public static void ConfigureFreeTime(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(FreeTimeConsts.DbTablePrefix + "YourEntities", FreeTimeConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}