#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["proxy/app/yarp.proxy/yarp.proxy.csproj", "proxy/app/yarp.proxy/"]

RUN dotnet restore "proxy/app/yarp.proxy/yarp.proxy.csproj"
COPY . .
WORKDIR "/src/proxy/app/yarp.proxy"
RUN dotnet build "yarp.proxy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "yarp.proxy.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "yarp.proxy.dll"]