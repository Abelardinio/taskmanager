FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
COPY ["bin/.", "."]
COPY ["appsettings.prod.json", "."]
ENTRYPOINT ["dotnet", "TaskManager.HomeService.WebApi.dll"]