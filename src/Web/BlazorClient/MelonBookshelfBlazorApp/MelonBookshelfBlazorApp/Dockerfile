# Use the .NET SDK for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore dependencies
COPY ["MelonBookshelfBlazorApp.csproj", "./"]
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Start the app
ENTRYPOINT ["dotnet", "MelonBookshelfBlazorApp.dll"]
