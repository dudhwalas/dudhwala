using Catalog.Domain.Shared;

namespace Catalog.Domain.Test
{
    public class BrandTest
	{
		[Fact]
		public void Should_Create_Brand_With_Empty_Brand_Name_Throw_Argument_Exception_Async()
		{
            var brandName = "";
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Name),() => new Brand(Guid.NewGuid(), brandName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid()));
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Brand_With_Null_Brand_Name_Throw_Argument_Exception_Async()
        {
            string? brandName = null;
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Name), () => new Brand(Guid.NewGuid(), brandName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid()));
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Brand_With_Whitespace_Brand_Name_Throw_Argument_Exception_Async()
        {
            var brandName = " ";
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Name), () => new Brand(Guid.NewGuid(), brandName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid()));
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Brand_With_Brand_Name()
        {
            var brandName = "Brand 1";
            var brand = new Brand(Guid.NewGuid(),brandName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            Assert.Equal(brandName, brand.Name);
        }

        [Fact]
        public void Should_Set_Empty_Brand_Name_Throw_Argument_Exception_Async()
        {
            var brandName = "Brand 1";
            var brand = new Brand(Guid.NewGuid(), brandName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Name), () => brand.SetName(""));
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Null_Brand_Name_Throw_Argument_Exception_Async()
        {
            var brandName = "Brand 1";
            var brand = new Brand(Guid.NewGuid(), brandName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Name), () => brand.SetName(null));
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Whitespace_Brand_Name_Throw_Argument_Exception_Async()
        {
            var brandName = "Brand 1";
            var brand = new Brand(Guid.NewGuid(), brandName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Name), () => brand.SetName(" "));
            Assert.Equal(nameof(Brand.Name), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Brand_Name()
        {
            var brandName = "Brand 1";
            var brandNameToSet = "Brand 2";
            var brand = new Brand(Guid.NewGuid(), brandName, "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            brand.SetName(brandNameToSet);
            Assert.Equal(brandNameToSet, brand.Name);
        }

        [Fact]
        public void Should_Create_Brand_With_Empty_Brand_Image_Throw_Argument_Exception_Async()
        {
            var imagePath = "";
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Image), () => new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid()));
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Brand_With_Null_Brand_Image_Throw_Argument_Exception_Async()
        {
            string? imagePath = null;
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Image), () => new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid()));
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Brand_With_Whitespace_Brand_Image_Throw_Argument_Exception_Async()
        {
            string? imagePath = " ";
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Image), () => new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid()));
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Brand_With_Brand_Image()
        {
            var imagePath = "filepath";
            var brand = new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            Assert.Equal(imagePath, brand.Image);
        }

        [Fact]
        public void Should_Set_Empty_Brand_Image_Throw_Argument_Exception_Async()
        {
            var imagePath = "filepath";
            var brand = new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Image), () => brand.SetImage(""));
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Null_Brand_Image_Throw_Argument_Exception_Async()
        {
            var imagePath = "filepath";
            var brand = new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Image), () => brand.SetImage(null));
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Whitespace_Brand_Image_Throw_Argument_Exception_Async()
        {
            var imagePath = "filepath";
            var brand = new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.Image), () => brand.SetImage(" "));
            Assert.Equal(nameof(Brand.Image), ex.ParamName);
        }

        [Fact]
        public void Should_Set_Brand_Image()
        {
            var imagePath = "filepath 1";
            var imagePathToSet = "filepath 2";
            var brand = new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            brand.SetImage(imagePathToSet);
            Assert.Equal(imagePathToSet, brand.Image);
        }

        [Fact]
        public void Should_Create_Brand_With_Brand_Status()
        {
            EnumCatalogStatus status = EnumCatalogStatus.ACTIVE;
            var brand = new Brand(Guid.NewGuid(), "Brand 1", "filepath", status, Guid.NewGuid());
            Assert.Equal(status, brand.Status);
        }

        [Fact]
        public void Should_Set_Brand_Status()
        {
            EnumCatalogStatus status = EnumCatalogStatus.ACTIVE;
            EnumCatalogStatus statusToSet = EnumCatalogStatus.INACTIVE;
            var brand = new Brand(Guid.NewGuid(), "Brand 1", "filepath", status, Guid.NewGuid());
            brand.SetStatus(statusToSet);
            Assert.Equal(statusToSet, brand.Status);
        }

        [Fact]
        public void Should_Create_Brand_With_Empty_RealmId_Throw_Argument_Exception_Async()
        {
            Guid realmId = Guid.Empty;
            var ex = Assert.Throws<ArgumentException>(nameof(Brand.RealmId), () => new Brand(Guid.NewGuid(), "Brand 1", "filepath", EnumCatalogStatus.ACTIVE, realmId));
            Assert.Equal(nameof(Brand.RealmId), ex.ParamName);
        }

        [Fact]
        public void Should_Create_Brand_With_RealmId()
        {
            Guid realmId = Guid.NewGuid();
            var brand = new Brand(Guid.NewGuid(), "Brand 1", "filepath", EnumCatalogStatus.ACTIVE, realmId);
            Assert.Equal(realmId, brand.RealmId);
        }

        [Fact]
        public void Should_Set_Brand_RealmId()
        {
            Guid realmId = Guid.NewGuid();
            Guid realmIdToSet = Guid.NewGuid();
            var brand = new Brand(Guid.NewGuid(), "Brand 1", "filepath", EnumCatalogStatus.ACTIVE, realmId);
            brand.SetRealmId(realmIdToSet);
            Assert.Equal(realmIdToSet, brand.RealmId);
        }

        [Fact]
        public void Should_Set_Brand_Id()
        {
            Guid brandId = Guid.NewGuid();
            Guid brandIdToSet = Guid.NewGuid();
            var brand = new Brand(brandId, "Brand 1", "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            brand.SetId(brandIdToSet);
            Assert.Equal(brandIdToSet, brand.Id);
        }

        [Fact]
        public void Should_Set_Empty_Brand_Id_Throws_Argument_Exception_Async()
        {
            Guid brandId = Guid.NewGuid();
            Guid brandIdToSet = Guid.Empty;
            var brand = new Brand(brandId, "Brand 1", "filepath", EnumCatalogStatus.ACTIVE, Guid.NewGuid());
            var ex = Assert.Throws<ArgumentException>(()=> brand.SetId(brandIdToSet));
            Assert.Equal(nameof(Brand.Id), ex.ParamName);
        }
    }
}