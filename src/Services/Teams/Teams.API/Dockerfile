#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Teams/Teams.API/Teams.API.csproj", "Services/Teams/Teams.API/"]
COPY ["Services/Teams/Teams.Infrastructure/Teams.Infrastructure.csproj", "Services/Teams/Teams.Infrastructure/"]
COPY ["Services/Teams/Teams.Application/Teams.Application.csproj", "Services/Teams/Teams.Application/"]
COPY ["Services/Teams/Teams.Domain/Teams.Domain.csproj", "Services/Teams/Teams.Domain/"]
RUN dotnet restore "Services/Teams/Teams.API/Teams.API.csproj"
COPY . .
WORKDIR "/src/Services/Teams/Teams.API"
RUN dotnet build "Teams.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Teams.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Teams.API.dll"]