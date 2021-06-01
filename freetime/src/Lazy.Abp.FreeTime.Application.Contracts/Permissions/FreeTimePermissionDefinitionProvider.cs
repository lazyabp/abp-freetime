using Lazy.Abp.FreeTime.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Lazy.Abp.FreeTime.Permissions
{
    public class FreeTimePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(FreeTimePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(FreeTimePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FreeTimeResource>(name);
        }
    }
}
