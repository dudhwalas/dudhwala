using System;
using Catalog.Domain;
using Grpc.Core;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using static Catalog.Application.BrandService;

namespace Catalog.Application.Services
{
	public class BrandService : BrandServiceBase
    {
        private readonly IRepository<Brand, Guid> _brandRepo;
        private readonly IObjectMapper _objMapper;

        public BrandService(IRepository<Brand, Guid> brandRepo, IObjectMapper objMapper)
		{
            _brandRepo = brandRepo;
            _objMapper = objMapper;

        }

        public override async Task<BrandDto> GetBrand(GetBrandRequest request, ServerCallContext context)
        {
            var brandDto = _objMapper.Map<Brand, BrandDto>(await _brandRepo.GetAsync(Guid.Parse(request.Id)));

            return brandDto;
        }
    }
}

