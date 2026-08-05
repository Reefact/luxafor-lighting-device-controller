_[Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)_

# Luxafor Device Controller

A .Net library that provides a simple API to control Luxafor devices.

## Luxafor

### Company Overview

[Luxafor](https://luxafor.com) is a company that designs and sells products for office productivity, such as availability indicators and notification tools. 

Their flagship product is an [LED availability indicator](https://luxafor.com/product/flag) that can be programmed to display different colors depending on the user's availability status. 

Luxafor's goal is to provide users with a simple and effective way to signal their availability to co-workers and improve communication and collaboration in the workplace.

### Quick overview of the devices

Here is a non-exhaustive list of [Luxafor devices](https://luxafor.com/products):

- `Luxafor Flag`: an LED availability indicator that displays personal availability
- `Luxafor Bluetooth`: a wireless, software-controlled LED availability indicator that displays notifications and personal availability
- `Luxafor Switch`: a wireless, remote-controlled availability indicator that displays the availability of meeting rooms and workstations in real time
- `Luxafor Cube`: a standalone LED availability indicator that displays meeting room availability
- `Luxafor Pomodoro-Timer`: a USB-powered LED timer that divides work into smaller time slots (see [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: a wide angle USB LED availability indicator
- `Luxafor CO2 Monitor`: a sensor that analyzes the air quality of a room and alerts you when it needs to be ventilated
- `Luxafor Mute Button`: turn on/off the microphone with a single touch and indicate if you are available with the red/green
- `Luxafor Colorblind Flag`: monochrome USB LED availability light eliminates distractions and boosts productivity

### Integration

These different devices are designed to be driven manually ('mechanical') for some, semi-automatically (manual driving via [software](https://luxaformanual.com)) / automatically (integration via [software](https://luxaformanual.com) to tools like Teams, Skype, Cisco, Zappier or via Webhook) for others. 

## Presentation of the library

This library aims to allow the integration of USB LED devices to your in-house applications without having to go through the Luxafor server (webhook).

It targets `.NET Standard 2.0` and `.NET Framework 4.6.2`, and is based on the library [HidLibrary](https://github.com/mikeobrien/HidLibrary) which allows to enumerate and communicate with HID compatible USB devices in .NET.

> **Windows only.** The devices are driven through the Windows HID stack: the package installs on any platform, but the devices can only be enumerated and controlled on Windows.

### Supported devices

The library speaks the USB HID protocol of the Luxafor devices identified by the vendor id `1240` (`0x04D8`) and the product id `62322` (`0xF372`).

| Device | Status |
| --- | --- |
| `Luxafor Orb` | **Tested**: the device used to develop and validate the library (6 addressable LEDs). |
| `Luxafor Flag` | **Expected to work, not tested**: same identifiers and same lighting protocol (6 addressable LEDs). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Expected to work, not tested**: the lighting commands are the same; the LED layout, the number of LEDs and the color rendering may differ. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Not supported**: these devices are not driven through this USB HID protocol. |

Feedback about an untested device is very welcome: please [open an issue](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

### Installation

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

### Getting started

The code below presents an example of basic use of the library for the control of a [Luxafor Orb](https://luxafor.com/product/orb/) device.

```csharp
[Fact]
public void french_sequence() {
    using ILuxaforDevice orb = Luxafor.GetDevices().First();
    for (var i = 0; i < 3; i++) {
        orb.SetColor(BrightColor.Blue);
        Thread.Sleep(500);
        orb.SetColor(BrightColor.White);
        Thread.Sleep(500);
        orb.SetColor(BrightColor.Red);
        Thread.Sleep(500);
        orb.TurnOff();
        Thread.Sleep(1000);
    }
}
```

Line 3 shows how to connect to a single Orb connected to the machine's USB port. `ILuxaforDevice` implements `IDisposable`: the `using` statement releases the device handle at the end of the block.

### Getting a device

```csharp
IEnumerable<ILuxaforDevice> GetDevices(); // All the Luxafor devices connected to the USB ports (empty when none is plugged in)
ILuxaforDevice GetDevice(string devicePath); // The Luxafor device located at the given path
```

`Luxafor.GetDevice` throws a `LuxaforDeviceNotFoundException` when no device is found at the given path, when the device found there is not a supported Luxafor device, or when it is not connected anymore.

```csharp
using ILuxaforDevice orb = Luxafor.GetDevice(@"\\?\hid#vid_04d8&pid_f372#...");
```

I will quickly go through all the possible commands to send to devices from the `ILuxaforDevice`.

Every command returns a `bool`: `true` when the device accepted the command, `false` when the write failed (device unplugged, taken by another application, ...). Invalid arguments throw (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

### Turn off

```csharp
bool TurnOff(); // Turns off all the LEDs of the device
bool TurnOff(TargetedLeds targetedLeds); // Turn off the targeted LEDs of the device
```

### Set a single color

```csharp
bool SetColor(BrightColor color); // Turns on the device's LEDs in a custom color.
bool SetColor(TargetedLeds targetedLeds, BrightColor color); // Turns on the targeted device LEDs in a custom color.
```

### Make a transition (fade)

```csharp
bool FadeColor(BrightColor color, FadeDuration duration); // Make a transition from all the LEDs of the device to a custom color
bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration); // Performs a transition from the targeted device LEDs to a custom color
```

### Flashing (strobe effect)

```csharp
bool Strobe(BrightColor color, Speed speed, Repeat repeat); // Flashes all the LEDs of the device in a custom color
bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat); // Flashes the targeted device LEDs in a custom color
```

### Waves / built-in patterns

```csharp
bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat); // Starts a wave pattern that targets all the LEDs of the device based on a custom color
bool PlayPattern(BuiltInPattern pattern, Repeat repeat); // Starts an embedded pattern that targets all LEDs on the device
```

### Send a command

It is possible to create custom commands called `LightingCommand` so that they can be reused in the code:

```csharp
var command = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

The `Send` method allows you to use these commands.

```csharp
bool Send(LightingCommand command); // Send a command to the device
```

### Colors

```csharp
BrightColor.Red; // and Green, Blue, Yellow, Cyan, Magenta, White, Black
BrightColor.From("#0F11A8"); // From its hexadecimal representation
BrightColor.From(15, 17, 168); // From its red, green and blue components
```

## Building the library

```shell
dotnet build -c Release
dotnet test -c Release
dotnet pack Reefact.LuxaforLightingDeviceController -c Release -o artifacts
```

## License

This library is distributed under the [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE) license.
