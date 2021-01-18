param( [string]$file );

$jsonObj =(Get-Content $file) | ConvertFrom-Json
$jsonV = $jsonObj.Data.AppVersion
$versionString = $jsonV
$version = [Version]$versionString
$newVersionString = (New-Object -TypeName 'System.Version' -ArgumentList @($version.Major, $version.Minor, $version.Build, ($version.Revision+1))).ToString()
$jsonObj.Data.AppVersion = $newVersionString
$jsonObj | ConvertTo-Json -depth 100 | Out-File $file