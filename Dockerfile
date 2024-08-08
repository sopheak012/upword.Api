# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build the app
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy the build output to the runtime image
COPY --from=build-env /app/out .

# Set environment variables for production
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the port that the app runs on
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "upword.Api.dll"]
