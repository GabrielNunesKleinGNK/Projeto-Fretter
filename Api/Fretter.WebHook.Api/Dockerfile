#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 6000
EXPOSE 6001

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Api/Fretter.WebHook.Api/Fretter.WebHook.Api.csproj", "Api/Fretter.WebHook.Api/"]
COPY ["Domain/Fretter.Domain/Fretter.Domain.csproj", "Domain/Fretter.Domain/"]
COPY ["Infra/Fretter.Ioc/Fretter.Ioc.csproj", "Infra/Fretter.Ioc/"]
COPY ["Application/Fretter.Application/Fretter.Application.csproj", "Application/Fretter.Application/"]
COPY ["Infra/Fretter.Mensageria/Fretter.Mensageria.csproj", "Infra/Fretter.Mensageria/"]
COPY ["Infra/Fretter.Repository/Fretter.Repository.csproj", "Infra/Fretter.Repository/"]
RUN dotnet restore "Api/Fretter.WebHook.Api/Fretter.WebHook.Api.csproj"
COPY . .
WORKDIR "/src/Api/Fretter.WebHook.Api"
RUN dotnet build "Fretter.WebHook.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fretter.WebHook.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fretter.WebHook.Api.dll"]
