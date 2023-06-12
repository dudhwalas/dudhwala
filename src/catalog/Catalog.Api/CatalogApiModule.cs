using System;
using Catalog.Application;
using Catalog.PostgreSql;
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
        typeof(CatalogDatabaseModule),
        typeof(CatalogApplicationModule))]
	public class CatalogApiModule : AbpModule
	{
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddGrpc((opt) => {
                opt.EnableDetailedErrors = true;
            }).AddJsonTranscoding();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseRouting();
            app.UseConfiguredEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<Catalog.Application.Services.BrandService>();
            });
        }
    }
}

