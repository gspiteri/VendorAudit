#Get the working dir so we can get back there at the end #
$workingDir = split-path -parent $MyInvocation.MyCommand.Definition

#Get msbuild location
$msb = "$env:windir\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
if(!(Test-Path $msb))
{
	$msb = "$env:windir\Microsoft.NET\Framework\v3.5\msbuild.exe"
	if(!(Test-Path $msb))
	{
	    Write-Error "MSBuild not found!"
	    exit 1
	}
}

#cd .\Services\CrownConnect.NotificationManagement\

# Get the absolute path so life is a little easier.
$rootDir = (Get-Location).Path
# /target:Clean /target:Build  /t:Clean,Build  /p:VisualStudioVersion=15.0

& $msb $rootDir\VendorAuditTracker.sln /target:Clean /target:Build /p:VisualStudioVersion=15.0
#echo $msb
#echo $rootDir
# & $msb $rootDir\CrownConnect.NotificationManagement.Api\CrownConnect.NotificationManagement.Api.csproj /t:Clean,Build /p:VisualStudioVersion=12.0

# Its easier to run the owin host when we're in the bin directory.
cd $rootDir\VendorAuditTracker.Webai\bin
[xml] $csprojFile = Get-Content -Path $rootDir\VendorAuditTracker.Webai\VendorAuditTracker.Webapi.csproj
& $rootDir\packages\OwinHost.3.0.1\tools\OwinHost.exe -u $csprojFile.Project.ProjectExtensions.VisualStudio.FlavorProperties.WebProjectProperties.servers.server.url

cd $workingDir