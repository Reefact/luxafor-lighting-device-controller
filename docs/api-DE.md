_[English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api.md) - [Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-FR.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-NL.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-SE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-ES.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-GR.md) - [Polski](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-PL.md)_

[← Zurück zur README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md)

# API-Referenz

Alles liegt in einem einzigen Namespace, `Reefact.LuxaforLightingDeviceController`. Die statische Klasse `Luxafor` ist der Einstiegspunkt: Sie liefert `ILuxaforDevice`-Instanzen, an die anschließend Befehle gesendet werden.

Die Beispiele dieser Seite stammen aus dem Ordner [`samples`](https://github.com/Reefact/luxafor-lighting-device-controller/tree/main/samples), den die CI kompiliert: Sie können nicht vom Code abweichen.

## Ein Gerät abrufen

| Signatur | Beschreibung |
| --- | --- |
| `IEnumerable<ILuxaforDevice> Luxafor.GetDevices()` | Alle an die USB-Anschlüsse angeschlossenen Luxafor-Geräte. Die Aufzählung ist leer, wenn keines angeschlossen ist. |
| `ILuxaforDevice Luxafor.GetDevice(string devicePath)` | Das Luxafor-Gerät unter dem angegebenen Pfad. |

<!-- snippet: list-devices -->
```csharp
foreach (ILuxaforDevice device in Luxafor.GetDevices()) {
    using (device) {
        Console.Out.WriteLine(device.Description + " — " + device.Path);
    }
}
```
<!-- endSnippet -->

`ILuxaforDevice` implementiert `IDisposable`: Die `using`-Anweisung gibt das Handle des Geräts am Ende des Blocks wieder frei. Ein Gerät stellt außerdem seinen Pfad (`Path`) und seine Beschreibung (`Description`) bereit, so wie Windows sie meldet.

`Luxafor.GetDevice` löst eine `LuxaforDeviceNotFoundException` aus, wenn unter dem angegebenen Pfad kein Gerät gefunden wird, wenn das dort gefundene Gerät kein unterstütztes Luxafor-Gerät ist oder wenn es nicht mehr angeschlossen ist. Die Eigenschaft `DevicePath` der Ausnahme gibt den angefragten Pfad zurück.

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

## Ergebnis eines Befehls

Jeder Befehl gibt einen `bool` zurück: `true`, wenn das Gerät den Befehl angenommen hat, `false`, wenn das Schreiben fehlgeschlagen ist (Gerät abgezogen, von einer anderen Anwendung belegt, ...). Ungültige Argumente lösen dagegen eine Ausnahme aus (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

<!-- snippet: command-result -->
```csharp
if (!device.SetColor(BrightColor.Red)) {
    Console.Error.WriteLine("The device refused the command: unplugged, or held by another application.");
}
```
<!-- endSnippet -->

## Ausschalten

| Signatur | Beschreibung |
| --- | --- |
| `bool TurnOff()` | Schaltet alle LEDs des Geräts aus. |
| `bool TurnOff(TargetedLeds targetedLeds)` | Schaltet die angesprochenen LEDs des Geräts aus. |

<!-- snippet: turn-off -->
```csharp
device.TurnOff();
device.TurnOff(TargetedLeds.BackSide);
```
<!-- endSnippet -->

## Eine Farbe festlegen

| Signatur | Beschreibung |
| --- | --- |
| `bool SetColor(BrightColor color)` | Schaltet alle LEDs des Geräts in einer Farbe ein. |
| `bool SetColor(TargetedLeds targetedLeds, BrightColor color)` | Schaltet die angesprochenen LEDs in einer Farbe ein. |

<!-- snippet: set-color -->
```csharp
device.SetColor(BrightColor.Green);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);
```
<!-- endSnippet -->

## Einen Übergang (Fade) durchführen

| Signatur | Beschreibung |
| --- | --- |
| `bool FadeColor(BrightColor color, FadeDuration duration)` | Lässt alle LEDs zu einer Farbe überblenden. |
| `bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration)` | Lässt die angesprochenen LEDs zu einer Farbe überblenden. |

Die Dauer wird in der Einheit des Geräts angegeben: `FadeDuration.From(byte)`, von `0` (sofort) bis `255` (am langsamsten).

<!-- snippet: fade-color -->
```csharp
device.FadeColor(BrightColor.Blue, FadeDuration.From(30));
device.FadeColor(TargetedLeds.BackSide, BrightColor.Blue, FadeDuration.From(30));
```
<!-- endSnippet -->

## Blinken (Stroboskopeffekt)

| Signatur | Beschreibung |
| --- | --- |
| `bool Strobe(BrightColor color, Speed speed, Repeat repeat)` | Lässt alle LEDs in einer Farbe blinken. |
| `bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat)` | Lässt die angesprochenen LEDs in einer Farbe blinken. |

Die Geschwindigkeit wird ebenfalls in der Einheit des Geräts angegeben: `Speed.FromByte(byte)`. Die Anzahl der Wiederholungen wird mit `Repeat.Once`, `Repeat.Twice` oder `Repeat.Count(byte)` festgelegt.

<!-- snippet: strobe -->
```csharp
device.Strobe(BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
device.Strobe(TargetedLeds.TabSide, BrightColor.Yellow, Speed.FromByte(20), Repeat.Twice);
```
<!-- endSnippet -->

## Wellen und integrierte Muster

| Signatur | Beschreibung |
| --- | --- |
| `bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat)` | Startet eine farbige Welle auf allen LEDs des Geräts. |
| `bool PlayPattern(BuiltInPattern pattern, Repeat repeat)` | Startet ein im Gerät integriertes Muster. |

Die Wellen reichen von `WavePattern.Wave_1` bis `WavePattern.Wave_5`. Die integrierten Muster sind `BuiltInPattern.Pattern_1` bis `Pattern_5`, dazu `Rainbow`, `TrafficLight`, `Police` und `Off`.

<!-- snippet: play-pattern -->
```csharp
device.PlayPattern(WavePattern.Wave_1, BrightColor.Cyan, Speed.FromByte(10), Repeat.Count(5));
device.PlayPattern(BuiltInPattern.Police, Repeat.Twice);
```
<!-- endSnippet -->

## LEDs gezielt ansprechen

`TargetedLeds` bezeichnet die LEDs, die ein Befehl gleichzeitig ein-, ausschaltet oder animiert.

| Wert | Betroffene LEDs |
| --- | --- |
| `TargetedLeds.All` | Die sechs LEDs. |
| `TargetedLeds.TabSide` | Die LEDs Nr. 1, 2 und 3. |
| `TargetedLeds.BackSide` | Die LEDs Nr. 4, 5 und 6. |
| `TargetedLeds.Led_1` ... `TargetedLeds.Led_6` | Eine einzelne LED. |

Ein `LedIndex` (`LedIndex._1` bis `LedIndex._6` oder `LedIndex.From(byte)`) wird implizit in `TargetedLeds` umgewandelt, sodass ein Index dort übergeben werden kann, wo ein Ziel erwartet wird. Die übrigen Kombinationen lassen sich nicht ausdrücken: Das Gerät akzeptiert sie nicht. Dann sind mehrere aufeinanderfolgende Befehle nötig, um den Preis eines sichtbaren Welleneffekts, da das Schalten sequenziell wird.

<!-- snippet: targeted-leds -->
```csharp
device.SetColor(TargetedLeds.All, BrightColor.Black);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);   // LEDs n° 1, 2 and 3
device.SetColor(TargetedLeds.BackSide, BrightColor.Red);    // LEDs n° 4, 5 and 6
device.SetColor(TargetedLeds.Led_1, BrightColor.Blue);
device.SetColor(LedIndex.From(6), BrightColor.Blue);
```
<!-- endSnippet -->

## Farben

`BrightColor` bietet die Farben `Red`, `Green`, `Blue`, `Yellow`, `Cyan`, `Magenta`, `White` und `Black` oder wird aus einer hexadezimalen Darstellung (`#RRGGBB`) bzw. aus drei Anteilen erzeugt.

<!-- snippet: colors -->
```csharp
device.SetColor(BrightColor.Red);                    // Green, Blue, Yellow, Cyan, Magenta, White, Black
device.SetColor(BrightColor.From("#0F11A8"));        // from an hexadecimal representation
device.SetColor(BrightColor.From(15, 17, 168));      // from its red, green and blue components
```
<!-- endSnippet -->

`BrightColor.From(string)` löst eine `FormatException` aus, wenn die Zeichenkette keine gültige hexadezimale Darstellung ist.

## Wiederverwendbare Befehle

Ein `LightingCommand` beschreibt einen Befehl ein für alle Mal, um ihn später mit `Send` erneut abzuspielen.

| Fabrikmethode | Entspricht |
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
