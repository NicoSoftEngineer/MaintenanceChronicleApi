# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MaintenanceChronicle.Api/MaintenanceChronicle.Api.csproj", "MaintenanceChronicle.Api/"]
COPY ["src/MaintenanceChronicle.Application.Contracts/MaintenanceChronicle.Application.Contracts.csproj", "MaintenanceChronicle.Application.Contracts/"]
COPY ["src/MaintenanceChronicle.Data/MaintenanceChronicle.Data.csproj", "MaintenanceChronicle.Data/"]
COPY ["src/MaintenanceChronicle.Utilities/MaintenanceChronicle.Utilities.csproj", "MaintenanceChronicle.Utilities/"]
COPY ["src/MaintenanceChronicle.Application/MaintenanceChronicle.Application.csproj", "MaintenanceChronicle.Application/"]
COPY ["src/MaintenanceChronicle.BackgroundServices/MaintenanceChronicle.BackgroundServices.csproj", "MaintenanceChronicle.BackgroundServices/"]
RUN dotnet restore "./MaintenanceChronicle.Api/MaintenanceChronicle.Api.csproj"
COPY ./src ./
COPY ./src/MaintenanceChronicle.Utilities/EmailTemplates/EmailConfirmation.html ./MaintenanceChronicle.Utilities/EmailTemplates/
COPY ./src/MaintenanceChronicle.Utilities/EmailTemplates/PasswordReset.html ./MaintenanceChronicle.Utilities/EmailTemplates/
COPY ./src/MaintenanceChronicle.Utilities/EmailTemplates/UserInvitationEmail.html ./MaintenanceChronicle.Utilities/EmailTemplates/
COPY ./src/MaintenanceChronicle.Utilities/EmailTemplates/MaintenanceReminderEmail.html ./MaintenanceChronicle.Utilities/EmailTemplates/
WORKDIR "/src/MaintenanceChronicle.Api"
RUN dotnet build "./MaintenanceChronicle.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MaintenanceChronicle.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MaintenanceChronicle.Api.dll"]
