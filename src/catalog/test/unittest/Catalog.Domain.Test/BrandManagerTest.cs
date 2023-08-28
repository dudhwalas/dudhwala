using Catalog.Domain.Shared;
using Moq;
using Volo.Abp;

namespace Catalog.Domain.Test
{
    public class BrandManagerTest
	{
		public BrandManagerTest()
		{
		}

		[Fact]
		public async Task Should_Create_Brand_Async()
		{
			//Arrange
			var brandName = "xHamster";
			var brandImage = "/xHamster.png";
			var realmId = Guid.NewGuid();
			var brandId = Guid.NewGuid();
            var brandStatus = EnumStatus.ACTIVE;
			var brandToCreate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
			var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName))
                .Returns(Task.FromResult(default(Brand)));
            mockBrandRepo.Setup(repo => repo.CreateAsync(brandToCreate)).Returns(Task.FromResult(brandToCreate));
			var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
			var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

			//Act
			var createdBrand = await brandManager.CreateAsync(brandToCreate);

			//Assert
			Assert.Equal(brandName, createdBrand.Name);
        }

        [Fact]
        public async Task Should_Create_Brand_Throw_Business_Exception_Async()
        {
            //Arrange
            var brandName = "xHamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumStatus.ACTIVE;
            var brandToCreate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName))
                .Returns(Task.FromResult(brandToCreate));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            //Assert
            await Assert.ThrowsAsync<BusinessException>(() => brandManager.CreateAsync(brandToCreate));
        }

    }
}

