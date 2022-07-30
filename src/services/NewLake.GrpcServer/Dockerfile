FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["src/services/NewLake.GrpcServer/", "/src/NewLake.GrpcServer/"]
COPY ["src/services/NewLake.Core/", "/src/NewLake.Core/"]

RUN dotnet restore "NewLake.GrpcServer/NewLake.GrpcServer.csproj"

WORKDIR /src
COPY . .

RUN dotnet build "NewLake.GrpcServer/NewLake.GrpcServer.csproj"

FROM build AS publish
RUN dotnet publish "NewLake.GrpcServer/NewLake.GrpcServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewLake.GrpcServer.dll"]