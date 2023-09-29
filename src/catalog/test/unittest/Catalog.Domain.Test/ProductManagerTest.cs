using Catalog.Domain.Shared;
using Moq;
using Volo.Abp;

namespace Catalog.Domain.Test
{
    public class ProductManagerTest
	{
        private readonly Brand brand;

        public ProductManagerTest()
		{
            brand = new Brand(Guid.NewGuid(), "xxx", "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid());
        }

        [Fact]
		public async Task Should_Create_Product_Async()
		{
			//Arrange
			var ProductName = "xHamster";
			var ProductImage = "/xHamster.png";
			var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var ProductToCreate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId);
			var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByNameAsync(ProductName))
                .Returns(Task.FromResult(default(Product)));
            mockProductRepo.Setup(repo => repo.CreateAsync(It.IsAny<Product>())).ReturnsAsync(ProductToCreate);

            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
			var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object, mockGuidGenerator);
			//Act
			var createdProduct = await ProductManager.UpdateAsync(ProductId, ProductName,ProductImage,ProductStatus,realmId,brandId);
			//Assert
			Assert.Equal(ProductName, createdProduct.Name);
        }

        [Fact]
        public async Task Should_Create_Product_Throw_Aleardy_Exist_Business_Exception_Async()
        {
            //Arrange
            var ProductName = "xHamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var ProductToCreate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByNameAsync(ProductName))
                .ReturnsAsync(ProductToCreate);

            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Product_NameAlreadyExist, ex.Code);

        }

        [Fact]
        public async Task Should_Create_Product_Throw_Create_Failed_Business_Exception_Async()
        {
            //Arrange
            var ProductName = "xHamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var ProductToCreate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByNameAsync(ProductName))
                .ReturnsAsync(default(Product));
            mockProductRepo.Setup(repo => repo.CreateAsync(It.IsAny<Product>())).ReturnsAsync(default(Product));

            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Product_CreateFailed, ex.Code);
        }

        [Fact]
        public async Task Should_Create_Product_With_Empty_Product_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.Empty;
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByNameAsync(ProductName)).ReturnsAsync(default(Product));

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Name), () => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId));
            //Assert
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }

        [Fact]
        public async Task Should_Create_Product_With_Null_Product_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            string? ProductName = null;
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.Empty;
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(default(Product));

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Name), () => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId));
            //Assert
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }

        [Fact]
        public async Task Should_Create_Product_With_Whitespace_Product_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = " ";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.Empty;
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByNameAsync(ProductName)).ReturnsAsync(default(Product));

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Name), () => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }
 
        [Fact]
        public async Task Should_Create_Product_With_Empty_Product_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductImage = "";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.Empty;
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByNameAsync(ProductName)).ReturnsAsync(default(Product));

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Image), () => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId));
            //Assert
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Create_Product_With_Null_Product_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            string? ProductImage = null;
            var realmId = Guid.NewGuid();
            var ProductId = Guid.Empty;
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByNameAsync(ProductName)).ReturnsAsync(default(Product));

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Image), () => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Create_Product_With_Whitespace_Product_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            string? ProductImage = " ";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.Empty;
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByNameAsync(ProductName)).ReturnsAsync(default(Product));

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Image), () => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId));
            //Assert
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Create_Product_With_Empty_RealmId_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductImage = "/xHamster.png";
            var ProductId = Guid.Empty;
            var realmId = Guid.Empty;
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByNameAsync(ProductName)).ReturnsAsync(default(Product));

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.RealmId), () => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId));
            //Assert
            Assert.Equal(nameof(Product.RealmId),ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Product_Async()
        {
            //Arrange
            var ProductName = "xHamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var ProductNameToUpdate = "xnxx";
            var brandId = Guid.NewGuid();

            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            ProductToUpdate.SetName(ProductNameToUpdate);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(ProductToUpdate);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var updatedProduct = await ProductManager.UpdateAsync(ProductId, ProductNameToUpdate, ProductImage, ProductStatus, realmId,brandId);
            //Assert
            Assert.Equal(ProductNameToUpdate, updatedProduct.Name);
        }

        [Fact]
        public async Task Should_Update_Product_With_Existing_Product_Name_Throw_Aleardy_Exist_Business_Exception_Async()
        {
            //Arrange
            var ProductName = "xHamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var ProductNameToUpdate = "xnxx";

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            mockProductRepo.Setup(repo => repo.GetByNameAsync(ProductNameToUpdate)).ReturnsAsync(ProductToUpdate);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => ProductManager.UpdateAsync(ProductId, ProductNameToUpdate, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Product_NameAlreadyExist, ex.Code);
        }

        [Fact]
        public async Task Should_Update_Product_Throw_Update_Failed_Business_Exception_Async()
        {
            //Arrange
            var ProductName = "xHamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var ProductNameToUpdate = "xnxx";

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            ProductToUpdate.SetName(ProductNameToUpdate);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(default(Product));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Product_UpdateFailed, ex.Code);
        }

        [Fact]
        public async Task Should_Update_Product_With_Empty_Product_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductNameToUpdate = "";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var mockProductRepo = new Mock<IProductRepository>();

            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);

            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(default(Product));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Name), () => ProductManager.UpdateAsync(ProductId, ProductNameToUpdate, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Product_With_Null_Product_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            string? ProductNameToUpdate = null;
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var mockProductRepo = new Mock<IProductRepository>();
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId);
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(default(Product));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Name), () => ProductManager.UpdateAsync(ProductId, ProductNameToUpdate, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Product_With_Whitespace_Product_Name_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductNameToUpdate = " ";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var mockProductRepo = new Mock<IProductRepository>();
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(default(Product));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object,mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Name), () => ProductManager.UpdateAsync(ProductId, ProductNameToUpdate, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Product_With_Empty_Product_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductImage = "/xHamster.png";
            var ProductImageToUpdate = "";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var mockProductRepo = new Mock<IProductRepository>();
            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(default(Product));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Image), () => ProductManager.UpdateAsync(ProductId, ProductName, ProductImageToUpdate, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Product_With_Null_Product_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductImage = "/xHamster.png";
            string? ProductImageToUpdate = null;
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var mockProductRepo = new Mock<IProductRepository>();
            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(default(Product));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Image), () => ProductManager.UpdateAsync(ProductId, ProductName, ProductImageToUpdate, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Product_With_Whitespace_Product_Image_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductImage = "/xHamster.png";
            string? ProductImageToUpdate = " ";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var mockProductRepo = new Mock<IProductRepository>();
            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(default(Product));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.Image), () => ProductManager.UpdateAsync(ProductId, ProductName, ProductImageToUpdate, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public async Task Should_Update_Product_With_Throw_Argument_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var realmIdToUpdate = Guid.Empty;
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .Returns(Task.FromResult(brand));
            var mockProductRepo = new Mock<IProductRepository>();
            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(default(Product));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(nameof(Product.RealmId), () => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmIdToUpdate,brandId));
            //Assert
            Assert.Equal(nameof(Product.RealmId), ex.ParamName);
        }

        [Fact]
        public async Task Should_Patch_Product_Name_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductNameToPatch = "xhamster1";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var existingProduct = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(x => x.GetByIdAsync(ProductId)).ReturnsAsync(existingProduct);
            mockProductRepo.Setup(x => x.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(existingProduct);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            var patchedProduct = await ProductManager.PatchAsync(ProductId,ProductNameToPatch,null,null,null,brandId);
            //Assert
            Assert.Equal(ProductNameToPatch, patchedProduct.Name);
        }

        [Fact]
        public async Task Should_Patch_Product_Image_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductImageToPatch = "xhamster1.png";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var existingProduct = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(x => x.GetByIdAsync(ProductId)).ReturnsAsync(existingProduct);
            mockProductRepo.Setup(x => x.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(existingProduct);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            var patchedProduct = await ProductManager.PatchAsync(ProductId, null , ProductImageToPatch, null,null,brandId);
            //Assert
            Assert.Equal(ProductImageToPatch, patchedProduct.Image);
        }

        [Fact]
        public async Task Should_Patch_Product_Status_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductStatusToPatch = EnumCatalogStatus.INACTIVE;
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var existingProduct = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(x => x.GetByIdAsync(ProductId)).ReturnsAsync(existingProduct);
            mockProductRepo.Setup(x => x.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(existingProduct);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            var patchedProduct = await ProductManager.PatchAsync(ProductId, null, null, EnumCatalogStatus.INACTIVE, null,brandId);
            //Assert
            Assert.Equal(ProductStatusToPatch, patchedProduct.Status);
        }

        [Fact]
        public async Task Should_Patch_Product_RealmId_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductRealmIdToPatch = Guid.NewGuid();
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var existingProduct = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(x => x.GetByIdAsync(ProductId)).ReturnsAsync(existingProduct);
            mockProductRepo.Setup(x => x.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(existingProduct);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            var patchedProduct = await ProductManager.PatchAsync(ProductId, null, null, null, ProductRealmIdToPatch,brandId);
            //Assert
            Assert.Equal(ProductRealmIdToPatch, patchedProduct.RealmId);
        }

        [Fact]
        public async Task Should_Patch_With_Empty_ProductId_Throw_Business_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.Empty;
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var mockProductRepo = new Mock<IProductRepository>();
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            var ex = await Assert.ThrowsAsync<BusinessException>(() => ProductManager.PatchAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Product_UpdateFailed, ex.Code);
        }

        [Fact]
        public async Task Should_Patch_With_Non_Existing_Product_Throw_Business_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(mockRepo => mockRepo.GetByIdAsync(ProductId)).ReturnsAsync(default(Product));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            var ex = await Assert.ThrowsAsync<BusinessException>(() => ProductManager.PatchAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Product_UpdateFailed, ex.Code);
        }

        [Fact]
        public async Task Should_Patch_With_Existing_Product_Name_Throw_Business_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var existingProduct = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(mockRepo => mockRepo.GetByIdAsync(ProductId)).ReturnsAsync(existingProduct);
            mockProductRepo.Setup(mockRepo => mockRepo.GetByNameAsync(ProductName)).ReturnsAsync(existingProduct);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            var ex = await Assert.ThrowsAsync<BusinessException>(() => ProductManager.PatchAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Product_NameAlreadyExist, ex.Code);
        }

        [Fact]
        public async Task Should_Patch_Product_Throw_Business_Exception_Async()
        {
            //Arrange
            var ProductName = "xhamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var brandId = Guid.NewGuid();
            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(brand);
            var existingProduct = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(mockRepo => mockRepo.GetByIdAsync(ProductId)).ReturnsAsync(existingProduct);
            mockProductRepo.Setup(mockRepo => mockRepo.GetByNameAsync(ProductName)).ReturnsAsync(default(Product));
            mockProductRepo.Setup(mockRepo => mockRepo.UpdateAsync(existingProduct)).ReturnsAsync(default(Product));
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            //Act
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object,mockGuidGenerator);
            var ex = await Assert.ThrowsAsync<BusinessException>(() => ProductManager.PatchAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId,brandId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Product_UpdateFailed, ex.Code);
        }

        [Fact]
        public async Task Should_Update_Product_Throw_Brand_Not_Exist_Business_Exception_Async()
        {
            //Arrange
            var ProductName = "xHamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var ProductNameToUpdate = "xnxx";
            var brandId = Guid.NewGuid();

            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(default(Brand));

            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            ProductToUpdate.SetName(ProductNameToUpdate);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(ProductToUpdate);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => ProductManager.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Product_BrandNotAvailable, ex.Code);
        }

        [Fact]
        public async Task Should_Patch_Product_Throw_Brand_Not_Exist_Business_Exception_Async()
        {
            //Arrange
            var ProductName = "xHamster";
            var ProductImage = "/xHamster.png";
            var realmId = Guid.NewGuid();
            var ProductId = Guid.NewGuid();
            var ProductStatus = EnumCatalogStatus.ACTIVE;
            var ProductNameToUpdate = "xnxx";
            var brandId = Guid.NewGuid();

            var mockBrandRepo = new Mock<IBrandRepository>();
            mockBrandRepo.Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(default(Brand));

            var ProductToUpdate = new Product(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId);
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetByIdAsync(ProductId)).ReturnsAsync(ProductToUpdate);
            ProductToUpdate.SetName(ProductNameToUpdate);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(ProductToUpdate);
            var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
            var ProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object, mockGuidGenerator);
            //Act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => ProductManager.PatchAsync(ProductId, ProductName, ProductImage, ProductStatus, realmId, brandId));
            //Assert
            Assert.Equal(CatalogErrorCodes.Product_BrandNotAvailable, ex.Code);
        }
    }
}