using System;
namespace Catalog.Domain.SeedWork
{
	public interface IDomainService<T,T1> where T : IAggregateRoot where T1 : IRepository<T>
	{
		T1 Repository { get;}
	}
}