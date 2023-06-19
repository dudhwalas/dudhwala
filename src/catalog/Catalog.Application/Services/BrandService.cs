using Catalog.Domain;
using Catalog.Domain.Shared;
using Grpc.Core;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
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

        public BrandService(IRepository<Brand,Guid> brandRepo, IObjectMapper objMapper, BrandManager brandManager)
		{
            _brandRepo = brandRepo;
            _objMapper = objMapper;
            _brandManager = brandManager;
        }

        public override async Task<BrandDto> GetBrand(GetBrandRequestDto request, ServerCallContext context)
        {
            try
            {
                var brandDto = _objMapper.Map<Brand, BrandDto>(await _brandRepo.GetAsync(Guid.Parse(request.Id)));
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

        [UnitOfWork]
        public override async Task<BrandDto> AddBrand(CreateBrandRequestDto request, ServerCallContext context)
        {
            try
            {
                var realmId = Guid.Parse(request.RealmId);
                var createdBrand = await _brandManager.CreateAsync(request.Name, request.Image, (EnumStatus)request.Status, realmId);
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
        }
    }
}