using System;
using Catalog.Domain.SeedWork;

namespace Catalog.UnitTest.Domain.Seedwork
{
	public class ValueObjectTests
	{

        [Fact]
        public void WithNoPropertySet_GetHashCodeShouldBeZero()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();

            //Act

            //Assert
            Assert.Equal(0, _mockValueObject1.GetHashCode());
        }

        [Fact]
        public void WithMockProperty1Set_GetHashCodeShouldNotBeZero()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();

            //Act
            _mockValueObject1.MockProperty1 = 1;

            //Assert
            Assert.NotEqual(0, _mockValueObject1.GetHashCode());
        }

        [Fact]
        public void WhenValueObjectComparedWithNull_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();

            //Act


            //Assert
            Assert.False(_mockValueObject1.Equals(null));
        }

        [Fact]
        public void WhenValueObjectComparedWithOtherValueObject_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject2();

            //Act


            //Assert
            Assert.False(_mockValueObject1.Equals(_mockValueObject2));
        }

        [Fact]
        public void WhenValueObjectComparedWithOtherValueObjectType_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject2();

            //Act


            //Assert
            Assert.False(_mockValueObject1.Equals(_mockValueObject2));
        }

        [Fact]
        public void WhenValueObjectHavingMockPropertyComparedWithOtherValueObjectHavingSameMockProperty_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject2();
            var _mockProperty1 = 1;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;
            _mockValueObject2.MockProperty1 = _mockProperty1;

            //Assert
            Assert.False(_mockValueObject1.Equals(_mockValueObject2));
        }

        [Fact]
        public void WhenValueObjectHavingMockPropertyComparedWithOtherValueObjectHavingNoMockProperty_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject2();
            var _mockProperty1 = 1;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;

            //Assert
            Assert.False(_mockValueObject1.Equals(_mockValueObject2));
        }

        [Fact]
        public void WhenValueObjectHavingMockPropertyComparedWithOtherValueObjectHavingDifferentMockProperty_EqualsShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject1();
            var Id1 = 1;
            var Id2 = 2;

            //Act
            _mockValueObject1.MockProperty1 = Id1;
            _mockValueObject2.MockProperty1 = Id2;

            //Assert
            Assert.False(_mockValueObject1.Equals(_mockValueObject2));
        }

        [Fact]
        public void WhenValueObjectHavingNullMockPropertyComparedWithOtherValueObjectOfSameTypeHavingNullMockProperty_EqualsShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject1();
            
            //Act

            //Assert
            Assert.True(_mockValueObject1.Equals(_mockValueObject2));
        }

        [Fact]
        public void WhenValueObjectHavingMockPropertyComparedWithOtherValueObjectOfSameTypeHavingSameMockProperty_EqualsShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject1();
            var Id = 1;

            //Act
            _mockValueObject1.MockProperty1 = Id;
            _mockValueObject2.MockProperty1 = Id;

            //Assert
            Assert.True(_mockValueObject1.Equals(_mockValueObject2));
        }

        [Fact]
        public void WhenValueObjectHavingNoMockPropertySetComparedWithSameValueObject_EqualsShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();

            //Act

            //Assert
            Assert.True(_mockValueObject1.Equals(_mockValueObject1));
        }

        [Fact]
        public void WhenValueObjectyHavingMockPropertySetComparedWithSameValueObject_EqualsShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var Id = 1;

            //Act
            _mockValueObject1.MockProperty1 = Id;

            //Assert
            Assert.True(_mockValueObject1.Equals(_mockValueObject1));
        }
    }

    class MockValueObject1 : ValueObject
    {
        public int? MockProperty1 { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MockProperty1;
        }
    }

    class MockValueObject2 : ValueObject
    {
        public int? MockProperty1 { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MockProperty1;
        }
    }
}

