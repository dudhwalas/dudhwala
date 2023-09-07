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
        public async Task Should_Create_Brand_Throw_Aleardy_Exist_Business_Exception_Async()
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
                .ReturnsAsync(brandToCreate);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            //Assert
            var ex = await Assert.ThrowsAsync<BusinessException>(() => brandManager.CreateAsync(brandId,brandName, brandImage, brandStatus, realmId));
            Assert.Equal(CatalogErrorCodes.BrandAlreadyExist, ex.Code);

        }

        [Fact]
        public async Task Should_Create_Brand_Throw_Create_Failed_Business_Exception_Async()
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
            mockBrandRepo.Setup(repo => repo.CreateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            //Assert
            var ex = await Assert.ThrowsAsync<BusinessException>(() => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            Assert.Equal(CatalogErrorCodes.CreateBrandFailed, ex.Code);
        }

        [Fact]
        public void Should_Create_Brand_Throw_Empty_Brand_Name_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Create_Brand_Throw_Null_Brand_Name_Argument_Exception_Async()
        {
            //Arrange
            string? brandName = null;
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Create_Brand_Throw_Whitespace_Brand_Name_Argument_Exception_Async()
        {
            //Arrange
            var brandName = " ";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }
 
        [Fact]
        public void Should_Create_Brand_Throw_Empty_Brand_Image_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Create_Brand_Throw_Null_Brand_Image_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            string? brandImage = null;
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Create_Brand_Throw_Whitespace_Brand_Image_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            string? brandImage = " ";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Create_Brand_Throw_Empty_RealmId_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.RealmId), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
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
        public async Task Should_Update_Brand_Throw_Update_Failed_Business_Exception_Async()
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
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);

            //Assert
            var ex = await Assert.ThrowsAsync<BusinessException>(() => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            Assert.Equal(CatalogErrorCodes.UpdateBrandFailed, ex.Code);
        }

        [Fact]
        public void Should_Update_Brand_Throw_Empty_Brand_Name_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandNameToUpdate = "";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.CreateAsync(brandId, brandNameToUpdate, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Update_Brand_Throw_Null_Brand_Name_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            string? brandNameToUpdate = null;
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.CreateAsync(brandId, brandNameToUpdate, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Update_Brand_Throw_Whitespace_Brand_Name_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandNameToUpdate = " ";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.CreateAsync(brandId, brandNameToUpdate, brandImage, brandStatus, realmId));
        }

        [Fact]
        public void Should_Update_Brand_Throw_Empty_Brand_Image_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            var brandImageToUpdate = "";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.CreateAsync(brandId, brandName, brandImageToUpdate, brandStatus, realmId));
        }

        [Fact]
        public void Should_Update_Brand_Throw_Null_Brand_Image_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            string? brandImageToUpdate = null;
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.CreateAsync(brandId, brandName, brandImageToUpdate, brandStatus, realmId));
        }

        [Fact]
        public void Should_Update_Brand_Throw_Whitespace_Brand_Image_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            string? brandImageToUpdate = " ";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.CreateAsync(brandId, brandName, brandImageToUpdate, brandStatus, realmId));
        }

        [Fact]
        public void Should_Update_Brand_Throw_Empty_RealmId_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var realmIdToUpdate = Guid.Empty;
            var brandId = Guid.NewGuid();
            var brandStatus = EnumStatus.ACTIVE;
            var mockBrandRepo = new Mock<IRepository<Brand, Guid>>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Assert
            Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.CreateAsync(brandId, brandName, brandImage, brandStatus, realmIdToUpdate));
        }
    }
}