#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OMAPI/OMAPI.csproj", "OMAPI/"]
RUN dotnet restore "OMAPI/OMAPI.csproj"
COPY . .
WORKDIR "/src/OMAPI"
RUN dotnet build "OMAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OMAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/publish/publicimg
ENTRYPOINT ["dotnet", "OMAPI.dll"]