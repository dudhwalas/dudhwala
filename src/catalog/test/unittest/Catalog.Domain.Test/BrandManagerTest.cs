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
            var brandStatus = EnumCatalogStatus.ACTIVE;
			var brandToCreate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
			var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName))
                .ReturnsAsync(default(Brand));
            mockBrandRepo.Setup(repo => repo.CreateAsync(It.IsAny<Brand>())).ReturnsAsync(brandToCreate);
			var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
			var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
			//Act
			var createdBrand = await brandManager.UpdateAsync(brandId, brandName,brandImage,brandStatus,realmId);
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
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var brandToCreate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName))
                .ReturnsAsync(brandToCreate);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => brandManager.UpdateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Brand_NameAlreadyExist, ex.Code);

        }

        [Fact]
        public async Task Should_Create_Brand_Throw_Create_Failed_Business_Exception_Async()
        {
            //Arrange
            var brandName = "xHamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var brandToCreate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName))
                .ReturnsAsync(default(Brand));
            mockBrandRepo.Setup(repo => repo.CreateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => brandManager.UpdateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Brand_CreateFailed, ex.Code);
        }

        [Fact]
        public async Task Should_Create_Brand_With_Empty_Brand_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.UpdateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }

        [Fact]
        public async Task Should_Create_Brand_With_Null_Brand_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            string? brandName = null;
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.UpdateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }

        [Fact]
        public async Task Should_Create_Brand_With_Whitespace_Brand_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = " ";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.UpdateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }
 
        [Fact]
        public async Task Should_Create_Brand_With_Empty_Brand_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.UpdateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Create_Brand_With_Null_Brand_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            string? brandImage = null;
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.UpdateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Create_Brand_With_Whitespace_Brand_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            string? brandImage = " ";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.UpdateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Create_Brand_With_Empty_RealmId_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            var brandId = Guid.Empty;
            var realmId = Guid.Empty;
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.RealmId), () => brandManager.UpdateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.RealmId),ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Brand_Async()
        {
            //Arrange
            var brandName = "xHamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var brandNameToUpdate = "xnxx";
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            brandToUpdate.SetName(brandNameToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(brandToUpdate);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var updatedBrand = await brandManager.UpdateAsync(brandId, brandNameToUpdate, brandImage, brandStatus, realmId);
            //Assert
            Assert.Equal(brandNameToUpdate, updatedBrand.Name);
        }

        [Fact]
        public async Task Should_Update_Brand_With_Existing_Brand_Name_Throw_Aleardy_Exist_Business_Exception_Async()
        {
            //Arrange
            var brandName = "xHamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var brandNameToUpdate = "xnxx";
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.GetByNameAsync(brandNameToUpdate)).ReturnsAsync(brandToUpdate);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => brandManager.UpdateAsync(brandId, brandNameToUpdate, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Brand_NameAlreadyExist, ex.Code);
        }

        [Fact]
        public async Task Should_Update_Brand_Throw_Update_Failed_Business_Exception_Async()
        {
            //Arrange
            var brandName = "xHamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var brandNameToUpdate = "xnxx";
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            brandToUpdate.SetName(brandNameToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => brandManager.UpdateAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Brand_UpdateFailed, ex.Code);
        }

        [Fact]
        public async Task Should_Update_Brand_With_Empty_Brand_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandNameToUpdate = "";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.UpdateAsync(brandId, brandNameToUpdate, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Brand_With_Null_Brand_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            string? brandNameToUpdate = null;
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.UpdateAsync(brandId, brandNameToUpdate, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Brand_With_Whitespace_Brand_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandNameToUpdate = " ";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Name), () => brandManager.UpdateAsync(brandId, brandNameToUpdate, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Brand_With_Empty_Brand_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            var brandImageToUpdate = "";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.UpdateAsync(brandId, brandName, brandImageToUpdate, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Brand_With_Null_Brand_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            string? brandImageToUpdate = null;
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.UpdateAsync(brandId, brandName, brandImageToUpdate, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Brand_With_Whitespace_Brand_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            string? brandImageToUpdate = " ";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.Image), () => brandManager.UpdateAsync(brandId, brandName, brandImageToUpdate, brandStatus, realmId));
            //Assert
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Brand_With_Throw_Argument_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var realmIdToUpdate = Guid.Empty;
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            var brandToUpdate = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId)).ReturnsAsync(brandToUpdate);
            mockBrandRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Brand.RealmId), () => brandManager.UpdateAsync(brandId, brandName, brandImage, brandStatus, realmIdToUpdate));
            //Assert
            Assert.Equal(nameof(Brand.RealmId), ex.ParamName);
        }

        [Fact]
        public async Task Should_Patch_Brand_Name_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandNameToPatch = "xhamster1";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var existingBrand = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(x => x.GetByIdAsync(brandId)).ReturnsAsync(existingBrand);
            mockBrandRepo.Setup(x => x.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(existingBrand);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            var patchedBrand = await brandManager.PatchAsync(brandId,brandNameToPatch,null,null,null);
            //Assert
            Assert.Equal(brandNameToPatch, patchedBrand.Name);
        }

        [Fact]
        public async Task Should_Patch_Brand_Image_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImageToPatch = "xhamster1.png";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var existingBrand = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(x => x.GetByIdAsync(brandId)).ReturnsAsync(existingBrand);
            mockBrandRepo.Setup(x => x.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(existingBrand);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            var patchedBrand = await brandManager.PatchAsync(brandId, null , brandImageToPatch, null,null);
            //Assert
            Assert.Equal(brandImageToPatch, patchedBrand.Image);
        }

        [Fact]
        public async Task Should_Patch_Brand_Status_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandStatusToPatch = EnumCatalogStatus.INACTIVE;
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var existingBrand = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(x => x.GetByIdAsync(brandId)).ReturnsAsync(existingBrand);
            mockBrandRepo.Setup(x => x.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(existingBrand);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            var patchedBrand = await brandManager.PatchAsync(brandId, null, null, EnumCatalogStatus.INACTIVE, null);
            //Assert
            Assert.Equal(brandStatusToPatch, patchedBrand.Status);
        }

        [Fact]
        public async Task Should_Patch_Brand_RealmId_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandRealmIdToPatch = Guid.NewGuid();
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var existingBrand = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(x => x.GetByIdAsync(brandId)).ReturnsAsync(existingBrand);
            mockBrandRepo.Setup(x => x.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(existingBrand);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            var patchedBrand = await brandManager.PatchAsync(brandId, null, null, null, brandRealmIdToPatch);
            //Assert
            Assert.Equal(brandRealmIdToPatch, patchedBrand.RealmId);
        }

        [Fact]
        public async Task Should_Patch_With_Empty_BrandId_Throw_Business_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.Empty;
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            var ex = await Assert.ThrowsAsync<BusinessException>(() => brandManager.PatchAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Brand_UpdateFailed, ex.Code);
        }

        [Fact]
        public async Task Should_Patch_With_Non_Existing_Brand_Throw_Business_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(mockRepo => mockRepo.GetByIdAsync(brandId)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            var ex = await Assert.ThrowsAsync<BusinessException>(() => brandManager.PatchAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Brand_UpdateFailed, ex.Code);
        }

        [Fact]
        public async Task Should_Patch_With_Existing_Brand_Name_Throw_Business_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var existingBrand = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(mockRepo => mockRepo.GetByIdAsync(brandId)).ReturnsAsync(existingBrand);
            mockBrandRepo.Setup(mockRepo => mockRepo.GetByNameAsync(brandName)).ReturnsAsync(existingBrand);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            var ex = await Assert.ThrowsAsync<BusinessException>(() => brandManager.PatchAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Brand_NameAlreadyExist, ex.Code);
        }

        [Fact]
        public async Task Should_Patch_Brand_Throw_Business_Exception_Async()
        {
            //Arrange
            var brandName = "xhamster";
            var brandImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var brandId = Guid.NewGuid();
            var brandStatus = EnumCatalogStatus.ACTIVE;
            var existingBrand = new Brand(brandId, brandName, brandImage, brandStatus, realmId);
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(mockRepo => mockRepo.GetByIdAsync(brandId)).ReturnsAsync(existingBrand);
            mockBrandRepo.Setup(mockRepo => mockRepo.GetByNameAsync(brandName)).ReturnsAsync(default(Brand));
            mockBrandRepo.Setup(mockRepo => mockRepo.UpdateAsync(existingBrand)).ReturnsAsync(default(Brand));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var brandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
            var ex = await Assert.ThrowsAsync<BusinessException>(() => brandManager.PatchAsync(brandId, brandName, brandImage, brandStatus, realmId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Brand_UpdateFailed, ex.Code);
        }
    }
}