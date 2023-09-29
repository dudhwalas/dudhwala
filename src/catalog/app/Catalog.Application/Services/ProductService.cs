using System.Linq.Dynamic.Core.Exceptions;
using Catalog.Domain;
using Catalog.Domain.Shared;
using Catalog.Domain.Shared.Localization;
using Grpc.Core;
using Microsoft.Extensions.Localization;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectMapping;
using static Catalog.Application.ProductService;

namespace Catalog.Application.Services
{
    public class ProductService : ProductServiceBase, IApplicationService
    {
        private readonly IProductRepository _productRepo;
        private readonly IObjectMapper _objMapper;
        private readonly IProductManager _productManager;
        private readonly IStringLocalizer<CatalogResource> _localizer;

        public ProductService(IProductRepository ProductRepo, IObjectMapper objMapper, IStringLocalizer<CatalogResource> localizer, IProductManager ProductManager)
        {
            _productRepo = ProductRepo;
            _objMapper = objMapper;
            _productManager = ProductManager;
            _localizer = localizer;
        }

        public override async Task<ProductDto> GetProduct(GetProductRequestDto request, ServerCallContext context)
        {
            try
            {
                request.Id = Check.NotNullOrEmpty(request.Id, nameof(request.Id));
                var Product = await _productRepo.GetByIdAsync(Guid.Parse(request.Id));

                if (Product is null)
                    throw new RpcException(new Status(StatusCode.NotFound, _localizer[CatalogErrorCodes.Product_NotAvailable]));

                return _objMapper.Map<Product, ProductDto>(Product);
            }
            catch (EntityNotFoundException ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
            catch (FormatException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
        }

        public override async Task<ListProductResponseDto> ListProduct(ListProductRequestDto request, ServerCallContext context)
        {
            try
            {
                if (request.PageToken <= 0 || request.PageSize <= 0)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.Product_InvalidPageTokenPageSize]));

                var ProductDto = _objMapper.Map<List<Product>, List<ProductDto>>(await _productRepo.GetAsync(string.IsNullOrWhiteSpace(request.BrandId) ? Guid.Empty : Guid.Parse(request.BrandId), request.PageToken - 1, request.PageSize, request.Sorting));

                if (ProductDto.IsNullOrEmpty())
                    throw new RpcException(new Status(StatusCode.NotFound, _localizer[CatalogErrorCodes.Product_NotAvailable]));

                var totalCount = await _productRepo.GetTotalAsync();

                var response = new ListProductResponseDto
                {
                    TotalSize = totalCount,
                    NextPageToken = request.PageToken * request.PageSize >= totalCount ? 1 : request.PageToken + 1,
                };
                response.Products.AddRange(ProductDto);
                return response;
            }
            catch (ParseException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.Product_InvalidSortFields,request.Sorting]));
            }
            catch (FormatException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
        }

        public override async Task<ProductDto> UpdateProduct(ProductDto request, ServerCallContext context)
        {
            try
            {
                request.Name = Check.NotNullOrEmpty(request.Name, nameof(request.Name));
                request.Image = Check.NotNullOrEmpty(request.Image, nameof(request.Image));
                request.Status = Check.NotNull(request.Status, nameof(request.Status));
                request.RealmId = Check.NotNullOrEmpty(request.RealmId, nameof(request.RealmId));
                request.BrandId = Check.NotNullOrEmpty(request.BrandId, nameof(request.BrandId));

                var createdProduct = await _productManager.UpdateAsync(string.IsNullOrEmpty(request.Id) ?
                    Guid.Empty : Guid.Parse(request.Id),
                    request.Name, request.Image,
                    (EnumCatalogStatus)request.Status,
                    string.IsNullOrEmpty(request.RealmId) ? Guid.Empty : Guid.Parse(request.RealmId),
                    string.IsNullOrEmpty(request.BrandId) ? Guid.Empty : Guid.Parse(request.BrandId));
                return _objMapper.Map<Product, ProductDto>(createdProduct);
            }
            catch (FormatException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
            catch (BusinessException ex)
            {
                if (ex.Code == CatalogErrorCodes.Product_NameAlreadyExist)
                    throw new RpcException(new Status(StatusCode.AlreadyExists, _localizer[ex.Code, request.Name]));

                if (ex.Code == CatalogErrorCodes.Product_UpdateFailed)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code, request.Id]));

                if (ex.Code == CatalogErrorCodes.Product_CreateFailed)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code]));

                if (ex.Code == CatalogErrorCodes.Product_BrandNotAvailable)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code, request.BrandId]));

                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
        }

        public override async Task<ProductDto> PatchProduct(UpdateProductRequestDto request, ServerCallContext context)
        {
            try
            {
                request.Product = Check.NotNull(request.Product, nameof(request.Product));

                request.Product.Id = Check.NotNullOrEmpty(request.Product.Id, nameof(request.Product.Id));

                if (request.FieldToUpdate == null)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.Product_UpdateFailed_MissingProductFields]));

                var ProductToPatch = new ProductDto();

                request.FieldToUpdate.Merge(request.Product, ProductToPatch);

                var patchedProduct = await _productManager.PatchAsync(Guid.Parse(request.Product.Id),
                    ProductToPatch.Name,
                    ProductToPatch.Image,

                    request.FieldToUpdate.Paths.Contains(nameof(ProductToPatch.Status)) ?
                    (EnumCatalogStatus)ProductToPatch.Status : null,

                    string.IsNullOrEmpty(ProductToPatch.RealmId) ?
                    null : Guid.Parse(ProductToPatch.RealmId),

                    string.IsNullOrEmpty(ProductToPatch.BrandId) ?
                    null : Guid.Parse(ProductToPatch.BrandId));

                return _objMapper.Map<Product, ProductDto>(patchedProduct);
            }
            catch (FormatException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
            catch (BusinessException ex)
            {
                if (ex.Code == CatalogErrorCodes.Product_UpdateFailed)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code, request.Product.Id]));

                if (ex.Code == CatalogErrorCodes.Product_NameAlreadyExist)
                    throw new RpcException(new Status(StatusCode.AlreadyExists, _localizer[ex.Code, request.Product.Name]));

                if (ex.Code == CatalogErrorCodes.Product_BrandNotAvailable)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code, request.Product.BrandId]));

                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
        }
    }
}