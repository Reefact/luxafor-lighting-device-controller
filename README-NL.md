_[Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-EN.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)_

# Luxafor Device Controller

Stuur uw [Luxafor](https://luxafor.com) beschikbaarheidsindicatoren rechtstreeks aan vanuit uw eigen .NET-toepassingen, via hun USB HID-protocol: geen Luxafor-server, geen webhook, geen software van derden te installeren.

- Een directe API: `SetColor`, `FadeColor`, `Strobe`, `PlayPattern`, tot het aansturen van één LED tegelijk.
- `.NET Standard 2.0` en `.NET Framework 4.6.2`, met één enkele afhankelijkheid ([HidLibrary](https://github.com/mikeobrien/HidLibrary)).
- **Alleen Windows**: de apparaten worden aangestuurd via de HID-laag van Windows. Het pakket kan op elk platform worden geïnstalleerd, maar de apparaten kunnen alleen onder Windows worden opgesomd en aangestuurd.

## Installatie

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

## Snelstart

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

`Luxafor.GetDevices()` somt de Luxafor-apparaten op die op de USB-poorten van de machine zijn aangesloten; de opsomming is leeg wanneer er geen is aangesloten. `ILuxaforDevice` implementeert `IDisposable`: de `using`-instructie geeft de handle van het apparaat aan het einde van het blok vrij. Elk commando geeft een `bool` terug: `true` wanneer het apparaat het commando heeft aanvaard, `false` wanneer het schrijven is mislukt.

## Compatibele apparaten

De bibliotheek stuurt, via hun USB HID-protocol, de Luxafor-apparaten aan met vendor id `1240` (`0x04D8`) en product id `62322` (`0xF372`).

| Apparaat | Status |
| --- | --- |
| `Luxafor Orb` | **Getest**: het apparaat dat is gebruikt om de bibliotheek te ontwikkelen en te valideren (6 adresseerbare LED's). |
| `Luxafor Flag` | **Zou moeten werken, niet getest**: dezelfde identificatoren en hetzelfde verlichtingsprotocol (6 adresseerbare LED's). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Zouden moeten werken, niet getest**: de verlichtingscommando's zijn dezelfde; de opstelling van de LED's, hun aantal en de kleurweergave kunnen verschillen. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Niet ondersteund**: deze apparaten worden niet via dit USB HID-protocol aangestuurd. |

Feedback over een niet-getest apparaat is zeer welkom: [open gerust een issue](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

## Functionaliteiten

- **Vaste kleur** (`SetColor`) vanuit een benoemde kleur, een hexadecimale code of rode / groene / blauwe componenten.
- **Overgang** (`FadeColor`) naar een kleur, over een gekozen overgangsduur.
- **Knipperen** (`Strobe`) met een gekozen snelheid en een gekozen aantal herhalingen.
- **Patronen** (`PlayPattern`): gekleurde golven, of in het apparaat ingebouwde patronen (`Police`, `Rainbow`, `TrafficLight`, ...).
- **Uitschakelen** (`TurnOff`), alle LED's of slechts een deel ervan.
- **LED's richten**: alle, één zijde (`TabSide`, `BackSide`) of één specifieke LED (`Led_1` tot `Led_6`).
- **Herbruikbare commando's**: `LightingCommand` beschrijft een commando één keer, om het later met `Send` opnieuw af te spelen.

## Uitgebreide documentatie

- [API-referentie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-NL.md): alle commando's, hun parameters en hun fouten.
- [Luxafor, het bedrijf en zijn apparaten](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-NL.md): waar deze indicatoren voor dienen, en welke deze bibliotheek aanstuurt.
- [Wijzigingslogboek](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG.md) en [bijdragegids](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CONTRIBUTING.md).

## Licentie

Deze bibliotheek wordt verspreid onder de [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE)-licentie.
