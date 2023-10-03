using Catalog.Domain.Shared;

namespace Catalog.Domain.Test
{
    public class ProductTest
	{
		[Fact]
		public void Should_Create_Product_With_Empty_Product_Name_Throw_Argument_Exception_Async()
		{
            var ProductName = "";
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Name),() => new Product(Guid.NewGuid(), ProductName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid()));
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Product_With_Null_Product_Name_Throw_Argument_Exception_Async()
        {
            string? ProductName = null;
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Name), () => new Product(Guid.NewGuid(), ProductName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid()));
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Product_With_Whitespace_Product_Name_Throw_Argument_Exception_Async()
        {
            var ProductName = " ";
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Name), () => new Product(Guid.NewGuid(), ProductName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid()));
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Product_With_Product_Name()
        {
            var ProductName = "Product 1";
            var Product = new Product(Guid.NewGuid(),ProductName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid());
            Assert.Equal(ProductName, Product.Name);
        }

        [Fact]
        public void Should_Set_Empty_Product_Name_Throw_Argument_Exception_Async()
        {
            var ProductName = "Product 1";
            var Product = new Product(Guid.NewGuid(), ProductName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Name), () => Product.SetName(""));
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Null_Product_Name_Throw_Argument_Exception_Async()
        {
            var ProductName = "Product 1";
            var Product = new Product(Guid.NewGuid(), ProductName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Name), () => Product.SetName(null));
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Whitespace_Product_Name_Throw_Argument_Exception_Async()
        {
            var ProductName = "Product 1";
            var Product = new Product(Guid.NewGuid(), ProductName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Name), () => Product.SetName(" "));
            Assert.Equal(nameof(Product.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Product_Name()
        {
            var ProductName = "Product 1";
            var ProductNameToSet = "Product 2";
            var Product = new Product(Guid.NewGuid(), ProductName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid());
            Product.SetName(ProductNameToSet);
            Assert.Equal(ProductNameToSet, Product.Name);
        }

        [Fact]
        public void Should_Create_Product_With_Empty_Product_Image_Throw_Argument_Exception_Async()
        {
            var imagePath = "";
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Image), () => new Product(Guid.NewGuid(), "Product 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid()));
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Product_With_Null_Product_Image_Throw_Argument_Exception_Async()
        {
            string? imagePath = null;
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Image), () => new Product(Guid.NewGuid(), "Product 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid()));
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Product_With_Whitespace_Product_Image_Throw_Argument_Exception_Async()
        {
            string? imagePath = " ";
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Image), () => new Product(Guid.NewGuid(), "Product 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid()));
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Product_With_Product_Image()
        {
            var imagePath = "filepath";
            var Product = new Product(Guid.NewGuid(), "Product 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid());
            Assert.Equal(imagePath, Product.Image);
        }

        [Fact]
        public void Should_Set_Empty_Product_Image_Throw_Argument_Exception_Async()
        {
            var imagePath = "filepath";
            var Product = new Product(Guid.NewGuid(), "Product 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Image), () => Product.SetImage(""));
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Null_Product_Image_Throw_Argument_Exception_Async()
        {
            var imagePath = "filepath";
            var Product = new Product(Guid.NewGuid(), "Product 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Image), () => Product.SetImage(null));
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Whitespace_Product_Image_Throw_Argument_Exception_Async()
        {
            var imagePath = "filepath";
            var Product = new Product(Guid.NewGuid(), "Product 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Product.Image), () => Product.SetImage(" "));
            Assert.Equal(nameof(Product.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Product_Image()
        {
            var imagePath = "filepath 1";
            var imagePathToSet = "filepath 2";
            var Product = new Product(Guid.NewGuid(), "Product 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid(), Guid.NewGuid());
            Product.SetImage(imagePathToSet);
            Assert.Equal(imagePathToSet, Product.Image);
        }

        [Fact]
        public void Should_Create_Product_With_Product_Status()
        {
            EnumCatalogStatus status = EnumCatalogStatus.ACTIVE;
            var Product = new Product(Guid.NewGuid(), "Product 1", "filepath", status, Guid.NewGuid(), Guid.NewGuid());
            Assert.Equal(status, Product.Status);
        }

        [Fact]
        public void Should_Set_Product_Status()
        {
            EnumCatalogStatus status = EnumCatalogStatus.ACTIVE;
            EnumCatalogStatus statusToSet = EnumCatalogStatus.INACTIVE;
            var Product = new Product(Guid.NewGuid(), "Product 1", "filepath", status, Guid.NewGuid(), Guid.NewGuid());
            Product.SetStatus(statusToSet);
            Assert.Equal(statusToSet, Product.Status);
        }

        [Fact]
        public void Should_Create_Product_With_Empty_RealmId_Throw_Argument_Exception_Async()
        {
            Guid realmId = Guid.Empty;
            var ex = Assert.Throws<ArgumentException>(nameof(Product.RealmId), () => new Product(Guid.NewGuid(), "Product 1", "filepath", EnumCatalogStatus.ACTIVE, realmId, Guid.NewGuid()));
            Assert.Equal(nameof(Product.RealmId), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Product_With_RealmId()
        {
            Guid realmId = Guid.NewGuid();
            var Product = new Product(Guid.NewGuid(), "Product 1", "filepath", EnumCatalogStatus.ACTIVE, realmId, Guid.NewGuid());
            Assert.Equal(realmId, Product.RealmId);
        }

        [Fact]
        public void Should_Set_Product_RealmId()
        {
            Guid realmId = Guid.NewGuid();
            Guid realmIdToSet = Guid.NewGuid();
            var Product = new Product(Guid.NewGuid(), "Product 1", "filepath", EnumCatalogStatus.ACTIVE, realmId, Guid.NewGuid());
            Product.SetRealmId(realmIdToSet);
            Assert.Equal(realmIdToSet, Product.RealmId);
        }

        [Fact]
        public void Should_Create_Product_With_Empty_BrandId_Throw_Argument_Exception_Async()
        {
            Guid brandId = Guid.Empty;
            var ex = Assert.Throws<ArgumentException>(nameof(Product.BrandId), () => new Product(Guid.NewGuid(), "Product 1", "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(), brandId));
            Assert.Equal(nameof(Product.BrandId), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Product_With_BrandId()
        {
            Guid brandId = Guid.NewGuid();
            var Product = new Product(Guid.NewGuid(), "Product 1", "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(), brandId);
            Assert.Equal(brandId, Product.BrandId);
        }

        [Fact]
        public void Should_Set_Product_BrandId()
        {
            Guid brandId = Guid.NewGuid();
            Guid brandIdToSet = Guid.NewGuid();
            var Product = new Product(Guid.NewGuid(), "Product 1", "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(), brandId);
            Product.SetBrandId(brandIdToSet);
            Assert.Equal(brandIdToSet, Product.BrandId);
        }

        [Fact]
        public void Should_Set_Product_Id()
        {
            Guid productId = Guid.NewGuid();
            Guid productIdToSet = Guid.NewGuid();
            var brand = new Product(productId, "Brand 1", "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(),Guid.NewGuid());
            brand.SetId(productIdToSet);
            Assert.Equal(productIdToSet, brand.Id);
        }

        [Fact]
        public void Should_Set_Empty_Product_Id_Throws_Argument_Exception_Async()
        {
            Guid productId = Guid.NewGuid();
            Guid productIdToSet = Guid.Empty;
            var brand = new Product(productId, "Brand 1", "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid(),Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(() => brand.SetId(productIdToSet));
            Assert.Equal(nameof(Brand.Id), ex.ParamName);
        }
    }
}