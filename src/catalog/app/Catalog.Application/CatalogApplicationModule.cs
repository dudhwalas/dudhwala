﻿using Catalog.Application.Contract;
using Catalog.Domain;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Catalog.Application
{
    [DependsOn(
		typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
		typeof(CatalogDomainModule),
		typeof(CatalogApplicationContractModule)
		)]
	public class CatalogApplicationModule : AbpModule
	{
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
            
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<CatalogApplicationModule>();
            });
        }
    }
}