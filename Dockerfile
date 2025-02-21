FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Set the environment variable
ENV ASPNETCORE_ENVIRONMENT=Release

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy project file first
COPY ["KSO-SalesHub/KSO-SalesHub.csproj", "KSO-SalesHub/"]
WORKDIR "/src/KSO-SalesHub"
RUN dotnet restore

# Copy full source code
COPY ./KSO-SalesHub/ /src/KSO-SalesHub/
WORKDIR "/src/KSO-SalesHub"

# Use explicit "Release" config and add verbose logs
RUN dotnet build KSO-SalesHub.csproj -c Release -o /src/build /p:UseAppHost=false -v:detailed

FROM build AS publish
WORKDIR "/src/KSO-SalesHub"
RUN dotnet publish KSO-SalesHub.csproj -c Release -o /src/publish /p:UseAppHost=false -v:detailed

FROM base AS final
WORKDIR /app
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "KSO-SalesHub.dll"]
