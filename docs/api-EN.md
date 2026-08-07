_[Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-NL.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-SE.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-DE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-ES.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-GR.md) - [Polski](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-PL.md)_

[← Back to the README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md)

# API reference

Everything lives in a single namespace, `Reefact.LuxaforLightingDeviceController`. The static `Luxafor` class is the entry point: it hands out `ILuxaforDevice` instances, which commands are then sent to.

The examples of this page are extracted from the [`samples`](https://github.com/Reefact/luxafor-lighting-device-controller/tree/main/samples) folder, which the CI compiles: they cannot drift away from the code.

## Getting a device

| Signature | Description |
| --- | --- |
| `IEnumerable<ILuxaforDevice> Luxafor.GetDevices()` | All the Luxafor devices plugged into the USB ports. The enumeration is empty when none is plugged in. |
| `ILuxaforDevice Luxafor.GetDevice(string devicePath)` | The Luxafor device located at the given path. |

<!-- snippet: list-devices -->
```csharp
foreach (ILuxaforDevice device in Luxafor.GetDevices()) {
    using (device) {
        Console.Out.WriteLine(device.Description + " — " + device.Path);
    }
}
```
<!-- endSnippet -->

`ILuxaforDevice` implements `IDisposable`: the `using` statement releases the device handle at the end of the block. A device also exposes its path (`Path`) and its description (`Description`), as reported by Windows.

`Luxafor.GetDevice` throws a `LuxaforDeviceNotFoundException` when no device is found at the given path, when the device found there is not a supported Luxafor device, or when it is not connected anymore. The `DevicePath` property of the exception carries the requested path back.

<!-- snippet: get-device-by-path -->
```csharp
try {
    using ILuxaforDevice device = Luxafor.GetDevice(devicePath);

    device.SetColor(BrightColor.Red);
} catch (LuxaforDeviceNotFoundException exception) {
    Console.Error.WriteLine(exception.DevicePath + " is not a connected Luxafor device.");
}
```
<!-- endSnippet -->

## Result of a command

Every command returns a `bool`: `true` when the device accepted the command, `false` when the write failed (device unplugged, taken by another application, ...). Invalid arguments, on the other hand, throw (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

<!-- snippet: command-result -->
```csharp
if (!device.SetColor(BrightColor.Red)) {
    Console.Error.WriteLine("The device refused the command: unplugged, or held by another application.");
}
```
<!-- endSnippet -->

## Turning off

| Signature | Description |
| --- | --- |
| `bool TurnOff()` | Turns off all the LEDs of the device. |
| `bool TurnOff(TargetedLeds targetedLeds)` | Turns off the targeted LEDs of the device. |

<!-- snippet: turn-off -->
```csharp
device.TurnOff();
device.TurnOff(TargetedLeds.BackSide);
```
<!-- endSnippet -->

## Setting a color

| Signature | Description |
| --- | --- |
| `bool SetColor(BrightColor color)` | Turns on all the LEDs of the device in a color. |
| `bool SetColor(TargetedLeds targetedLeds, BrightColor color)` | Turns on the targeted LEDs in a color. |

<!-- snippet: set-color -->
```csharp
device.SetColor(BrightColor.Green);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);
```
<!-- endSnippet -->

## Making a transition (fade)

| Signature | Description |
| --- | --- |
| `bool FadeColor(BrightColor color, FadeDuration duration)` | Fades all the LEDs to a color. |
| `bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration)` | Fades the targeted LEDs to a color. |

The duration is expressed in the unit of the device: `FadeDuration.From(byte)`, from `0` (immediate) to `255` (the slowest).

<!-- snippet: fade-color -->
```csharp
device.FadeColor(BrightColor.Blue, FadeDuration.From(30));
device.FadeColor(TargetedLeds.BackSide, BrightColor.Blue, FadeDuration.From(30));
```
<!-- endSnippet -->

## Flashing (strobe effect)

| Signature | Description |
| --- | --- |
| `bool Strobe(BrightColor color, Speed speed, Repeat repeat)` | Flashes all the LEDs in a color. |
| `bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat)` | Flashes the targeted LEDs in a color. |

The speed is expressed in the unit of the device as well: `Speed.FromByte(byte)`. The number of repetitions is declared with `Repeat.Once`, `Repeat.Twice` or `Repeat.Count(byte)`.

<!-- snippet: strobe -->
```csharp
device.Strobe(BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
device.Strobe(TargetedLeds.TabSide, BrightColor.Yellow, Speed.FromByte(20), Repeat.Twice);
```
<!-- endSnippet -->

## Waves and built-in patterns

| Signature | Description |
| --- | --- |
| `bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat)` | Starts a colored wave on all the LEDs of the device. |
| `bool PlayPattern(BuiltInPattern pattern, Repeat repeat)` | Starts a pattern built into the device. |

Waves go from `WavePattern.Wave_1` to `WavePattern.Wave_5`. The built-in patterns are `BuiltInPattern.Pattern_1` to `Pattern_5`, plus `Rainbow`, `TrafficLight`, `Police` and `Off`.

<!-- snippet: play-pattern -->
```csharp
device.PlayPattern(WavePattern.Wave_1, BrightColor.Cyan, Speed.FromByte(10), Repeat.Count(5));
device.PlayPattern(BuiltInPattern.Police, Repeat.Twice);
```
<!-- endSnippet -->

## Targeting LEDs

`TargetedLeds` designates the LEDs a command turns on, turns off or animates simultaneously.

| Value | LEDs involved |
| --- | --- |
| `TargetedLeds.All` | The six LEDs. |
| `TargetedLeds.TabSide` | The LEDs n° 1, 2 and 3. |
| `TargetedLeds.BackSide` | The LEDs n° 4, 5 and 6. |
| `TargetedLeds.Led_1` ... `TargetedLeds.Led_6` | A single LED. |

A `LedIndex` (`LedIndex._1` to `LedIndex._6`, or `LedIndex.From(byte)`) implicitly converts to `TargetedLeds`, so an index can be passed where a target is expected. The other combinations cannot be expressed: the device does not accept them. Several sequential commands are then needed, at the cost of a visual ripple effect since the lighting becomes sequential.

<!-- snippet: targeted-leds -->
```csharp
device.SetColor(TargetedLeds.All, BrightColor.Black);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);   // LEDs n° 1, 2 and 3
device.SetColor(TargetedLeds.BackSide, BrightColor.Red);    // LEDs n° 4, 5 and 6
device.SetColor(TargetedLeds.Led_1, BrightColor.Blue);
device.SetColor(LedIndex.From(6), BrightColor.Blue);
```
<!-- endSnippet -->

## Colors

`BrightColor` offers the `Red`, `Green`, `Blue`, `Yellow`, `Cyan`, `Magenta`, `White` and `Black` colors, or is built from a hexadecimal representation (`#RRGGBB`) or from three components.

<!-- snippet: colors -->
```csharp
device.SetColor(BrightColor.Red);                    // Green, Blue, Yellow, Cyan, Magenta, White, Black
device.SetColor(BrightColor.From("#0F11A8"));        // from an hexadecimal representation
device.SetColor(BrightColor.From(15, 17, 168));      // from its red, green and blue components
```
<!-- endSnippet -->

`BrightColor.From(string)` throws a `FormatException` when the string is not a valid hexadecimal representation.

## Reusable commands

A `LightingCommand` describes a command once and for all, to replay it later with `Send`.

| Factory | Equivalent to |
| --- | --- |
| `LightingCommand.CreateTurnOffCommand()` | `TurnOff()` |
| `LightingCommand.CreateTurnOffCommand(TargetedLeds)` | `TurnOff(TargetedLeds)` |
| `LightingCommand.CreateSetColorCommand(BrightColor)` | `SetColor(BrightColor)` |
| `LightingCommand.CreateSetColorCommand(TargetedLeds, BrightColor)` | `SetColor(TargetedLeds, BrightColor)` |
| `LightingCommand.CreateFadeColorCommand(TargetedLeds, BrightColor, FadeDuration)` | `FadeColor(TargetedLeds, BrightColor, FadeDuration)` |
| `LightingCommand.CreateStrobeCommand(TargetedLeds, BrightColor, Speed, Repeat)` | `Strobe(TargetedLeds, BrightColor, Speed, Repeat)` |
| `LightingCommand.CreatePlayWavePatternCommand(WavePattern, BrightColor, Speed, Repeat)` | `PlayPattern(WavePattern, BrightColor, Speed, Repeat)` |
| `LightingCommand.CreatePlayBuiltInPatternCommand(BuiltInPattern, Repeat)` | `PlayPattern(BuiltInPattern, Repeat)` |

<!-- snippet: reusable-commands -->
```csharp
LightingCommand alert = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
LightingCommand calm  = LightingCommand.CreateSetColorCommand(BrightColor.Green);

device.Send(alert);
device.Send(calm);
```
<!-- endSnippet -->
