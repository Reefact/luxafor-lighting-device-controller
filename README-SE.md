_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-FR.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md) - [Wersja polska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-PL.md)_

# Luxafor Device Controller

Styr dina [Luxafor](https://luxafor.com)-tillgänglighetsindikatorer direkt från dina egna .NET-applikationer, via deras USB HID-protokoll: ingen Luxafor-server, ingen webhook, ingen programvara från tredje part att installera.

- Ett direkt API: `SetColor`, `FadeColor`, `Strobe`, `PlayPattern`, ända ner till att styra en lysdiod i taget.
- `.NET Standard 2.0` och `.NET Framework 4.6.2`, med ett enda beroende ([HidLibrary](https://github.com/mikeobrien/HidLibrary)).
- **Endast Windows**: enheterna styrs via Windows HID-lager. Paketet kan installeras på alla plattformar, men enheterna kan endast räknas upp och styras under Windows.

## Installation

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

## Snabbstart

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

`Luxafor.GetDevices()` räknar upp de Luxafor-enheter som är anslutna till maskinens USB-portar; uppräkningen är tom när ingen är ansluten. `ILuxaforDevice` implementerar `IDisposable`: `using`-satsen frigör enhetens handtag i slutet av blocket. Varje kommando returnerar en `bool`: `true` när enheten har accepterat kommandot, `false` när skrivningen misslyckades.

## Kompatibla enheter

Biblioteket styr, via deras USB HID-protokoll, de Luxafor-enheter som identifieras av vendor id `1240` (`0x04D8`) och product id `62322` (`0xF372`).

| Enhet | Status |
| --- | --- |
| `Luxafor Orb` | **Testad**: den enhet som användes för att utveckla och validera biblioteket (6 adresserbara lysdioder). |
| `Luxafor Flag` | **Bör fungera, inte testad**: samma identifierare och samma belysningsprotokoll (6 adresserbara lysdioder). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Bör fungera, inte testade**: belysningskommandona är desamma; lysdiodernas placering, antal och färgåtergivning kan skilja sig åt. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Stöds inte**: dessa enheter styrs inte via detta USB HID-protokoll. |

Återkoppling om en enhet som inte testats är mycket välkommen: [öppna gärna ett ärende](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

## Funktioner

- **Fast färg** (`SetColor`) från en namngiven färg, en hexadecimal kod eller röda / gröna / blå komponenter.
- **Toning** (`FadeColor`) till en färg, över en vald övergångstid.
- **Blinkning** (`Strobe`) med vald hastighet och valt antal upprepningar.
- **Mönster** (`PlayPattern`): färgade vågor, eller mönster inbyggda i enheten (`Police`, `Rainbow`, `TrafficLight`, ...).
- **Släckning** (`TurnOff`), av alla lysdioder eller bara några av dem.
- **Rikta lysdioder**: alla, en sida (`TabSide`, `BackSide`) eller en enskild lysdiod (`Led_1` till `Led_6`).
- **Återanvändbara kommandon**: `LightingCommand` beskriver ett kommando en gång, för att spela upp det igen med `Send`.

## Utförlig dokumentation

- [API-referens](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-SE.md): alla kommandon, deras parametrar och deras fel.
- [Luxafor, företaget och dess enheter](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-SE.md): vad dessa indikatorer är till för, och vilka det här biblioteket styr.
- [Ändringslogg](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-SE.md) och [bidragsguide](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CONTRIBUTING.md).

## Licens

Detta bibliotek distribueras under licensen [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE).
