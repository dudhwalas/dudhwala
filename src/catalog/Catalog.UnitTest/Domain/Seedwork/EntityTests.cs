using System;
using Catalog.Domain.SeedWork;

namespace Catalog.UnitTest.Domain.Seedwork
{
	public class EntityTests
	{
		public EntityTests()
		{
        }

		[Fact]
		public void WithIdNotSet_GetIdShouldReturnDefault()
		{
            //Arrange
            var _mockEntity = new MockEntity();

			//Act

			//Assert
			Assert.Equal(_mockEntity.Id, default(Guid));
        }

        [Fact]
        public void WithIdSetToGuid_GetIdShouldReturnGuid()
        {
            //Arrange
            var _mockEntity = new MockEntity();
            var _mockGuid = Guid.NewGuid();

            //Act
            _mockEntity.Id = _mockGuid;

            //Assert
            Assert.Equal(_mockEntity.Id, _mockGuid);
        }

        [Fact]
        public void WithIdNotSet_IsTransientShouldReturnTrue()
        {
            //Arrange
            var _mockEntity = new MockEntity();

            //Act

            //Assert
            Assert.True(_mockEntity.IsTransient);
        }

        [Fact]
        public void WithIdNotSetToEmptyGuid_IsTransientShouldReturnTrue()
        {
            //Arrange
            var _mockEntity = new MockEntity();

            //Act
            _mockEntity.Id = Guid.Empty;

            //Assert
            Assert.True(_mockEntity.IsTransient);
        }

        [Fact]
        public void WithIdNotSetToGuid_IsTransientShouldReturnFalse()
        {
            //Arrange
            var _mockEntity = new MockEntity();

            //Act
            _mockEntity.Id = Guid.NewGuid();

            //Assert
            Assert.False(_mockEntity.IsTransient);
        }

        [Fact]
        public void WithIdNotSet_GetHashcodeShouldNotReturnZero()
        {
            //Arrange
            var _mockEntity = new MockEntity();

            //Act
            
            //Assert
            Assert.NotEqual(0,_mockEntity.GetHashCode());
        }

        [Fact]
        public void WithIdSetToEmptyGuid_GetHashcodeShouldNotReturnZero()
        {
            //Arrange
            var _mockEntity = new MockEntity();

            //Act
            _mockEntity.Id = Guid.Empty;

            //Assert
            Assert.NotEqual(0, _mockEntity.GetHashCode());
        }

        [Fact]
        public void WithIdSetToGuid_GetHashcodeShouldNotReturnZero()
        {
            //Arrange
            var _mockEntity = new MockEntity();

            //Act
            _mockEntity.Id = Guid.NewGuid();

            //Assert
            Assert.NotEqual(0, _mockEntity.GetHashCode());
        }
    }

    class MockEntity : Entity
	{

	}
}

