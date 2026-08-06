<#
.SYNOPSIS
    Copies the code examples of the `samples` folder into the markdown pages.

.DESCRIPTION
    The examples shown in the READMEs and in the documentation pages are compiled code: they live in
    the `samples` folder, between a `// begin-snippet: <name>` and a `// end-snippet` marker, and the
    markdown pages only declare where they go:

        <!-- snippet: quick-start -->
        ```csharp
        (generated, do not edit)
        ```
        <!-- endSnippet -->

    Running the script rewrites the fenced blocks from the sources. Running it with -Check rewrites
    nothing and fails when a page is out of date, which is what the CI does: an example can then no
    longer drift away from the code it is supposed to show.

.PARAMETER SourceDirectory
    The directory holding the C# sources that carry the snippets.

.PARAMETER MarkdownFiles
    The markdown files to fill in. Defaults to every .md file of the repository (except CHANGELOG.md).

.PARAMETER Check
    Verify only: nothing is written, and the script fails when a page is out of date.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [string] $SourceDirectory,

    [Parameter(Mandatory = $false)]
    [string[]] $MarkdownFiles,

    [Parameter(Mandatory = $false)]
    [switch] $Check
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repositoryRoot = Split-Path -Parent $PSScriptRoot
if ([string]::IsNullOrWhiteSpace($SourceDirectory)) { $SourceDirectory = Join-Path $repositoryRoot 'samples' }

$beginMarker    = '^\s*//\s*begin-snippet:\s*(?<name>[\w\-\.]+)\s*$'
$endMarker      = '^\s*//\s*end-snippet\s*$'
$snippetOpening = '^(?<indent>[ \t]*)<!--\s*snippet:\s*(?<name>[\w\-\.]+)\s*-->\s*$'
$snippetClosing = '^\s*<!--\s*endSnippet\s*-->\s*$'
# A marker written inside a fenced block documents the markers (CONTRIBUTING.md), it does not ask for
# a snippet. The blocks the script generates are themselves fenced, so the count stays balanced.
$codeFence      = '^\s*```'

$errors = New-Object System.Collections.Generic.List[string]

function Add-Error([string] $message) {
    $errors.Add($message)
    Write-Host "  [KO] $message"
}

<#
    Reads the snippets of one source file. A snippet is everything between its two markers, with the
    indentation shared by all its lines removed, so that a snippet taken from a method body starts at
    column 0 in the markdown page.
#>
function Read-Snippets([string] $filePath) {
    $snippets = @{}
    $lines    = [System.IO.File]::ReadAllLines($filePath)
    $current  = $null
    $buffer   = $null

    for ($index = 0; $index -lt $lines.Length; $index++) {
        $line = $lines[$index]
        if ($line -match $beginMarker) {
            if ($null -ne $current) { throw "$filePath (line $($index + 1)): snippet '$current' is not closed." }
            $current = $Matches['name']
            $buffer  = New-Object System.Collections.Generic.List[string]

            continue
        }
        if ($line -match $endMarker) {
            if ($null -eq $current) { throw "$filePath (line $($index + 1)): 'end-snippet' without a matching 'begin-snippet'." }
            $snippets[$current] = Format-Snippet $buffer
            $current            = $null
            $buffer             = $null

            continue
        }
        if ($null -ne $current) { $buffer.Add($line) }
    }

    if ($null -ne $current) { throw "$filePath : snippet '$current' is not closed." }

    return $snippets
}

function Format-Snippet($lines) {
    $content = @($lines)
    while ($content.Count -gt 0 -and [string]::IsNullOrWhiteSpace($content[0])) { $content = $content[1..($content.Count - 1)] }
    while ($content.Count -gt 0 -and [string]::IsNullOrWhiteSpace($content[-1])) { $content = $content[0..($content.Count - 2)] }
    if ($content.Count -eq 0) { return @() }

    $indents = @($content | Where-Object { -not [string]::IsNullOrWhiteSpace($_) } | ForEach-Object { $_.Length - $_.TrimStart().Length })
    $margin  = ($indents | Measure-Object -Minimum).Minimum

    return @($content | ForEach-Object { if ([string]::IsNullOrWhiteSpace($_)) { '' } else { $_.Substring($margin) } })
}

<#
    Rebuilds one markdown file: every fenced block between a `<!-- snippet: name -->` and the matching
    `<!-- endSnippet -->` is replaced by the content of the snippet. Returns the new lines, or $null
    when the file declares no snippet at all.
#>
function Update-MarkdownFile([string] $filePath, [hashtable] $snippets) {
    $lines     = [System.IO.File]::ReadAllLines($filePath)
    $result    = New-Object System.Collections.Generic.List[string]
    $touched   = $false
    $inFence   = $false

    for ($index = 0; $index -lt $lines.Length; $index++) {
        $line = $lines[$index]
        $result.Add($line)
        if ($line -match $codeFence) { $inFence = -not $inFence; continue }
        if ($inFence) { continue }
        if ($line -notmatch $snippetOpening) { continue }

        $name    = $Matches['name']
        $indent  = $Matches['indent']
        $touched = $true

        $closing = -1
        for ($lookahead = $index + 1; $lookahead -lt $lines.Length; $lookahead++) {
            if ($lines[$lookahead] -match $snippetClosing) { $closing = $lookahead; break }
        }
        if ($closing -lt 0) {
            Add-Error "$filePath (line $($index + 1)): snippet '$name' is opened but never closed by <!-- endSnippet -->."

            continue
        }
        if (-not $snippets.ContainsKey($name)) {
            Add-Error "$filePath (line $($index + 1)): unknown snippet '$name' (no '// begin-snippet: $name' in $SourceDirectory)."
            for ($copy = $index + 1; $copy -le $closing; $copy++) { $result.Add($lines[$copy]) }
            $index = $closing

            continue
        }

        $result.Add("$indent``````csharp")
        foreach ($snippetLine in $snippets[$name]) {
            $result.Add($(if ([string]::IsNullOrEmpty($snippetLine)) { '' } else { "$indent$snippetLine" }))
        }
        $result.Add("$indent``````")
        $result.Add($lines[$closing])

        $index = $closing
    }

    if (-not $touched) { return $null }

    return $result.ToArray()
}

$sources = @(Get-ChildItem -Path $SourceDirectory -Filter '*.cs' -Recurse -File)
if ($sources.Count -eq 0) { throw "no C# source found in '$SourceDirectory'." }

$snippets = @{}
foreach ($source in $sources) {
    foreach ($entry in (Read-Snippets $source.FullName).GetEnumerator()) {
        if ($snippets.ContainsKey($entry.Key)) { throw "snippet '$($entry.Key)' is declared twice (second one in $($source.FullName))." }
        $snippets[$entry.Key] = $entry.Value
    }
}
Write-Host "Found $($snippets.Count) snippet(s) in $($sources.Count) source file(s)."

if ($null -eq $MarkdownFiles -or $MarkdownFiles.Count -eq 0) {
    $MarkdownFiles = @(Get-ChildItem -Path $repositoryRoot -Filter '*.md' -Recurse -File |
            Where-Object { $_.FullName -notmatch '[\\/](bin|obj|\.git)[\\/]' } |
            ForEach-Object { $_.FullName })
}

$used    = New-Object System.Collections.Generic.HashSet[string]
$updated = New-Object System.Collections.Generic.List[string]
$stale   = New-Object System.Collections.Generic.List[string]

foreach ($markdownFile in $MarkdownFiles) {
    $path    = (Resolve-Path -Path $markdownFile).Path
    $inFence = $false
    foreach ($line in [System.IO.File]::ReadAllLines($path)) {
        if ($line -match $codeFence) { $inFence = -not $inFence; continue }
        if (-not $inFence -and $line -match $snippetOpening) { [void] $used.Add($Matches['name']) }
    }

    $newLines = Update-MarkdownFile $path $snippets
    if ($null -eq $newLines) { continue }

    # The repository stores markdown with LF endings (see .editorconfig), whatever the checkout does.
    $newContent = ($newLines -join "`n") + "`n"
    $oldContent = [System.IO.File]::ReadAllText($path).Replace("`r`n", "`n")
    if ($newContent -eq $oldContent) { continue }

    if ($Check) {
        $stale.Add($path)
        Write-Host "  [KO] out of date: $path"
    } else {
        [System.IO.File]::WriteAllText($path, $newContent, (New-Object System.Text.UTF8Encoding($false)))
        $updated.Add($path)
        Write-Host "  [--] updated: $path"
    }
}

$unused = @($snippets.Keys | Where-Object { -not $used.Contains($_) } | Sort-Object)
if ($unused.Count -gt 0) { Add-Error "snippet(s) declared in $SourceDirectory but shown in no page: $($unused -join ', ')" }

if ($stale.Count -gt 0) {
    Add-Error "$($stale.Count) page(s) no longer match the samples; run ./build/Sync-Snippets.ps1 and commit the result."
}

if ($errors.Count -gt 0) {
    Write-Host ''
    Write-Host "Snippet synchronization failed with $($errors.Count) error(s):"
    $errors | ForEach-Object { Write-Host " - $_" }
    exit 1
}

Write-Host ''
if ($Check) {
    Write-Host 'All the pages are up to date.'
} elseif ($updated.Count -eq 0) {
    Write-Host 'All the pages were already up to date.'
} else {
    Write-Host "$($updated.Count) page(s) updated."
}
