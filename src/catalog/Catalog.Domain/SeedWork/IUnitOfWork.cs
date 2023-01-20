using System;
namespace Catalog.Domain.SeedWork
{
	/// <summary>
	/// Add unit of work behaviour to perform business transaction.
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Perform the business transaction.
		/// </summary>
		/// <returns>True - If transaction is successful.</returns>
		Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}