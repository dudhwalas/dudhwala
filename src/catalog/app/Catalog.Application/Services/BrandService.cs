using Catalog.Domain;
using Catalog.Domain.Shared;
using Catalog.Domain.Shared.Localization;
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
        private readonly IRepository<Brand,Guid> _brandRepo;
        private readonly IObjectMapper _objMapper;
        private readonly BrandManager _brandManager;
        private readonly IStringLocalizer<CatalogResource> _localizer;

        public BrandService(IRepository<Brand, Guid> brandRepo, IObjectMapper objMapper, IStringLocalizer<CatalogResource> localizer, BrandManager brandManager)
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
                var brandDto = _objMapper.Map<Brand, BrandDto>(await _brandRepo.GetByIdAsync(Guid.Parse(request.Id)));
                return brandDto;
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
            if (request.PageToken <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.NoBrandExist]));

            var brandDto = _objMapper.Map<List<Brand>, List<BrandDto>>(await _brandRepo.GetAsync(request.PageToken-1,request.PageSize));

            if (!brandDto.Any())
                throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.NoBrandExist]));

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
        public override async Task<BrandDto> UpdateBrand(CreateBrandRequestDto request, ServerCallContext context)
        {
            try
            {
                var createdBrand = await _brandManager.CreateAsync(string.IsNullOrEmpty(request.Id) ? Guid.Empty : Guid.Parse(request.Id), request.Name, request.Image, (EnumStatus)request.Status, string.IsNullOrEmpty(request.RealmId) ? Guid.Empty : Guid.Parse(request.RealmId));
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
                if (ex.Code == CatalogErrorCodes.BrandAlreadyExist)
                    throw new RpcException(new Status(StatusCode.AlreadyExists, _localizer[ex.Code, request.Name]));

                if (ex.Code == CatalogErrorCodes.UpdateBrandFailed)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code, request.Id]));

                if (ex.Code == CatalogErrorCodes.CreateBrandFailed)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[ex.Code]));

                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
        }
    }
}