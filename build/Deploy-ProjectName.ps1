
$projectName = "__ProjectName__"
$projectSource = "$pwd\content\"
$projectDestination = "C:\inetpub\wwwroot\$projectName\"

Write-Host "Deploying to $projectDestination"

Write-Host "Make $projectDestination writable"
attrib -r "$projectDestination\*.*" /S /D
Write-Host "Copy files from $projectSource to $projectDestination"
$exclude = @()
Get-ChildItem $projectSource -Recurse -Exclude $exclude | Copy-Item -Destination { Join-Path $projectDestination $_.FullName.Substring($projectSource.length) } -Force