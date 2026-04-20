FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["NuGet.Config", "./"]
COPY ["src/WebApiDemo.Domain/WebApiDemo.Domain.csproj", "src/WebApiDemo.Domain/"]
COPY ["src/WebApiDemo.Application/WebApiDemo.Application.csproj", "src/WebApiDemo.Application/"]
COPY ["src/WebApiDemo.Infrastructure/WebApiDemo.Infrastructure.csproj", "src/WebApiDemo.Infrastructure/"]
COPY ["src/WebApiDemo.WebAPI/WebApiDemo.WebAPI.csproj", "src/WebApiDemo.WebAPI/"]
RUN dotnet restore "src/WebApiDemo.WebAPI/WebApiDemo.WebAPI.csproj" --configfile NuGet.Config

COPY . .
RUN dotnet publish "src/WebApiDemo.WebAPI/WebApiDemo.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .
COPY --from=build /src/database.db ./database.db

ENTRYPOINT ["dotnet", "WebApiDemo.WebAPI.dll"]
