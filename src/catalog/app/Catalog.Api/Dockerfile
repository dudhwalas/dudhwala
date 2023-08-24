#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5086

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["catalog/app/Catalog.Api/Catalog.Api.csproj", "catalog/app/Catalog.Api/"]
COPY ["catalog/app/Catalog.Application/Catalog.Application.csproj", "catalog/app/Catalog.Application/"]
COPY ["catalog/app/Catalog.Database/Catalog.Database.csproj", "catalog/app/Catalog.Database/"]
COPY ["catalog/app/Catalog.Domain/Catalog.Domain.csproj", "catalog/app/Catalog.Domain/"]
COPY ["catalog/app/Catalog.Domain.Shared/Catalog.Domain.Shared.csproj", "catalog/app/Catalog.Domain.Shared/"]

RUN dotnet restore "catalog/app/Catalog.Api/Catalog.Api.csproj"
COPY . .
WORKDIR "/src/catalog/app/Catalog.Api"
RUN dotnet build "Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]