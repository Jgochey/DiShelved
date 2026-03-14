# Use the official .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DiShelved/DiShelved.csproj", "DiShelved/"]
RUN dotnet restore "DiShelved/DiShelved.csproj"
COPY . .
WORKDIR "/src/DiShelved"
RUN dotnet build "DiShelved.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiShelved.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY docker-entrypoint.sh .
RUN chmod +x docker-entrypoint.sh
ENTRYPOINT ["./docker-entrypoint.sh"]
