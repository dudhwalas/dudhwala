using System;
using Catalog.Domain;
using Catalog.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Catalog.PostgreSql
{
    public class CatalogDbContext :  AbpDbContext<CatalogDbContext>, ICatalogDbContext
    {
        public DbSet<Brand> BrandDb { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>(b =>
            {
                b.ConfigureByConvention();
                b.ToTable<Brand>(CatalogDatabaseConstants.TABLE_BRAND, CatalogDatabaseConstants.DB_SCHEMA);
                b.Property(brand => brand.Name).IsRequired().HasMaxLength(100);
                b.Property(brand => brand.Image).IsRequired(false);
                b.Property(brand => brand.RealmId).IsRequired();
                b.Property(brand => brand.Status).IsRequired();
            });
        }
    }
}