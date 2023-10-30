using Catalog.Application;
using Catalog.Database;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
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
            context.Services.AddGrpc((opt) =>
            {
                opt.EnableDetailedErrors = true;
                opt.Interceptors.Add<GrpcGlobalExceptionHandlerInterceptor>();
            }).AddJsonTranscoding();
            context.Services.AddGrpcSwagger();
            context.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo { Title = "Catalog API", Version = "V1" });
            });

            Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = false;
                options.SendStackTraceToClients = false;
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseRouting();
            app.UseSwagger(c =>
            {
                //c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                //{
                //    OpenApiPaths paths = new OpenApiPaths();
                //    foreach (var path in swaggerDoc.Paths)
                //    {
                //        paths.Add(path.Key, path.Value);
                //    }
                //    swaggerDoc.Paths = paths;
                //});
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Catalog API V1");
            });
            app.UseConfiguredEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<Application.Services.BrandService>();
                endpoints.MapGrpcService<Application.Services.ProductService>();
            });
        }
    }
}

