FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia a solução e restaura os projetos
COPY BikeRoubada.sln .
COPY src/ ./src/
RUN dotnet restore src/BikeRoubada.Api/BikeRoubada.Api.csproj

# Publica a API
RUN dotnet publish src/BikeRoubada.Api/BikeRoubada.Api.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Força a porta que o Railway espera
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "BikeRoubada.Api.dll"]