#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Players/Players.API/Players.API.csproj", "Services/Players/Players.API/"]
COPY ["Services/Players/Players.Infrastructure/Players.Infrastructure.csproj", "Services/Players/Players.Infrastructure/"]
COPY ["Services/Players/Players.Application/Players.Application.csproj", "Services/Players/Players.Application/"]
COPY ["Services/Players/Players.Domain/Players.Domain.csproj", "Services/Players/Players.Domain/"]
RUN dotnet restore "Services/Players/Players.API/Players.API.csproj"
COPY . .
WORKDIR "/src/Services/Players/Players.API"
RUN dotnet build "Players.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Players.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Players.API.dll"]