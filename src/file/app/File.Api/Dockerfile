#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["file/app/File.Api/File.Api.csproj", "file/app/File.Api/"]
RUN dotnet restore "file/app/File.Api/File.Api.csproj"
COPY . .
WORKDIR "/src/file/app/File.Api"
RUN dotnet build "File.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "File.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "File.Api.dll"]
