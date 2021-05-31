using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.FeatureManagement;
using Volo.Abp.SettingManagement;

namespace Lazy.Abp
{
    [DependsOn(
        typeof(LazyAbpDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule)
        )]
    public class LazyAbpApplicationContractsModule : AbpModule
    {

    }
}
