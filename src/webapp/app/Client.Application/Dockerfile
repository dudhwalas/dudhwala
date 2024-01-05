#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["webapp/app/Client.Application/Client.Application.csproj", "webapp/app/Client.Application/"]

RUN dotnet restore "webapp/app/Client.Application/Client.Application.csproj"
COPY . .
WORKDIR "/src/webapp/app/Client.Application"
RUN dotnet build "Client.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Client.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY  webapp/app/Client.Application/nginx.conf /etc/nginx/nginx.conf
EXPOSE 5126