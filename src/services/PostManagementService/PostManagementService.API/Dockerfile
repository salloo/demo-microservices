FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Infrastructure/EventBus/EventBus/EventBus.csproj", "Infrastructure/EventBus/EventBus/"]
COPY ["Infrastructure/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "Infrastructure/EventBus/EventBusRabbitMQ/"]
COPY ["Services/PostManagementService/PostManagementService.API/PostManagementService.csproj", "Services/PostManagementService/PostManagementService.API/"]
COPY ["Services/Services.Common/Services.Common.csproj", "Services/Services.Common/"]
COPY "Directory.Packages.props" "Directory.Packages.props"
COPY "NuGet.config" "NuGet.config"

RUN dotnet restore Services/PostManagementService/PostManagementService.API/PostManagementService.csproj -nowarn:msb3202,nu1503

COPY . .
WORKDIR "/src/Services/PostManagementService/PostManagementService.API"
RUN dotnet build "PostManagementService.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "PostManagementService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "PostManagementService.dll"]

