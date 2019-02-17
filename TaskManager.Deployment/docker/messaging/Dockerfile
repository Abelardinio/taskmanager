FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["TaskManager.MessagingService/TaskManager.MessagingService.csproj", "TaskManager.MessagingService/"]
COPY ["TaskManager.Core/TaskManager.Core.csproj", "TaskManager.Core/"]
COPY ["TaskManager.Common.AspNetCore/TaskManager.Common.AspNetCore.csproj", "TaskManager.Common.AspNetCore/"]
COPY ["TaskManager.ServiceBus/TaskManager.ServiceBus.csproj", "TaskManager.ServiceBus/"]
COPY ["TaskManager.Common.Resources/TaskManager.Common.Resources.csproj", "askManager.Common.Resources/"]
RUN dotnet restore "TaskManager.MessagingService/TaskManager.MessagingService.csproj"
COPY . .
WORKDIR "/src/TaskManager.MessagingService"
RUN dotnet build "TaskManager.MessagingService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TaskManager.MessagingService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY ["TaskManager.Deployment/docker/messaging/appsettings.prod.json", "."]
ENTRYPOINT ["dotnet", "TaskManager.MessagingService.dll"]