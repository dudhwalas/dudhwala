using Catalog.Domain;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Catalog.Database
{
    public class BrandRepository : EfCoreRepository<CatalogDbContext, Brand, Guid>, IRepository<Brand, Guid>
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

        public async Task<Brand> UpdateAsync([NotNull] Brand _entity)
        {
            var dbcontext = await GetDbContextAsync();
            var brandToUpdate = dbcontext.BrandDb.FirstOrDefault( x => x.Id == _entity.Id);
            if (brandToUpdate != null)
            {
                brandToUpdate.SetName(_entity.Name);
                brandToUpdate.SetImage(_entity.Image);
                brandToUpdate.SetRealmId(_entity.RealmId);
                brandToUpdate.SetStatus(_entity.Status.Value);
                await dbcontext.SaveChangesAsync();
            }
            return brandToUpdate;
        }

        public async Task<Brand> GetByIdAsync([NotNull] Guid _id)
        {
            var dbcontext = await GetDbContextAsync();
            return dbcontext.BrandDb.FirstOrDefault((br) => br.Id == _id);
        }

        public async Task<Brand> GetByNameAsync([NotNull] string _brandName)
        {
            var dbcontext = await GetDbContextAsync();
            return dbcontext.BrandDb.FirstOrDefault(brand => brand.Name == _brandName);
        }

        public async Task<List<Brand>> GetAsync([NotNull] int pageToken, [NotNull] int pageSize)
        {
            var dbcontext = await GetDbContextAsync();
            var result = dbcontext.BrandDb.Skip(pageToken * pageSize).Take(pageSize);
            return result.ToList();
        }

        public async Task<int> GetTotalAsync()
        {
            var dbcontext = await GetDbContextAsync();
            return dbcontext.BrandDb.Count();
        }
    }
}