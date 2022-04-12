#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5002/tcp
ENV ASPNETCORE_URLS http://*:5002

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HeyUrl.API/HeyUrl.API.csproj", "HeyUrl.API/"]
RUN dotnet restore "HeyUrl.API/HeyUrl.API.csproj"
COPY . .
WORKDIR "/src/HeyUrl.API"
RUN dotnet build "HeyUrl.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HeyUrl.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY /HeyUrl.API/wait-for-it.sh wait-for-it.sh
RUN chmod +x wait-for-it.sh