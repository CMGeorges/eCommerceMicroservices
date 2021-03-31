#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["eCommerce.Api.Products.Tests/eCommerce.Api.Products.Tests.csproj", "eCommerce.Api.Products.Tests/"]
COPY ["eCommerce.Api.Products/eCommerce.Api.Products.csproj", "eCommerce.Api.Products/"]
RUN dotnet restore "eCommerce.Api.Products.Tests/eCommerce.Api.Products.Tests.csproj"
COPY . .
WORKDIR "/src/eCommerce.Api.Products.Tests"
RUN dotnet build "eCommerce.Api.Products.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eCommerce.Api.Products.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Api.Products.Tests.dll"]
