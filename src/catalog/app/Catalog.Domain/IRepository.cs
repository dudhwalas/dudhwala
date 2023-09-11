using System.Security.Cryptography;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace Catalog.Domain
{
    public interface IBrandRepository: IRepository<Brand, Guid>
    {
        public Task<Brand?> GetByNameAsync([NotNull] string brandName);

        public Task<Brand?> GetByIdAsync([NotNull] Guid id);

        public Task<Brand?> CreateAsync([NotNull] Brand _entity);

        public Task<Brand?> UpdateAsync([NotNull] Brand _entity);

        public Task<List<Brand>> GetAsync([NotNull] int pageToken, [NotNull]int pageSize);

        public Task<long> GetTotalAsync();
    }
}

