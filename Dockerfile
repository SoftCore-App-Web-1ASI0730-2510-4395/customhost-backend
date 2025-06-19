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
COPY ["customhost.platform.API.csproj", "./"]
RUN dotnet restore "customhost.platform.API.csproj"
COPY .  .
WORKDIR "/src"
RUN dotnet build "customhost.platform.API.csproj" -c $BUIL_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUIL_CONFIGURATION=Release
RUN dotnet publish "customhost.platform.API.csproj" -c $BUIL_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "customhost.platform.API.dll"]