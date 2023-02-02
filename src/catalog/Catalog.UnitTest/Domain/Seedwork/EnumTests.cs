using System;
using Catalog.Domain.SeedWork;

namespace Catalog.UnitTest.Domain.Seedwork
{
	public class EnumTests
	{
		public EnumTests()
		{
		}

        [Theory,MemberData(nameof(MockEnumsWithExpectedIds))]
        public void WithMockEnums_GetIdShouldReturnAssignedId(int exptected,MockEnum1 mockEnum)
        {
            Assert.Equal(exptected, mockEnum.Id);
        }

        [Theory, MemberData(nameof(MockEnumsWithExpectedNames))]
        public void WithMockEnums_GetNameShouldReturnAssignedName(string exptected, MockEnum1 mockEnum)
        {
            Assert.Equal(exptected, mockEnum.Name);
        }

        [Theory, MemberData(nameof(MockEnumsWithExpectedNames))]
        public void WithMockEnums_ToStringShouldReturnAssignedName(string exptected, MockEnum1 mockEnum)
        {
            Assert.Equal(exptected, mockEnum.ToString());
        }

        [Fact]
        public void WithMockEnum_GetAllShouldReturnThreeEnums()
        {
            Assert.Equal(3, Enumeration.GetAll<MockEnum1>().Count());
        }

        [Fact]
        public void WhenMockEnum1ComparedWithMockEnum_EqualsShouldReturnFalse()
        {
            Assert.False(MockEnum1.Enum1.Equals(MockEnum2.Enum1));
        }

        [Fact]
        public void WhenMockEnum1ComparedWithMockEnum1HavingDifferentEnumType_EqualsShouldReturnFalse()
        {
            Assert.False(MockEnum1.Enum1.Equals(MockEnum1.Enum2));
        }

        [Fact]
        public void WhenMockEnum1ComparedWithNull_EqualsShouldReturnFalse()
        {
            Assert.False(MockEnum1.Enum1.Equals(null));
        }

        [Fact]
        public void WhenMockEnum1ComparedWithMockEnum1HavingSameEnumType_EqualsShouldReturnTrue()
        {
            Assert.True(MockEnum1.Enum1.Equals(MockEnum1.Enum1));
        }

        [Theory,MemberData(nameof(MockEnumsWithExpectedIds))]
        public void WithMockEnum_GetHashcodeShouldNotBeZero(int id,Enumeration mockEnum)
        {
            Assert.NotEqual(0,mockEnum.GetHashCode());
        }

        [Fact]
        public void WithMockEnum2AndMockEnum1_AbsoluteDifferenceShouldBeOne()
        {
            Assert.Equal(1, Enumeration.AbsoluteDifference(MockEnum1.Enum2,MockEnum1.Enum1));
        }

        [Fact]
        public void WithMockEnum1AndMockEnum2_AbsoluteDifferenceShouldBeOne()
        {
            Assert.Equal(1, Enumeration.AbsoluteDifference(MockEnum1.Enum1, MockEnum1.Enum2));
        }

        [Fact]
        public void WithMockEnumValue1_FromValueShouldReturnMockEnum1Type()
        {
            Assert.IsType<MockEnum1>(Enumeration.FromValue<MockEnum1>(1));
        }

        [Fact]
        public void WithMockEnumValue1_FromValueShouldReturnMockEnum1()
        {
            Assert.Equal(MockEnum1.Enum1,Enumeration.FromValue<MockEnum1>(1));
        }

        [Fact]
        public void WithMockEnumValue1_FromNameShouldReturnMockEnum1Type()
        {
            Assert.IsType<MockEnum1>(Enumeration.FromDisplayName<MockEnum1>(nameof(MockEnum1.Enum1)));
        }

        [Fact]
        public void WithMockEnumValue1_FromNameShouldReturnMockEnum1()
        {
            Assert.Equal(MockEnum1.Enum1, Enumeration.FromDisplayName<MockEnum1>(nameof(MockEnum1.Enum1)));
        }

        [Fact]
        public void WithMockEnumValue5_FromValueShouldThrowInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(()=>Enumeration.FromValue<MockEnum1>(5));
        }

        [Fact]
        public void WithMockEnumValue1_FromNameShouldThrowInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => Enumeration.FromDisplayName<MockEnum1>("NoEnumWithThisName"));
        }

        [Fact]
        public void WhenMockEnumValue1ComparedToMockEnumValue2_ShouldReturnMinusOne()
        {
            Assert.Equal(-1, MockEnum1.Enum1.CompareTo(MockEnum1.Enum2));
        }

        [Fact]
        public void WhenMockEnumValue1ComparedToMockEnumValue1_ShouldReturnZero()
        {
            Assert.Equal(0, MockEnum1.Enum1.CompareTo(MockEnum1.Enum1));
        }

        [Fact]
        public void WhenMockEnumValue2ComparedToMockEnumValue1_ShouldReturnOne()
        {
            Assert.Equal(1, MockEnum1.Enum2.CompareTo(MockEnum1.Enum1));
        }

        [Fact]
        public void WhenMockEnumValue1ComparedToNull_ShouldReturnOne()
        {
            Assert.Equal(1, MockEnum1.Enum1.CompareTo(null));
        }

        [Fact]
        public void WhenMockEnumValue1ComparedToMockEnumValue1OfOtherType_ShouldReturnZero()
        {
            Assert.Equal(0, MockEnum1.Enum1.CompareTo(MockEnum2.Enum1));
        }

        [Fact]
        public void WhenMockEnumValue1ComparedToMockEnumValue2OfOtherType_ShouldReturnMinusOne()
        {
            Assert.Equal(-1, MockEnum1.Enum1.CompareTo(MockEnum2.Enum2));
        }

        [Fact]
        public void WhenMockEnumValue2ComparedToMockEnumValue1OfOtherType_ShouldReturnOne()
        {
            Assert.Equal(1, MockEnum1.Enum2.CompareTo(MockEnum2.Enum1));
        }

        public static IEnumerable<object[]> MockEnumsWithExpectedIds
        {
            get
            {
                return new[]
                {
                new object[] { 1, MockEnum1.Enum1},
                new object[] { 2, MockEnum1.Enum2},
                new object[] { 3, MockEnum1.Enum3}
                };
            }
        }

        public static IEnumerable<object[]> MockEnumsWithExpectedNames
        {
            get
            {
                return new[]
                {
                new object[] { "Enum1", MockEnum1.Enum1},
                new object[] { "Enum2", MockEnum1.Enum2},
                new object[] { "Enum3", MockEnum1.Enum3}
                };
            }
        }
    }

    public class MockEnum1 : Enumeration
    {
        public static MockEnum1 Enum1 = new(1, nameof(Enum1));
        public static MockEnum1 Enum2 = new(2, nameof(Enum2));
        public static MockEnum1 Enum3 = new(3, nameof(Enum3));

        public MockEnum1(int id, string name) : base(id, name)
        {
        }
    }

    public class MockEnum2 : Enumeration
    {
        public static MockEnum2 Enum1 = new(1, nameof(Enum1));
        public static MockEnum2 Enum2 = new(2, nameof(Enum2));
        public static MockEnum2 Enum3 = new(3, nameof(Enum3));

        public MockEnum2(int id, string name) : base(id, name)
        {
        }
    }
}