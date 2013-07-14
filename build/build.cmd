@echo off
set task=default
set version=1.0.0.0

if not '%1' == '' set task=%1
if not '%2' == '' set version=%2

echo Restoring nuget packages
nuget install ..\src\.nuget\packages.config -OutputDirectory ..\src\packages

echo Executing psake script Build.ps1 with task "%task%" and version "%version%"
powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command "& { Import-Module '..\src\packages\psake.4.2.0.1\tools\psake.psm1'; Invoke-psake '.\Build.ps1' -task %task% -parameters @{version='%version%'} ; if ($lastexitcode) { write-host "ERROR: $lastexitcode" -fore RED; exit $lastexitcode }; if ($psake.build_success -eq $false) { exit 1 } else { exit 0 } }"