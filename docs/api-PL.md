_[English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-EN.md) - [Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-NL.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-SE.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-DE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-ES.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-GR.md)_

[← Powrót do README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-PL.md)

# Dokumentacja API

Wszystko mieści się w jednej przestrzeni nazw, `Reefact.LuxaforLightingDeviceController`. Klasa statyczna `Luxafor` jest punktem wejścia: wydaje instancje `ILuxaforDevice`, do których następnie wysyła się polecenia.

Przykłady na tej stronie pochodzą z katalogu [`samples`](https://github.com/Reefact/luxafor-lighting-device-controller/tree/main/samples), który kompiluje CI: nie mogą rozejść się z kodem.

## Pobranie urządzenia

| Sygnatura | Opis |
| --- | --- |
| `IEnumerable<ILuxaforDevice> Luxafor.GetDevices()` | Wszystkie urządzenia Luxafor podłączone do portów USB. Wyliczenie jest puste, gdy żadne nie jest podłączone. |
| `ILuxaforDevice Luxafor.GetDevice(string devicePath)` | Urządzenie Luxafor znajdujące się pod wskazaną ścieżką. |

<!-- snippet: list-devices -->
```csharp
foreach (ILuxaforDevice device in Luxafor.GetDevices()) {
    using (device) {
        Console.Out.WriteLine(device.Description + " — " + device.Path);
    }
}
```
<!-- endSnippet -->

`ILuxaforDevice` implementuje `IDisposable`: instrukcja `using` zwalnia uchwyt urządzenia na końcu bloku. Urządzenie udostępnia też swoją ścieżkę (`Path`) i opis (`Description`) w postaci zgłaszanej przez Windows.

`Luxafor.GetDevice` rzuca `LuxaforDeviceNotFoundException`, gdy pod wskazaną ścieżką nie ma urządzenia, gdy znalezione urządzenie nie jest obsługiwanym urządzeniem Luxafor albo gdy nie jest już podłączone. Właściwość `DevicePath` wyjątku zwraca żądaną ścieżkę.

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

## Wynik polecenia

Każde polecenie zwraca `bool`: `true`, gdy urządzenie je przyjęło, `false`, gdy zapis się nie powiódł (urządzenie odłączone, zajęte przez inną aplikację, ...). Nieprawidłowe argumenty natomiast rzucają wyjątek (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

<!-- snippet: command-result -->
```csharp
if (!device.SetColor(BrightColor.Red)) {
    Console.Error.WriteLine("The device refused the command: unplugged, or held by another application.");
}
```
<!-- endSnippet -->

## Wygaszanie

| Sygnatura | Opis |
| --- | --- |
| `bool TurnOff()` | Gasi wszystkie diody urządzenia. |
| `bool TurnOff(TargetedLeds targetedLeds)` | Gasi wskazane diody urządzenia. |

<!-- snippet: turn-off -->
```csharp
device.TurnOff();
device.TurnOff(TargetedLeds.BackSide);
```
<!-- endSnippet -->

## Ustawienie koloru

| Sygnatura | Opis |
| --- | --- |
| `bool SetColor(BrightColor color)` | Zapala wszystkie diody urządzenia w danym kolorze. |
| `bool SetColor(TargetedLeds targetedLeds, BrightColor color)` | Zapala wskazane diody w danym kolorze. |

<!-- snippet: set-color -->
```csharp
device.SetColor(BrightColor.Green);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);
```
<!-- endSnippet -->

## Przejście (fade)

| Sygnatura | Opis |
| --- | --- |
| `bool FadeColor(BrightColor color, FadeDuration duration)` | Przeprowadza wszystkie diody do danego koloru. |
| `bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration)` | Przeprowadza wskazane diody do danego koloru. |

Czas trwania wyraża się w jednostce urządzenia: `FadeDuration.From(byte)`, od `0` (natychmiast) do `255` (najwolniej).

<!-- snippet: fade-color -->
```csharp
device.FadeColor(BrightColor.Blue, FadeDuration.From(30));
device.FadeColor(TargetedLeds.BackSide, BrightColor.Blue, FadeDuration.From(30));
```
<!-- endSnippet -->

## Miganie (efekt stroboskopowy)

| Sygnatura | Opis |
| --- | --- |
| `bool Strobe(BrightColor color, Speed speed, Repeat repeat)` | Miga wszystkimi diodami w danym kolorze. |
| `bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat)` | Miga wskazanymi diodami w danym kolorze. |

Prędkość również wyraża się w jednostce urządzenia: `Speed.FromByte(byte)`. Liczbę powtórzeń deklaruje się przez `Repeat.Once`, `Repeat.Twice` albo `Repeat.Count(byte)`.

<!-- snippet: strobe -->
```csharp
device.Strobe(BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
device.Strobe(TargetedLeds.TabSide, BrightColor.Yellow, Speed.FromByte(20), Repeat.Twice);
```
<!-- endSnippet -->

## Fale i wzory wbudowane

| Sygnatura | Opis |
| --- | --- |
| `bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat)` | Uruchamia kolorową falę na wszystkich diodach urządzenia. |
| `bool PlayPattern(BuiltInPattern pattern, Repeat repeat)` | Uruchamia wzór wbudowany w urządzenie. |

Fale idą od `WavePattern.Wave_1` do `WavePattern.Wave_5`. Wzory wbudowane to `BuiltInPattern.Pattern_1` do `Pattern_5`, a także `Rainbow`, `TrafficLight`, `Police` i `Off`.

<!-- snippet: play-pattern -->
```csharp
device.PlayPattern(WavePattern.Wave_1, BrightColor.Cyan, Speed.FromByte(10), Repeat.Count(5));
device.PlayPattern(BuiltInPattern.Police, Repeat.Twice);
```
<!-- endSnippet -->

## Wskazywanie diod

`TargetedLeds` określa diody, które polecenie zapala, gasi lub animuje jednocześnie.

| Wartość | Diody, których dotyczy |
| --- | --- |
| `TargetedLeds.All` | Wszystkie sześć diod. |
| `TargetedLeds.TabSide` | Diody nr 1, 2 i 3. |
| `TargetedLeds.BackSide` | Diody nr 4, 5 i 6. |
| `TargetedLeds.Led_1` ... `TargetedLeds.Led_6` | Pojedyncza dioda. |

`LedIndex` (`LedIndex._1` do `LedIndex._6` albo `LedIndex.From(byte)`) konwertuje się niejawnie na `TargetedLeds`, dzięki czemu indeks można podać tam, gdzie oczekiwany jest cel. Pozostałych kombinacji nie da się wyrazić: urządzenie ich nie przyjmuje. Trzeba wtedy wysłać kilka poleceń po kolei, kosztem widocznego efektu falowania, ponieważ zapalanie staje się sekwencyjne.

<!-- snippet: targeted-leds -->
```csharp
device.SetColor(TargetedLeds.All, BrightColor.Black);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);   // LEDs n° 1, 2 and 3
device.SetColor(TargetedLeds.BackSide, BrightColor.Red);    // LEDs n° 4, 5 and 6
device.SetColor(TargetedLeds.Led_1, BrightColor.Blue);
device.SetColor(LedIndex.From(6), BrightColor.Blue);
```
<!-- endSnippet -->

## Kolory

`BrightColor` udostępnia kolory `Red`, `Green`, `Blue`, `Yellow`, `Cyan`, `Magenta`, `White` i `Black`, albo powstaje z reprezentacji szesnastkowej (`#RRGGBB`) lub z trzech składowych.

<!-- snippet: colors -->
```csharp
device.SetColor(BrightColor.Red);                    // Green, Blue, Yellow, Cyan, Magenta, White, Black
device.SetColor(BrightColor.From("#0F11A8"));        // from an hexadecimal representation
device.SetColor(BrightColor.From(15, 17, 168));      // from its red, green and blue components
```
<!-- endSnippet -->

`BrightColor.From(string)` rzuca `FormatException`, gdy łańcuch nie jest poprawną reprezentacją szesnastkową.

## Polecenia wielokrotnego użytku

`LightingCommand` opisuje polecenie raz na zawsze, aby odtworzyć je później przez `Send`.

| Fabryka | Odpowiada |
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
