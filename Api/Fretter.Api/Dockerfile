#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001


FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Api/Fretter.Api/Fretter.Api.csproj", "Api/Fretter.Api/"]
COPY ["Domain/Fretter.Domain/Fretter.Domain.csproj", "Domain/Fretter.Domain/"]
COPY ["Infra/Fretter.Ioc/Fretter.Ioc.csproj", "Infra/Fretter.Ioc/"]
COPY ["Application/Fretter.Application/Fretter.Application.csproj", "Application/Fretter.Application/"]
COPY ["Infra/Fretter.Mensageria/Fretter.Mensageria.csproj", "Infra/Fretter.Mensageria/"]
COPY ["Infra/Fretter.Repository/Fretter.Repository.csproj", "Infra/Fretter.Repository/"]
RUN dotnet restore "Api/Fretter.Api/Fretter.Api.csproj"
COPY . .
WORKDIR "/src/Api/Fretter.Api"
RUN dotnet build "Fretter.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fretter.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV TZ America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

RUN apt-get update && apt-get install -y libgdiplus

ENTRYPOINT ["dotnet", "Fretter.Api.dll"]
