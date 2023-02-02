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

        [Fact]
        public void WhenValueObjectComparedWithNull_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = default(MockValueObject2);

            //Act


            //Assert
            Assert.False(_mockValueObject1 == _mockValueObject2);
        }

        [Fact]
        public void WhenNullComparedWithValueObject_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = default(MockValueObject1);
            var _mockValueObject2 = new MockValueObject1();

            //Act


            //Assert
            Assert.False(_mockValueObject1 == _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectComparedWithOtherValueObjectOfDifferentTypeWithSameMockProperty_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject2();

            var _mockProperty = 1;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty;
            _mockValueObject2.MockProperty1 = _mockProperty;

            //Assert
            Assert.False(_mockValueObject1 == _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectComparedWithOtherValueObjectOfDifferentType_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject2();

            //Act


            //Assert
            Assert.False(_mockValueObject1 == _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectHavingMockPropertyComparedWithOtherEntityOfSameTypeHavingNoMockProperty_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject1();
            var _mockProperty = 1;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty;

            //Assert
            Assert.False(_mockValueObject1 == _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectHavingMockPropertyComparedWithOtherValueObjectOfSameTypeHavingDifferentMockProperty_EqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject2();
            var _mockProperty1 = 1;
            var _mockProperty2 = 1;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;
            _mockValueObject2.MockProperty1 = _mockProperty2;

            //Assert
            Assert.False(_mockValueObject1 == _mockValueObject2);
        }

        [Fact]
        public void WhenEntityHavingMockPropertyComparedWithOtherValueObjectOfSameTypeHavingDifferentMockProperty_EqualOperatorShouldReturnFalse ()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject1();
            var _mockProperty1 = 1;
            var _mockProperty2 = 2;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;
            _mockValueObject2.MockProperty1 = _mockProperty2;

            //Assert
            Assert.False(_mockValueObject1 == _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectHavingMockPropertyComparedWithOtherValueObjectOfSameTypeHavingSameMockProperty_EqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject1();
            var _mockProperty1 = 1;
            var _mockProperty2 = 1;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;
            _mockValueObject2.MockProperty1 = _mockProperty2;

            //Assert
            Assert.True(_mockValueObject1 == _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectHavingNoMockPropertyComparedWithSameValueObject_EqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();

            //Act

            //Assert
            Assert.True(_mockValueObject1 == _mockValueObject1);
        }

        [Fact]
        public void WhenValueObjectHavingMockPropertyComparedWithSameValueObject_EqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockProperty = 1;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty;

            //Assert
            Assert.True(_mockValueObject1 == _mockValueObject1);
        }

        [Fact]
        public void WhenValueObjectComparedWithNull_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = default(MockValueObject1);

            //Act


            //Assert
            Assert.True(_mockValueObject1 != _mockValueObject2);
        }

        [Fact]
        public void WhenNullComparedWithEntity_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = default(MockValueObject1);
            var _mockValueObject2 = new MockValueObject1();

            //Act


            //Assert
            Assert.True(_mockValueObject1 != _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectComparedWithOtherValueObjectOfSameType_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject2();

            //Act


            //Assert
            Assert.True(_mockValueObject1 != _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectHavingMockPropertyComparedWithOtherValueObjectOfSameTypeHavingDifferentMockProperty_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject2();

            var _mockProperty1 = 1;
            var _mockProperty2 = 2;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;
            _mockValueObject2.MockProperty1 = _mockProperty2;

            //Assert
            Assert.True(_mockValueObject1 != _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectHavingNullMockPropertyComparedWithOtherValueObjectOfSameTypeHavingDifferentMockProperty_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject1();
            int? _mockProperty1 = null;
            var _mockProperty2 = 1;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;
            _mockValueObject2.MockProperty1 = _mockProperty2;

            //Assert
            Assert.True(_mockValueObject1 != _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectHavingDifferentMockPropertyComparedWithOtherValueObjectOfSameTypeHavingNullMockProperty_NotEqualOperatorShouldReturnTrue()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject1();
            var _mockProperty1 = 1;
            int? _mockProperty2 = null;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;
            _mockValueObject2.MockProperty1 = _mockProperty2;

            //Assert
            Assert.True(_mockValueObject1 != _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectHavingMockPropertyComparedWithOtherValueObjectOfSameTypeHavingSameMockProperty_NotEqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockValueObject2 = new MockValueObject1();
            var _mockProperty1 = 1;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;
            _mockValueObject2.MockProperty1 = _mockProperty1;

            //Assert
            Assert.False(_mockValueObject1 != _mockValueObject2);
        }

        [Fact]
        public void WhenValueObjectHavingNoMockPropertyComparedWithSameValueObject_NotEqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();

            //Act

            //Assert
            Assert.False(_mockValueObject1 != _mockValueObject1);
        }

        [Fact]
        public void WhenValueObjectHavingMockPropertyComparedWithSameValueObject_NotEqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            var _mockProperty1 = 1;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;

            //Assert
            Assert.False(_mockValueObject1 != _mockValueObject1);
        }

        [Fact]
        public void WhenValueObjectHavingNullMockPropertyComparedWithSameValueObject_NotEqualOperatorShouldReturnFalse()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            int? _mockProperty1 = null;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;

            //Assert
            Assert.False(_mockValueObject1 != _mockValueObject1);
        }

        [Fact]
        public void GetCopyMethodOfValueObject_ShouldReturnClonedValueObject()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            
            //Act
            var _clonedObject = _mockValueObject1.GetCopy() as MockValueObject1;

            //Assert
            Assert.NotNull(_clonedObject);
        }

        [Fact]
        public void GetCopyMethodOfValueObjectWithNullMockProperty_ShouldReturnClonedValueObjectWithNullMockProperty()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            int? _mockProperty1 = null;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;
            var _clonedObject = _mockValueObject1.GetCopy() as MockValueObject1;

            //Assert
            Assert.Null(_clonedObject?.MockProperty1);
        }

        [Fact]
        public void GetCopyMethodOfValueObjectWithMockProperty_ShouldReturnClonedValueObjectWithMockProperty()
        {
            //Arrange
            var _mockValueObject1 = new MockValueObject1();
            int? _mockProperty1 = 1;

            //Act
            _mockValueObject1.MockProperty1 = _mockProperty1;
            var _clonedObject = _mockValueObject1.GetCopy() as MockValueObject1;

            //Assert
            Assert.Equal(1,_mockValueObject1.MockProperty1);
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

