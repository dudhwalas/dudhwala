using System;
using Catalog.Database;
using Catalog.Domain;
using Catalog.Domain.Shared;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Catalog.Api.Test
{
	public class BrandDataSeedContributor : IDataSeedContributor, ITransientDependency
	{
        private readonly IRepository<Brand, Guid> _brandRepository;
        private readonly IGuidGenerator _guidGenerator;

        public BrandDataSeedContributor(IRepository<Brand, Guid> brandRepository,
            IGuidGenerator guidGenerator)
        {
            _brandRepository = brandRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var realmId = _guidGenerator.Create();

            await _brandRepository.InsertManyAsync(new List<Brand> {
                new Brand(_guidGenerator.Create(), "xxx1", "/var/lib/files/data/indian.jpg", EnumCatalogStatus.ACTIVE, realmId),
                new Brand(_guidGenerator.Create(), "xxx2", "/var/lib/files/data/indian.jpg", EnumCatalogStatus.ACTIVE, realmId),
                new Brand(_guidGenerator.Create(), "xxx3", "/var/lib/files/data/indian.jpg", EnumCatalogStatus.ACTIVE, realmId),
            });
        }
    }
}

