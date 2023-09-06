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
                .ReturnsAsync(default(Brand));
            mockBrandRepo.Setup(repo => repo.CreateAsync(It.IsAny<Brand>())).ReturnsAsync(brandToCreate);
			var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
			var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

			//Act
			var createdBrand = await brandManager.CreateAsync(brandId, brandName,brandImage,brandStatus,realmId);

			//Assert
			Assert.Equal(brandName, createdBrand.Name);
        }

        [Fact]
        public async Task Should_Update_Brand_Async()
        {
            //Arrange
            var brandName = "xHamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumStatus.ACTIVE;
            var brandNameToUpdate = "xnxx";
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            brandToUpdate.SetName(brandNameToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(brandToUpdate);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            //Act
            var updatedBrand = await brandManager.CreateAsync(brandId, brandNameToUpdate, brandImage, brandStatus, realmId);

            //Assert
            Assert.Equal(brandNameToUpdate, updatedBrand.Name);
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
            await Assert.ThrowsAsync<BusinessException>(() => brandManager.CreateAsync(brandId,brandName, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Create_Throw_Argument_Exception_For_Empty_Brand_Name()
        {
            var brandName = "";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Create_Throw_Argument_Exception_For_Null_Brand_Name()
        {
            string? brandName = null;
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Create_Throw_Argument_Exception_For_Whitespace_Brand_Name()
        {
            var brandName = " ";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }
 
        [Fact]
        public void Should_Create_Throw_Argument_Exception_For_Empty_Brand_Image()
        {
            var brandName = "xhamster";
            var brandImage = "";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Create_Throw_Argument_Exception_For_Null_Brand_Image()
        {
            var brandName = "xhamster";
            string? brandImage = null;
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Create_Throw_Argument_Exception_For_Whitespace_Brand_Image()
        {
            var brandName = "xhamster";
            string? brandImage = " ";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Create_Throw_Argument_Exception_For_Empty_RealmId()
        {
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.RealmId), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }
    }
}