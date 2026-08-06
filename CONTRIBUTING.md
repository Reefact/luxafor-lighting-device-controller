# Contributing

Thanks for taking the time. This page is about building the library and about what the CI checks;
the [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-EN.md)
is about using it.

## Building and testing

```shell
dotnet build -c Release
dotnet test -c Release
dotnet pack Reefact.LuxaforLightingDeviceController -c Release -o artifacts
```

The library targets `netstandard2.0` and `net462`; the tests target `net10.0`, plus `net472` on
Windows so that the `net462` assembly is really exercised. The `net472` target can be *compiled* on
any OS, which catches an API missing from .NET Framework without waiting for the CI:

```shell
dotnet build Reefact.LuxaforLightingDeviceController.UnitTests -p:TargetFrameworks=net472
```

Warnings are errors (`Directory.Build.props`), and the .NET analyzers run on every build.

The tests that drive a real device (`Reefact.LuxaforLightingDeviceController.UnitTests/UsageExamples.cs`)
are skipped by default: plug a Luxafor Orb in, remove the `Skip` and watch what happens.

## The public API is approved

`PublicApi_should` compares the exported surface of the library with
`Reefact.LuxaforLightingDeviceController.UnitTests/PublicApi.approved.txt`. Any change to the public
API therefore shows up in the diff. When the change is intended, update the approved file in the
same commit — and, if it is a breaking change, say so in `CHANGELOG.md`.

## The documentation examples are compiled code

The examples printed in the READMEs and in the `docs` pages are not written in markdown. They live in
[`samples`](https://github.com/Reefact/luxafor-lighting-device-controller/tree/main/samples), between
a `// begin-snippet: <name>` and a `// end-snippet` marker, and each page only declares where a
snippet goes:

```markdown
<!-- snippet: quick-start -->
<!-- endSnippet -->
```

The fenced block in between is generated; a marker written inside a code fence, like the one above,
is left alone.

The samples are compiled with the unit tests, on every target framework. After changing one, rewrite
the pages:

```shell
pwsh ./build/Sync-Snippets.ps1
```

The CI runs `./build/Sync-Snippets.ps1 -Check`, which writes nothing and fails when a page is out of
date, when a page asks for a snippet that does not exist, or when a snippet is shown nowhere.

## The package is validated, then consumed

Two scripts run on the packed output, in the CI and in the release workflow:

```shell
pwsh ./build/Validate-Package.ps1 -ArtifactsDirectory artifacts
pwsh ./build/Test-PackageConsumption.ps1 -ArtifactsDirectory artifacts
```

`Validate-Package.ps1` looks *inside* the `.nupkg`: both target frameworks, the XML documentation,
the readme, the icon, the expected dependencies (and only those), the license expression, the Source
Link commit, and the portable PDBs in the `.snupkg`.

`Test-PackageConsumption.ps1` looks at the package *from the outside*: it creates a throwaway project
outside the repository, restores the freshly built package from a local feed into a private package
cache, compiles the `samples` folder against it for `net472` and `net10.0`, checks that the right
assets were picked (`lib/net462` and `lib/netstandard2.0`, XML documentation included) and that the
`hidlibrary` dependency flowed through, then runs the resulting program on Windows. That is what
catches a broken dependency, a missing asset or an example that no longer compiles for a consumer,
none of which reading the archive can see.

## Releasing

1. set `<Version>` in `Reefact.LuxaforLightingDeviceController/Reefact.LuxaforLightingDeviceController.csproj`
2. update `CHANGELOG.md`, merge into `main`
3. `git tag v<version> && git push origin v<version>`

The release workflow refuses to publish when the tag does not match the version of the project, and
it runs the tests, the package validation and the consumption test before pushing anything to
nuget.org.
