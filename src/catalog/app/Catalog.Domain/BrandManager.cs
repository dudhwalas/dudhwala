using Catalog.Domain.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Catalog.Domain
{
    public class BrandManager : DomainService
	{
        private readonly IRepository<Brand,Guid> _brandRepository;
        private readonly IGuidGenerator _guidGenerator;

        public BrandManager(IRepository<Brand, Guid> brandRepository,IGuidGenerator guidGenerator)
		{
            _brandRepository = brandRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task<Brand> CreateAsync([NotNull] Guid id,[NotNull] string name, [NotNull] string image, EnumStatus status,Guid realmId)
        {
            if (id != Guid.Empty)
            {
                var existingBrand = await _brandRepository.GetByIdAsync(id);

                if (existingBrand is not null)
                {
                    var updatedBrand = await _brandRepository.UpdateAsync(new Brand(id, name, image, status, realmId));

                    if (updatedBrand == null)
                        throw new BusinessException(CatalogErrorCodes.UpdateBrandFailed);

                    return updatedBrand;
                }
            }
            var brandToCheck = await _brandRepository.GetByNameAsync(name);
            if (brandToCheck is not null)
                throw new BusinessException(CatalogErrorCodes.BrandAlreadyExist);
            
            var createdBrand = await _brandRepository.CreateAsync(new Brand(_guidGenerator.Create(), name, image, status, realmId));

            if(createdBrand is null)
                throw new BusinessException(CatalogErrorCodes.CreateBrandFailed);
            return createdBrand;
        }
    }
}