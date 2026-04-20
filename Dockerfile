FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["NuGet.Config", "./"]
COPY ["Directory.Packages.props", "./"]
COPY ["src/WebApiDemo.Domain/WebApiDemo.Domain.csproj", "src/WebApiDemo.Domain/"]
COPY ["src/WebApiDemo.Domain/packages.lock.json", "src/WebApiDemo.Domain/"]
COPY ["src/WebApiDemo.Application/WebApiDemo.Application.csproj", "src/WebApiDemo.Application/"]
COPY ["src/WebApiDemo.Application/packages.lock.json", "src/WebApiDemo.Application/"]
COPY ["src/WebApiDemo.Infrastructure/WebApiDemo.Infrastructure.csproj", "src/WebApiDemo.Infrastructure/"]
COPY ["src/WebApiDemo.Infrastructure/packages.lock.json", "src/WebApiDemo.Infrastructure/"]
COPY ["src/WebApiDemo.WebAPI/WebApiDemo.WebAPI.csproj", "src/WebApiDemo.WebAPI/"]
COPY ["src/WebApiDemo.WebAPI/packages.lock.json", "src/WebApiDemo.WebAPI/"]
RUN dotnet restore "src/WebApiDemo.WebAPI/WebApiDemo.WebAPI.csproj" --configfile NuGet.Config --locked-mode

COPY . .
RUN dotnet publish "src/WebApiDemo.WebAPI/WebApiDemo.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WebApiDemo.WebAPI.dll"]
