FROM mcr.microsoft.com/dotnet/runtime:6.0-jammy AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-jammy AS build
WORKDIR /src

COPY ["src/services/NewLake.GrpcGenerator/", "/src/NewLake.GrpcGenerator/"]

RUN dotnet restore "NewLake.GrpcGenerator/NewLake.GrpcGenerator.csproj"

WORKDIR /src
COPY . .

RUN dotnet build "NewLake.GrpcGenerator/NewLake.GrpcGenerator.csproj"

FROM build AS publish
RUN dotnet publish "NewLake.GrpcGenerator/NewLake.GrpcGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewLake.GrpcGenerator.dll"]