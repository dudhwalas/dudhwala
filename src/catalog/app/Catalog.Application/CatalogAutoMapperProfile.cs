using AutoMapper;
using Catalog.Domain;

namespace Catalog.Application
{
    public class CatalogAutoMapperProfile : Profile
    {
		public CatalogAutoMapperProfile()
		{
			CreateMap<Brand, BrandDto>();
        }
	}
}

