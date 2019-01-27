$location = Get-Location
$clientAppPoolName = "TaskManager"
$apiAppPoolName = "TaskManagerApi"
$messagingAppPoolName = "TaskManagerMessaging"
$sitename = "taskmanager";
$apiname = "taskmanager\api";
$messagingname = "taskmanager\messaging";
$clientPath = [System.IO.Path]::GetFullPath((Join-Path $location.Path "..\TaskManager.Client\dist\task-manager-client"))
$apiPath = [System.IO.Path]::GetFullPath((Join-Path $location.Path "..\TaskManager.WebApi\bin\Debug\netcoreapp2.1"))
$messagingPath = [System.IO.Path]::GetFullPath((Join-Path $location.Path "..\TaskManager.MessagingService\bin\Debug\netcoreapp2.1"))


Import-Module WebAdministration

cd IIS:\AppPools\

if (!(Test-Path $clientAppPoolName -pathType container))
{
	$appPool = New-Item $clientAppPoolName
	$appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value ""
}

if (!(Test-Path $apiAppPoolName -pathType container))
{
	$appPool = New-Item $apiAppPoolName
	$appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value ""
}

if (!(Test-Path $messagingAppPoolName -pathType container))
{
	$appPool = New-Item $messagingAppPoolName
	$appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value ""
}

cd IIS:\Sites\

if (!(Test-Path $sitename -pathType container))
{
	$iisApp = New-Item $sitename -bindings @{protocol="http";bindingInformation=":80:" + $sitename} -physicalPath $clientPath 
	$iisApp | Set-ItemProperty -Name "applicationPool" -Value $clientAppPoolName
}

if (!(Test-Path $apiname -pathType container))
{
	$apiApp = New-Item $apiname -physicalPath $apiPath -type Application
	Set-ItemProperty IIS:\Sites\taskmanager\api -Name "applicationPool" -Value $apiAppPoolName
}

if (!(Test-Path $messagingname -pathType container))
{
	$apiApp = New-Item $messagingname -physicalPath $messagingPath -type Application
	Set-ItemProperty IIS:\Sites\taskmanager\messaging -Name "applicationPool" -Value $messagingAppPoolName
}

exit