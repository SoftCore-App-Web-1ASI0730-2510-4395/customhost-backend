# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["customhost.platform.API.sln", "."]
COPY ["customhost.platform.API/customhost.platform.API.csproj", "customhost.platform.API/"]
RUN dotnet restore "customhost.platform.API.sln"
COPY . .
WORKDIR "/src/customhost.platform.API"
RUN dotnet build "customhost.platform.API.csproj" -c Release -o /app/build
RUN dotnet publish "customhost.platform.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
COPY --from=build /app/publish .

# Configuración de puerto
ENV PORT=8080  
ENV ASPNETCORE_URLS=http://*:$PORT
EXPOSE $PORT 

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENTRYPOINT ["dotnet", "customhost.platform.API.dll"]