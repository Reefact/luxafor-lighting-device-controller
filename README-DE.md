_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-FR.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md) - [Wersja polska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-PL.md)_

# Controller für Luxafor-Geräte

Steuern Sie Ihre [Luxafor](https://luxafor.com)-Verfügbarkeitsanzeigen direkt aus Ihren eigenen .NET-Anwendungen heraus, über deren USB-HID-Protokoll: kein Luxafor-Server, kein Webhook, keine Fremdsoftware zu installieren.

- Eine direkte API: `SetColor`, `FadeColor`, `Strobe`, `PlayPattern`, bis hin zur Steuerung einzelner LEDs.
- `.NET Standard 2.0` und `.NET Framework 4.6.2`, mit einer einzigen Abhängigkeit ([HidLibrary](https://github.com/mikeobrien/HidLibrary)).
- **Nur Windows**: Die Geräte werden über den HID-Stack von Windows gesteuert. Das Paket lässt sich auf jeder Plattform installieren, aber die Geräte können nur unter Windows aufgelistet und gesteuert werden.

## Installation

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

## Schnellstart

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

`Luxafor.GetDevices()` listet die an den USB-Anschlüssen des Rechners angeschlossenen Luxafor-Geräte auf; die Aufzählung ist leer, wenn keines angeschlossen ist. `ILuxaforDevice` implementiert `IDisposable`: Die `using`-Anweisung gibt das Handle des Geräts am Ende des Blocks wieder frei. Jeder Befehl gibt einen `bool` zurück: `true`, wenn das Gerät den Befehl angenommen hat, `false`, wenn das Schreiben fehlgeschlagen ist.

## Kompatible Geräte

Die Bibliothek steuert über deren USB-HID-Protokoll die Luxafor-Geräte mit der Vendor-ID `1240` (`0x04D8`) und der Produkt-ID `62322` (`0xF372`).

| Gerät | Status |
| --- | --- |
| `Luxafor Orb` | **Getestet**: das Gerät, mit dem die Bibliothek entwickelt und validiert wurde (6 adressierbare LEDs). |
| `Luxafor Flag` | **Sollte funktionieren, nicht getestet**: gleiche Kennungen und gleiches Beleuchtungsprotokoll (6 adressierbare LEDs). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Sollten funktionieren, nicht getestet**: die Beleuchtungsbefehle sind dieselben; Anordnung und Anzahl der LEDs sowie die Farbwiedergabe können abweichen. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Nicht unterstützt**: Diese Geräte werden nicht über dieses USB-HID-Protokoll gesteuert. |

Rückmeldungen zu einem nicht getesteten Gerät sind sehr willkommen: Bitte [eröffnen Sie ein Issue](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

## Funktionen

- **Feste Farbe** (`SetColor`) aus einer benannten Farbe, einem Hexadezimalcode oder Rot-/Grün-/Blau-Anteilen.
- **Überblendung** (`FadeColor`) zu einer Farbe, über eine gewählte Übergangsdauer.
- **Blinken** (`Strobe`) mit gewählter Geschwindigkeit und gewählter Anzahl von Wiederholungen.
- **Muster** (`PlayPattern`): farbige Wellen oder im Gerät integrierte Muster (`Police`, `Rainbow`, `TrafficLight`, ...).
- **Ausschalten** (`TurnOff`), aller LEDs oder nur eines Teils davon.
- **LEDs gezielt ansprechen**: alle, eine Seite (`TabSide`, `BackSide`) oder eine einzelne LED (`Led_1` bis `Led_6`).
- **Wiederverwendbare Befehle**: `LightingCommand` beschreibt einen Befehl einmal, um ihn später mit `Send` erneut abzuspielen.

## Ausführliche Dokumentation

- [API-Referenz](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-DE.md): alle Befehle, ihre Parameter und ihre Fehler.
- [Luxafor, das Unternehmen und seine Geräte](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-DE.md): wozu diese Anzeigen dienen und welche diese Bibliothek steuert.
- [Änderungsprotokoll](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-DE.md) und [Beitragsleitfaden](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CONTRIBUTING.md).

## Lizenz

Diese Bibliothek wird unter der Lizenz [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE) vertrieben.
