# Build Stage
#FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
#FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0 AS build
FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0.303 AS build

# Changed from /api/src for better convention
WORKDIR /src 

# Copy csproj files and restore dependencies
COPY Stockly-Api.slnf .
COPY Stockly-Api.sln .
COPY *.props .
COPY ["Stockly.Api/Stockly.Api.csproj", "Stockly.Api/"]
COPY ["Stockly.Core/Stockly.Core.csproj", "Stockly.Core/"]
COPY ["Infrastructure/Stockly.Api.Contracts/Stockly.Api.Contracts.csproj", "Infrastructure/Stockly.Api.Contracts/"]
COPY ["Infrastructure/Stockly.Adapter.FirebaseDb/Stockly.Adapter.FirebaseDb.csproj", "Infrastructure/Stockly.Adapter.FirebaseDb/"]

RUN dotnet restore Stockly-Api.slnf

# Copy the rest of the source code
COPY . .

# Build and publish the application
# Changed from /api/src for better convention
WORKDIR /src/Stockly.Api
RUN dotnet build "Stockly.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stockly.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

## Final Stage - Use a distroless base image
#FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS final
#FROM mcr.microsoft.com/dotnet/nightly/aspnet:9.0 AS final
FROM mcr.microsoft.com/dotnet/nightly/aspnet:9.0.7 AS final
WORKDIR /app

# Copy the published application from the build stage
COPY --from=publish /app/publish .

# Add a non-root user and set permissions
USER 1000

# Expose the ports
EXPOSE 5103
EXPOSE 5200

# Set environment variables
#ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Stockly.Api.dll"]