#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["eCommerce.Api.Orders.Tests/eCommerce.Api.Orders.Tests.csproj", "eCommerce.Api.Orders.Tests/"]
COPY ["eCommerce.Api.Orders/eCommerce.Api.Orders.csproj", "eCommerce.Api.Orders/"]
RUN dotnet restore "eCommerce.Api.Orders.Tests/eCommerce.Api.Orders.Tests.csproj"
COPY . .
WORKDIR "/src/eCommerce.Api.Orders.Tests"
RUN dotnet build "eCommerce.Api.Orders.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eCommerce.Api.Orders.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Api.Orders.Tests.dll"]
