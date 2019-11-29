#FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /dotnetapp
COPY ./bin/Docker .

ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "ActioServices.Identity.dll"]
