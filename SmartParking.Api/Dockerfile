FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files for better caching
COPY SmartParking.sln ./
COPY SmartParking.Api/SmartParking.Api.csproj SmartParking.Api/
COPY SmartParking.Domain/SmartParking.Domain.csproj SmartParking.Domain/
COPY SmartParking.Application/SmartParking.Application.csproj SmartParking.Application/
COPY SmartParking.Infrastructure/SmartParking.Infrastructure.csproj SmartParking.Infrastructure/

RUN dotnet restore SmartParking.Api/SmartParking.Api.csproj

# Copy the rest and publish
COPY SmartParking.Api/ SmartParking.Api/
COPY SmartParking.Domain/ SmartParking.Domain/
COPY SmartParking.Application/ SmartParking.Application/
COPY SmartParking.Infrastructure/ SmartParking.Infrastructure/

WORKDIR /src/SmartParking.Api
RUN dotnet publish -c Release -o /app/publish

# ---------- runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build /app/publish .

# Run as a non-root user
USER $APP_UID

ENTRYPOINT ["dotnet", "SmartParking.Api.dll"]