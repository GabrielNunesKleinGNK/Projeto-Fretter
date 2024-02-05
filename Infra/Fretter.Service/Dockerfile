#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

RUN aspnetcore_version=3.1.21 \
    && curl -SL --output aspnetcore.tar.gz https://dotnetcli.azureedge.net/dotnet/aspnetcore/Runtime/$aspnetcore_version/aspnetcore-runtime-$aspnetcore_version-linux-x64.tar.gz \
    && aspnetcore_sha512='f59252166dbfe11a78373226222d6a34484b9132e24283222aea8a950a5e9657da2e4d4e9ff8cbcc2fd7c7705e13bf42a31232a6012d1e247efc718e3d8e2df1' \
    && echo "$aspnetcore_sha512  aspnetcore.tar.gz" | sha512sum -c - \
    && tar -ozxf aspnetcore.tar.gz -C /usr/share/dotnet ./shared/Microsoft.AspNetCore.App \
    && rm aspnetcore.tar.gz

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Infra/Fretter.Service/Fretter.Service.csproj", "Infra/Fretter.Service/"]
COPY ["Application/Fretter.Application/Fretter.Application.csproj", "Application/Fretter.Application/"]
COPY ["Domain/Fretter.Domain/Fretter.Domain.csproj", "Domain/Fretter.Domain/"]
COPY ["Infra/Fretter.Ioc/Fretter.Ioc.csproj", "Infra/Fretter.Ioc/"]
COPY ["Infra/Fretter.Mensageria/Fretter.Mensageria.csproj", "Infra/Fretter.Mensageria/"]
COPY ["Infra/Fretter.Repository/Fretter.Repository.csproj", "Infra/Fretter.Repository/"]
RUN dotnet restore "Infra/Fretter.Service/Fretter.Service.csproj"
COPY . .
WORKDIR "/src/Infra/Fretter.Service"
RUN dotnet build "Fretter.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fretter.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV TZ America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
RUN apt-get update

ENTRYPOINT ["dotnet", "Fretter.Service.dll"]
