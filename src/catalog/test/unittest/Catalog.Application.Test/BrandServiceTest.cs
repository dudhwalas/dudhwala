using Volo.Abp.ObjectMapping;
using Catalog.Domain;
using Catalog.Domain.Shared.Localization;
using Microsoft.Extensions.Localization;
using Moq;
using Grpc.Core;

namespace Catalog.Application.Test;

public class BrandServiceTest
{
    [Fact]
    public async Task Should_Get_Brand_With_Null_BrandId_Throw_Argument_Exception_Async()
    {
        //Arrange
        var mockBrandRepo = Mock.Of<IBrandRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => brandService.GetBrand(new GetBrandRequestDto(), mockServerCallContxt));

        //Assert
        Assert.Equal(nameof(GetBrandRequestDto.Id), ex.ParamName);
    }

    [Fact]
    public async Task Should_Get_Brand_With_Empty_BrandId_Throw_Argument_Exception_Async()
    {
        //Arrange
        var mockBrandRepo = Mock.Of<IBrandRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => brandService.GetBrand(new GetBrandRequestDto() { Id = String.Empty}, mockServerCallContxt));

        //Assert
        Assert.Equal(nameof(GetBrandRequestDto.Id), ex.ParamName);
    }

    [Fact]
    public async Task Should_Get_Brand_With_Not_Formatted_BrandId_Throw_Argument_Exception_Async()
    {
        //Arrange
        var mockBrandRepo = Mock.Of<IBrandRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => brandService.GetBrand(new GetBrandRequestDto() { Id = "000-000-000-000" }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }
}
