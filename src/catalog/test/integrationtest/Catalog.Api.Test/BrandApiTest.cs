using Catalog.Application;
using Grpc.Core;
using Volo.Abp.Data;
using Xunit.Abstractions;

namespace Catalog.Api.Test;

public class BrandApiTest : IntegrationTestBase
{

    public BrandApiTest(GrpcTestFixture fixture, ITestOutputHelper outputHelper)
        : base(fixture, outputHelper)
    {
       
    }

    [Fact]
    public async Task Should_List_Brand_Count_Return_3_Async()
    {
        var client = new BrandService.BrandServiceClient(Channel);

        var listBrandResp = await client.ListBrandAsync(new ListBrandRequestDto()
        {
            PageSize = 3,
            PageToken = 1,
        }).ResponseAsync;

        Assert.Equal(3, listBrandResp.Brands.Count);
    }

    [Fact]
    public async Task Should_List_Brand_Throw_RPC_NotFound_Exception_Async()
    {
        var client = new BrandService.BrandServiceClient(Channel);

        var ex = await Assert.ThrowsAsync<RpcException>(() => client.ListBrandAsync(new ListBrandRequestDto()
        {
            PageSize = 3,
            PageToken = 2
        }).ResponseAsync);

        Assert.Equal(StatusCode.NotFound, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Brand_By_Sort_Name_Desc_Return_True()
    {
        var client = new BrandService.BrandServiceClient(Channel);

        var listBrandResp = await client.ListBrandAsync(new ListBrandRequestDto()
        {
            PageSize = 3,
            PageToken = 1,
            Sorting = "name desc"
        }).ResponseAsync;

        var start = 3;

        var isOrderedDesc = listBrandResp.Brands.Select((brand, i) => brand.Name == "xxx" + (start - i)).All(res => res);

        Assert.True(isOrderedDesc);
    }

    [Fact]
    public async Task Should_Get_Brand_By_Id_Throw_RPC_NotFound_Exception_Async()
    {
        var brandId = "3a0d4ec4-4715-b913-b68b-b475b7da9d27";
        var client = new BrandService.BrandServiceClient(Channel);

        var ex = await Assert.ThrowsAsync<RpcException>(()=> client.GetBrandAsync(new GetBrandRequestDto()
        {
            Id = brandId
        }).ResponseAsync);

        Assert.Equal(StatusCode.NotFound, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Get_Brand_By_Id_Return_Brand_Async()
    {
        var client = new BrandService.BrandServiceClient(Channel);

        var listBrandResp = await client.ListBrandAsync(new ListBrandRequestDto()
        {
            PageSize = 3,
            PageToken = 1
        }).ResponseAsync;

        var brandId = listBrandResp.Brands[0].Id;

        var getBrandResp = await client.GetBrandAsync(new GetBrandRequestDto()
        {
            Id = brandId
        }).ResponseAsync;

        Assert.Equal(brandId, getBrandResp.Id);
    }
}