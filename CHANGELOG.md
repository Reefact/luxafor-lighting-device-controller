_[Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-FR.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-GR.md) - [Wersja polska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-PL.md)_

# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) and this project adheres to
[Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.1]

Documentation, tooling and CI only: the library itself is unchanged since 2.0.0, and so is its public API.

### Added

- A consumption test of the produced package, `build/Test-PackageConsumption.ps1`: a throwaway project
  outside the repository installs the `.nupkg` from a local feed into a private package cache, compiles the
  public examples against it for `net472` and `net10.0`, checks the resolved assets (`lib/net462`,
  `lib/netstandard2.0`, XML documentation) and the `hidlibrary` dependency, then runs the result on Windows.
  Reading the archive cannot catch a dependency that does not resolve or an asset that never reaches the
  consumer; this can. It runs in the CI, and in the release workflow before anything is published.
- A `samples` folder holding the examples printed in the documentation, compiled with the tests on every
  target framework, plus `build/Sync-Snippets.ps1` which copies them into the markdown pages. The CI runs it
  in check mode, so an example can no longer drift away from the code it shows.
- A `CONTRIBUTING.md` describing how to build, what the CI checks and how a release is published, and a pull
  request template.
- This changelog is now translated into the seven languages of the README. `CHANGELOG.md` stays the English
  one — the reference the translations follow — because the release notes of the package point at it and
  because tooling expects the Keep a Changelog headings in English; the other languages are suffixed
  (`CHANGELOG-FR.md`, `CHANGELOG-NL.md`, ...).
- Polish joins the languages of the documentation: `README-PL.md`, `docs/api-PL.md`, `docs/luxafor-PL.md`
  and `CHANGELOG-PL.md`.
- The consumption test now pins the package under test to the local feed with NuGet Package Source
  Mapping, and reads back the source recorded in `.nupkg.metadata` to prove it. Both the local feed and
  nuget.org are configured for the throwaway project, so once a version is published the restore could
  serve it instead of the freshly built one, and the run would check the wrong package. The dependency is
  asserted to still come from nuget.org, so pinning the id cannot quietly drag the rest of the graph to
  the local feed.

### Changed

- Documentation: each README is now what a reader needs first — what the library is for, how to install it,
  a quick start, the compatible devices, the features, then where to read more. The presentation of the
  Luxafor company and its product catalogue moved to `docs/luxafor*.md`, and the command-by-command
  walkthrough became an API reference in `docs/api*.md`, both in the same seven languages.
- `README.md` is now the English one: it is what GitHub shows on the repository home page and what the
  package ships, and English is the language its international readers expect. The French version moved to
  `README-FR.md`, and the other six keep their suffix. Any link pointing at `README-EN.md` has to be
  updated.
- The workflows moved to `actions/checkout@v5`, `actions/setup-dotnet@v5` and `actions/upload-artifact@v6`,
  the first major of each that runs on Node 24, Node 20 being deprecated on the runners.

### Fixed

- `docs/api.md` and `docs/luxafor.md` are the English pages, the French ones moving to `docs/api-FR.md`
  and `docs/luxafor-FR.md`. The repository now follows a single rule — the file without a suffix is the
  English one — where `docs/` still had the French page unsuffixed while the READMEs and changelogs no
  longer did. Links pointing at `docs/api-EN.md` or `docs/luxafor-EN.md` have to be updated.
- The XML documentation of `TargetedLeds` no longer reads "the on/off or animation will also be sequential
  and could: it can cause a visual ripple effect".
- The metadata check of `build/Validate-Package.ps1` combined its two conditions with `-and`, which no value
  could satisfy: an empty `<description>` passed. It now reports empty, blank, absent and attribute-less
  elements alike.

## [2.0.0]

### Breaking changes

- `TargetedLeds.TabSide` and `TargetedLeds.BackSide` now light the side they name. They were inverted:
  `TabSide` sent the lux code 66 (`0x42`) and `BackSide` 65 (`0x41`), while the Luxafor protocol assigns
  65 to the tab side (LEDs n° 1, 2 and 3) and 66 to the back side (LEDs n° 4, 5 and 6). The API does not
  change and nothing fails to compile, but **the LEDs that light up do change**: code written against
  1.x drove the opposite side, so any workaround swapping the two has to be removed. Verified on a
  device before the change.
- The device interface is renamed `LuxaforDevice` → `ILuxaforDevice`, aligning the public API with the .NET
  naming convention the rest of the ecosystem uses (`IDisposable`, `IEnumerable<T>`, ...). Consumers have to
  update the type name; the members themselves are unchanged, so the migration is a rename:
  `using ILuxaforDevice orb = Luxafor.GetDevices().First();`. The implementations keep naming their
  specialization (`HidLuxaforDevice`), and `Luxafor`, `LuxaforDeviceLocator` and
  `LuxaforDeviceNotFoundException` keep their names — they are not interfaces.
- The value objects (`BrightColor`, `FadeDuration`, `LedIndex`, `LightingCommand`, `Repeat`, `Speed`,
  `TargetedLeds`) no longer derive from `Value.ValueType<T>`: they implement their own equality
  (`Equals`, `GetHashCode`, `==`, `!=`, `IEquatable<T>`) with the same value semantics, and the `Value`
  package is not a dependency anymore. Source compatible for any normal usage (comparisons, dictionary keys,
  `IEquatable<T>`), but binary incompatible: recompile against 2.0.0. Only code explicitly referring to the
  `Value.ValueType<T>` base type (or overriding `GetAllAttributesToBeUsedForEquality`) needs to be adapted.
- `Luxafor.GetDevice(devicePath)` now reports invalid paths explicitly instead of failing with an obscure
  `ArgumentNullException`: it throws `ArgumentException` on a blank path and `LuxaforDeviceNotFoundException`
  when no device sits at the path, when it is not a supported Luxafor device, or when it is not connected
  anymore.
- The package no longer ships a `net46` assembly under a `net462` folder: the `net462` target is really
  compiled against .NET Framework 4.6.2.

### Added

- `LuxaforDeviceNotFoundException`, carrying the offending `DevicePath`.
- `TargetedLeds.FromLedIndex(LedIndex)`, the named alternative to the existing implicit conversion.
- Source Link, a symbol package (`.snupkg`) and deterministic builds.
- A GitHub Actions CI (Windows) running build, tests, packaging and a validation of the `.nupkg` content.
- A GitHub Actions release workflow publishing to nuget.org when a `v*` tag is pushed, after checking
  that the tag matches the version of the project, running the tests and validating the package. It
  authenticates through trusted publishing (OIDC): no long-lived API key is stored in the repository.
- Tests covering write failure propagation, `Dispose`, missing devices, invalid HID paths, argument guards,
  value object equality and the public API surface.

### Changed

- Single SDK-style project multi-targeting `netstandard2.0` and `net462`, replacing the Shared Project, the
  non-SDK .NET Framework project and the hand written `.nuspec`; the package is now produced by
  `dotnet pack -c Release` (from Release binaries, where the previous `.nuspec` picked up Debug ones).
- Nullable reference types, warnings as errors and the .NET analyzers are enabled.
- HidLibrary is used through an internal abstraction (`IHidDeviceRegistry`, `IHidDeviceHandle`), which keeps
  it out of the public API and makes the device logic testable without hardware.
- Internal renamings, without impact on the public API: `LuxaforDeviceImp` becomes `HidLuxaforDevice`, the
  `Lightning*` files and folders become `Lighting*` (matching the `LightingCommand` type they contain), and
  the internal `LightingCommandFactory` interface becomes `ILightingCommandFactory`.
- Documentation: the READMEs (7 languages) are synchronized with the code — obsolete `BasicColor` /
  `SetBasicColor` examples replaced by `BrightColor` / `SetColor`, `void` signatures corrected to `bool`,
  `using` shown on `ILuxaforDevice`, plus installation, device lookup, error handling, supported devices and
  license sections.

### Fixed

- The lux codes sent for `TargetedLeds.TabSide` and `TargetedLeds.BackSide` were inverted, so both lit
  the opposite side of the device. See the breaking changes above: this one is silent, it changes
  behaviour without breaking the build.
- `FadeColor` commands no longer describe themselves with a "duration od" typo in `ToString()`.

## [1.2.0]

### Added

- Device commands return a `bool` to indicate whether the operation succeeded.
- `LuxaforDevice` implements `IDisposable` to enable proper resource cleanup.
