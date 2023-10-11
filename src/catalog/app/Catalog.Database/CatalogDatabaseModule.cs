using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Catalog.Database;

[DependsOn(
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(CatalogDomainModule)
    )]
public class CatalogDatabaseModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(opts =>
            {
                opts.UseNpgsql();
            });
        });

        context.Services.AddAbpDbContext<CatalogDbContext>(opt => {
            opt.AddDefaultRepositories(true);
            opt.AddRepository<Brand, BrandRepository>();
            opt.AddRepository<Product, ProductRepository>();
        });
    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await base.OnApplicationInitializationAsync(context);

        try
        {
            var dbContext = context.ServiceProvider.GetRequiredService<CatalogDbContext>();

            var pendingMigrations = await dbContext
                .Database
                .GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync();
            }
        }
        catch (NpgsqlException ex)
        {
           var logger = context.ServiceProvider.GetService<ILogger<CatalogDatabaseModule>>();
           logger?.LogException(ex);
        }
    }
}