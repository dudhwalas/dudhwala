using System;
using Catalog.Database;
using Catalog.Domain;
using Catalog.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Catalog.Api.Test
{
    public class BrandDataSeedContributor : IDataSeedContributor, IDisposable
    {
        private readonly IRepository<Brand, Guid> _brandRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly CatalogDbContext _dbContext;

        public BrandDataSeedContributor(IRepository<Brand, Guid> brandRepository,
            IGuidGenerator guidGenerator, CatalogDbContext dbContext)
        {
            _brandRepository = brandRepository;
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
                new Brand(_guidGenerator.Create(), "xxx1", "/var/lib/files/data/indian.jpg", EnumCatalogStatus.ACTIVE, realmId),
                new Brand(_guidGenerator.Create(), "xxx2", "/var/lib/files/data/indian.jpg", EnumCatalogStatus.ACTIVE, realmId),
                new Brand(_guidGenerator.Create(), "xxx3", "/var/lib/files/data/indian.jpg", EnumCatalogStatus.ACTIVE, realmId),
            }, true);
        }
    }
}

