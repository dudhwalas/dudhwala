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

        public async Task<Brand> CreateAsync([NotNull] Brand brandToCreate)
        {
            var brand = await _brandRepository.GetByNameAsync(brandToCreate.Name ?? "");
            if (brand is not null)
            {
                throw new BusinessException(CatalogErrorCodes.BrandAlreadyExist);
            }
            brandToCreate.SetId(_guidGenerator.Create());
            return await _brandRepository.CreateAsync(brandToCreate);
        }
	}
}