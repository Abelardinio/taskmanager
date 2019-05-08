# *** FUNCTION DEFINITIONS

function RemoveSite($sitename)
{
	Import-Module WebAdministration

	cd IIS:\Sites\

	if ((Test-Path $sitename -pathType container))
	{
		Remove-Item $sitename -recurse -Force
	}
}

function RemoveAppPool($appPoolName)
{
	Import-Module WebAdministration

	cd IIS:\AppPools\

	if ((Test-Path $appPoolName -pathType container))
	{
		Remove-Item $appPoolName -recurse -Force
	}
}

function CreateAppPool($appPoolName)
{
	Import-Module WebAdministration

	cd IIS:\AppPools\

	$appPool = New-Item $appPoolName
	$appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value ""
}

function CreateSite($sitename, $appPoolName, $path)
{
	Import-Module WebAdministration

	cd IIS:\Sites\

	$iisApp = New-Item $sitename -bindings @{protocol="http";bindingInformation=":80:" + $sitename} -physicalPath $path 
	$iisApp | Set-ItemProperty -Name "applicationPool" -Value $appPoolName
}

function CreateApplication($appName, $appPoolName, $path)
{
	Import-Module WebAdministration
	
	cd IIS:\Sites\

	New-Item $appName -physicalPath $path -type Application
	Set-ItemProperty ("IIS:\Sites\" + $appName) -Name "applicationPool" -Value $appPoolName
}

# *** BEGIN MAIN SCRIPT
$location = Get-Location
$clientAppPoolName = "TaskManager"
$apiAppPoolName = "TaskManagerApi"
$messagingAppPoolName = "TaskManagerMessaging"
$authAppPoolName = "TaskManagerAuth"
$homeAppPoolName = "TaskManagerHome"

$sitename = "taskmanager";
$apiname = "taskmanager\api";
$messagingname = "taskmanager\api\messaging";
$authname = "taskmanager\api\auth";
$homename = "taskmanager\api\home";

$clientPath = [System.IO.Path]::GetFullPath((Join-Path $location.Path "..\TaskManager.Client\dist\task-manager-client"))
$apiPath = [System.IO.Path]::GetFullPath((Join-Path $location.Path "..\TaskManager.WebApi\bin\Debug\netcoreapp2.2"))
$messagingPath = [System.IO.Path]::GetFullPath((Join-Path $location.Path "..\TaskManager.MessagingService\bin\Debug\netcoreapp2.2"))
$authPath = [System.IO.Path]::GetFullPath((Join-Path $location.Path "..\TaskManager.AuthService.WebApi\bin\Debug\netcoreapp2.2"))
$homePath = [System.IO.Path]::GetFullPath((Join-Path $location.Path "..\TaskManager.HomeService.WebApi\bin\Debug\netcoreapp2.2"))

RemoveSite $sitename

RemoveAppPool $clientAppPoolName
RemoveAppPool $apiAppPoolName
RemoveAppPool $messagingAppPoolName
RemoveAppPool $authAppPoolName
RemoveAppPool $homeAppPoolName

CreateAppPool $clientAppPoolName
CreateAppPool $apiAppPoolName
CreateAppPool $messagingAppPoolName
CreateAppPool $authAppPoolName
CreateAppPool $homeAppPoolName

CreateSite $sitename $clientAppPoolName $clientPath

CreateApplication $apiname $apiAppPoolName $apiPath
CreateApplication $messagingname $messagingAppPoolName $messagingPath
CreateApplication $authname $authAppPoolName $authPath
CreateApplication $homename $homeAppPoolName $homePath

exit