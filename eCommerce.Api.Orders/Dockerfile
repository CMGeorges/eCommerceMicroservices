#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["eCommerce.Api.Orders/eCommerce.Api.Orders.csproj", "eCommerce.Api.Orders/"]
RUN dotnet restore "eCommerce.Api.Orders/eCommerce.Api.Orders.csproj"
COPY . .
WORKDIR "/src/eCommerce.Api.Orders"
RUN dotnet build "eCommerce.Api.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eCommerce.Api.Orders.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Api.Orders.dll"]