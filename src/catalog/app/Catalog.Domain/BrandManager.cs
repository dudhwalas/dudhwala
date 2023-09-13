using Catalog.Domain.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Catalog.Domain
{
    public class BrandManager : DomainService
	{
        private readonly IBrandRepository _brandRepository;
        private readonly IGuidGenerator _guidGenerator;

        public BrandManager(IBrandRepository brandRepository,IGuidGenerator guidGenerator)
		{
            _brandRepository = brandRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task<Brand> UpdateAsync([NotNull] Guid id,[NotNull] string name, [NotNull] string image, EnumStatus status,Guid realmId)
        {
            if (id != Guid.Empty)
            {
                var existingBrand = await _brandRepository.GetByIdAsync(id);

                if (existingBrand is not null)
                {
                    var brandNameToCheck = await _brandRepository.GetByNameAsync(name);
                    if (brandNameToCheck is not null)
                        throw new BusinessException(CatalogErrorCodes.Brand_NameAlreadyExist);

                    var updatedBrand = await _brandRepository.UpdateAsync(new Brand(id, name, image, status, realmId));

                    if (updatedBrand == null)
                        throw new BusinessException(CatalogErrorCodes.Brand_UpdateFailed);

                    return updatedBrand;
                }
            }
            var brandToCheck = await _brandRepository.GetByNameAsync(name);
            if (brandToCheck is not null)
                throw new BusinessException(CatalogErrorCodes.Brand_NameAlreadyExist);
            
            var createdBrand = await _brandRepository.CreateAsync(new Brand(_guidGenerator.Create(), name, image, status, realmId));

            if(createdBrand is null)
                throw new BusinessException(CatalogErrorCodes.Brand_CreateFailed);
            return createdBrand;
        }

        public async Task<Brand> PatchAsync(Guid id, string? name, string? image, EnumStatus? status, Guid? realmId)
        {
            if (id != Guid.Empty)
            {
                var existingBrand = await _brandRepository.GetByIdAsync(id);

                if (existingBrand is not null)
                {
                    if (!name.IsNullOrWhiteSpace())
                    {
                        var brandNameToCheck = await _brandRepository.GetByNameAsync(name);
                        if (brandNameToCheck is not null)
                            throw new BusinessException(CatalogErrorCodes.Brand_NameAlreadyExist);
                        existingBrand.SetName(name);
                    }

                    if (!image.IsNullOrWhiteSpace())
                        existingBrand.SetImage(image);

                    if (status != null)
                        existingBrand.SetStatus(status.Value);

                    if (realmId != null)
                        existingBrand.SetRealmId(realmId.Value);

                    var updatedBrand = await _brandRepository.UpdateAsync(existingBrand);

                    if (updatedBrand == null)
                        throw new BusinessException(CatalogErrorCodes.Brand_UpdateFailed);

                    return updatedBrand;
                }
            }
            throw new BusinessException(CatalogErrorCodes.Brand_UpdateFailed);
        }
    }
}