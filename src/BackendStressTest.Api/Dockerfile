#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://*:9999
RUN apt-get update && apt-get install curl -y

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore .
COPY . .
WORKDIR "/src/BackendStressTest.Api"
RUN dotnet build . -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish . -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendStressTest.Api.dll"]