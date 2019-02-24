sqlcmd -b -v DATABASE_NAME="TaskManager" USER_NAME="TaskManagerUser" PASSWORD="nNx4vp3R3vsh8$d2" -i "CreateDatabaseAndUser.sql"

powershell -executionPolicy bypass -file "iis.ps1"

echo.  >> %WINDIR%\System32\Drivers\Etc\Hosts
echo 127.0.0.1 taskmanager >> %WINDIR%\System32\Drivers\Etc\Hosts
cd ..\TaskManager.Deployment

call build FolderProfile