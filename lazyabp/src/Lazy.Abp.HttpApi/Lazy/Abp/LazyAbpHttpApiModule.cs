﻿using Localization.Resources.AbpUi;
using Lazy.Abp.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.TenantManagement;
using Volo.Abp.FeatureManagement;

namespace Lazy.Abp
{
    [DependsOn(
        typeof(LazyAbpApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule)
    )]
    public class LazyAbpHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(LazyAbpHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<LazyAbpResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}