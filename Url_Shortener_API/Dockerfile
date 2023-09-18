#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Url_Shortener_API/Url_Shortener_API.csproj", "Url_Shortener_API/"]
RUN dotnet restore "Url_Shortener_API/Url_Shortener_API.csproj"
COPY . .
WORKDIR "/src/Url_Shortener_API"
RUN dotnet build "Url_Shortener_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Url_Shortener_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Url_Shortener_API.dll"]