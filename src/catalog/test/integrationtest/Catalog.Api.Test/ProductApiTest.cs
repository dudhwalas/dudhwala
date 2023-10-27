using Catalog.Application;
using Catalog.Domain.Shared;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Xunit.Abstractions;

namespace Catalog.Api.Test;

public class ProductApiTest : IntegrationTestBase
{

    public ProductApiTest(GrpcTestFixture fixture, ITestOutputHelper outputHelper)
        : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async Task Should_List_Product_Count_Return_3_Async()
    {
        var client = new ProductService.ProductServiceClient(Channel);

        var listBrandResp = await client.ListProductAsync(new ListProductRequestDto()
        {
            PageSize = 3,
            PageToken = 1,
        }).ResponseAsync;

        Assert.Equal(3, listBrandResp.Products.Count);
    }

    [Fact]
    public async Task Should_List_Product_Throw_RPC_NotFound_Exception_Async()
    {
        var client = new ProductService.ProductServiceClient(Channel);

        var ex = await Assert.ThrowsAsync<RpcException>(() => client.ListProductAsync(new ListProductRequestDto()
        {
            PageSize = 3,
            PageToken = 10
        }).ResponseAsync);

        Assert.Equal(StatusCode.NotFound, ex.StatusCode);
    }
    
    [Fact]
    public async Task Should_Get_Product_By_Id_Throw_RPC_NotFound_Exception_Async()
    {
        var productId = "3a0d4ec4-4715-b913-b68b-b475b7da9d27";
        var client = new ProductService.ProductServiceClient(Channel);

        var ex = await Assert.ThrowsAsync<RpcException>(()=> client.GetProductAsync(new GetProductRequestDto()
        {
            Id = productId
        }).ResponseAsync);

        Assert.Equal(StatusCode.NotFound, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Get_Product_By_Id_Return_Product_Async()
    {
        var client = new ProductService.ProductServiceClient(Channel);

        var listProductResp = await client.ListProductAsync(new ListProductRequestDto()
        {
            PageSize = 3,
            PageToken = 1
        }).ResponseAsync;

        var productId = listProductResp.Products[0].Id;

        var getProductResp = await client.GetProductAsync(new GetProductRequestDto()
        {
            Id = productId
        }).ResponseAsync;

        Assert.Equal(productId, getProductResp.Id);
    }

    [Fact]
    public async Task Should_Create_Product_Return_Created_Product_Async()
    {
        var client = new ProductService.ProductServiceClient(Channel);

        var brandClient = new BrandService.BrandServiceClient(Channel);

        var brandResp = await brandClient.ListBrandAsync(new ListBrandRequestDto
        {
            PageSize = 3,
            PageToken = 1
        }).ResponseAsync;

        var productName = "xxx-p4";

        var createdProduct = await client.UpdateProductAsync(new ProductDto
        {

            Name = productName,
            Image = "/var/lib/files/data/xxx-p4.jpg",
            RealmId = Fixture.GuidGenerator?.Create().ToString(),
            Status = EnumCatalogStatus.ACTIVE.To<int>(),
            BrandId = brandResp.Brands[0].Id
        }).ResponseAsync;

        Assert.Equal(productName, createdProduct.Name);
    }

    [Fact]
    public async Task Should_Update_Product_Return_Updated_Product_Async()
    {
        var client = new ProductService.ProductServiceClient(Channel);

        var brandClient = new BrandService.BrandServiceClient(Channel);

        var brandResp = await brandClient.ListBrandAsync(new ListBrandRequestDto
        {
            PageSize = 3,
            PageToken = 1
        }).ResponseAsync;

        var productNameToUpdate = "xxx-p6";

        var createdProduct = await client.UpdateProductAsync(new ProductDto
        {

            Name = "xxx-p5",
            Image = "/var/lib/files/data/xxx-p5.jpg",
            RealmId = Fixture.GuidGenerator?.Create().ToString(),
            Status = EnumCatalogStatus.ACTIVE.To<int>(),
            BrandId = brandResp.Brands[0].Id
        }).ResponseAsync;

        createdProduct.Name = productNameToUpdate;

        var updatedProduct = await client.UpdateProductAsync(createdProduct).ResponseAsync;

        Assert.Equal(productNameToUpdate, updatedProduct.Name);
    }

    [Fact]
    public async Task Should_Patch_Product_Return_Updated_Product_Async()
    {
        var client = new ProductService.ProductServiceClient(Channel);

        var brandClient = new BrandService.BrandServiceClient(Channel);

        var brandResp = await brandClient.ListBrandAsync(new ListBrandRequestDto
        {
            PageSize = 3,
            PageToken = 1
        }).ResponseAsync;

        var productNameToUpdate = "xxx-p8";

        var createdProduct = await client.UpdateProductAsync(new ProductDto
        {

            Name = "xxx-p7",
            Image = "/var/lib/files/data/xxx-p7.jpg",
            RealmId = Fixture.GuidGenerator?.Create().ToString(),
            Status = EnumCatalogStatus.ACTIVE.To<int>(),
            BrandId = brandResp.Brands[0].Id
        }).ResponseAsync;

        createdProduct.Name = productNameToUpdate;

        var patchedProduct = await client.PatchProductAsync(new UpdateProductRequestDto {
            FieldToUpdate = FieldMask.FromString("name"),
            Product = createdProduct
        }).ResponseAsync;
        
        Assert.Equal(productNameToUpdate, patchedProduct.Name);
    }
}