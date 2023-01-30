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

        [Fact]
        public void WhenEntityComparedWithNull_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity = new MockEntity();

            //Act
            

            //Assert
            Assert.False(_mockEntity.Equals(null));
        }

        [Fact]
        public void WhenEntityComparedWithOtherEntity_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            var _mockEntity2 = new MockEntity();

            //Act


            //Assert
            Assert.False(_mockEntity1.Equals(_mockEntity2));
        }

        [Fact]
        public void WhenEntityComparedWithOtherType_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            var _mockEntity2 = new object();

            //Act


            //Assert
            Assert.False(_mockEntity1.Equals(_mockEntity2));
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithOtherEntityHavingNoId_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            var _mockEntity2 = new MockEntity();
            var Id1 = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id1;

            //Assert
            Assert.False(_mockEntity1.Equals(_mockEntity2));
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithOtherEntityHavingDifferentId_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            var _mockEntity2 = new MockEntity();
            var Id1 = Guid.NewGuid();
            var Id2 = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.False(_mockEntity1.Equals(_mockEntity2));
        }

        [Fact]
        public void WhenEntityHavingEmptyIdComparedWithOtherEntityHavingEmptyId_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            var _mockEntity2 = new MockEntity();
            var Id1 = Guid.Empty;
            var Id2 = Guid.Empty;

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.False(_mockEntity1.Equals(_mockEntity2));
        }

        [Fact]
        public void WhenEntityHavingEmptyIdComparedWithOtherEntityHavingDifferentId_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            var _mockEntity2 = new MockEntity();
            var Id1 = Guid.Empty;
            var Id2 = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.False(_mockEntity1.Equals(_mockEntity2));
        }

        [Fact]
        public void WhenEntityHavingDifferentIdComparedWithOtherEntityHavingEmptyId_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            var _mockEntity2 = new MockEntity();
            var Id1 = Guid.NewGuid();
            var Id2 = Guid.Empty;

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.False(_mockEntity1.Equals(_mockEntity2));
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithOtherEntityHavingSameId_EqualsShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            var _mockEntity2 = new MockEntity();
            var Id = Guid.NewGuid();
            
            //Act
            _mockEntity1.Id = Id;
            _mockEntity2.Id = Id;

            //Assert
            Assert.True(_mockEntity1.Equals(_mockEntity2));
        }

        [Fact]
        public void WhenEntityHavingNoIdComparedWithSameEntity_EqualsShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            
            //Act
            
            //Assert
            Assert.True(_mockEntity1.Equals(_mockEntity1));
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithSameEntity_EqualsShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            var Id = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id;

            //Assert
            Assert.True(_mockEntity1.Equals(_mockEntity1));
        }

        [Fact]
        public void WhenEntityHavingEmptyIdComparedWithSameEntity_EqualsShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            var Id = Guid.Empty;

            //Act
            _mockEntity1.Id = Id;

            //Assert
            Assert.True(_mockEntity1.Equals(_mockEntity1));
        }

        [Fact]
        public void WhenEntityComparedWithNull_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity();
            var _mockEntity2 = default(MockEntity);

            //Act


            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenNullComparedWithEntity_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = default(MockEntity);
            var _mockEntity2 = new MockEntity();

            //Act


            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }
    }

    class MockEntity : Entity
	{

	}
}

