using System;
using Catalog.Domain.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Catalog.Domain
{
	public class BrandManager : DomainService
	{
        private readonly IBrandRepository<Brand,Guid> _brandRepository;

        public BrandManager(IBrandRepository<Brand, Guid> brandRepository)
		{
            _brandRepository = brandRepository;
        }

        public async Task<Brand> CreateAsync([NotNull] string name, [NotNull] string image, EnumStatus status,Guid realmId)
        {
            var brand = await _brandRepository.GetBrandByNameAsync(name);
            if (brand is not null)
            {
                throw new BusinessException(CatalogErrorCodes.BrandAlreadyExist);
            }
            return await _brandRepository.CreateAsync(new Brand(GuidGenerator.Create(), name, image, status, realmId));
        }
	}
}