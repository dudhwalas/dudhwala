using System;
namespace Catalog.Domain.SeedWork
{
	/// <summary>
	/// Add behaviour to perform database operation.
	/// </summary>
	public interface IRepository<T> where T : IAggregateRoot
	{
		/// <summary>
		/// Unit of work associated with repository.
		/// </summary>
		public IUnitOfWork UnitOfWork { get; }
	}
}

