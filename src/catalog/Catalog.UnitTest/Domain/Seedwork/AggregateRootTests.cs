using Catalog.Domain.SeedWork;
using Moq;

namespace Catalog.UnitTest.Domain.Seedwork
{
	public class AggregateRootTests
	{
		[Fact]
		public void MockAggregateRoot_ShouldImplementIAggregateRoot()
		{
			//Arrange
			var mockAggregateRoot = Mock.Of<IAggregateRoot>();

			//Assert
			Assert.IsAssignableFrom<IAggregateRoot>(mockAggregateRoot);
		}
	}
}