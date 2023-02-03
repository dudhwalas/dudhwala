using System;
using Catalog.Domain.SeedWork;

namespace Catalog.Domain.AggregateModel.BrandAggregate
{
    public interface  IBrandRepository : IRepository<BrandEntity>
    {
        Task<BrandEntity> AddBrand(BrandEntity brand);
    }
}