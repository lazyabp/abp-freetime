using Lazy.Abp.FreeTime.Localization;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Lazy.Abp.FreeTime.Web.Menus
{
    public class FreeTimeMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<FreeTimeResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    FreeTimeMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fas fa-home",
                    order: 0
                )
            );

            await Task.CompletedTask;
        }
    }
}
