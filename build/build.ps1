# required parameters :
#       $buildNumber

Framework "4.0"

properties {
    # build properties - change as needed
    $baseDir = resolve-path .\..
    $sourceDir = "$baseDir\src"
    $buildDir = "$baseDir\build"
	$outputDir = "$baseDir\output"
    $testDir = "$outputDir\test-results"
    $packageDir = "$outputDir\packages"    
    $companyName = "Matthew Cochran"
    $solutionName = "MC.Selenium.DSL"
	
    $solutionConfig = "Release"

    # if not provided, default to 1.0.0.0
    if(!$version)
    {
        $version = "1.0.0.0"
    }

    # tools
    $testExecutable = "$sourceDir\packages\NUnit.Runners.2.6.2\tools\nunit-console-x86.exe"
    $nuget = "$sourceDir\packages\NuGet.CommandLine.2.6.1\tools\NuGet.exe"

    # if not provided, default to Dev
    if (!$nuGetSuffix)
    {
        $nuGetSuffix = "Dev"
    }
	
	#tests
	$unitTestProject1 = "MC.Selenium.DSL.Tests"
	$unitTestProject2 = "MC.Selenium.DSL.Runner.Model.Tests"
	
	#deploy projects
	$coreProject = "MC.Selenium.DSL"
	$runnerModelProject = "MC.Selenium.DSL.Runner.Model"
	$runnerProject = "MC.Selenium.DSL.Runner"
	

    # source locations
    $projectSourceDir = "$sourceDir\$solutionName\"

    # package locations
    $projectPackageDir = "$packageDir\$solutionName\"

    # nuspec files
    $projectNuspec = "$projectPackageDir\$solutionName.nuspec"
    $projectNuspecTitle = "$solutionName title"
    $projectNuspecDescription = "$solutionName description"

    # deploy scripts
    $projectDeployFile = "$buildDir\Deploy-$solutionName.ps1"
}

task default -depends UnitTest, PackageNuGet

# Initialize the build, delete any existing package or test folders
task Init {
    Write-Host "Deleting the package directory"
	CreateDirectory $packageDir
    DeleteFile $packageDir
	
    Write-Host "Deleting the test directory"
	CreateDirectory $testDir
    DeleteFile $testDir
	
	Write-Host "Deleting the output directory"
    DeleteFile $outputDir
	CreateDirectory $packageDir
	
	
}

# Compile the Project solution and any other solutions necessary
task Compile -depends Init {
    Write-Host "Cleaning the solution"
    exec { msbuild /t:clean /v:q /nologo /p:Configuration=$solutionConfig $sourceDir\$solutionName.sln }
    DeleteFile $error_dir
    Write-Host "Building the solution"
    exec { msbuild /t:build /v:q /nologo /p:Configuration=$solutionConfig $sourceDir\$solutionName.sln }
}

task UnitTest -depends UnitTest1, UnitTest2

# Execute unit tests
task UnitTest1 -depends Compile { 
    exec { & $testExecutable "$sourceDir\$unitTestProject1\bin\$solutionConfig\$unitTestProject1.dll" }# /output "$testDir\1.xml" }
}

task UnitTest2 -depends Compile { 
    exec { & $testExecutable "$sourceDir\$unitTestProject2\bin\$solutionConfig\$unitTestProject2.dll"}# /output "$testDir\2.xml"  }
}

# TODO
# Create a common assembly info file to be shared by all projects with the provided version number
task CommonAssemblyInfo {
    $version = "1.0.0.0"   
    CreateCommonAssemblyInfo "$version" $solutionName "$source_dir\CommonAssemblyInfo.cs"
}

# PackageNuGet creates the NuGet packages for each package needed to deploy the solution
task PackageNuGet -depends Compile, PackageCoreNuget, PackageRunnerModelNuget, PackageRunnerNuget

task PackageCoreNuget {    

    Write-Host "Create $coreProject nuget manifest"
	$tempFile = "$sourceDir\$coreProject\temp.nuspec"
	TransformNuGetManifest "$sourceDir\$coreProject\template.nuspec" $version $solutionConfig $tempFile
    Write-Host "Package $projectNuspec with base path $projectPackageDir and package dir $packageDir"
    exec { & $nuget pack $tempFile -OutputDirectory $packageDir }
	DeleteFile $tempFile
}

task PackageRunnerModelNuget {    

    Write-Host "Create $runnerModelProject nuget manifest"
	$tempFile = "$sourceDir\$runnerModelProject\temp.nuspec"
	TransformNuGetManifest "$sourceDir\$runnerModelProject\template.nuspec" $version $solutionConfig $tempFile
    Write-Host "Package $runnerModelProject with base path $projectPackageDir and package dir $packageDir"
    exec { & $nuget pack $tempFile -OutputDirectory $packageDir }
	DeleteFile $tempFile
}

task PackageRunnerNuget {    

    Write-Host "Create $runnerProject nuget manifest"
	$tempFile = "$sourceDir\$runnerProject\temp.nuspec"
	TransformNuGetManifest "$sourceDir\$runnerProject\template.nuspec" $version $solutionConfig $tempFile
    Write-Host "Package $runnerProject with base path $projectPackageDir and package dir $packageDir"
    exec { & $nuget pack $tempFile -OutputDirectory $packageDir }
	DeleteFile $tempFile
}

# Deploy the project locally
task DeployProject -depends PackageProject {
    cd $projectPackageDir
    & ".\Deploy.ps1"
    cd $baseDir
}

# ------------------------------------------------------------------------------------#
# Utility methods
# ------------------------------------------------------------------------------------#

# Copy files needed for a website, ignore source files and other unneeded files
function global:CopyWebSiteFiles($source, $destination){
    $exclude = @('*.user', '*.dtd', '*.tt', '*.cs', '*.csproj', '*.orig', '*.log')
    CopyFiles $source $destination $exclude
    DeleteDirectory "$destination\obj"
}

# copy files to a destination
# create the directory if it does not exist
function global:CopyFiles($source, $destination, $exclude = @()){    
    CreateDirectory $destination
    Get-ChildItem $source -Recurse -Exclude $exclude | Copy-Item -Destination { Join-Path $destination $_.FullName.Substring($source.length); }
}

# Create a directory
function global:CreateDirectory($directoryName)
{
    mkdir $directoryName -ErrorAction SilentlyContinue | Out-Null
}

# Delete a directory
function global:DeleteDirectory($directory_name)
{
    rd $directory_name -recurse -force -ErrorAction SilentlyContinue | Out-Null
}

# Delete a file if it exists
function global:DeleteFile($file) {
    if ($file)
    {
        Remove-Item $file -force -recurse -ErrorAction SilentlyContinue | Out-Null
    }
}

# Transform the NuGet manifest file
function global:TransformNuGetManifest($source, $version, $config, $filename)
{
	$val = Get-Content $source 
	$val = $val.Replace("||VERSION||", $version ).Replace("||CONFIG||", $config )
	$val | Out-File $filename -encoding "ASCII"
}

# Create a CommonAssemblyInfo file
function global:CreateCommonAssemblyInfo($version, $applicationName, $filename)
{
"using System;
using System.Reflection;
using System.Runtime.InteropServices;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: ComVisibleAttribute(false)]
[assembly: AssemblyVersionAttribute(""$version"")]
[assembly: AssemblyFileVersionAttribute(""$version"")]
[assembly: AssemblyCopyrightAttribute(""Copyright 2010"")]
[assembly: AssemblyProductAttribute(""$applicationName"")]
[assembly: AssemblyCompanyAttribute(""Headspring"")]
[assembly: AssemblyConfigurationAttribute(""release"")]
[assembly: AssemblyInformationalVersionAttribute(""$version"")]"  | Out-File $filename -encoding "ASCII"    
}
