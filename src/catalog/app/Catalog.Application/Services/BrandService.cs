using Catalog.Domain;
using Catalog.Domain.Shared;
using Catalog.Domain.Shared.Localization;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Localization;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using static Catalog.Application.BrandService;

namespace Catalog.Application.Services
{
    public class BrandService : BrandServiceBase, ITransientDependency
    {
        private readonly IBrandRepository _brandRepo;
        private readonly IObjectMapper _objMapper;
        private readonly BrandManager _brandManager;
        private readonly IStringLocalizer<CatalogResource> _localizer;

        public BrandService(IBrandRepository brandRepo, IObjectMapper objMapper, IStringLocalizer<CatalogResource> localizer, BrandManager brandManager)
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

                if(brand is null)
                    throw new RpcException(new Status(StatusCode.NotFound, _localizer[CatalogErrorCodes.NoBrandAvailable]));

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
            if (request.PageToken <= 0 || request.PageSize <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.InvalidPageTokenPageSize]));

            var brandDto = _objMapper.Map<List<Brand>, List<BrandDto>>(await _brandRepo.GetAsync(request.PageToken-1,request.PageSize));

            if (!brandDto.Any())
                throw new RpcException(new Status(StatusCode.NotFound, _localizer[CatalogErrorCodes.NoBrandAvailable]));

            var totalCount = await _brandRepo.GetTotalAsync();

            var response = new ListBrandResponseDto
            {
                TotalSize = totalCount,
                NextPageToken = request.PageToken * request.PageSize >= totalCount? 1 : request.PageToken + 1,
            };
            response.Brands.AddRange(brandDto);
            return response;
        }

        [UnitOfWork]
        public override async Task<BrandDto> UpdateBrand(BrandDto request, ServerCallContext context)
        {
            try
            {

                request.Name = Check.NotNullOrEmpty(request.Name, nameof(request.Name));
                request.Image = Check.NotNullOrEmpty(request.Image, nameof(request.Image));
                request.Status = Check.NotNull(request.Status, nameof(request.Status));
                request.RealmId = Check.NotNullOrEmpty(request.RealmId, nameof(request.RealmId));

                var createdBrand = await _brandManager.UpdateAsync(string.IsNullOrEmpty(request.Id) ? Guid.Empty : Guid.Parse(request.Id), request.Name, request.Image, (EnumStatus)request.Status, string.IsNullOrEmpty(request.RealmId) ? Guid.Empty : Guid.Parse(request.RealmId));
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
                if (ex.Code == CatalogErrorCodes.BrandNameAlreadyExist)
                    throw new RpcException(new Status(StatusCode.AlreadyExists, _localizer[ex.Code, request.Name]));

                if (ex.Code == CatalogErrorCodes.UpdateBrandFailed)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code, request.Id]));

                if (ex.Code == CatalogErrorCodes.CreateBrandFailed)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code]));

                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
        }

        public override async Task<BrandDto> PatchBrand(UpdateBrandRequestDto request, ServerCallContext context)
        {
            try
            {
                request.Brand = Check.NotNull(request.Brand, nameof(request.Brand));

                request.Brand.Id = Check.NotNullOrEmpty(request.Brand.Id, nameof(request.Brand.Id));

                if (request.FieldToUpdate == null)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.UpdateMissingBrandFields]));

                var brandToPatch = new BrandDto();

                request.FieldToUpdate.Merge(request.Brand, brandToPatch);

                var patchedBrand = await _brandManager.PatchAsync(Guid.Parse(request.Brand.Id), brandToPatch.Name, brandToPatch.Image, request.FieldToUpdate.Paths.Contains(nameof(brandToPatch.Status)) ? (EnumStatus)brandToPatch.Status : null, string.IsNullOrEmpty(brandToPatch.RealmId) ? null : Guid.Parse(brandToPatch.RealmId));
                return _objMapper.Map<Brand, BrandDto>(patchedBrand);
            }
            catch (FormatException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
            catch (BusinessException ex)
            {
                if (ex.Code == CatalogErrorCodes.UpdateBrandFailed)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code, request.Brand.Id]));

                if (ex.Code == CatalogErrorCodes.BrandNameAlreadyExist)
                    throw new RpcException(new Status(StatusCode.AlreadyExists, _localizer[ex.Code, request.Brand.Name]));

                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
        }
    }
}