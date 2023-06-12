using System;
using Grpc.Core;
using static Catalog.Application.BrandService;

namespace Catalog.Application.Services
{
	public class BrandService : BrandServiceBase
    {
		public BrandService()
		{
		}

        public override Task<Brand> GetBrand(GetBrandRequest request, ServerCallContext context)
        {
            return base.GetBrand(request, context);
        }

    }
}

