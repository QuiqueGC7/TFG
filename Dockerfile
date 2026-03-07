# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY restaurant_api.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish restaurant_api.csproj -c Release -o /app/publish

# Etapa de runtime — imagen limpia, sin sqlcmd
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "restaurant_api.dll"]