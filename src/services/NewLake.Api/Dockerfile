FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# FROM mcr.microsoft.com/dotnet/aspnet:6.0-jammy AS base
# WORKDIR /app

# FROM mcr.microsoft.com/dotnet/sdk:6.0-jammy AS build
# WORKDIR /src

COPY ["src/services/NewLake.Api/", "/src/NewLake.Api/"]

RUN dotnet restore "NewLake.Api/NewLake.Api.csproj"

WORKDIR /src
COPY . .

RUN dotnet build "NewLake.Api/NewLake.Api.csproj"

FROM build AS publish
RUN dotnet publish "NewLake.Api/NewLake.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewLake.Api.dll"]