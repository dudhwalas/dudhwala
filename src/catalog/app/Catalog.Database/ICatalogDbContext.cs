using System;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Catalog.PostgreSql
{
	public interface ICatalogDbContext : IEfCoreDbContext
	{
		public DbSet<Brand> BrandDb { get; set; }
	}
}