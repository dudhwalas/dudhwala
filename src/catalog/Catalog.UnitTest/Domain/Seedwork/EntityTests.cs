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
            var _mockEntity = new MockEntity1();

			//Act

			//Assert
			Assert.Equal(_mockEntity.Id, default(Guid));
        }

        [Fact]
        public void WithIdSetToGuid_GetIdShouldReturnGuid()
        {
            //Arrange
            var _mockEntity = new MockEntity1();
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
            var _mockEntity = new MockEntity1();

            //Act

            //Assert
            Assert.True(_mockEntity.IsTransient);
        }

        [Fact]
        public void WithIdNotSetToEmptyGuid_IsTransientShouldReturnTrue()
        {
            //Arrange
            var _mockEntity = new MockEntity1();

            //Act
            _mockEntity.Id = Guid.Empty;

            //Assert
            Assert.True(_mockEntity.IsTransient);
        }

        [Fact]
        public void WithIdNotSetToGuid_IsTransientShouldReturnFalse()
        {
            //Arrange
            var _mockEntity = new MockEntity1();

            //Act
            _mockEntity.Id = Guid.NewGuid();

            //Assert
            Assert.False(_mockEntity.IsTransient);
        }

        [Fact]
        public void WithIdNotSet_GetHashcodeShouldNotReturnZero()
        {
            //Arrange
            var _mockEntity = new MockEntity1();

            //Act
            
            //Assert
            Assert.NotEqual(0,_mockEntity.GetHashCode());
        }

        [Fact]
        public void WithIdSetToEmptyGuid_GetHashcodeShouldNotReturnZero()
        {
            //Arrange
            var _mockEntity = new MockEntity1();

            //Act
            _mockEntity.Id = Guid.Empty;

            //Assert
            Assert.NotEqual(0, _mockEntity.GetHashCode());
        }

        [Fact]
        public void WithIdSetToGuid_GetHashcodeShouldNotReturnZero()
        {
            //Arrange
            var _mockEntity = new MockEntity1();

            //Act
            _mockEntity.Id = Guid.NewGuid();

            //Assert
            Assert.NotEqual(0, _mockEntity.GetHashCode());
        }

        [Fact]
        public void WhenEntityComparedWithNull_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity = new MockEntity1();

            //Act
            

            //Assert
            Assert.False(_mockEntity.Equals(null));
        }

        [Fact]
        public void WhenEntityComparedWithOtherEntity_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();

            //Act


            //Assert
            Assert.False(_mockEntity1.Equals(_mockEntity2));
        }

        [Fact]
        public void WhenEntityComparedWithOtherType_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity2();

            //Act


            //Assert
            Assert.False(_mockEntity1.Equals(_mockEntity2));
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithOtherEntityHavingSameId_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity2();
            var Id = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id;
            _mockEntity2.Id = Id;

            //Assert
            Assert.False(_mockEntity1.Equals(_mockEntity2));
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithOtherEntityHavingNoId_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
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
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
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
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
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
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
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
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
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
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
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
            var _mockEntity1 = new MockEntity1();

            //Act

            //Assert
            Assert.True(_mockEntity1.Equals(_mockEntity1));
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithSameEntity_EqualsShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
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
            var _mockEntity1 = new MockEntity1();
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
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = default(MockEntity1);

            //Act


            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenNullComparedWithEntity_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = default(MockEntity1);
            var _mockEntity2 = new MockEntity1();

            //Act


            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenEntityComparedWithOtherEntityOfSameType_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();

            //Act


            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenEntityComparedWithOtherEntityOfDifferentTypeWithSameId_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity2();
            var Id = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id;
            _mockEntity2.Id = Id;

            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenEntityComparedWithOtherEntityOfDifferentType_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity2();

            //Act


            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithOtherEntityOfSameTypeHavingNoId_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id1 = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id1;

            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithOtherEntityOfSameTypeHavingDifferentId_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id1 = Guid.NewGuid();
            var Id2 = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingEmptyIdComparedWithOtherEntityOfSameTypeHavingEmptyId_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id1 = Guid.Empty;
            var Id2 = Guid.Empty;

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingEmptyIdComparedWithOtherEntityOfSameTypeHavingDifferentId_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id1 = Guid.Empty;
            var Id2 = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingDifferentIdComparedWithOtherEntityOfSameTypeHavingEmptyId_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id1 = Guid.NewGuid();
            var Id2 = Guid.Empty;

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.False(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithOtherEntityOfSameTypeHavingSameId_EqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id;
            _mockEntity2.Id = Id;

            //Assert
            Assert.True(_mockEntity1 == _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingNoIdComparedWithSameEntity_EqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();

            //Act

            //Assert
            Assert.True(_mockEntity1 == _mockEntity1);
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithSameEntity_EqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var Id = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id;

            //Assert
            Assert.True(_mockEntity1 == _mockEntity1);
        }

        [Fact]
        public void WhenEntityHavingEmptyIdComparedWithSameEntity_EqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var Id = Guid.Empty;

            //Act
            _mockEntity1.Id = Id;

            //Assert
            Assert.True(_mockEntity1 ==_mockEntity1);
        }

        [Fact]
        public void WhenEntityComparedWithNull_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = default(MockEntity1);

            //Act


            //Assert
            Assert.True(_mockEntity1 != _mockEntity2);
        }

        [Fact]
        public void WhenNullComparedWithEntity_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = default(MockEntity1);
            var _mockEntity2 = new MockEntity1();

            //Act


            //Assert
            Assert.True(_mockEntity1 != _mockEntity2);
        }

        [Fact]
        public void WhenEntityComparedWithOtherEntityOfSameType_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();

            //Act


            //Assert
            Assert.True(_mockEntity1 != _mockEntity2);
        }

        [Fact]
        public void WhenEntityComparedWithOtherEntityOfDifferentTypeWithSameId_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity2();
            var Id = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id;
            _mockEntity2.Id = Id;

            //Assert
            Assert.True(_mockEntity1 != _mockEntity2);
        }

        [Fact]
        public void WhenEntityComparedWithOtherEntityOfDifferentType_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity2();

            //Act


            //Assert
            Assert.True(_mockEntity1 != _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithOtherEntityOfSameTypeHavingNoId_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id1 = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id1;

            //Assert
            Assert.True(_mockEntity1 != _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithOtherEntityOfSameTypeHavingDifferentId_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id1 = Guid.NewGuid();
            var Id2 = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.True(_mockEntity1 != _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingEmptyIdComparedWithOtherEntityOfSameTypeHavingEmptyId_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id1 = Guid.Empty;
            var Id2 = Guid.Empty;

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.True(_mockEntity1 != _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingEmptyIdComparedWithOtherEntityOfSameTypeHavingDifferentId_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id1 = Guid.Empty;
            var Id2 = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.True(_mockEntity1 != _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingDifferentIdComparedWithOtherEntityOfSameTypeHavingEmptyId_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id1 = Guid.NewGuid();
            var Id2 = Guid.Empty;

            //Act
            _mockEntity1.Id = Id1;
            _mockEntity2.Id = Id2;

            //Assert
            Assert.True(_mockEntity1 != _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithOtherEntityOfSameTypeHavingSameId_NotEqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var _mockEntity2 = new MockEntity1();
            var Id = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id;
            _mockEntity2.Id = Id;

            //Assert
            Assert.False(_mockEntity1 != _mockEntity2);
        }

        [Fact]
        public void WhenEntityHavingNoIdComparedWithSameEntity_NotEqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();

            //Act

            //Assert
            Assert.False(_mockEntity1 != _mockEntity1);
        }

        [Fact]
        public void WhenEntityHavingIdComparedWithSameEntity_NotEqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var Id = Guid.NewGuid();

            //Act
            _mockEntity1.Id = Id;

            //Assert
            Assert.False(_mockEntity1 != _mockEntity1);
        }

        [Fact]
        public void WhenEntityHavingEmptyIdComparedWithSameEntity_NotEqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockEntity1 = new MockEntity1();
            var Id = Guid.Empty;

            //Act
            _mockEntity1.Id = Id;

            //Assert
            Assert.False(_mockEntity1 != _mockEntity1);
        }
    }

    class MockEntity1 : Entity
	{

	}

    class MockEntity2 : Entity
    {

    }
}

