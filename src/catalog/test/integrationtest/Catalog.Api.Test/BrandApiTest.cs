using System.Diagnostics.Metrics;
using Catalog.Application;
using Catalog.Domain;
using Xunit.Abstractions;

namespace Catalog.Api.Test;

public class BrandApiTest : IntegrationTestBase
{

    public BrandApiTest(GrpcTestFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
        
    }

    [Fact]
    public void Should_Get_Brand_By_Id_Return_Brand()
    {
        var brandId = "3a0d4ec4-4715-b913-b68b-b475b7da9d27";
        var client = new BrandService.BrandServiceClient(Channel);
        var dto = client.GetBrand(new GetBrandRequestDto()
        {
            Id = brandId
        });

        Assert.Equal(brandId, dto.Id);
    }
}
