# Use Microsoft's .NET 8.0 SDK image

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set up the app environment

WORKDIR /Tiketix.API

# Copy everything and build

COPY . ./

RUN dotnet publish -c Release -o out

# Runtime image

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /Tiketix.API/Tiketix

COPY --from=build-env /Tiketix.API/out .

# Start the app

ENTRYPOINT ["dotnet", "Tiketix.dll"]