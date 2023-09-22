using Catalog.Domain;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Catalog.Database
{
    public class ProductRepository : EfCoreRepository<CatalogDbContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<CatalogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<Product> CreateAsync([NotNull] Product _entity)
        {
            return InsertAsync(_entity);
        }

        public async Task<Product?> UpdateAsync([NotNull] Product _entity)
        {
            var entityToUpdate = await FindAsync(_entity.Id);
            if (entityToUpdate != null) {
                entityToUpdate.SetName(_entity.Name ?? string.Empty);
                entityToUpdate.SetImage(_entity.Image ?? string.Empty);
                entityToUpdate.SetRealmId(_entity.RealmId);
                entityToUpdate.SetStatus(_entity.Status ?? 0);
                entityToUpdate.SetBrandId(_entity.BrandId);
            }
            return entityToUpdate;
        }

        public Task<Product> GetByIdAsync([NotNull] Guid _id)
        {
            return FindAsync(_id);
        }

        public Task<Product> GetByNameAsync([NotNull] string _prodName)
        {
            return FindAsync(prod => prod.Name == _prodName);
        }

        public Task<long> GetTotalAsync()
        {
            return GetCountAsync();
        }

        public Task<List<Product>> GetAsync([NotNull] int pageToken, [NotNull] int pageSize, string? sorting)
        {
            return GetPagedListAsync(pageToken * pageSize, pageSize, sorting);
        }

        public async Task<List<Product>> GetAsync([NotNull] Guid brandId, [NotNull] int pageToken, [NotNull] int pageSize, string? sorting)
        {
            if (brandId == Guid.Empty)
                return await GetAsync(pageToken, pageSize, sorting);

            var prodDbSet = await GetDbSetAsync();
            return prodDbSet.Where(prod => prod.BrandId == brandId).OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(Product.Name) : sorting).Skip(pageToken * pageSize).Take(pageSize).ToList();
        }
    }
}