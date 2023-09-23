
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Infrastructure/EventBus/EventBus/EventBus.csproj", "Infrastructure/EventBus/EventBus/"]
COPY ["Infrastructure/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "Infrastructure/EventBus/EventBusRabbitMQ/"]
COPY ["Services/CommentManagementService/CommentManagementService.API/CommentManagementService.csproj", "Services/CommentManagementService/CommentManagementService.API/"]
COPY ["Services/Services.Common/Services.Common.csproj", "Services/Services.Common/"]
COPY "Directory.Packages.props" "Directory.Packages.props"
COPY "NuGet.config" "NuGet.config"

RUN dotnet restore Services/CommentManagementService/CommentManagementService.API/CommentManagementService.csproj -nowarn:msb3202,nu1503

COPY . .
WORKDIR "/src/Services/CommentManagementService/CommentManagementService.API"
RUN dotnet build "CommentManagementService.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "CommentManagementService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "CommentManagementService.dll"]