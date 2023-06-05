using System;
using Catalog.Domain;
using Catalog.Domain.Shared;
using Volo.Abp;

namespace Catalog.UnitTest.Domain
{
	public class BrandTest
	{
		[Fact]
		public void Should_Throw_Argument_Exception_For_Empty_Brand_Name()
		{
            var brandName = "";
            Assert.Throws<ArgumentException>(nameof(Brand.Name),() => new Brand(Guid.NewGuid(), brandName, "filepath", EnumStatus.ACTIVE, Guid.NewGuid()));
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Null_Brand_Name()
        {
            string? brandName = null;
            Assert.Throws<ArgumentException>(nameof(Brand.Name), () => new Brand(Guid.NewGuid(), brandName, "filepath", EnumStatus.ACTIVE, Guid.NewGuid()));
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Whitespace_Brand_Name()
        {
            var brandName = " ";
            Assert.Throws<ArgumentException>(nameof(Brand.Name), () => new Brand(Guid.NewGuid(), brandName, "filepath", EnumStatus.ACTIVE, Guid.NewGuid()));
        }

        [Fact]
        public void Should_Create_Brand_With_Brand_Name()
        {
            var brandName = "Brand 1";
            var brand = new Brand(Guid.NewGuid(),brandName, "filepath", EnumStatus.ACTIVE, Guid.NewGuid());
            Assert.Equal(brandName, brand.Name);
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Set_Empty_Brand_Name()
        {
            var brandName = "Brand 1";
            var brand = new Brand(Guid.NewGuid(), brandName, "filepath", EnumStatus.ACTIVE, Guid.NewGuid());

            Assert.Throws<ArgumentException>(nameof(Brand.Name), () => brand.SetName(""));
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Set_Null_Brand_Name()
        {
            var brandName = "Brand 1";
            var brand = new Brand(Guid.NewGuid(), brandName, "filepath", EnumStatus.ACTIVE, Guid.NewGuid());

            Assert.Throws<ArgumentException>(nameof(Brand.Name), () => brand.SetName(null));
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Set_Whitespace_Brand_Name()
        {
            var brandName = "Brand 1";
            var brand = new Brand(Guid.NewGuid(), brandName, "filepath", EnumStatus.ACTIVE, Guid.NewGuid());

            Assert.Throws<ArgumentException>(nameof(Brand.Name), () => brand.SetName(" "));
        }

        [Fact]
        public void Should_Set_Brand_Name()
        {
            var brandName = "Brand 1";
            var brandNameToSet = "Brand 2";
            var brand = new Brand(Guid.NewGuid(), brandName, "filepath", EnumStatus.ACTIVE, Guid.NewGuid());
            brand.SetName(brandNameToSet);
            Assert.Equal(brandNameToSet, brand.Name);
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Empty_Brand_Image()
        {
            var imagePath = "";
            Assert.Throws<ArgumentException>(nameof(Brand.Image), () => new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumStatus.ACTIVE, Guid.NewGuid()));
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Null_Brand_Image()
        {
            string? imagePath = null;
            Assert.Throws<ArgumentException>(nameof(Brand.Image), () => new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumStatus.ACTIVE, Guid.NewGuid()));
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Whitespace_Brand_Image()
        {
            string? imagePath = " ";
            Assert.Throws<ArgumentException>(nameof(Brand.Image), () => new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumStatus.ACTIVE, Guid.NewGuid()));
        }

        [Fact]
        public void Should_Create_Brand_With_Brand_Image()
        {
            var imagePath = "filepath";
            var brand = new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumStatus.ACTIVE, Guid.NewGuid());
            Assert.Equal(imagePath, brand.Image);
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Set_Empty_Brand_Image()
        {
            var imagePath = "filepath";
            var brand = new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumStatus.ACTIVE, Guid.NewGuid());

            Assert.Throws<ArgumentException>(nameof(Brand.Image), () => brand.SetImage(""));
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Set_Null_Brand_Image()
        {
            var imagePath = "filepath";
            var brand = new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumStatus.ACTIVE, Guid.NewGuid());

            Assert.Throws<ArgumentException>(nameof(Brand.Image), () => brand.SetImage(null));
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Set_Whitespace_Brand_Image()
        {
            var imagePath = "filepath";
            var brand = new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumStatus.ACTIVE, Guid.NewGuid());

            Assert.Throws<ArgumentException>(nameof(Brand.Image), () => brand.SetImage(" "));
        }

        [Fact]
        public void Should_Set_Brand_Image()
        {
            var imagePath = "filepath 1";
            var imagePathToSet = "filepath 2";
            var brand = new Brand(Guid.NewGuid(), "Brand 1", imagePath, EnumStatus.ACTIVE, Guid.NewGuid());
            brand.SetImage(imagePathToSet);
            Assert.Equal(imagePathToSet, brand.Image);
        }

        [Fact]
        public void Should_Create_Brand_With_Brand_Status()
        {
            EnumStatus status = EnumStatus.ACTIVE;
            var brand = new Brand(Guid.NewGuid(), "Brand 1", "filepath", status, Guid.NewGuid());
            Assert.Equal(status, brand.Status);
        }

        [Fact]
        public void Should_Set_Brand_Status()
        {
            EnumStatus status = EnumStatus.ACTIVE;
            EnumStatus statusToSet = EnumStatus.INACTIVE;
            var brand = new Brand(Guid.NewGuid(), "Brand 1", "filepath", status, Guid.NewGuid());
            brand.SetStatus(statusToSet);
            Assert.Equal(statusToSet, brand.Status);
        }

        [Fact]
        public void Should_Throw_Argument_Exception_For_Empty_RealmId()
        {
            Guid realmId = Guid.Empty;
            Assert.Throws<ArgumentException>(nameof(Brand.RealmId), () => new Brand(Guid.NewGuid(), "Brand 1", "filepath", EnumStatus.ACTIVE, realmId));
        }

        [Fact]
        public void Should_Create_Brand_With_RealmId()
        {
            Guid realmId = Guid.NewGuid();
            var brand = new Brand(Guid.NewGuid(), "Brand 1", "filepath", EnumStatus.ACTIVE, realmId);
            Assert.Equal(realmId, brand.RealmId);

        }

        [Fact]
        public void Should_Set_Brand_RealmId()
        {
            Guid realmId = Guid.NewGuid();
            Guid realmIdToSet = Guid.NewGuid();
            var brand = new Brand(Guid.NewGuid(), "Brand 1", "filepath", EnumStatus.ACTIVE, realmId);
            brand.SetRealmId(realmIdToSet);
            Assert.Equal(realmIdToSet, brand.RealmId);
        }
    }
}