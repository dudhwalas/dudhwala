using System;
using Catalog.Api.Services;
using Catalog.Application;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Catalog.Api
{
	[DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(CatalogApplicationModule))]
	public class CatalogApiModule : AbpModule
	{
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddGrpc((opt) => {
                opt.EnableDetailedErrors = true;
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseRouting();
            app.UseConfiguredEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
            });
        }
    }
}

