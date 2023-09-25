﻿using Volo.Abp.ObjectMapping;
using Catalog.Domain;
using Catalog.Domain.Shared.Localization;
using Microsoft.Extensions.Localization;
using Moq;
using Grpc.Core;
using Catalog.Domain.Shared;
using System.Linq.Dynamic.Core.Exceptions;

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
    public async Task Should_Get_Brand_With_Not_Formatted_BrandId_Throw_RPC_Invalid_Argument_Exception_Async()
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

    [Fact]
    public async Task Should_Get_Brand_With_BrandId_Throw_RPC_Not_Found_Exception_Async()
    {
        //Arrange
        var brandId = Guid.NewGuid();
        var mockBrandRepo = Mock.Of<IBrandRepository>(repo => repo.GetByIdAsync(brandId) == Task.FromResult(default(Brand)));
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => brandService.GetBrand(new GetBrandRequestDto() { Id = brandId.ToString() }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.NotFound, ex.StatusCode);
    }

    [Fact]
    public async Task Should_Get_Brand_With_BrandId_Return_Brand_Async()
    {
        //Arrange
        var brandId = Guid.NewGuid();
        var brandName = "xxx";
        var brandImage = "xxx.png";
        var brandStatus = EnumCatalogStatus.ACTIVE;
        var brandRealmId = Guid.NewGuid();
        var brand = new Brand(brandId, brandName, brandImage, brandStatus, brandRealmId);
        var mockBrandRepo = Mock.Of<IBrandRepository>(repo => repo.GetByIdAsync(brandId) == Task.FromResult(brand));
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<Brand, BrandDto>(brand) == new BrandDto() {
            Id = brandId.ToString(),
            Name = brandName,
            Image = brandImage,
            Status = brandStatus.To<int>(),
            RealmId = brandRealmId.ToString()
        });
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var brandDto = await brandService.GetBrand(new GetBrandRequestDto() { Id = brandId.ToString() }, mockServerCallContxt);

        //Assert
        Assert.Equal(brandId.ToString(), brandDto.Id);
    }

    [Fact]
    public async Task Should_List_Brand_With_PageToken_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = 0;
        var pageSize = 1;
        var sorting = string.Empty;
        var mockBrandRepo = Mock.Of<IBrandRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => brandService.ListBrand(new ListBrandRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Brand_With_PageToken_Less_Than_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = -1;
        var pageSize = 1;
        var sorting = string.Empty;
        var mockBrandRepo = Mock.Of<IBrandRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => brandService.ListBrand(new ListBrandRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Brand_With_PageSize_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = 1;
        var pageSize = 0;
        var sorting = string.Empty;
        var mockBrandRepo = Mock.Of<IBrandRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => brandService.ListBrand(new ListBrandRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Brand_With_PageSize_Less_Than_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = 1;
        var pageSize = -1;
        var sorting = string.Empty;
        var mockBrandRepo = Mock.Of<IBrandRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => brandService.ListBrand(new ListBrandRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Brand_With_PageToken_PageSize_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = 0;
        var pageSize = 0;
        var sorting = string.Empty;
        var mockBrandRepo = Mock.Of<IBrandRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => brandService.ListBrand(new ListBrandRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Brand_With_PageSize_PageToken_Less_Than_0_Throw_RPC_Invalid_Argument_Exception_Async()
    {
        //Arrange
        var pageToken = -1;
        var pageSize = -1;
        var sorting = string.Empty;
        var mockBrandRepo = Mock.Of<IBrandRepository>();
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => brandService.ListBrand(new ListBrandRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Brand_With_Invalid_Sorting_Throw_Argument_RPC_Exception_Async()
    {
        //Arrange
        var pageToken = 1;
        var pageSize = 1;
        var sorting = "names descsa";
        var brandList = new List<Brand>();
        var mockBrandRepo = new Mock<IBrandRepository>();
        mockBrandRepo.Setup(repo => repo.GetAsync(pageToken - 1, pageSize, sorting)).Throws(new ParseException("",1));
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo.Object, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo.Object, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => brandService.ListBrand(new ListBrandRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.InvalidArgument, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Brand_With_PageSize_1_PageToken_1_Throw_RPC_Not_Found_Exception_Async()
    {
        //Arrange
        var pageToken = 1;
        var pageSize = 1;
        var sorting = string.Empty;
        var brandList = new List<Brand>();
        var mockBrandRepo = Mock.Of<IBrandRepository>(repo => repo.GetAsync(pageToken-1,pageSize,sorting) ==
        Task.FromResult(brandList));
        var mockObjectMapper = Mock.Of<IObjectMapper>();
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var ex = await Assert.ThrowsAsync<RpcException>(() => brandService.ListBrand(new ListBrandRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt));

        //Assert
        Assert.Equal(StatusCode.NotFound, ex.StatusCode);
    }

    [Fact]
    public async Task Should_List_Brand_With_PageSize_1_PageToken_1_TotalCount_1_Return_Brands_Async()
    {
        //Arrange

        var brandId = Guid.NewGuid();
        var brandName = "xxx";
        var brandImage = "xxx.png";
        var brandStatus = EnumCatalogStatus.ACTIVE;
        var brandRealmId = Guid.NewGuid();
        var brand = new Brand(brandId, brandName, brandImage, brandStatus, brandRealmId);

        var pageToken = 1;
        var pageSize = 1;
        var sorting = string.Empty;
        var brandList = new List<Brand>() {brand};
        long totalCount = 1;
        var mockBrandRepo = Mock.Of<IBrandRepository>(repo => repo.GetAsync(pageToken - 1, pageSize, sorting) ==
        Task.FromResult(brandList) && repo.GetTotalAsync() == Task.FromResult(totalCount));
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<List<Brand>,
            List<BrandDto>>(brandList) == brandList.Select(brand => new BrandDto()
        {
            Id = brand.Id.ToString(),
            Name = brand.Name,
            Image = brand.Image,
            Status = brand.Status != null ? brand.Status.Value.To<int>() : 1,
            RealmId = brand.RealmId.ToString()
        }).ToList());
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var brandResponse = await brandService.ListBrand(new ListBrandRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt);

        //Assert
        Assert.True(brandResponse.Brands.Any());
        Assert.Equal(1, brandResponse.NextPageToken);
    }

    [Fact]
    public async Task Should_List_Brand_With_PageSize_1_PageToken_1_TotalCount_2_Return_Brands_Async()
    {
        //Arrange

        var brandId = Guid.NewGuid();
        var brandName = "xxx";
        var brandImage = "xxx.png";
        var brandStatus = EnumCatalogStatus.ACTIVE;
        var brandRealmId = Guid.NewGuid();
        var brand = new Brand(brandId, brandName, brandImage, brandStatus, brandRealmId);

        var pageToken = 1;
        var pageSize = 1;
        var sorting = string.Empty;
        var brandList = new List<Brand>() { brand };
        long totalCount = 2;
        var mockBrandRepo = Mock.Of<IBrandRepository>(repo => repo.GetAsync(pageToken - 1, pageSize, sorting) ==
        Task.FromResult(brandList) && repo.GetTotalAsync() == Task.FromResult(totalCount));
        var mockObjectMapper = Mock.Of<IObjectMapper>(objMap => objMap.Map<List<Brand>,
            List<BrandDto>>(brandList) == brandList.Select(brand => new BrandDto()
            {
                Id = brand.Id.ToString(),
                Name = brand.Name,
                Image = brand.Image,
                Status = brand.Status != null ? brand.Status.Value.To<int>() : 1,
                RealmId = brand.RealmId.ToString()
            }).ToList());
        var mockLocal = Mock.Of<IStringLocalizer<CatalogResource>>();
        var mockGuidGenerator = Mock.Of<Volo.Abp.Guids.IGuidGenerator>(guidGen => guidGen.Create() == Guid.NewGuid());
        var mockBrandManager = new BrandManager(mockBrandRepo, mockGuidGenerator);
        var mockServerCallContxt = Mock.Of<ServerCallContext>();
        var brandService = new Services.BrandService(mockBrandRepo, mockObjectMapper, mockLocal, mockBrandManager);

        //Act
        var brandResponse = await brandService.ListBrand(new ListBrandRequestDto()
        {
            PageToken = pageToken,
            PageSize = pageSize,
            Sorting = sorting
        }, mockServerCallContxt);

        //Assert
        Assert.True(brandResponse.Brands.Any());
        Assert.Equal(2, brandResponse.NextPageToken);
    }
}