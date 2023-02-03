using System;
using Catalog.Domain.SeedWork;

namespace Catalog.UnitTest.Domain.Seedwork
{
	public class RepositoryTests
	{
		[Fact]
		public void WithUnitOfWorkSetInStubRepository_GetUnitOfWorkShouldReturnUnitOfWork()
		{
            //Arrange 
            var mockRepo = new MockRepository();

			//Act

			//Assert
			Assert.IsType<MockUnitOfWorkForRepo>(mockRepo.UnitOfWork);
		}
    }

    class MockEntity3 : Entity, IAggregateRoot
    {

    }

    class MockRepository : IRepository<MockEntity3>
    {
        public IUnitOfWork UnitOfWork => new MockUnitOfWorkForRepo();
    }

    class MockUnitOfWorkForRepo : IUnitOfWork
    {
        public void Dispose()
        {

        }

        public Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }
    }
}