# Verwende das offizielle .NET Core SDK-Image als Basis
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

# Setze das Arbeitsverzeichnis im Container
WORKDIR /app

# Kopiere die Projektdateien in das Arbeitsverzeichnis
COPY . .

# Restauriere die NuGet-Pakete und führe den Build durch
RUN dotnet restore 
RUN dotnet publish -c Release -o /app/published-app Spg.Payment.Payment.csproj

# Preset Image for Dotnet REST Api for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime

# Setze das Arbeitsverzeichnis im Container
WORKDIR /app

# Kopiere die Veröffentlichung aus dem vorherigen Build-Stage
COPY --from=build /app/published-app /app

# Setze den Startbefehl für die Anwendung
ENTRYPOINT ["dotnet", "Spg.Payment.Payment.dll"]
