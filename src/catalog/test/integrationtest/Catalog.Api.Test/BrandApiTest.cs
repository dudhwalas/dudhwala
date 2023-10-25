using Catalog.Application;
using Catalog.Domain.Shared;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Volo.Abp.Guids;
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
    public async Task Should_List_Brand_Sort_By_Name_Desc_Return_True()
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

    [Fact]
    public async Task Should_Create_Brand_Return_Created_Brand_Async()
    {
        var client = new BrandService.BrandServiceClient(Channel);

        var brandName = "xxx4";

        var createdBrand = await client.UpdateBrandAsync(new BrandDto
        {

            Name = brandName,
            Image = "/var/lib/files/data/xxx4.jpg",
            RealmId = Fixture.GuidGenerator?.Create().ToString(),
            Status = EnumCatalogStatus.ACTIVE.To<int>(),
        }).ResponseAsync;

        Assert.Equal(brandName, createdBrand.Name);
    }

    [Fact]
    public async Task Should_Update_Brand_Return_Updated_Brand_Async()
    {
        var client = new BrandService.BrandServiceClient(Channel);

        var brandNameToUpdate = "xxx6";

        var createdBrand = await client.UpdateBrandAsync(new BrandDto
        {

            Name = "xxx5",
            Image = "/var/lib/files/data/xxx5.jpg",
            RealmId = Fixture.GuidGenerator?.Create().ToString(),
            Status = EnumCatalogStatus.ACTIVE.To<int>(),
        }).ResponseAsync;

        createdBrand.Name = brandNameToUpdate;

        var updatedBrand = await client.UpdateBrandAsync(createdBrand).ResponseAsync;

        Assert.Equal(brandNameToUpdate, updatedBrand.Name);
    }

    [Fact]
    public async Task Should_Patch_Brand_Return_Updated_Brand_Async()
    {
        var client = new BrandService.BrandServiceClient(Channel);

        var brandNameToUpdate = "xxx8";

        var createdBrand = await client.UpdateBrandAsync(new BrandDto
        {

            Name = "xxx7",
            Image = "/var/lib/files/data/xxx7.jpg",
            RealmId = Fixture.GuidGenerator?.Create().ToString(),
            Status = EnumCatalogStatus.ACTIVE.To<int>(),
        }).ResponseAsync;

        createdBrand.Name = brandNameToUpdate;

        var patchedBrand = await client.PatchBrandAsync(new UpdateBrandRequestDto {
            FieldToUpdate = FieldMask.FromString("name"),
            Brand = createdBrand
        }).ResponseAsync;
        
        Assert.Equal(brandNameToUpdate, patchedBrand.Name);
    }
}