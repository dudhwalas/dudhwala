using System;
using Catalog.Domain.SeedWork;

namespace Catalog.Domain.AggregateModel.BrandAggregate
{
    public class BrandDomainService : IBrandDomainService
    {
        private readonly IBrandRepository _repository;

        public BrandDomainService(IBrandRepository repository)
        {
            _repository = repository;
        }

        public IBrandRepository Repository => _repository;

        public Task<BrandEntity> AddBrand(BrandEntity brand)
        {
            return Repository.AddBrand(brand);
        }
    }
}