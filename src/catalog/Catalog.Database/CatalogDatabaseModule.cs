using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Catalog.PostgreSql;

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
        });
    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await base.OnApplicationInitializationAsync(context);
        var dbContext = context.ServiceProvider.GetRequiredService<CatalogDbContext>();

        var pendingMigrations = await dbContext
            .Database
            .GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            await dbContext.Database.MigrateAsync();
        }
    }

}