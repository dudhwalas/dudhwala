using JetBrains.Annotations;

namespace Catalog.Domain
{
    public interface IRepository<TEntity,TId>
	{
        public Task<TEntity> GetByNameAsync([NotNull] string brandName);

        public Task<TEntity> GetByIdAsync([NotNull] TId id);

        public Task<TEntity> CreateAsync([NotNull] TEntity _entity);
    }
}

