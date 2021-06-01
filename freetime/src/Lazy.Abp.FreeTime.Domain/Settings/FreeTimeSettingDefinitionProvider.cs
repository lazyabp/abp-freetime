using Volo.Abp.Settings;

namespace Lazy.Abp.FreeTime.Settings
{
    public class FreeTimeSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(FreeTimeSettings.MySetting1));
        }
    }
}
