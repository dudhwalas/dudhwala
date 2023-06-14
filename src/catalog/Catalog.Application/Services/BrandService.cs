using Catalog.Domain;
using Catalog.Domain.Shared;
using Grpc.Core;
using Volo.Abp.DependencyInjection;
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
        private readonly IGuidGenerator _guidGenerator;

        public BrandService(IRepository<Brand,Guid> brandRepo, IObjectMapper objMapper, BrandManager brandManager)
		{
            _brandRepo = brandRepo;
            _objMapper = objMapper;
            _brandManager = brandManager;
            _guidGenerator = _brandManager.LazyServiceProvider.LazyGetService<IGuidGenerator>();
        }

        public override async Task<BrandDto> GetBrand(GetBrandRequest request, ServerCallContext context)
        {
            var brandDto = _objMapper.Map<Brand,BrandDto>(await _brandRepo.GetAsync(Guid.Parse(request.Id)));
            return brandDto;
        }

        [UnitOfWork]
        public override async Task<BrandDto> AddBrand(BrandDto request, ServerCallContext context)
        {
            var createdBrand = await _brandManager.CreateAsync(request.Name, request.Image, (EnumStatus)request.Status, _guidGenerator.Create());
            return _objMapper.Map<Brand,BrandDto>(createdBrand);
        }
    }
}