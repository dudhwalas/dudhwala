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

        public Task<Brand> CreateAsync([NotNull] Brand _entity)
        {
            return InsertAsync(_entity);
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

        public Task<Brand> GetByIdAsync([NotNull] Guid _id)
        {
            return FindAsync(_id);
        }

        public Task<Brand> GetByNameAsync([NotNull] string _brandName)
        {
            return FindAsync(brand => brand.Name == _brandName);
        }

        public Task<long> GetTotalAsync()
        {
            return GetCountAsync();
        }

        public Task<List<Brand>> GetAsync([NotNull] int pageToken, [NotNull] int pageSize, string? sorting)
        {
            return GetPagedListAsync(pageToken * pageSize, pageSize, sorting);
        }
    }
}