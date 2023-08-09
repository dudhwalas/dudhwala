using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Catalog.Database
{
    public interface ICatalogDbContext : IEfCoreDbContext
	{
		public DbSet<Brand> BrandDb { get; set; }
	}
}