# Etapa base: imagen para tiempo de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa build: para compilar y publicar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY ["prctica3.csproj", "./"]
RUN dotnet restore "prctica3.csproj"

# Copiar todo el proyecto y compilar en modo Release
COPY . .
RUN dotnet publish "prctica3.csproj" -c Release -o /app/publish --no-restore

# Etapa final: imagen lista para producción
FROM base AS final
WORKDIR /app

# Copiar los archivos publicados
COPY --from=build /app/publish .

# Establecer puerto de escucha (Render usa $PORT, por defecto 8080)
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Iniciar la aplicación
ENTRYPOINT ["dotnet", "prctica3.dll"]
