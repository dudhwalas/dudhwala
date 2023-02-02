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
			var stubRepo = new StubRepository()
			{
				UnitOfWork = new MockUnitOfWork()
			};

			//Act

			//Assert
			Assert.IsType<MockUnitOfWork>(stubRepo.UnitOfWork);
		}

        [Fact]
        public async void WithUnitOfWorkSetInStubRepository_SaveChangesAsyncShouldReturnTrue()
        {
            //Arrange 
            var stubRepo = new StubRepository()
            {
                UnitOfWork = new MockUnitOfWork()
            };

			//Act
			var result = await stubRepo.UnitOfWork.SaveChangesAsync();

            //Assert
            Assert.True(result);
        }
    }

    class MockEntity3 : Entity, IAggregateRoot
    {

    }

	class StubRepository : IRepository<MockEntity3>
	{
		
	}

	class MockUnitOfWork : IUnitOfWork
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