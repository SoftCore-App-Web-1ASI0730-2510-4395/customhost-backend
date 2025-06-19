# Build stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Runtime stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUIL_CONFIGURATION=Release
WORKDIR /src
COPY ["customhost_backend/customhost_backend.csproj", "customhost_backend/"]
COPY .  .
WORKDIR "/src"
RUN dotnet buil "customhost_backend/customhost_backend.csproj" -c $BUIL_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUIL_CONFIGURATION=Release
RUN dotnet publish "customhost_backend/customhost_backend.csproj" -c $BUIL_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "customhost_backend.dll"]