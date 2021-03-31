#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["eCommerce.Api.Search/eCommerce.Api.Search.csproj", "eCommerce.Api.Search/"]
RUN dotnet restore "eCommerce.Api.Search/eCommerce.Api.Search.csproj"
COPY . .
WORKDIR "/src/eCommerce.Api.Search"
RUN dotnet build "eCommerce.Api.Search.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eCommerce.Api.Search.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Api.Search.dll"]