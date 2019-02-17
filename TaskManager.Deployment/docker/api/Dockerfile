FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["TaskManager.WebApi/TaskManager.WebApi.csproj", "TaskManager.WebApi/"]
COPY ["TaskManager.Core/TaskManager.Core.csproj", "TaskManager.Core/"]
COPY ["TaskManager.Common.AspNetCore/TaskManager.Common.AspNetCore.csproj", "TaskManager.Common.AspNetCore/"]
COPY ["TaskManager.Common.Resources/TaskManager.Common.Resources.csproj", "TaskManager.Common.Resources/"]
COPY ["TaskManager.ServiceBus/TaskManager.ServiceBus.csproj", "TaskManager.ServiceBus/"]
COPY ["TaskManager.Data/TaskManager.Data.csproj", "TaskManager.Data/"]
COPY ["TaskManager.DbConnection/TaskManager.DbConnection.csproj", "TaskManager.DbConnection/"]
RUN dotnet restore "TaskManager.WebApi/TaskManager.WebApi.csproj"
COPY . .
WORKDIR "/src/TaskManager.WebApi"
RUN dotnet build "TaskManager.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TaskManager.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY ["TaskManager.Deployment/docker/api/appsettings.prod.json", "."]
ENTRYPOINT ["dotnet", "TaskManager.WebApi.dll"]