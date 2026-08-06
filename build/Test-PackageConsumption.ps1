<#
.SYNOPSIS
    Consumes the NuGet package produced by `dotnet pack`, the way a user would.

.DESCRIPTION
    Validate-Package.ps1 reads the archive; this script uses it. It creates a throwaway project
    outside the repository, restores the packed .nupkg from a local feed into a private package cache
    (so that no copy from nuget.org and no previously cached copy of the same version can be picked
    instead), compiles the public examples of the `samples` folder against it for net472 and net10.0,
    and runs the resulting program on Windows.

    It then checks what the restore actually resolved: the net472 consumer must get the lib/net462
    assets and the net10.0 consumer the lib/netstandard2.0 ones, XML documentation included, and the
    hidlibrary dependency must have flowed through to the output folder.

    That covers what reading the archive cannot: a dependency that does not resolve, an asset that
    does not flow to the consumer, and an example of the READMEs that no longer compiles.

.PARAMETER ArtifactsDirectory
    The directory containing the .nupkg file to consume.

.PARAMETER WorkingDirectory
    Where to create the throwaway project. Defaults to a new directory in the temporary folder of the
    machine, deliberately outside the repository so that Directory.Build.props does not apply.

.PARAMETER KeepWorkingDirectory
    Keep the throwaway project instead of deleting it, to inspect a failure.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [string] $ArtifactsDirectory = 'artifacts',

    [Parameter(Mandatory = $false)]
    [string] $WorkingDirectory,

    [Parameter(Mandatory = $false)]
    [switch] $KeepWorkingDirectory
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Add-Type -AssemblyName System.IO.Compression.FileSystem

$repositoryRoot   = Split-Path -Parent $PSScriptRoot
$samplesDirectory = Join-Path $repositoryRoot 'samples'
$consumerName     = 'PackageConsumer'
$consumerTargets  = @(
    @{ TargetFramework = 'net472';  ExpectedAssetFolder = 'net462' },
    @{ TargetFramework = 'net10.0'; ExpectedAssetFolder = 'netstandard2.0' }
)

$errors = New-Object System.Collections.Generic.List[string]

function Add-Error([string] $message) {
    $errors.Add($message)
    Write-Host "  [KO] $message"
}

function Assert-True([bool] $condition, [string] $success, [string] $failure) {
    if ($condition) { Write-Host "  [OK] $success" } else { Add-Error $failure }
}

function Get-PackageIdentity([string] $archivePath) {
    $archive = [System.IO.Compression.ZipFile]::OpenRead($archivePath)
    try {
        $entry = $archive.Entries | Where-Object { $_.FullName -like '*.nuspec' } | Select-Object -First 1
        if ($null -eq $entry) { throw "no .nuspec found in $archivePath" }
        $reader = New-Object System.IO.StreamReader($entry.Open())
        try {
            $nuspec = [xml] $reader.ReadToEnd()

            return @{ Id = [string] $nuspec.package.metadata.id; Version = [string] $nuspec.package.metadata.version }
        } finally {
            $reader.Dispose()
        }
    } finally {
        $archive.Dispose()
    }
}

<#
    project.assets.json keys its targets with the long framework name for .NET Framework
    (".NETFramework,Version=v4.7.2") and with the short one for the others ("net10.0"), optionally
    followed by a runtime identifier. This brings both back to the moniker of the project file.
#>
function ConvertTo-ShortTargetFramework([string] $assetsTarget) {
    $withoutRuntime = ($assetsTarget -split '/')[0]
    if ($withoutRuntime -match '^\.NETFramework,Version=v(?<version>[\d\.]+)$') {
        return 'net' + ($Matches['version'] -replace '\.', '')
    }

    return $withoutRuntime
}

function Invoke-Dotnet([string] $description, [string[]] $dotnetArguments) {
    Write-Host ''
    Write-Host "$description : dotnet $($dotnetArguments -join ' ')"
    & dotnet @dotnetArguments
    if ($LASTEXITCODE -ne 0) { throw "$description failed (exit code $LASTEXITCODE)." }
}

if (-not (Test-Path -Path $samplesDirectory)) { throw "the samples directory '$samplesDirectory' does not exist." }

$resolvedArtifacts = (Resolve-Path -Path $ArtifactsDirectory).Path
$packages          = @(Get-ChildItem -Path $resolvedArtifacts -Filter '*.nupkg' | Where-Object { $_.Name -notlike '*.symbols.nupkg' })
if ($packages.Count -ne 1) { throw "expected exactly one .nupkg in '$resolvedArtifacts', found $($packages.Count)." }

$package  = $packages[0]
$identity = Get-PackageIdentity $package.FullName
Write-Host "Consuming $($identity.Id) $($identity.Version) from $resolvedArtifacts"

if ([string]::IsNullOrWhiteSpace($WorkingDirectory)) {
    $WorkingDirectory = Join-Path ([System.IO.Path]::GetTempPath()) "luxafor-consumer-$($identity.Version)-$([System.Guid]::NewGuid().ToString('N').Substring(0, 8))"
}
$projectDirectory = Join-Path $WorkingDirectory $consumerName
$packagesCache    = Join-Path $WorkingDirectory 'packages'
$previousPackages = $env:NUGET_PACKAGES

New-Item -Path $projectDirectory -ItemType Directory -Force | Out-Null
Write-Host "Throwaway project: $projectDirectory"

try {
    # A private cache: the package under test can only come from the local feed, never from a copy of
    # the same version already downloaded from nuget.org.
    $env:NUGET_PACKAGES = $packagesCache

    @"
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="local-artifacts" value="$resolvedArtifacts" />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
  </packageSources>
</configuration>
"@ | Set-Content -Path (Join-Path $WorkingDirectory 'NuGet.config') -Encoding utf8

    # Stops any Directory.Build.props sitting above the temporary folder from reaching the consumer.
    '<Project />' | Set-Content -Path (Join-Path $WorkingDirectory 'Directory.Build.props') -Encoding utf8
    '<Project />' | Set-Content -Path (Join-Path $WorkingDirectory 'Directory.Build.targets') -Encoding utf8

    @"
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFrameworks>$(($consumerTargets | ForEach-Object { $_.TargetFramework }) -join ';')</TargetFrameworks>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
    <!-- A consumer does not necessarily enable the implicit usings: the samples must carry their own. -->
    <ImplicitUsings>disable</ImplicitUsings>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <!-- The point is to compile against the package, not to lint the sample style. -->
    <EnableNETAnalyzers>false</EnableNETAnalyzers>
    <GenerateDocumentationFile>false</GenerateDocumentationFile>
    <CopyDocumentationFilesFromPackages>true</CopyDocumentationFilesFromPackages>
  </PropertyGroup>

  <ItemGroup>
    <!-- The exact version that was just packed, and nothing else. -->
    <PackageReference Include="$($identity.Id)" Version="[$($identity.Version)]" />
  </ItemGroup>

  <ItemGroup Condition="'`$(TargetFramework)' == 'net472'">
    <PackageReference Include="Microsoft.NETFramework.ReferenceAssemblies" Version="1.0.3" PrivateAssets="all" />
  </ItemGroup>

</Project>
"@ | Set-Content -Path (Join-Path $projectDirectory "$consumerName.csproj") -Encoding utf8

    # The examples of the READMEs and of the documentation pages, compiled against the package.
    Copy-Item -Path (Join-Path $samplesDirectory '*.cs') -Destination $projectDirectory

    @"
using System;
using System.Linq;

using Reefact.LuxaforLightingDeviceController;

namespace PackageConsumer {

    internal static class Program {

        private static void Main() {
            // Compiling the samples is the point of this project. Running it only proves that the
            // assembly and its dependency load, so nothing below drives a device: on a build agent
            // the enumeration is simply empty.
            int deviceCount = Luxafor.GetDevices().Count();
            Console.Out.WriteLine("Luxafor.GetDevices() returned " + deviceCount + " device(s).");

            LightingCommand command = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
            Console.Out.WriteLine("Built a command: " + command);
        }

    }

}
"@ | Set-Content -Path (Join-Path $projectDirectory 'Program.cs') -Encoding utf8

    Invoke-Dotnet 'Restore' @('restore', $projectDirectory)
    Invoke-Dotnet 'Build' @('build', $projectDirectory, '-c', 'Release', '--no-restore')

    Write-Host ''
    Write-Host 'Validating what the consumer resolved'

    $assets = Get-Content -Path (Join-Path $projectDirectory 'obj/project.assets.json') -Raw | ConvertFrom-Json
    foreach ($target in $consumerTargets) {
        $framework      = $target.TargetFramework
        $expectedFolder = $target.ExpectedAssetFolder
        $outputDirectory = Join-Path $projectDirectory "bin/Release/$framework"

        $library = $assets.targets.PSObject.Properties |
            Where-Object { (ConvertTo-ShortTargetFramework $_.Name) -eq $framework } |
            ForEach-Object { $_.Value.PSObject.Properties } |
            Where-Object { $_.Name -like "$($identity.Id)/*" } |
            Select-Object -First 1

        if ($null -eq $library) {
            Add-Error "$framework : $($identity.Id) is not in the resolved graph."

            continue
        }

        $compileAssets = @($library.Value.compile.PSObject.Properties.Name)
        $runtimeAssets = @($library.Value.runtime.PSObject.Properties.Name)
        $dependencies  = @($library.Value.dependencies.PSObject.Properties.Name)

        Assert-True ([bool] ($compileAssets -contains "lib/$expectedFolder/$($identity.Id).dll")) `
                    "$framework compiles against lib/$expectedFolder" `
                    "$framework should compile against lib/$expectedFolder, resolved: $($compileAssets -join ', ')"
        Assert-True ([bool] ($runtimeAssets -contains "lib/$expectedFolder/$($identity.Id).dll")) `
                    "$framework runs against lib/$expectedFolder" `
                    "$framework should run against lib/$expectedFolder, resolved: $($runtimeAssets -join ', ')"
        Assert-True ([bool] ($dependencies -contains 'HidLibrary')) `
                    "$framework carries the hidlibrary dependency" `
                    "$framework should carry the hidlibrary dependency, found: $($dependencies -join ', ')"

        Assert-True (Test-Path -Path (Join-Path $outputDirectory "$($identity.Id).dll")) `
                    "$framework copied $($identity.Id).dll next to the application" `
                    "$framework did not copy $($identity.Id).dll next to the application"
        Assert-True (Test-Path -Path (Join-Path $outputDirectory 'HidLibrary.dll')) `
                    "$framework copied HidLibrary.dll next to the application" `
                    "$framework did not copy HidLibrary.dll next to the application (broken dependency)"
        Assert-True (Test-Path -Path (Join-Path $outputDirectory "$($identity.Id).xml")) `
                    "$framework copied the XML documentation next to the application" `
                    "$framework did not copy $($identity.Id).xml next to the application (the package ships it, IntelliSense needs it)"
    }

    Write-Host ''
    if ($IsWindows) {
        foreach ($target in $consumerTargets) {
            Invoke-Dotnet "Run ($($target.TargetFramework))" @('run', '--project', $projectDirectory, '-c', 'Release', '--no-build', '-f', $target.TargetFramework)
        }
    } else {
        Write-Host 'Skipped running the consumer: the devices are enumerated through the Windows HID stack, so the executable only runs on Windows. It was compiled for every target framework above.'
    }
} finally {
    if ($null -eq $previousPackages) {
        Remove-Item Env:\NUGET_PACKAGES -ErrorAction SilentlyContinue
    } else {
        $env:NUGET_PACKAGES = $previousPackages
    }
    if ($KeepWorkingDirectory) {
        Write-Host ''
        Write-Host "Throwaway project kept: $WorkingDirectory"
    } else {
        Remove-Item -Path $WorkingDirectory -Recurse -Force -ErrorAction SilentlyContinue
    }
}

if ($errors.Count -gt 0) {
    Write-Host ''
    Write-Host "Package consumption failed with $($errors.Count) error(s):"
    $errors | ForEach-Object { Write-Host " - $_" }
    exit 1
}

Write-Host ''
Write-Host 'Package consumption succeeded.'
