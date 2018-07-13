Function RestoreAllPackages ($BaseDirectory)
	{
		Write-Host "Starting Package Restore - This may take a few minutes ..."
		$PACKAGECONFIGS = Get-ChildItem -Recurse -Force $BaseDirectory -ErrorAction SilentlyContinue | 
			Where-Object { ($_.PSIsContainer -eq $false) -and  ( $_.Name -eq "packages.config")}
		ForEach($PACKAGECONFIG in $PACKAGECONFIGS)
			{
				Write-Host $PACKAGECONFIG.FullName
				$NugetRestore = $NUGETLOCATION + " install " + " '" + $PACKAGECONFIG.FullName + "' -OutputDirectory '" + $PACKAGECONFIG.Directory.parent.FullName + "\packages'"
				Write-Host $NugetRestore
				Invoke-Expression $NugetRestore
			}
	}
	
Function BuildSolution ($BaseDirectory)
{
	
}

Function Get-ScriptDirectory
{
    $Invocation = (Get-Variable MyInvocation -Scope 1).Value;
    if($Invocation.PSScriptRoot)
    {
        $Invocation.PSScriptRoot;
    }
    Elseif($Invocation.MyCommand.Path)
    {
        Split-Path $Invocation.MyCommand.Path
    }
    else
    {
        $Invocation.InvocationName.Substring(0,$Invocation.InvocationName.LastIndexOf("\"));
    }
}

$scriptPath = (Get-ScriptDirectory);

$SOLUTIONROOT = (get-item $scriptPath ).parent.FullName
#This is where your NuGet.exe is located
$NUGETLOCATION = "$($scriptPath)\NuGet.exe"
$msbuild = "$((Get-ItemProperty hklm:\software\Microsoft\MSBuild\ToolsVersions\4.0).MSBuildToolsPath)MSBuild.exe"

RestoreAllPackages $SOLUTIONROOT
Write-Host "Building $($SOLUTIONROOT)" -foregroundcolor green
Invoke-Expression "$($msbuild) $($SOLUTIONROOT)\TaskManager.sln /t:Build /m  /p:VisualStudioVersion=14.0"

#Write-Host "$($SOLUTIONROOT)\TaskManager.sln"
$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")