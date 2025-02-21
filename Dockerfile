FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only the .csproj first to speed up builds
COPY ["KSO-SalesHub/KSO-SalesHub.csproj", "KSO-SalesHub/"]
WORKDIR "/src/KSO-SalesHub"
RUN dotnet restore

# Copy all source files
COPY ./KSO-SalesHub/ /src/KSO-SalesHub/
WORKDIR "/src/KSO-SalesHub"
RUN dotnet build -c $BUILD_CONFIGURATION -o /src/build /p:UseAppHost=false

FROM build AS publish
WORKDIR "/src/KSO-SalesHub"
RUN dotnet publish KSO-SalesHub.csproj -c $BUILD_CONFIGURATION -o /src/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "KSO-SalesHub.dll"]
