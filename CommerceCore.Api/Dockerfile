# Fase base para ejecutar el contenedor
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CommerceCore.Api/CommerceCore.Api.csproj", "CommerceCore.Api/"]
RUN dotnet restore "./CommerceCore.Api/CommerceCore.Api.csproj"
COPY . .
WORKDIR "/src/CommerceCore.Api"
RUN dotnet build "./CommerceCore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CommerceCore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final para producción
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Configuración para exponer el contenedor en la red local
ENV ASPNETCORE_URLS=http://+:8080

# Configura el punto de entrada
ENTRYPOINT ["dotnet", "CommerceCore.Api.dll"]
