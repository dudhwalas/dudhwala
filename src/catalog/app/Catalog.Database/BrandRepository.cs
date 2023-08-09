using Catalog.Domain;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Catalog.Database
{
    public class BrandRepository : EfCoreRepository<CatalogDbContext, Brand, Guid>, IBrandRepository<Brand, Guid>
    {
        public BrandRepository(IDbContextProvider<CatalogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Brand> CreateAsync([NotNull] Brand _entity)
        {
            var dbcontext = await GetDbContextAsync();
            var insertedEntity = (await dbcontext.BrandDb.AddAsync(_entity)).Entity;
            await dbcontext.SaveChangesAsync();
            return insertedEntity;
        }

        public async Task<Brand> GetBrandByIdAsync([NotNull] Guid _id)
        {
            var dbcontext = await GetDbContextAsync();
            return dbcontext.BrandDb.FirstOrDefault((br) => br.Id == _id);
        }

        public async Task<Brand> GetBrandByNameAsync([NotNull] string _brandName)
        {
            var dbcontext = await GetDbContextAsync();
            return dbcontext.BrandDb.FirstOrDefault(brand => brand.Name == _brandName);
        }
    }
}

