using Volo.Abp.Domain;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Lazy.Abp
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(LazyAbpDomainSharedModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpFeatureManagementDomainModule)
    )]
    public class LazyAbpDomainModule : AbpModule
    {

    }
}
