SET PROFILE_NAME=%1
SET CLIENT_OUTPUT_PATH=%2

cd ..\TaskManager.WebApi
dotnet build /p:DeployOnBuild=true /p:PublishProfile=%PROFILE_NAME%

cd ..\TaskManager.MessagingService
dotnet build /p:DeployOnBuild=true /p:PublishProfile=%PROFILE_NAME%

cd ..\TaskManager.Client
call npm install
ng build --prod --extract-css=false --output-path=%CLIENT_OUTPUT_PATH%