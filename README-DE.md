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

Sie wurde in .Net Core entwickelt und basiert auf der [HidLibrairy]-Bibliothek (https://github.com/mikeobrien/HidLibrary), die es ermöglicht, HID-kompatible USB-Geräte in .NET aufzulisten und mit ihnen zu kommunizieren.

Der folgende Code zeigt ein Beispiel für die grundlegende Verwendung der Bibliothek zur Steuerung eines [Luxafor Orb](https://luxafor.com/product/orb/)-Geräts.

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController.UnitTests/UsageExamples.cs#L20-L32

Zeile 21 zeigt, wie man sich mit einem einzelnen Orb verbindet, der an den USB-Anschluss des Geräts angeschlossen ist.

Ich werde kurz die Gesamtheit der möglichen Befehle vorstellen, die vom `LuxaforDevice` aus an die Geräte gesendet werden können.

### Ausschalten

```csharp
void TurnOff(); // Schaltet alle LEDs des Geräts aus.
void TurnOff(TargetedLeds targetedLeds); // Schaltet die LEDs des Zielgeräts aus.
```

### Definieren Sie eine einzelne Farbe.

```csharp
void SetColor(BasicColor basicColor); // Schaltet alle LEDs des Geräts in einer Grundfarbe ein.
void SetColor(CustomColor customColor); // Schaltet die LEDs des Geräts in einer benutzerdefinierten Farbe ein.
void SetColor(TargetedLeds targetedLeds, BasicColor basicColor); // Schaltet alle Ziel-LEDs des Geräts in einer Grundfarbe ein.
void SetColor(TargetedLeds targetedLeds, CustomColor color); // Schaltet die LEDs des Zielgeräts in einer benutzerdefinierten Farbe ein.
```

### Einen Übergang (Fade) durchführen.

```csharp
void FadeColor(BasicColor basicColor, FadeDuration duration); // Überblenden Sie alle LEDs des Geräts in eine Grundfarbe.
void FadeColor(CustomColor color, FadeDuration duration); // Alle LEDs des Geräts werden in eine benutzerdefinierte Farbe umgewandelt.
void FadeColor(TargetedLeds targetedLeds, BasicColor basicColor, FadeDuration duration); // Führt einen Übergang der Ziel-LEDs des Geräts zu einer Grundfarbe durch.
void FadeColor(TargetedLeds targetedLeds, CustomColor color, FadeDuration duration); // Überblendet die LEDs des Zielgeräts in eine benutzerdefinierte Farbe.
```

### Blinken (Stroboskopeffekt)

````csharp
void Strobe(BasicColor basicColor, Speed speed, Repeat repeat); // Lässt alle LEDs des Geräts in einer Grundfarbe blinken.
void Strobe(CustomColor customColor, Speed speed, Repeat repeat); // Lässt alle LEDs des Geräts in einer benutzerdefinierten Farbe blinken.
void Strobe(TargetedLeds targetedLeds, BasicColor basicColor, Speed speed, Repeat repeat); // Lässt die LEDs des Zielgeräts in einer Grundfarbe blinken.
void Strobe(TargetedLeds targetedLeds, CustomColor customColor, Speed speed, Repeat repeat); // Lässt die LEDs des Zielgeräts in einer benutzerdefinierten Farbe blinken.
```

### Wellen

```csharp
void Wave(WaveType waveType, BasicColor basicColor, Speed speed, Repeat repeat); // Startet ein Wellenmuster, das alle LEDs des Geräts basierend auf einer Grundfarbe anvisiert.
void Wave(WaveType waveType, CustomColor customColor, Speed speed, Repeat repeat); // Startet ein wellenförmiges Muster, das alle LEDs des Geräts auf der Grundlage einer benutzerdefinierten Farbe anvisiert.
```

### Integrierte Muster

```csharp
void PlayPattern(BuiltInPattern pattern, Repeat repeat); // Startet ein eingebettetes Muster, das auf alle LEDs des Geräts zielt.
```

### Einen Befehl senden

Es ist möglich, eigene Befehle mit dem Namen ``LightningCommand`` zu erstellen, um sie im Code wiederverwenden zu können:

```csharp
var command = LightningCommand.CreateStrobeCommand(TargetedLeds.All, BasicColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

Mit der Methode `Send` können Sie diese Befehle verwenden.

```csharp
void Send(LightningCommand command); // Sendet einen Befehl an das Gerät.
```
