_[Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api.md) - [English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-EN.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-SE.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-DE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-ES.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-GR.md)_

[← Terug naar de README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md)

# API-referentie

Alles zit in één namespace, `Reefact.LuxaforLightingDeviceController`. De statische klasse `Luxafor` is het startpunt: ze levert `ILuxaforDevice`-instanties op, waaraan vervolgens commando's worden gestuurd.

De voorbeelden op deze pagina komen uit de map [`samples`](https://github.com/Reefact/luxafor-lighting-device-controller/tree/main/samples), die door de CI wordt gecompileerd: ze kunnen niet uit de pas gaan lopen met de code.

## Een apparaat verkrijgen

| Signatuur | Beschrijving |
| --- | --- |
| `IEnumerable<ILuxaforDevice> Luxafor.GetDevices()` | Alle Luxafor-apparaten die op de USB-poorten zijn aangesloten. De opsomming is leeg wanneer er geen is aangesloten. |
| `ILuxaforDevice Luxafor.GetDevice(string devicePath)` | Het Luxafor-apparaat op het opgegeven pad. |

<!-- snippet: list-devices -->
```csharp
foreach (ILuxaforDevice device in Luxafor.GetDevices()) {
    using (device) {
        Console.Out.WriteLine(device.Description + " — " + device.Path);
    }
}
```
<!-- endSnippet -->

`ILuxaforDevice` implementeert `IDisposable`: de `using`-instructie geeft de handle van het apparaat aan het einde van het blok vrij. Een apparaat toont ook zijn pad (`Path`) en zijn beschrijving (`Description`), zoals door Windows gerapporteerd.

`Luxafor.GetDevice` gooit een `LuxaforDeviceNotFoundException` wanneer er geen apparaat op het opgegeven pad wordt gevonden, wanneer het gevonden apparaat geen ondersteund Luxafor-apparaat is, of wanneer het niet meer is aangesloten. De eigenschap `DevicePath` van de uitzondering geeft het gevraagde pad terug.

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

## Resultaat van een commando

Elk commando geeft een `bool` terug: `true` wanneer het apparaat het commando heeft aanvaard, `false` wanneer het schrijven is mislukt (apparaat losgekoppeld, in gebruik door een andere toepassing, ...). Ongeldige argumenten daarentegen gooien een uitzondering (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

<!-- snippet: command-result -->
```csharp
if (!device.SetColor(BrightColor.Red)) {
    Console.Error.WriteLine("The device refused the command: unplugged, or held by another application.");
}
```
<!-- endSnippet -->

## Uitschakelen

| Signatuur | Beschrijving |
| --- | --- |
| `bool TurnOff()` | Schakelt alle LED's van het apparaat uit. |
| `bool TurnOff(TargetedLeds targetedLeds)` | Schakelt de gerichte LED's van het apparaat uit. |

<!-- snippet: turn-off -->
```csharp
device.TurnOff();
device.TurnOff(TargetedLeds.BackSide);
```
<!-- endSnippet -->

## Een kleur instellen

| Signatuur | Beschrijving |
| --- | --- |
| `bool SetColor(BrightColor color)` | Schakelt alle LED's van het apparaat in een kleur in. |
| `bool SetColor(TargetedLeds targetedLeds, BrightColor color)` | Schakelt de gerichte LED's in een kleur in. |

<!-- snippet: set-color -->
```csharp
device.SetColor(BrightColor.Green);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);
```
<!-- endSnippet -->

## Een overgang maken (fade)

| Signatuur | Beschrijving |
| --- | --- |
| `bool FadeColor(BrightColor color, FadeDuration duration)` | Laat alle LED's naar een kleur overgaan. |
| `bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration)` | Laat de gerichte LED's naar een kleur overgaan. |

De duur wordt uitgedrukt in de eenheid van het apparaat: `FadeDuration.From(byte)`, van `0` (onmiddellijk) tot `255` (het traagst).

<!-- snippet: fade-color -->
```csharp
device.FadeColor(BrightColor.Blue, FadeDuration.From(30));
device.FadeColor(TargetedLeds.BackSide, BrightColor.Blue, FadeDuration.From(30));
```
<!-- endSnippet -->

## Knipperen (stroboscoopeffect)

| Signatuur | Beschrijving |
| --- | --- |
| `bool Strobe(BrightColor color, Speed speed, Repeat repeat)` | Laat alle LED's in een kleur knipperen. |
| `bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat)` | Laat de gerichte LED's in een kleur knipperen. |

De snelheid wordt eveneens uitgedrukt in de eenheid van het apparaat: `Speed.FromByte(byte)`. Het aantal herhalingen wordt aangegeven met `Repeat.Once`, `Repeat.Twice` of `Repeat.Count(byte)`.

<!-- snippet: strobe -->
```csharp
device.Strobe(BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
device.Strobe(TargetedLeds.TabSide, BrightColor.Yellow, Speed.FromByte(20), Repeat.Twice);
```
<!-- endSnippet -->

## Golven en ingebouwde patronen

| Signatuur | Beschrijving |
| --- | --- |
| `bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat)` | Start een gekleurde golf op alle LED's van het apparaat. |
| `bool PlayPattern(BuiltInPattern pattern, Repeat repeat)` | Start een in het apparaat ingebouwd patroon. |

De golven lopen van `WavePattern.Wave_1` tot `WavePattern.Wave_5`. De ingebouwde patronen zijn `BuiltInPattern.Pattern_1` tot `Pattern_5`, plus `Rainbow`, `TrafficLight`, `Police` en `Off`.

<!-- snippet: play-pattern -->
```csharp
device.PlayPattern(WavePattern.Wave_1, BrightColor.Cyan, Speed.FromByte(10), Repeat.Count(5));
device.PlayPattern(BuiltInPattern.Police, Repeat.Twice);
```
<!-- endSnippet -->

## LED's richten

`TargetedLeds` duidt de LED's aan die een commando gelijktijdig aan-, uitzet of animeert.

| Waarde | Betrokken LED's |
| --- | --- |
| `TargetedLeds.All` | De zes LED's. |
| `TargetedLeds.TabSide` | De LED's nr. 1, 2 en 3. |
| `TargetedLeds.BackSide` | De LED's nr. 4, 5 en 6. |
| `TargetedLeds.Led_1` ... `TargetedLeds.Led_6` | Eén enkele LED. |

Een `LedIndex` (`LedIndex._1` tot `LedIndex._6`, of `LedIndex.From(byte)`) wordt impliciet omgezet naar `TargetedLeds`, zodat een index kan worden doorgegeven waar een doel wordt verwacht. De overige combinaties zijn niet uit te drukken: het apparaat aanvaardt ze niet. Dan zijn meerdere opeenvolgende commando's nodig, ten koste van een zichtbaar rimpeleffect omdat het aansturen sequentieel wordt.

<!-- snippet: targeted-leds -->
```csharp
device.SetColor(TargetedLeds.All, BrightColor.Black);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);   // LEDs n° 1, 2 and 3
device.SetColor(TargetedLeds.BackSide, BrightColor.Red);    // LEDs n° 4, 5 and 6
device.SetColor(TargetedLeds.Led_1, BrightColor.Blue);
device.SetColor(LedIndex.From(6), BrightColor.Blue);
```
<!-- endSnippet -->

## Kleuren

`BrightColor` biedt de kleuren `Red`, `Green`, `Blue`, `Yellow`, `Cyan`, `Magenta`, `White` en `Black`, of wordt opgebouwd uit een hexadecimale weergave (`#RRGGBB`) of uit drie componenten.

<!-- snippet: colors -->
```csharp
device.SetColor(BrightColor.Red);                    // Green, Blue, Yellow, Cyan, Magenta, White, Black
device.SetColor(BrightColor.From("#0F11A8"));        // from an hexadecimal representation
device.SetColor(BrightColor.From(15, 17, 168));      // from its red, green and blue components
```
<!-- endSnippet -->

`BrightColor.From(string)` gooit een `FormatException` wanneer de tekenreeks geen geldige hexadecimale weergave is.

## Herbruikbare commando's

Een `LightingCommand` beschrijft een commando eens en voor altijd, om het later met `Send` opnieuw af te spelen.

| Fabriek | Gelijk aan |
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
