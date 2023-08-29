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

        public async Task<Brand> CreateAsync([NotNull] string name, [NotNull] string image, EnumStatus status,Guid realmId)
        {
            var brand = await _brandRepository.GetByNameAsync(name);
            if (brand is not null)
            {
                throw new BusinessException(CatalogErrorCodes.BrandAlreadyExist);
            }
            return await _brandRepository.CreateAsync(new Brand(_guidGenerator.Create(), name, image, status, realmId));
        }
	}
}