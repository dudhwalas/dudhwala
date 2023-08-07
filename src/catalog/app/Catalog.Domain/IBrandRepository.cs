using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace Catalog.Domain
{
	public interface IBrandRepository<TEntity,TId>
	{
        public Task<TEntity> GetBrandByNameAsync([NotNull] string brandName);

        public Task<TEntity> GetBrandByIdAsync([NotNull] TId id);

        public Task<TEntity> CreateAsync([NotNull] TEntity _entity);
    }
}

