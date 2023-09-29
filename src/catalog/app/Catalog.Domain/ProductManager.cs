using Catalog.Domain.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Catalog.Domain
{
    public class ProductManager : DomainService, IProductManager
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IGuidGenerator _guidGenerator;

        public ProductManager(IProductRepository productRepository, IBrandRepository brandRepository, IGuidGenerator guidGenerator)
		{
            _ProductRepository = productRepository;
            _guidGenerator = guidGenerator;
            _brandRepository = brandRepository;
        }

        public async Task<Product> UpdateAsync([NotNull] Guid id,
            [NotNull] string name,
            [NotNull] string image,
            EnumCatalogStatus status,
            Guid realmId,
            Guid brandId
            )
        {
            var existingBrand = await _brandRepository.GetByIdAsync(brandId);
            if (existingBrand == null)
                throw new BusinessException(CatalogErrorCodes.Product_BrandNotAvailable);

            var ProductNameToCheck = await _ProductRepository.GetByNameAsync(name);
            if (ProductNameToCheck is not null)
                throw new BusinessException(CatalogErrorCodes.Product_NameAlreadyExist);

            if (id != Guid.Empty)
            {
                var existingProduct = await _ProductRepository.GetByIdAsync(id);

                if (existingProduct is not null)
                {
                    var updatedProduct = await _ProductRepository.UpdateAsync(new Product(id, name, image, status, realmId, brandId));

                    if (updatedProduct == null)
                        throw new BusinessException(CatalogErrorCodes.Product_UpdateFailed);

                    return updatedProduct;
                }
            }
            
            var createdProduct = await _ProductRepository.CreateAsync(new Product(_guidGenerator.Create(), name, image, status, realmId, brandId));

            if(createdProduct is null)
                throw new BusinessException(CatalogErrorCodes.Product_CreateFailed);

            return createdProduct;
        }

        public async Task<Product> PatchAsync(Guid id,
            string? name,
            string? image,
            EnumCatalogStatus? status,
            Guid? realmId,
            Guid? brandId)
        {
            if (id != Guid.Empty)
            {
                var existingProduct = await _ProductRepository.GetByIdAsync(id);

                if (existingProduct is not null)
                {
                    if (!name.IsNullOrWhiteSpace())
                    {
                        var ProductNameToCheck = await _ProductRepository.GetByNameAsync(name??"");
                        if (ProductNameToCheck is not null)
                            throw new BusinessException(CatalogErrorCodes.Product_NameAlreadyExist);
                        existingProduct.SetName(name??"");
                    }

                    if (!image.IsNullOrWhiteSpace())
                        existingProduct.SetImage(image??"");

                    if (status != null)
                        existingProduct.SetStatus(status.Value);

                    if (realmId != null)
                        existingProduct.SetRealmId(realmId.Value);

                    if (brandId != null)
                        existingProduct.SetBrandId(brandId.Value);

                    var existingBrand = await _brandRepository.GetByIdAsync(existingProduct.BrandId);
                    if (existingBrand == null)
                        throw new BusinessException(CatalogErrorCodes.Product_BrandNotAvailable);

                    var updatedProduct = await _ProductRepository.UpdateAsync(existingProduct);

                    if (updatedProduct == null)
                        throw new BusinessException(CatalogErrorCodes.Product_UpdateFailed);

                    return updatedProduct;
                }
            }
            throw new BusinessException(CatalogErrorCodes.Product_UpdateFailed);
        }
    }
}