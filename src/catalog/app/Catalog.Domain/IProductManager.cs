using Catalog.Domain.Shared;
using JetBrains.Annotations;

namespace Catalog.Domain
{
    public interface IProductManager
    {
        public Task<Product> UpdateAsync([NotNull] Guid id,
            [NotNull] string name,
            [NotNull] string image,
            EnumCatalogStatus status,
            Guid realmId,
            Guid brandId
            );

        public Task<Product> PatchAsync(Guid id,
            string? name,
            string? image,
            EnumCatalogStatus? status,
            Guid? realmId,
            Guid? brandId);
    }
}