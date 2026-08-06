_[Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)_

# Luxafor Device Controller

Drive your [Luxafor](https://luxafor.com) availability indicators from your own .NET applications, speaking their USB HID protocol directly: no Luxafor server, no webhook, no third-party software to install.

- A direct API: `SetColor`, `FadeColor`, `Strobe`, `PlayPattern`, down to driving one LED at a time.
- `.NET Standard 2.0` and `.NET Framework 4.6.2`, for a single dependency ([HidLibrary](https://github.com/mikeobrien/HidLibrary)).
- **Windows only**: the devices are driven through the Windows HID stack. The package installs on any platform, but the devices can only be enumerated and controlled on Windows.

## Installation

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

## Quick start

<!-- snippet: quick-start -->
```csharp
using System.Linq;

using Reefact.LuxaforLightingDeviceController;

namespace MyApplication {

    public static class BuildStatusLight {

        public static void Show(bool buildSucceeded) {
            using ILuxaforDevice device = Luxafor.GetDevices().First();

            if (buildSucceeded) {
                device.SetColor(BrightColor.Green);
            } else {
                device.Strobe(BrightColor.Red, Speed.FromByte(20), Repeat.Count(3));
            }
        }

    }

}
```
<!-- endSnippet -->

`Luxafor.GetDevices()` enumerates the Luxafor devices plugged into the USB ports of the machine; the enumeration is empty when none is plugged in. `ILuxaforDevice` implements `IDisposable`: the `using` statement releases the device handle at the end of the block. Every command returns a `bool`: `true` when the device accepted the command, `false` when the write failed.

## Compatible devices

The library drives, through their USB HID protocol, the Luxafor devices identified by the vendor id `1240` (`0x04D8`) and the product id `62322` (`0xF372`).

| Device | Status |
| --- | --- |
| `Luxafor Orb` | **Tested**: the device used to develop and validate the library (6 addressable LEDs). |
| `Luxafor Flag` | **Expected to work, not tested**: same identifiers and same lighting protocol (6 addressable LEDs). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Expected to work, not tested**: the lighting commands are the same; the LED layout, the number of LEDs and the color rendering may differ. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Not supported**: these devices are not driven through this USB HID protocol. |

Feedback about an untested device is very welcome: please [open an issue](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

## Features

- **Solid color** (`SetColor`) from a named color, a hexadecimal code or red / green / blue components.
- **Fade** (`FadeColor`) to a color, over a chosen transition duration.
- **Strobe** (`Strobe`) at a chosen speed and for a chosen number of repetitions.
- **Patterns** (`PlayPattern`): colored waves, or patterns built into the device (`Police`, `Rainbow`, `TrafficLight`, ...).
- **Turning off** (`TurnOff`), all the LEDs or only some of them.
- **LED targeting**: all of them, one side (`TabSide`, `BackSide`) or a single LED (`Led_1` to `Led_6`).
- **Reusable commands**: `LightingCommand` describes a command once, to replay it later with `Send`.

## Detailed documentation

- [API reference](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-EN.md): every command, its parameters and its errors.
- [Luxafor, the company and its devices](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-EN.md): what these indicators are for, and which ones this library drives.
- [Changelog](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG.md) and [contributing guide](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CONTRIBUTING.md).

## License

This library is distributed under the [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE) license.
