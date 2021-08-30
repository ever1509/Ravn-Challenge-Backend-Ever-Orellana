#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
EXPOSE 80
EXPOSE 443
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Movies.API/Movies.API.csproj", "src/Movies.API/"]
COPY ["src/Movies.Application/Movies.Application.csproj", "src/Movies.Application/"]
COPY ["src/Movies.Domain/Movies.Domain.csproj", "src/Movies.Domain/"]
COPY ["src/Movies.Infrastructure/Movies.Infrastructure.csproj", "src/Movies.Infrastructure/"]
RUN dotnet restore "src/Movies.API/Movies.API.csproj"
COPY . .
WORKDIR "/src/src/Movies.API"
RUN dotnet build "Movies.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Movies.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Movies.API.dll"]