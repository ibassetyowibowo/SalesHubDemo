FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["KSO-SalesHub/KSO-SalesHub.csproj", "KSO-SalesHub/"]
WORKDIR "/src/KSO-SalesHub"
RUN dotnet restore

COPY . .
RUN dotnet build -c $BUILD_CONFIGURATION -o /src/build /p:UseAppHost=false

FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /src/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "KSO-SalesHub.dll"]
