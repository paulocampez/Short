#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443



FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["hey-url-challenge-code-dotnet/hey-url-challenge-code-dotnet.csproj", "hey-url-challenge-code-dotnet/"]
RUN dotnet restore "hey-url-challenge-code-dotnet/hey-url-challenge-code-dotnet.csproj"

COPY wait-for-it.sh wait-for-it.sh
RUN chmod +x wait-for-it.sh

COPY . .
WORKDIR "/src/hey-url-challenge-code-dotnet"
RUN dotnet build "hey-url-challenge-code-dotnet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "hey-url-challenge-code-dotnet.csproj" -c Release -o /app/publish



FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hey-url-challenge-code-dotnet.dll"]