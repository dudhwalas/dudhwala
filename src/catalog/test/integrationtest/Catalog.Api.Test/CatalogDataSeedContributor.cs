using Catalog.Database;
using Catalog.Domain;
using Catalog.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Catalog.Api.Test
{
    public class CatalogDataSeedContributor : IDataSeedContributor, IDisposable
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly CatalogDbContext _dbContext;

        public CatalogDataSeedContributor(IBrandRepository brandRepository,
            IProductRepository productRepository,
            IGuidGenerator guidGenerator, CatalogDbContext dbContext)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _guidGenerator = guidGenerator;
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task SeedAsync(DataSeedContext context)
        {

            var tableNames = _dbContext.Model.GetEntityTypes()
                .Select(t => $"{t.GetSchema()}.{t.GetTableName()}")
                .Distinct()
                .ToList();

            foreach (var tableName in tableNames)
            {
                var comm = $"truncate table {tableName} cascade";
                await _dbContext.Database.ExecuteSqlRawAsync(comm);
            }

            var realmId = _guidGenerator.Create();

            await _brandRepository.InsertManyAsync(new List<Brand> {
                new Brand(_guidGenerator.Create(), "xxx1", "/var/lib/files/data/xxx1.jpg", EnumCatalogStatus.ACTIVE, realmId),
                new Brand(_guidGenerator.Create(), "xxx2", "/var/lib/files/data/xxx2.jpg", EnumCatalogStatus.ACTIVE, realmId),
                new Brand(_guidGenerator.Create(), "xxx3", "/var/lib/files/data/xxx3.jpg", EnumCatalogStatus.ACTIVE, realmId),
            }, true);

            var brand = await _dbContext.BrandDb.FirstAsync();

            await _productRepository.InsertManyAsync(new List<Product> {
                new Product(_guidGenerator.Create(), "xxx-p1", "/var/lib/files/data/xxx-p1.jpg", EnumCatalogStatus.ACTIVE, realmId, brand.Id),
                new Product(_guidGenerator.Create(), "xxx-p2", "/var/lib/files/data/xxx-p2.jpg", EnumCatalogStatus.ACTIVE, realmId, brand.Id),
                new Product(_guidGenerator.Create(), "xxx-p3", "/var/lib/files/data/xxx-p3.jpg", EnumCatalogStatus.ACTIVE, realmId , brand.Id)
            }, true);

        }
    }
}