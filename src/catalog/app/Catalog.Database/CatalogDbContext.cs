using System;
using Catalog.Domain;
using Catalog.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Catalog.Database
{
    public class CatalogDbContext :  AbpDbContext<CatalogDbContext>, ICatalogDbContext
    {
        public DbSet<Brand> BrandDb { get; set; }
        public DbSet<Product> ProductDb { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>(b =>
            {
                b.ConfigureByConvention();
                b.ToTable(CatalogDatabaseConstants.TABLE_BRAND, CatalogDatabaseConstants.DB_SCHEMA);
                b.Property(brand => brand.Name).IsRequired().HasMaxLength(100);
                b.Property(brand => brand.Image).IsRequired(false);
                b.Property(brand => brand.RealmId).IsRequired();
                b.Property(brand => brand.Status).IsRequired();
                b.HasMany(brand => brand.Products)
                .WithOne(prod => prod.Brand)
                .HasForeignKey(prod => prod.BrandId)
                .IsRequired();
            });

            modelBuilder.Entity<Product>(b =>
            {
                b.ConfigureByConvention();
                b.ToTable(CatalogDatabaseConstants.TABLE_PRODUCT, CatalogDatabaseConstants.DB_SCHEMA);
                b.Property(prod => prod.Name).IsRequired().HasMaxLength(100);
                b.Property(prod => prod.Image).IsRequired(false);
                b.Property(prod => prod.RealmId).IsRequired();
                b.Property(prod => prod.Status).IsRequired();
                b.Property(prod => prod.BrandId).IsRequired();
            });
        }
    }
}