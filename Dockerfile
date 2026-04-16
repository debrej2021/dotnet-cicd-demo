# ─────────────────────────────────────────────────────────────
# STAGE 1: BUILD STAGE
# Uses the full .NET SDK to compile and publish the app
# ─────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project file first (layer caching — only re-runs restore if .csproj changes)
COPY *.csproj ./
RUN dotnet restore

# Copy remaining source code
COPY . .

# Build and publish in Release mode to /app/publish
RUN dotnet publish -c Release -o /app/publish --no-restore


# ─────────────────────────────────────────────────────────────
# STAGE 2: RUNTIME STAGE
# Uses lightweight ASP.NET runtime image (NOT the full SDK)
# This keeps the final image small and production-safe
# ─────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copy only the published output from build stage
COPY --from=build /app/publish .

# Expose the default ASP.NET port
EXPOSE 8080

# Set environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

# Run the application
# Replace YourApp.dll with your actual project output DLL name
ENTRYPOINT ["dotnet", "YourApp.dll"]
