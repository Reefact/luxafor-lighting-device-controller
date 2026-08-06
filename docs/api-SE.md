_[Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api.md) - [English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-EN.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-NL.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-DE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-ES.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-GR.md)_

[← Tillbaka till README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md)

# API-referens

Allt ligger i en enda namnrymd, `Reefact.LuxaforLightingDeviceController`. Den statiska klassen `Luxafor` är ingången: den lämnar ut `ILuxaforDevice`-instanser, som kommandon sedan skickas till.

Exemplen på den här sidan hämtas från mappen [`samples`](https://github.com/Reefact/luxafor-lighting-device-controller/tree/main/samples), som CI:n kompilerar: de kan inte glida ifrån koden.

## Hämta en enhet

| Signatur | Beskrivning |
| --- | --- |
| `IEnumerable<ILuxaforDevice> Luxafor.GetDevices()` | Alla Luxafor-enheter som är anslutna till USB-portarna. Uppräkningen är tom när ingen är ansluten. |
| `ILuxaforDevice Luxafor.GetDevice(string devicePath)` | Luxafor-enheten på den angivna sökvägen. |

<!-- snippet: list-devices -->
```csharp
foreach (ILuxaforDevice device in Luxafor.GetDevices()) {
    using (device) {
        Console.Out.WriteLine(device.Description + " — " + device.Path);
    }
}
```
<!-- endSnippet -->

`ILuxaforDevice` implementerar `IDisposable`: `using`-satsen frigör enhetens handtag i slutet av blocket. En enhet visar också sin sökväg (`Path`) och sin beskrivning (`Description`), så som Windows rapporterar den.

`Luxafor.GetDevice` kastar ett `LuxaforDeviceNotFoundException` när ingen enhet hittas på den angivna sökvägen, när enheten som hittas där inte är en Luxafor-enhet som stöds, eller när den inte längre är ansluten. Egenskapen `DevicePath` på undantaget bär tillbaka den efterfrågade sökvägen.

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

## Resultatet av ett kommando

Varje kommando returnerar en `bool`: `true` när enheten har accepterat kommandot, `false` när skrivningen misslyckades (enheten är urkopplad, upptagen av ett annat program, ...). Ogiltiga argument kastar däremot ett undantag (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

<!-- snippet: command-result -->
```csharp
if (!device.SetColor(BrightColor.Red)) {
    Console.Error.WriteLine("The device refused the command: unplugged, or held by another application.");
}
```
<!-- endSnippet -->

## Släcka

| Signatur | Beskrivning |
| --- | --- |
| `bool TurnOff()` | Släcker alla lysdioder på enheten. |
| `bool TurnOff(TargetedLeds targetedLeds)` | Släcker de riktade lysdioderna på enheten. |

<!-- snippet: turn-off -->
```csharp
device.TurnOff();
device.TurnOff(TargetedLeds.BackSide);
```
<!-- endSnippet -->

## Ange en färg

| Signatur | Beskrivning |
| --- | --- |
| `bool SetColor(BrightColor color)` | Tänder alla lysdioder på enheten i en färg. |
| `bool SetColor(TargetedLeds targetedLeds, BrightColor color)` | Tänder de riktade lysdioderna i en färg. |

<!-- snippet: set-color -->
```csharp
device.SetColor(BrightColor.Green);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);
```
<!-- endSnippet -->

## Göra en övergång (toning)

| Signatur | Beskrivning |
| --- | --- |
| `bool FadeColor(BrightColor color, FadeDuration duration)` | Låter alla lysdioder övergå till en färg. |
| `bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration)` | Låter de riktade lysdioderna övergå till en färg. |

Tiden anges i enhetens egen enhet: `FadeDuration.From(byte)`, från `0` (omedelbart) till `255` (långsammast).

<!-- snippet: fade-color -->
```csharp
device.FadeColor(BrightColor.Blue, FadeDuration.From(30));
device.FadeColor(TargetedLeds.BackSide, BrightColor.Blue, FadeDuration.From(30));
```
<!-- endSnippet -->

## Blinkning (stroboskopeffekt)

| Signatur | Beskrivning |
| --- | --- |
| `bool Strobe(BrightColor color, Speed speed, Repeat repeat)` | Blinkar alla lysdioder i en färg. |
| `bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat)` | Blinkar de riktade lysdioderna i en färg. |

Hastigheten anges också i enhetens egen enhet: `Speed.FromByte(byte)`. Antalet upprepningar anges med `Repeat.Once`, `Repeat.Twice` eller `Repeat.Count(byte)`.

<!-- snippet: strobe -->
```csharp
device.Strobe(BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
device.Strobe(TargetedLeds.TabSide, BrightColor.Yellow, Speed.FromByte(20), Repeat.Twice);
```
<!-- endSnippet -->

## Vågor och inbyggda mönster

| Signatur | Beskrivning |
| --- | --- |
| `bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat)` | Startar en färgad våg på alla lysdioder på enheten. |
| `bool PlayPattern(BuiltInPattern pattern, Repeat repeat)` | Startar ett mönster som är inbyggt i enheten. |

Vågorna går från `WavePattern.Wave_1` till `WavePattern.Wave_5`. De inbyggda mönstren är `BuiltInPattern.Pattern_1` till `Pattern_5`, plus `Rainbow`, `TrafficLight`, `Police` och `Off`.

<!-- snippet: play-pattern -->
```csharp
device.PlayPattern(WavePattern.Wave_1, BrightColor.Cyan, Speed.FromByte(10), Repeat.Count(5));
device.PlayPattern(BuiltInPattern.Police, Repeat.Twice);
```
<!-- endSnippet -->

## Rikta lysdioder

`TargetedLeds` anger de lysdioder som ett kommando tänder, släcker eller animerar samtidigt.

| Värde | Berörda lysdioder |
| --- | --- |
| `TargetedLeds.All` | De sex lysdioderna. |
| `TargetedLeds.TabSide` | Lysdioderna nr 1, 2 och 3. |
| `TargetedLeds.BackSide` | Lysdioderna nr 4, 5 och 6. |
| `TargetedLeds.Led_1` ... `TargetedLeds.Led_6` | En enda lysdiod. |

Ett `LedIndex` (`LedIndex._1` till `LedIndex._6`, eller `LedIndex.From(byte)`) konverteras implicit till `TargetedLeds`, så att ett index kan skickas där ett mål förväntas. Övriga kombinationer går inte att uttrycka: enheten godtar dem inte. Då krävs flera kommandon i följd, till priset av en synlig krusningseffekt eftersom tändningen blir sekventiell.

<!-- snippet: targeted-leds -->
```csharp
device.SetColor(TargetedLeds.All, BrightColor.Black);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);   // LEDs n° 1, 2 and 3
device.SetColor(TargetedLeds.BackSide, BrightColor.Red);    // LEDs n° 4, 5 and 6
device.SetColor(TargetedLeds.Led_1, BrightColor.Blue);
device.SetColor(LedIndex.From(6), BrightColor.Blue);
```
<!-- endSnippet -->

## Färger

`BrightColor` erbjuder färgerna `Red`, `Green`, `Blue`, `Yellow`, `Cyan`, `Magenta`, `White` och `Black`, eller byggs från en hexadecimal representation (`#RRGGBB`) eller från tre komponenter.

<!-- snippet: colors -->
```csharp
device.SetColor(BrightColor.Red);                    // Green, Blue, Yellow, Cyan, Magenta, White, Black
device.SetColor(BrightColor.From("#0F11A8"));        // from an hexadecimal representation
device.SetColor(BrightColor.From(15, 17, 168));      // from its red, green and blue components
```
<!-- endSnippet -->

`BrightColor.From(string)` kastar ett `FormatException` när strängen inte är en giltig hexadecimal representation.

## Återanvändbara kommandon

Ett `LightingCommand` beskriver ett kommando en gång för alla, för att spela upp det igen med `Send`.

| Fabrik | Motsvarar |
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
