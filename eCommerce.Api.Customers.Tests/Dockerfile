#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["eCommerce.Api.Customers.Tests/eCommerce.Api.Customers.Tests.csproj", "eCommerce.Api.Customers.Tests/"]
COPY ["eCommerce.Api.Customers/eCommerce.Api.Customers.csproj", "eCommerce.Api.Customers/"]
RUN dotnet restore "eCommerce.Api.Customers.Tests/eCommerce.Api.Customers.Tests.csproj"
COPY . .
WORKDIR "/src/eCommerce.Api.Customers.Tests"
RUN dotnet build "eCommerce.Api.Customers.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eCommerce.Api.Customers.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Api.Customers.Tests.dll"]
