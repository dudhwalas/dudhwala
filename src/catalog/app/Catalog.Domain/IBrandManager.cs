using Catalog.Domain.Shared;
using JetBrains.Annotations;

namespace Catalog.Domain
{
    public interface IBrandManager
    {
        public Task<Brand> UpdateAsync([NotNull] Guid id, [NotNull] string name, [NotNull] string image, EnumCatalogStatus status, Guid realmId);

        public Task<Brand> PatchAsync(Guid id, string? name, string? image, EnumCatalogStatus? status, Guid? realmId);
    }
}