using Volo.Abp.ObjectMapping;
using Catalog.Domain;
using Catalog.Domain.Shared.Localization;
using Microsoft.Extensions.Localization;
using Moq;
using Grpc.Core;
using Catalog.Domain.Shared;
using System.Linq.Dynamic.Core.Exceptions;
using Volo.Abp;
using Google.Protobuf.WellKnownTypes;
using Volo.Abp.Domain.Entities;

namespace Catalog.Application.Test;

public class ProductServiceTest
{
    [Fact]
    public async Task Should_Get_Product_With_Null_ProductId_Throw_Argument_Exception_Async()
    {
        //Arrange
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => ProductService.GetProduct(new GetProductRequestDto(), mockServerCallContxt));

        //Assert
        Assert.Equal(nameof(GetProductRequestDto.Id), ex.ParamName);
    }

    [Fact]
    public async Task Should_Get_Product_With_Empty_ProductId_Throw_Argument_Exception_Async()
    {
        //Arrange
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => ProductService.GetProduct(new GetProductRequestDto() { Id = String.Empty}, mockServerCallContxt));

        //Assert
        Assert.Equal(nameof(GetProductRequestDto.Id), ex.ParamName);
    }

    [Fact]
    public async Task Should_Get_Product_With_Not_Formatted_ProductId_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.GetProduct(new GetProductRequestDto() { Id = "000-000-000-000" }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Get_Product_With_ProductId_Throw_RPC_Not_Found_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var mockProductRepo = Mock.Of<IProductRepository>(repo => repo.GetByIdAsync(ProductId) == Task.FromResult(default(Product)));
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.GetProduct(new GetProductRequestDto() { Id = ProductId.ToString() }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.NotFound, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Get_Product_Throw_Not_Found_Exception_Async()
    {
        //Arrange
        var productId = Guid.NewGuid();
        var mockProductRepo = new Mock<IProductRepository>();
        mockProductRepo.Setup(repo => repo.GetByIdAsync(productId)).ThrowsAsync(new EntityNotFoundException());
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var productService = new Services.ProductService(mockProductRepo.Object, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => productService.GetProduct(new GetProductRequestDto()
        { Id = productId.ToString() }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.NotFound, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Get_Product_With_ProductId_Return_Product_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductImage = "xxx.png";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId);
        var mockProductRepo = Mock.Of<IProductRepository>(repo => repo.GetByIdAsync(ProductId) == Task.FromResult(Product));
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<Product, ProductDto>(Product) == new ProductDto() {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = brandId.ToString()
        });
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ProductDto = await ProductService.GetProduct(new GetProductRequestDto() { Id = ProductId.ToString() }, mockServerCallContxt);

        //Assert
        Assert.Equal(ProductId.ToString(), ProductDto.Id);
    }

    [Fact]
    public async Task Should_List_Product_With_PageToken_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = 0;
        var pageSize = 1;
        var sorting = string.Empty;
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.ListProduct(new ListProductRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Product_With_PageToken_Less_Than_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = -1;
        var pageSize = 1;
        var sorting = string.Empty;
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.ListProduct(new ListProductRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Product_With_PageSize_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = 1;
        var pageSize = 0;
        var sorting = string.Empty;
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.ListProduct(new ListProductRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Product_With_PageSize_Less_Than_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = 1;
        var pageSize = -1;
        var sorting = string.Empty;
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.ListProduct(new ListProductRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Product_With_PageToken_PageSize_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = 0;
        var pageSize = 0;
        var sorting = string.Empty;
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.ListProduct(new ListProductRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Product_With_PageSize_PageToken_Less_Than_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = -1;
        var pageSize = -1;
        var sorting = string.Empty;
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.ListProduct(new ListProductRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Product_With_Invalid_Sorting_Throw_Argument_RPC_Exception_Async()
    {
        //Arrange
        var pageToken = 1;
        var pageSize = 1;
        var sorting = "names descsa";
        var brandId = Guid.Empty;
        var ProductList = new List<Product>();
        var mockProductRepo = new Mock<IProductRepository>();
        var mockBrandRepo = new Mock<IBrandRepository>();
        mockProductRepo.Setup(repo => repo.GetAsync(brandId, pageToken - 1, pageSize, sorting)).ThrowsAsync(new ParseException("",1));
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new ProductManager(mockProductRepo.Object, mockBrandRepo.Object, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo.Object, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.ListProduct(new ListProductRequestDto()
        {
            BrandId = brandId.ToString(),
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt)) ;

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Product_With_PageSize_1_PageToken_1_Throw_RPC_Not_Found_Exception_Async()
    {
        //Arrange
        var pageToken = 1;
        var pageSize = 1;
        var sorting = string.Empty;
        var brandId = Guid.NewGuid();
        var ProductList = new List<Product>();
        var mockProductRepo = Mock.Of<IProductRepository>(repo => repo.GetAsync(brandId,pageToken-1,pageSize,sorting) ==
        Task.FromResult(ProductList));
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.ListProduct(new ListProductRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting,
            BrandId = brandId.ToString()
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.NotFound, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Product_Invalid_BrandId_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange

        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductImage = "xxx.png";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId);

        var pageToken = 1;
        var pageSize = 1;
        var sorting = string.Empty;
        var ProductList = new List<Product>() { Product };
        long totalCount = 1;
        var mockProductRepo = Mock.Of<IProductRepository>(repo => repo.GetAsync(brandId, pageToken - 1, pageSize, sorting) ==
        Task.FromResult(ProductList) && repo.GetTotalAsync() == Task.FromResult(totalCount));
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<List<Product>,
            List<ProductDto>>(ProductList) == ProductList.Select(Product => new ProductDto()
            {
                Id = Product.Id.ToString(),
                Name = Product.Name,
                Image = Product.Image,
                Status = Product.Status != null ? Product.Status.Value.To<int>() : 1,
                RealmId = Product.RealmId.ToString(),
                BrandId = brandId.ToString()
            }).ToList());
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.ListProduct(new ListProductRequestDto()
        {
            BrandId = "xxx-xxx-xxx-xxx",
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Product_With_PageSize_1_PageToken_1_TotalCount_1_Return_Products_Async()
    {
        //Arrange

        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductImage = "xxx.png";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId,brandId);

        var pageToken = 1;
        var pageSize = 1;
        var sorting = string.Empty;
        var ProductList = new List<Product>() {Product};
        long totalCount = 1;
        var mockProductRepo = Mock.Of<IProductRepository>(repo => repo.GetAsync(brandId,pageToken - 1, pageSize, sorting) ==
        Task.FromResult(ProductList) && repo.GetTotalAsync() == Task.FromResult(totalCount));
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<List<Product>,
            List<ProductDto>>(ProductList) == ProductList.Select(Product => new ProductDto()
        {
            Id = Product.Id.ToString(),
            Name = Product.Name,
            Image = Product.Image,
            Status = Product.Status != null ? Product.Status.Value.To<int>() : 1,
            RealmId = Product.RealmId.ToString(),
            BrandId = brandId.ToString()
        }).ToList());
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ProductResponse = await ProductService.ListProduct(new ListProductRequestDto()
        {
            BrandId = brandId.ToString(),
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt);

        //Assert
        Assert.True(ProductResponse.Products.Any());
        Assert.Equal(1, ProductResponse.NextPageToken);
    }

    [Fact]
    public async Task Should_List_Product_With_PageSize_1_PageToken_1_TotalCount_2_Return_Products_Async()
    {
        //Arrange

        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductImage = "xxx.png";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId);

        var pageToken = 1;
        var pageSize = 1;
        var sorting = string.Empty;
        var ProductList = new List<Product>() { Product };
        long totalCount = 2;
        var mockProductRepo = Mock.Of<IProductRepository>(repo => repo.GetAsync(brandId,pageToken - 1, pageSize, sorting) ==
        Task.FromResult(ProductList) && repo.GetTotalAsync() == Task.FromResult(totalCount));
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<List<Product>,
            List<ProductDto>>(ProductList) == ProductList.Select(Product => new ProductDto()
            {
                Id = Product.Id.ToString(),
                Name = Product.Name,
                Image = Product.Image,
                Status = Product.Status != null ? Product.Status.Value.To<int>() : 1,
                RealmId = Product.RealmId.ToString(),
                BrandId = brandId.ToString()
            }).ToList());
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ProductResponse = await ProductService.ListProduct(new ListProductRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting,
            BrandId = brandId.ToString()
        }, mockServerCallContxt);

        //Assert
        Assert.True(ProductResponse.Products.Any());
        Assert.Equal(2, ProductResponse.NextPageToken);
    }

    [Fact]
    public async Task Should_Update_Product_With_Empty_Name_Throws_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = string.Empty;
        var ProductImage = "xxx.png";
        var ProductStatus = EnumCatalogStatus.ACTIVE.To<int>();
        var ProductRealmId = Guid.NewGuid();
        
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(()=>ProductService.UpdateProduct(new ProductDto()
        {
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus,
            RealmId = ProductRealmId.ToString()
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Update_Product_With_Empty_Image_Throws_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductImage = string.Empty;
        var ProductStatus = EnumCatalogStatus.ACTIVE.To<int>();
        var ProductRealmId = Guid.NewGuid();

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.UpdateProduct(new ProductDto()
        {
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus,
            RealmId = ProductRealmId.ToString()
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Update_Product_With_Empty_RealmId_Throws_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE.To<int>();
        var ProductImage = "xxx.png";
        var ProductRealmId = string.Empty;

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.UpdateProduct(new ProductDto()
        {
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus,
            RealmId = ProductRealmId
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Update_Product_With_Malformed_RealmId_Throws_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE.To<int>();
        var ProductImage = "xxx.png";
        var ProductRealmId = "0000-000-00-0";

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.UpdateProduct(new ProductDto()
        {
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus,
            RealmId = ProductRealmId
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Update_Product_With_Empty_BrandId_Throws_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE.To<int>();
        var ProductImage = "xxx.png";
        var ProductRealmId = string.Empty;
        var brandId = string.Empty;

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.UpdateProduct(new ProductDto()
        {
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus,
            RealmId = ProductRealmId,
            BrandId = brandId
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Update_Product_With_Malformed_BrandId_Throws_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE.To<int>();
        var ProductImage = "xxx.png";
        var ProductRealmId = string.Empty;
        var brandId = "0000-000-00-0";

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = Mock.Of<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.UpdateProduct(new ProductDto()
        {
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus,
            RealmId = ProductRealmId,
            BrandId = brandId
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Update_Product_With_Non_Existing_Brand_Throws_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId);

        var mockProductRepo = Mock.Of<IProductRepository>((repo) => repo.GetByNameAsync(ProductName) == Task.FromResult(Product));
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        mockProductManager.Setup(bm => bm.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId)).ThrowsAsync(new BusinessException(CatalogErrorCodes.Product_BrandNotAvailable));
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.UpdateProduct(new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = brandId.ToString()
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Update_Product_With_Existing_Product_Name_Throws_RPC_Already_Exist_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId);

        var mockProductRepo = Mock.Of<IProductRepository>((repo)=>repo.GetByNameAsync(ProductName) == Task.FromResult(Product));
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        mockProductManager.Setup(bm => bm.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId)).ThrowsAsync(new BusinessException(CatalogErrorCodes.Product_NameAlreadyExist));
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.UpdateProduct(new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = brandId.ToString()
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.AlreadyExists, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Update_Product_Throws_RPC_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId);

        var mockProductRepo = Mock.Of<IProductRepository>((repo) => repo.GetByIdAsync(ProductId) == Task.FromResult(Product) && repo.UpdateAsync(Product) == Task.FromResult(default(Product)));
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        mockProductManager.Setup(bm => bm.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId)).ThrowsAsync(new BusinessException(CatalogErrorCodes.Product_UpdateFailed));
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.UpdateProduct(new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = brandId.ToString()
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Create_Product_Throws_RPC_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId);

        var mockProductRepo = Mock.Of<IProductRepository>((repo) => repo.GetByIdAsync(ProductId) == Task.FromResult(Product) && repo.UpdateAsync(Product) == Task.FromResult(default(Product)));
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        mockProductManager.Setup(bm => bm.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId)).ThrowsAsync(new BusinessException(CatalogErrorCodes.Product_CreateFailed));
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.UpdateProduct(new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = brandId.ToString()
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Update_Product_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var productId = Guid.NewGuid();
        var productName = "xxx";
        var productStatus = EnumCatalogStatus.ACTIVE;
        var productImage = "xxx.png";
        var productRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        var product = new Product(productId, productName, productImage, productStatus, productRealmId,brandId);

        var mockproductRepo = Mock.Of<IProductRepository>((repo) => repo.GetByIdAsync(productId) == Task.FromResult(product) && repo.UpdateAsync(It.IsAny<Product>()) == Task.FromResult(product));
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<Product, ProductDto>(product) == new ProductDto()
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            Image = product.Image,
            Status = product.Status != null ? product.Status.To<int>() : 1,
            RealmId = product.RealmId.ToString()
        });
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockproductManager = new Mock<IProductManager>();
        mockproductManager.Setup(bm => bm.UpdateAsync(productId, productName, productImage, productStatus, productRealmId, brandId)).ReturnsAsync(product);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var productService = new Services.ProductService(mockproductRepo, mockObjectMapper, mockLocal, mockproductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => productService.UpdateProduct(new ProductDto()
        {
            Id = "xxx-xxx-xxx-xxx-xxx",
            Name = productName,
            Image = productImage,
            Status = productStatus.To<int>(),
            RealmId = productRealmId.ToString(),
            BrandId = brandId.ToString()
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Update_Product_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId);

        var mockProductRepo = Mock.Of<IProductRepository>((repo) => repo.GetByIdAsync(ProductId) == Task.FromResult(Product) && repo.UpdateAsync(It.IsAny<Product>()) == Task.FromResult(Product));
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<Product, ProductDto>(Product) == new ProductDto()
        {
            Id = Product.Id.ToString(),
            Name = Product.Name,
            Image = Product.Image,
            Status = Product.Status != null ? Product.Status.To<int>() : 1,
            RealmId = Product.RealmId.ToString(),
            BrandId = brandId.ToString()
        });
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        mockProductManager.Setup(bm => bm.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId)).ReturnsAsync(Product);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ProductResult = await ProductService.UpdateProduct(new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = brandId.ToString()
        }, mockServerCallContxt);

        //Assert
        Assert.Equal(ProductId.ToString(), ProductResult.Id);
    }

    [Fact]
    public async Task Should_Create_Product_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId);

        var mockProductRepo = Mock.Of<IProductRepository>((repo) => repo.CreateAsync(It.IsAny<Product>()) == Task.FromResult(Product));
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<Product, ProductDto>(Product) == new ProductDto()
        {
            Id = Product.Id.ToString(),
            Name = Product.Name,
            Image = Product.Image,
            Status = Product.Status != null ? Product.Status.To<int>() : 1,
            RealmId = Product.RealmId.ToString(),
            BrandId = brandId.ToString()
        });
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        mockProductManager.Setup(bm => bm.UpdateAsync(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, brandId)).ReturnsAsync(Product);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ProductResult = await ProductService.UpdateProduct(new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = brandId.ToString()
        }, mockServerCallContxt);

        //Assert
        Assert.Equal(ProductId.ToString(), ProductResult.Id);
    }

    [Fact]
    public async Task Should_Patch_Product_With_Null_Product_Throws_RPC_Argument_Exception_Async()
    {
        //Arrange
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = null,
            FieldToUpdate = null
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Patch_Product_With_Empty_Product_Id_Throws_RPC_Argument_Exception_Async()
    {
        //Arrange
        string? ProductId = string.Empty;
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var ProductBrandId = Guid.NewGuid();

        var ProductDto = new ProductDto()
        {
            Id = ProductId,
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = ProductBrandId.ToString()
        };
        
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = ProductDto,
            FieldToUpdate = null
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Patch_Product_With_Space_Product_Id_Throws_RPC_Argument_Exception_Async()
    {
        //Arrange
        string? ProductId = " ";
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var ProductBrandId = Guid.NewGuid();

        var ProductDto = new ProductDto()
        {
            Id = ProductId,
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = ProductBrandId.ToString()
        };

        //var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId);

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = ProductDto,
            FieldToUpdate = null
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Patch_Product_With_Malformed_Product_Id_Throws_RPC_Argument_Exception_Async()
    {
        //Arrange
        string? ProductId = "0.0.0.0";
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var ProductBrandId = Guid.NewGuid();

        var ProductDto = new ProductDto()
        {
            Id = ProductId,
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = ProductBrandId.ToString()
        };

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = ProductDto,
            FieldToUpdate = null
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Patch_Product_With_No_Update_Fields_Throws_RPC_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var ProductBrandId = Guid.NewGuid();

        var ProductDto = new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = ProductBrandId.ToString()
        };

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = ProductDto,
            FieldToUpdate = null
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Patch_Product_With_Empty_Update_Fields_Throws_RPC_Invalid_Argument_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var ProductBrandId = Guid.NewGuid();

        var ProductDto = new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = ProductBrandId.ToString()
        };
        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = ProductDto,
            FieldToUpdate = FieldMask.FromString("")
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Patch_Product_With_Unknown_Update_Fields_Throws_RPC_Invalid_Argument_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var ProductBrandId = Guid.NewGuid();

        var ProductDto = new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = ProductBrandId.ToString()
        };

        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId, ProductBrandId);

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<Product, ProductDto>(Product) == new ProductDto()
        {
            Id = Product.Id.ToString(),
            Name = Product.Name,
            Image = Product.Image,
            Status = Product.Status != null ? Product.Status.To<int>() : 1,
            RealmId = Product.RealmId.ToString()
        });
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        mockProductManager.Setup(bm => bm.PatchAsync(ProductId, string.Empty, string.Empty, null, null, null)).ReturnsAsync(Product);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = ProductDto,
            FieldToUpdate = FieldMask.FromString("unknown_field")
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Patch_Product_With_Update_Fields_Returns_Updated_Product_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var ProductBrandId = Guid.NewGuid();

        var ProductDto = new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString(),
            BrandId = ProductBrandId.ToString()
        };

        var Product = new Product(ProductId, ProductName, ProductImage, ProductStatus, ProductRealmId,ProductBrandId);

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<Product, ProductDto>(Product) == new ProductDto()
        {
            Id = Product.Id.ToString(),
            Name = Product.Name,
            Image = Product.Image,
            Status = Product.Status != null ? Product.Status.To<int>() : 1,
            RealmId = Product.RealmId.ToString()
        });
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        mockProductManager.Setup(bm => bm.PatchAsync(ProductId, ProductName, string.Empty, null, null,null)).ReturnsAsync(Product);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var patchedProduct = await ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = ProductDto,
            FieldToUpdate = FieldMask.FromString("name")
        }, mockServerCallContxt);

        //Assert
        Assert.Equal(ProductId.ToString(), patchedProduct.Id);
    }

    [Fact]
    public async Task Should_Patch_Product_Throws_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        
        var ProductDto = new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString()
        };

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        mockProductManager.Setup(bm => bm.PatchAsync(It.IsAny<Guid>(), ProductName, string.Empty, null, null, null)).ThrowsAsync(new BusinessException(CatalogErrorCodes.Product_UpdateFailed));
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = ProductDto,
            FieldToUpdate = FieldMask.FromString("name")
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Patch_Product_With_Existing_Product_Name_Throws_RPC_Already_Exist_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        
        var ProductDto = new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString()
        };

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        mockProductManager.Setup(bm => bm.PatchAsync(It.IsAny<Guid>(), ProductName, string.Empty, null, null, null)).ThrowsAsync(new BusinessException(CatalogErrorCodes.Product_NameAlreadyExist));
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = ProductDto,
            FieldToUpdate = FieldMask.FromString("name")
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.AlreadyExists, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Patch_Product_Invalid_ProductId_Throws_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();

        var ProductDto = new ProductDto()
        {
            Id = "xxx-xxx-xxx-xxx",
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString()
        };

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>();
        var mockProductManager = new Mock<IProductManager>();
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = ProductDto,
            FieldToUpdate = FieldMask.FromString("name")
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Patch_Product_With_Non_Existing_Brand_Throws_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var ProductId = Guid.NewGuid();
        var ProductName = "xxx";
        var ProductStatus = EnumCatalogStatus.ACTIVE;
        var ProductImage = "xxx.png";
        var ProductRealmId = Guid.NewGuid();
        var brandId = Guid.NewGuid();
        
        var ProductDto = new ProductDto()
        {
            Id = ProductId.ToString(),
            Name = ProductName,
            Image = ProductImage,
            Status = ProductStatus.To<int>(),
            RealmId = ProductRealmId.ToString()
        };

        var mockProductRepo = Mock.Of<IProductRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockProductManager = new Mock<IProductManager>();
        mockProductManager.Setup(bm => bm.PatchAsync(It.IsAny<Guid>(), ProductName, string.Empty, null, null, null)).ThrowsAsync(new BusinessException(CatalogErrorCodes.Product_BrandNotAvailable));
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var ProductService = new Services.ProductService(mockProductRepo, mockObjectMapper, mockLocal, mockProductManager.Object);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => ProductService.PatchProduct(new UpdateProductRequestDto
        {
            Product = ProductDto,
            FieldToUpdate = FieldMask.FromString("name")
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }
}