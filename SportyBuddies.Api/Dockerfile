FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SportyBuddies.Api/SportyBuddies.Api.csproj", "SportyBuddies.Api/"]
COPY ["SportyBuddies.Application/SportyBuddies.Application.csproj", "SportyBuddies.Application/"]
COPY ["SportyBuddies.Domain/SportyBuddies.Domain.csproj", "SportyBuddies.Domain/"]
COPY ["SportyBuddies.Identity/SportyBuddies.Identity.csproj", "SportyBuddies.Identity/"]
COPY ["SportyBuddies.Infrastructure/SportyBuddies.Infrastructure.csproj", "SportyBuddies.Infrastructure/"]
RUN dotnet restore "SportyBuddies.Api/SportyBuddies.Api.csproj"
COPY . .
WORKDIR "/src/SportyBuddies.Api"
RUN dotnet build "SportyBuddies.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SportyBuddies.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportyBuddies.Api.dll"]
