FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MinecraftSkins.Backend.sln", "./"]
COPY ["MinecraftSkins.Api/MinecraftSkins.Api.csproj", "MinecraftSkins.Api/"]
COPY ["MinecraftSkins.Domain/MinecraftSkins.Domain.csproj", "MinecraftSkins.Domain/"]
COPY ["MinecraftSkins.Infrastructure/MinecraftSkins.Infrastructure.csproj", "MinecraftSkins.Infrastructure/"]
COPY ["MinecraftSkins.Services/MinecraftSkins.Services.csproj", "MinecraftSkins.Services/"]

RUN dotnet restore

COPY . .
WORKDIR "/src/MinecraftSkins.Api"
RUN dotnet publish "MinecraftSkins.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "MinecraftSkins.Api.dll"]