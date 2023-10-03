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
using static Catalog.Application.BrandService;

namespace Catalog.Application.Services
{
    public class BrandService : BrandServiceBase, IApplicationService
    {
        private readonly IBrandRepository _brandRepo;
        private readonly IObjectMapper _objMapper;
        private readonly IBrandManager _brandManager;
        private readonly IStringLocalizer<CatalogResource> _localizer;

        public BrandService(IBrandRepository brandRepo, IObjectMapper objMapper, IStringLocalizer<CatalogResource> localizer, IBrandManager brandManager)
        {
            _brandRepo = brandRepo;
            _objMapper = objMapper;
            _brandManager = brandManager;
            _localizer = localizer;
        }

        public override async Task<BrandDto> GetBrand(GetBrandRequestDto request, ServerCallContext context)
        {
            try
            {
                request.Id = Check.NotNullOrEmpty(request.Id, nameof(request.Id));
                var brand = await _brandRepo.GetByIdAsync(Guid.Parse(request.Id));

                if (brand is null)
                    throw new RpcException(new Status(StatusCode.NotFound, _localizer[CatalogErrorCodes.Brand_NotAvailable]));

                return _objMapper.Map<Brand, BrandDto>(brand);
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

        public override async Task<ListBrandResponseDto> ListBrand(ListBrandRequestDto request, ServerCallContext context)
        {
            try
            {

                if (request.PageToken <= 0 || request.PageSize <= 0)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.Brand_InvalidPageTokenPageSize]));

                var brands = await _brandRepo.GetAsync(request.PageToken - 1, request.PageSize, request.Sorting);

                if (!brands.Any())
                    throw new RpcException(new Status(StatusCode.NotFound, _localizer[CatalogErrorCodes.Brand_NotAvailable]));

                var brandDto = _objMapper.Map<List<Brand>, List<BrandDto>>(brands);

                var totalCount = await _brandRepo.GetTotalAsync();

                var response = new ListBrandResponseDto
                {
                    TotalSize = totalCount,
                    NextPageToken = request.PageToken * request.PageSize >= totalCount ? 1 : request.PageToken + 1,
                };
                response.Brands.AddRange(brandDto);
                return response;
            }
            catch (ParseException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.Brand_InvalidSortFields, request.Sorting]));
            }
        }

        public override async Task<BrandDto> UpdateBrand(BrandDto request, ServerCallContext context)
        {
            try
            {
                request.Name = Check.NotNullOrEmpty(request.Name, nameof(request.Name));
                request.Image = Check.NotNullOrEmpty(request.Image, nameof(request.Image));
                request.Status = Check.NotNull(request.Status, nameof(request.Status));
                request.RealmId = Check.NotNullOrEmpty(request.RealmId, nameof(request.RealmId));

                var createdBrand = await _brandManager.UpdateAsync(string.IsNullOrEmpty(request.Id) ? Guid.Empty : Guid.Parse(request.Id), request.Name, request.Image, (EnumCatalogStatus)request.Status, string.IsNullOrEmpty(request.RealmId) ? Guid.Empty : Guid.Parse(request.RealmId));
                return _objMapper.Map<Brand, BrandDto>(createdBrand);
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
                if (ex.Code == CatalogErrorCodes.Brand_NameAlreadyExist)
                    throw new RpcException(new Status(StatusCode.AlreadyExists, _localizer[ex.Code, request.Name]));

                if (ex.Code == CatalogErrorCodes.Brand_UpdateFailed)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code, request.Id]));

                if (ex.Code == CatalogErrorCodes.Brand_CreateFailed)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code]));

                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        public override async Task<BrandDto> PatchBrand(UpdateBrandRequestDto request, ServerCallContext context)
        {
            try
            {
                request.Brand = Check.NotNull(request.Brand, nameof(request.Brand));

                request.Brand.Id = Check.NotNullOrWhiteSpace(request.Brand.Id, nameof(request.Brand.Id));

                if (request.FieldToUpdate is null || request.FieldToUpdate.Paths.IsNullOrEmpty())
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.Brand_UpdateFailed_MissingBrandFields]));

                var brandToPatch = new BrandDto();

                request.FieldToUpdate.Merge(request.Brand, brandToPatch);

                if (!request.FieldToUpdate.Paths.Any(path =>
                    path.Equals(nameof(brandToPatch.Status),StringComparison.CurrentCultureIgnoreCase) ||
                    path.Equals(nameof(brandToPatch.Name), StringComparison.CurrentCultureIgnoreCase) ||
                    path.Equals(nameof(brandToPatch.Image), StringComparison.CurrentCultureIgnoreCase) ||
                    path.Equals(nameof(brandToPatch.RealmId), StringComparison.CurrentCultureIgnoreCase)
                ))
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.Brand_UpdateFailed_MissingBrandFields]));

                var patchedBrand = await _brandManager.PatchAsync(Guid.Parse(request.Brand.Id), brandToPatch.Name, brandToPatch.Image, request.FieldToUpdate.Paths.Contains(nameof(brandToPatch.Status).ToLower()) ? (EnumCatalogStatus)brandToPatch.Status : null, string.IsNullOrEmpty(brandToPatch.RealmId) ? null : Guid.Parse(brandToPatch.RealmId));

                return _objMapper.Map<Brand, BrandDto>(patchedBrand);
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
                if (ex.Code == CatalogErrorCodes.Brand_UpdateFailed)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code, request.Brand.Id]));

                if (ex.Code == CatalogErrorCodes.Brand_NameAlreadyExist)
                    throw new RpcException(new Status(StatusCode.AlreadyExists, _localizer[ex.Code, request.Brand.Name]));

                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
        }
    }
}