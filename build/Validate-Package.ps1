<#
.SYNOPSIS
    Validates the content of the NuGet package produced by `dotnet pack`.

.DESCRIPTION
    Checks that the package ships what the project promises: both target frameworks, the XML
    documentation, the readme, the icon, the expected dependencies (and only those), and a symbol
    package containing the portable PDBs. Fails the build as soon as one of those is missing.

.PARAMETER ArtifactsDirectory
    The directory containing the .nupkg and .snupkg files.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [string] $ArtifactsDirectory = 'artifacts'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Add-Type -AssemblyName System.IO.Compression.FileSystem

$errors = New-Object System.Collections.Generic.List[string]

function Add-Error([string] $message) {
    $errors.Add($message)
    Write-Host "  [KO] $message"
}

function Assert-Entry($entries, [string] $path) {
    if ($entries -contains $path) {
        Write-Host "  [OK] $path"
    } else {
        Add-Error "missing entry: $path"
    }
}

function Get-Entries([string] $archivePath) {
    $archive = [System.IO.Compression.ZipFile]::OpenRead($archivePath)
    try {
        return @($archive.Entries | ForEach-Object { $_.FullName })
    } finally {
        $archive.Dispose()
    }
}

function Get-Nuspec([string] $archivePath) {
    $archive = [System.IO.Compression.ZipFile]::OpenRead($archivePath)
    try {
        $entry = $archive.Entries | Where-Object { $_.FullName -like '*.nuspec' } | Select-Object -First 1
        if ($null -eq $entry) { throw "no .nuspec found in $archivePath" }
        $reader = New-Object System.IO.StreamReader($entry.Open())
        try {
            return [xml] $reader.ReadToEnd()
        } finally {
            $reader.Dispose()
        }
    } finally {
        $archive.Dispose()
    }
}

$resolvedDirectory = Resolve-Path -Path $ArtifactsDirectory
$packages = @(Get-ChildItem -Path $resolvedDirectory -Filter '*.nupkg' | Where-Object { $_.Name -notlike '*.symbols.nupkg' })
$symbolPackages = @(Get-ChildItem -Path $resolvedDirectory -Filter '*.snupkg')

if ($packages.Count -ne 1) { throw "expected exactly one .nupkg in '$resolvedDirectory', found $($packages.Count)." }
if ($symbolPackages.Count -ne 1) { throw "expected exactly one .snupkg in '$resolvedDirectory', found $($symbolPackages.Count)." }

$package = $packages[0]
$symbolPackage = $symbolPackages[0]

Write-Host "Validating $($package.Name)"

$entries = Get-Entries $package.FullName
Assert-Entry $entries 'lib/netstandard2.0/Reefact.LuxaforLightingDeviceController.dll'
Assert-Entry $entries 'lib/netstandard2.0/Reefact.LuxaforLightingDeviceController.xml'
Assert-Entry $entries 'lib/net462/Reefact.LuxaforLightingDeviceController.dll'
Assert-Entry $entries 'lib/net462/Reefact.LuxaforLightingDeviceController.xml'
Assert-Entry $entries 'README.md'
Assert-Entry $entries 'icon.png'

# PDBs belong to the symbol package, not to the package itself.
$strayPdbs = @($entries | Where-Object { $_ -like '*.pdb' })
if ($strayPdbs.Count -gt 0) { Add-Error "the package should not embed PDBs (found: $($strayPdbs -join ', '))" }

Write-Host "Validating $($package.Name) metadata"

$nuspec = Get-Nuspec $package.FullName
$metadata = $nuspec.package.metadata

if ($metadata.id -ne 'Reefact.LuxaforLightingDeviceController') { Add-Error "unexpected package id: $($metadata.id)" } else { Write-Host "  [OK] id" }
foreach ($field in 'version', 'description', 'authors', 'projectUrl', 'icon', 'readme', 'repository') {
    if ([string]::IsNullOrWhiteSpace([string] $metadata.$field) -and $null -eq $metadata.$field) {
        Add-Error "missing metadata: $field"
    } else {
        Write-Host "  [OK] $field"
    }
}
if ($metadata.license.type -ne 'expression' -or $metadata.license.'#text' -ne 'Apache-2.0') {
    Add-Error 'the package should declare the Apache-2.0 license expression'
} else {
    Write-Host '  [OK] license'
}
if ([string]::IsNullOrWhiteSpace([string] $metadata.repository.commit)) {
    Add-Error 'the package should carry the Source Link repository commit'
} else {
    Write-Host '  [OK] repository commit'
}

$expectedFrameworks = @('.NETStandard2.0', '.NETFramework4.6.2')
$declaredFrameworks = @($metadata.dependencies.group | ForEach-Object { $_.targetFramework })
foreach ($framework in $expectedFrameworks) {
    if ($declaredFrameworks -contains $framework) {
        Write-Host "  [OK] dependency group $framework"
    } else {
        Add-Error "missing dependency group: $framework (found: $($declaredFrameworks -join ', '))"
    }
}

foreach ($group in $metadata.dependencies.group) {
    $dependencies = @($group.dependency | ForEach-Object { $_.id })
    if ($dependencies -contains 'Value') { Add-Error "the removed 'Value' dependency is back in $($group.targetFramework)" }
    if (-not ($dependencies -contains 'hidlibrary')) { Add-Error "missing 'hidlibrary' dependency in $($group.targetFramework)" }
}

Write-Host "Validating $($symbolPackage.Name)"

$symbolEntries = Get-Entries $symbolPackage.FullName
Assert-Entry $symbolEntries 'lib/netstandard2.0/Reefact.LuxaforLightingDeviceController.pdb'
Assert-Entry $symbolEntries 'lib/net462/Reefact.LuxaforLightingDeviceController.pdb'

if ($errors.Count -gt 0) {
    Write-Host ''
    Write-Host "Package validation failed with $($errors.Count) error(s):"
    $errors | ForEach-Object { Write-Host " - $_" }
    exit 1
}

Write-Host ''
Write-Host 'Package validation succeeded.'
