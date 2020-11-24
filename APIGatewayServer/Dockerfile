#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WebApplication1/Microservice.gateway.api.csproj", "WebApplication1/"]
RUN dotnet restore "WebApplication1/Microservice.gateway.api.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "Microservice.gateway.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.gateway.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.gateway.api.dll"]