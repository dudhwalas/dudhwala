using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace Catalog.Domain
{
    public interface IProductRepository: IRepository<Product, Guid>
    {
        public Task<Product?> GetByNameAsync([NotNull] string productName);

        public Task<Product?> GetByIdAsync([NotNull] Guid id);

        public Task<Product?> CreateAsync([NotNull] Product _entity);

        public Task<Product?> UpdateAsync([NotNull] Product _entity);

        public Task<List<Product>> GetAsync([NotNull] int pageToken, [NotNull]int pageSize,string? sorting);

        public Task<List<Product>> GetAsync([NotNull] Guid brandId,[NotNull] int pageToken, [NotNull] int pageSize, string? sorting);

        public Task<long> GetTotalAsync();
    }
}