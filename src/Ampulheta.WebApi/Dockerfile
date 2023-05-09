#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ampulheta.WebApi/Ampulheta.WebApi.csproj", "Ampulheta.WebApi/"]
RUN dotnet restore "Ampulheta.WebApi/Ampulheta.WebApi.csproj"
COPY . .
WORKDIR "/src/Ampulheta.WebApi"
RUN dotnet build "Ampulheta.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ampulheta.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ampulheta.WebApi.dll"]