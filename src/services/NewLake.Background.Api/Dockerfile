FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

COPY ["src/services/NewLake.Background.Api/", "/src/NewLake.Background.Api/"]


RUN dotnet restore "NewLake.Background.Api/NewLake.Background.Api.csproj"

WORKDIR /src
COPY . .

RUN dotnet build "NewLake.Background.Api/NewLake.Background.Api.csproj"

FROM build AS publish
RUN dotnet publish "NewLake.Background.Api/NewLake.Background.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewLake.Background.Api.dll"]