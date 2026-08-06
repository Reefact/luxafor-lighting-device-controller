# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) and this project adheres to
[Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
