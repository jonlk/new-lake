FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["src/services/NewLake.Queue.Listener/", "/src/NewLake.Queue.Listener/"]

RUN dotnet restore "NewLake.Queue.Listener/NewLake.Queue.Listener.csproj"

WORKDIR /src
COPY . .

RUN dotnet build "NewLake.Queue.Listener/NewLake.Queue.Listener.csproj"

FROM build AS publish
RUN dotnet publish "NewLake.Queue.Listener/NewLake.Queue.Listener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewLake.Queue.Listener.dll"]