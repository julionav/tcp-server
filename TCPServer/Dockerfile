FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
EXPOSE 8888

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TCPServer/TCPServer.csproj", "TCPServer/"]
RUN dotnet restore "TCPServer/TCPServer.csproj"
COPY . .
WORKDIR "/src/TCPServer"
RUN dotnet build "TCPServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TCPServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TCPServer.dll"]
