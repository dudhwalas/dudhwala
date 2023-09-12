using Catalog.Domain;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Catalog.Database
{
    public class BrandRepository : EfCoreRepository<CatalogDbContext, Brand, Guid>, IBrandRepository
    {
        public BrandRepository(IDbContextProvider<CatalogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Brand?> CreateAsync([NotNull] Brand _entity)
        {
            return await InsertAsync(_entity);
        }

        public async Task<Brand?> UpdateAsync([NotNull] Brand _entity)
        {
            var entityToUpdate = await FindAsync(_entity.Id);
            if (entityToUpdate != null) {
                entityToUpdate.SetName(_entity.Name ?? string.Empty);
                entityToUpdate.SetImage(_entity.Image ?? string.Empty);
                entityToUpdate.SetRealmId(_entity.RealmId);
                entityToUpdate.SetStatus(_entity.Status ?? 0);
            }
            return entityToUpdate;
        }

        public async Task<Brand?> GetByIdAsync([NotNull] Guid _id)
        {
            return await FindAsync(_id);
        }

        public async Task<Brand?> GetByNameAsync([NotNull] string _brandName)
        {
            return await FindAsync(brand => brand.Name == _brandName);
        }

        public async Task<List<Brand>> GetAsync([NotNull] int pageToken, [NotNull] int pageSize)
        {
            var result = await GetPagedListAsync(pageToken * pageSize, pageSize, null);
            return result;
        }

        public async Task<long> GetTotalAsync()
        {
            return await GetCountAsync();
        }
    }
}