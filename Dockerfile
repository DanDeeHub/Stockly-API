# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview-cbl-mariner2.0-amd64 AS build
WORKDIR /api/src

# Copy csproj files and restore dependencies
COPY Stockly-Api.slnf .
COPY Stockly-Api.sln .
COPY *.props .
COPY ["Src/Stockly.Api/Stockly.Api.csproj", "Src/Stockly.Api/"]
COPY ["Src/Stockly.Core/Stockly.Core.csproj", "Src/Stockly.Core/"]
COPY ["Src/Infrastructure/Stockly.Api.Contracts/Stockly.Api.Contracts.csproj", "Src/Infrastructure/Stockly.Api.Contracts/"]
COPY ["Src/Infrastructure/Stockly.Adapter.FirebaseDb/Stockly.Adapter.FirebaseDb.csproj", "Src/Infrastructure/Stockly.Adapter.FirebaseDb/"]

RUN dotnet restore Stockly-Api.slnf

# Copy the rest of the source code
COPY . .

# Build and publish the application
WORKDIR /api/src/Src/Stockly.Api
RUN dotnet build "Stockly.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stockly.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

## Final Stage - Use a distroless base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview-cbl-mariner2.0-distroless-amd64 AS final
WORKDIR /api/src

# Copy the published application from the build stage
COPY --from=publish /app/publish .

# Add a non-root user and set permissions
USER 1000

# Expose the ports
EXPOSE 5103
EXPOSE 5104

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5103;https://+:5104

ENTRYPOINT ["dotnet", "Stockly.Api.dll"]