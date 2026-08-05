_[Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-EN.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)_

# Controller für Luxafor-Geräte

Eine .Net-Bibliothek, die eine einfache API zur Steuerung von Luxafor-Geräten bietet.

## Luxafor

### Vorstellung des Unternehmens

[Luxafor](https://luxafor.com) ist ein Unternehmen, das Produkte für die Büroproduktivität entwickelt und verkauft, wie z. B. Verfügbarkeitsanzeigen und Benachrichtigungstools. 

Ihr Vorzeigeprodukt ist ein [LED-Verfügbarkeitsindikator](https://luxafor.com/product/flag), der so programmiert werden kann, dass er je nach Verfügbarkeitsstatus des Nutzers unterschiedliche Farben anzeigt. 

Das Ziel von Luxafor ist es, Nutzern eine einfache und effektive Möglichkeit zu bieten, Arbeitskollegen ihre Verfügbarkeit zu signalisieren und die Kommunikation und Zusammenarbeit im Unternehmen zu verbessern.

### Ein kurzer Überblick über die Geräte.

Hier ist eine nicht erschöpfende Liste der [Luxafor-Geräte](https://luxafor.com/products):

- `Luxafor Flag`: Eine LED-Anzeige, die die persönliche Verfügbarkeit anzeigt.
- `Luxafor Bluetooth`: Eine drahtlose, softwaregesteuerte LED-Verfügbarkeitsanzeige, die Benachrichtigungen und die persönliche Verfügbarkeit anzeigt.
- `Luxafor Switch`: Eine drahtlose, ferngesteuerte Verfügbarkeitsanzeige, die die Verfügbarkeit von Besprechungsräumen und Arbeitsplätzen in Echtzeit anzeigt.
- `Luxafor Cube`: Eine eigenständige LED-Verfügbarkeitsanzeige, die die Verfügbarkeit von Besprechungsräumen anzeigt.
- `Luxafor Pomodoro-Timer`: ein USB-betriebener LED-Timer, der die Arbeit in kleine Zeitfenster aufteilt (siehe [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: eine USB-LED-Weitwinkel-Verfügbarkeitsanzeige
- `Luxafor CO2 Monitor`: ein Sensor, der die Luftqualität in einem Raum analysiert und Sie warnt, wenn Sie den Raum lüften müssen.
- `Luxafor Mute Button`: Schalten Sie das Mikrofon mit einem einfachen Druck an/aus und zeigen Sie mit Rot/Grün an, ob Sie verfügbar sind.
- `Luxafor Colorblind Flag`: Einfarbiges USB-LED-Bereitschafts- und Besetzungslicht, das Ablenkungen eliminiert und die Produktivität steigert.

### Integration

Diese verschiedenen Geräte sind so konzipiert, dass sie teilweise manuell ('mechanisch'), teilweise halbautomatisch (manuelle Steuerung über [Software](https://luxaformanual.com)) / automatisch (Integration über [Software](https://luxaformanual.com) in Tools wie Teams, Skype, Cisco, Zappier oder über Webhook) gesteuert werden können. 

## Überblick über die Bibliothek

Diese Bibliothek soll die Integration von USB-LED-Geräten in Ihre In-House-Anwendungen ermöglichen, ohne dass Sie den Luxafor-Server (Webhook) nutzen müssen.

Sie zielt auf `.NET Standard 2.0` und `.NET Framework 4.6.2` ab und basiert auf der Bibliothek [HidLibrary](https://github.com/mikeobrien/HidLibrary), die es ermöglicht, HID-kompatible USB-Geräte in .NET aufzulisten und mit ihnen zu kommunizieren.

> **Nur Windows.** Die Geräte werden über den HID-Stack von Windows gesteuert: Das Paket lässt sich auf jeder Plattform installieren, aber die Geräte können nur unter Windows aufgelistet und gesteuert werden.

### Unterstützte Geräte

Die Bibliothek spricht das USB-HID-Protokoll der Luxafor-Geräte mit der Vendor-ID `1240` (`0x04D8`) und der Produkt-ID `62322` (`0xF372`).

| Gerät | Status |
| --- | --- |
| `Luxafor Orb` | **Getestet**: das Gerät, mit dem die Bibliothek entwickelt und validiert wurde (6 adressierbare LEDs). |
| `Luxafor Flag` | **Sollte funktionieren, nicht getestet**: gleiche Kennungen und gleiches Beleuchtungsprotokoll (6 adressierbare LEDs). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Sollten funktionieren, nicht getestet**: die Beleuchtungsbefehle sind dieselben; Anordnung und Anzahl der LEDs sowie die Farbwiedergabe können abweichen. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Nicht unterstützt**: Diese Geräte werden nicht über dieses USB-HID-Protokoll gesteuert. |

Rückmeldungen zu einem nicht getesteten Gerät sind sehr willkommen: Bitte [eröffnen Sie ein Issue](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

### Installation

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

### Schnellstart

Der folgende Code zeigt ein Beispiel für die grundlegende Verwendung der Bibliothek zur Steuerung eines [Luxafor Orb](https://luxafor.com/product/orb/)-Geräts.

```csharp
[Fact]
public void french_sequence() {
    using LuxaforDevice orb = Luxafor.GetDevices().First();
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

Zeile 3 zeigt, wie man sich mit einem einzelnen Orb verbindet, der am USB-Anschluss des Rechners angeschlossen ist. `LuxaforDevice` implementiert `IDisposable`: Die `using`-Anweisung gibt das Handle des Geräts am Ende des Blocks wieder frei.

### Ein Gerät abrufen

```csharp
IEnumerable<LuxaforDevice> GetDevices(); // Alle an die USB-Anschlüsse angeschlossenen Luxafor-Geräte (leer, wenn keines angeschlossen ist)
LuxaforDevice GetDevice(string devicePath); // Das Luxafor-Gerät unter dem angegebenen Pfad
```

`Luxafor.GetDevice` löst eine `LuxaforDeviceNotFoundException` aus, wenn unter dem angegebenen Pfad kein Gerät gefunden wird, wenn das dort gefundene Gerät kein unterstütztes Luxafor-Gerät ist oder wenn es nicht mehr angeschlossen ist.

```csharp
using LuxaforDevice orb = Luxafor.GetDevice(@"\\?\hid#vid_04d8&pid_f372#...");
```

Ich werde nun kurz alle Befehle vorstellen, die über das `LuxaforDevice` an die Geräte gesendet werden können.

Jeder Befehl gibt einen `bool` zurück: `true`, wenn das Gerät den Befehl angenommen hat, `false`, wenn das Schreiben fehlgeschlagen ist (Gerät abgezogen, von einer anderen Anwendung belegt, ...). Ungültige Argumente lösen eine Ausnahme aus (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

### Ausschalten

```csharp
bool TurnOff(); // Schaltet alle LEDs des Geräts aus.
bool TurnOff(TargetedLeds targetedLeds); // Schaltet die LEDs des Zielgeräts aus.
```

### Definieren Sie eine einzelne Farbe.

```csharp
bool SetColor(BrightColor color); // Schaltet die LEDs des Geräts in einer benutzerdefinierten Farbe ein.
bool SetColor(TargetedLeds targetedLeds, BrightColor color); // Schaltet die LEDs des Zielgeräts in einer benutzerdefinierten Farbe ein.
```

### Einen Übergang (Fade) durchführen.

```csharp
bool FadeColor(BrightColor color, FadeDuration duration); // Alle LEDs des Geräts werden in eine benutzerdefinierte Farbe umgewandelt.
bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration); // Überblendet die LEDs des Zielgeräts in eine benutzerdefinierte Farbe.
```

### Blinken (Stroboskopeffekt)

```csharp
bool Strobe(BrightColor color, Speed speed, Repeat repeat); // Lässt alle LEDs des Geräts in einer benutzerdefinierten Farbe blinken.
bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat); // Lässt die LEDs des Zielgeräts in einer benutzerdefinierten Farbe blinken.
```

### Wellen / Integrierte Muster

```csharp
bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat); // Startet ein wellenförmiges Muster, das alle LEDs des Geräts auf der Grundlage einer benutzerdefinierten Farbe anvisiert.
bool PlayPattern(BuiltInPattern pattern, Repeat repeat); // Startet ein eingebettetes Muster, das auf alle LEDs des Geräts zielt.
```

### Einen Befehl senden

Es ist möglich, benutzerdefinierte Befehle namens `LightingCommand` zu erstellen, um sie im Code wiederverwenden zu können:

```csharp
var command = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

Mit der Methode `Send` können Sie diese Befehle verwenden.

```csharp
bool Send(LightingCommand command); // Sendet einen Befehl an das Gerät.
```

### Farben

```csharp
BrightColor.Red; // sowie Green, Blue, Yellow, Cyan, Magenta, White, Black
BrightColor.From("#0F11A8"); // Aus der hexadezimalen Darstellung
BrightColor.From(15, 17, 168); // Aus den Rot-, Grün- und Blauanteilen
```

## Die Bibliothek bauen

```shell
dotnet build -c Release
dotnet test -c Release
dotnet pack Reefact.LuxaforLightingDeviceController -c Release -o artifacts
```

## Lizenz

Diese Bibliothek wird unter der Lizenz [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE) vertrieben.
