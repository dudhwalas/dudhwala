using System;
using Catalog.Domain.SeedWork;

namespace Catalog.UnitTest.Domain.Seedwork
{
	public class UnitOfWorkTests
	{
        [Fact]
        public async void SaveChangesAsyncShouldReturnTrue()
        {
            //Arrange 
            var mockUnitOfWork = new MockUnitOfWork();

            //Act
            var result = await mockUnitOfWork.SaveChangesAsync();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void WhenDisposeIsCalled_IsDisposeCalledShouldReturnTrue()
        {
            //Arrange 
            var mockUnitOfWork = new MockUnitOfWork();

            //Act
            mockUnitOfWork.Dispose();

            //Assert
            Assert.True(mockUnitOfWork.IsDisposeCalled);
        }
    }

    class MockUnitOfWork : IUnitOfWork
    {
        public bool IsDisposeCalled { get; private set; } = false;
        public void Dispose()
        {
            IsDisposeCalled = true;
        }

        public Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }
    }
}

