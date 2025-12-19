# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution/project files
COPY ["SafeBoda.Api/SafeBoda.Api.csproj", "SafeBoda.Api/"]
COPY ["SafeBoda.Application/SafeBoda.Application.csproj", "SafeBoda.Application/"]
COPY ["SafeBoda.Core/SafeBoda.Core.csproj", "SafeBoda.Core/"]
COPY ["SafeBoda.Infrastructure/SafeBoda.Infrastructure.csproj", "SafeBoda.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "SafeBoda.Api/SafeBoda.Api.csproj"

# Copy everything else
COPY . .

# Build and publish
WORKDIR "/src/SafeBoda.Api"
RUN dotnet build "SafeBoda.Api.csproj" -c Release -o /app/build
RUN dotnet publish "SafeBoda.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80

# Install curl for health check (optional, but useful)
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copy from build stage
COPY --from=build /app/publish .

# Set entry point
ENTRYPOINT ["dotnet", "SafeBoda.Api.dll"]