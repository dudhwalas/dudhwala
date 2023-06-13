using System;
using Catalog.Application.Contract;
using Catalog.Domain;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Catalog.Application
{
	[DependsOn(
		typeof(AbpDddApplicationModule),
		typeof(CatalogDomainModule),
		typeof(CatalogApplicationContractModule)
		)]
	public class CatalogApplicationModule : AbpModule
	{
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
            context.Services.AddGrpc((opt) => {
                opt.EnableDetailedErrors = true;
            }).AddJsonTranscoding();
        }
    }
}