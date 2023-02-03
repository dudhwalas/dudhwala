using System;
using Catalog.Domain.SeedWork;

namespace Catalog.Domain.AggregateModel.BrandAggregate
{
	public interface IBrandDomainService : IDomainService<BrandEntity,IBrandRepository>
	{
		Task<BrandEntity> AddBrand(BrandEntity brand);
	}
}