using Catalog.Application;
using Catalog.Database;
using Microsoft.OpenApi.Models;
using Volo.Abp;
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
            context.Services.AddGrpcSwagger();
            context.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo { Title = "Catalog API", Version = "V1" });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
            });
            app.UseConfiguredEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<Catalog.Application.Services.BrandService>();
            });
        }
    }
}

