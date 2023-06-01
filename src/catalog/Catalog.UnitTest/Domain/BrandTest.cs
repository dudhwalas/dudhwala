using System;
using Catalog.Domain;
using Volo.Abp;

namespace Catalog.UnitTest.Domain
{
	public class BrandTest
	{
		[Fact]
		public void Should_Throw_Exception_For_Empty_Brandname()
		{
			Assert.Throws<BusinessException>(()=>new Brand(Guid.NewGuid(),""));
		}
	}
}