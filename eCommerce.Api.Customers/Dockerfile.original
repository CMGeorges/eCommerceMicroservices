#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["eCommerce.Api.Customers/eCommerce.Api.Customers.csproj", "eCommerce.Api.Customers/"]
RUN dotnet restore "eCommerce.Api.Customers/eCommerce.Api.Customers.csproj"
COPY . .
WORKDIR "/src/eCommerce.Api.Customers"
RUN dotnet build "eCommerce.Api.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eCommerce.Api.Customers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Api.Customers.dll"]