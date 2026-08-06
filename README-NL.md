_[Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-EN.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)_

# Luxafor Device Controller

Een .Net-bibliotheek die een eenvoudige API biedt om Luxafor-apparaten aan te sturen.

## Luxafor

### Bedrijfsoverzicht

[Luxafor](https://luxafor.com) is een bedrijf dat producten ontwerpt en verkoopt voor kantoorproductiviteit, zoals beschikbaarheidsindicatoren en notificatiehulpmiddelen. 

Hun paradepaardje is een [LED beschikbaarheidsindicator](https://luxafor.com/product/flag) die kan worden geprogrammeerd om verschillende kleuren weer te geven, afhankelijk van de beschikbaarheidsstatus van de gebruiker. 

Het doel van Luxafor is om gebruikers een eenvoudige en effectieve manier te bieden om hun beschikbaarheid aan collega's kenbaar te maken en de communicatie en samenwerking op de werkplek te verbeteren.

### Snel overzicht van de apparaten

Hier is een niet-limitatieve lijst van [Luxafor-apparaten](https://luxafor.com/products):

- `Luxafor Flag`: een LED beschikbaarheidsindicator die de persoonlijke beschikbaarheid weergeeft
- `Luxafor Bluetooth`: een draadloze, softwaregestuurde LED beschikbaarheidsindicator die meldingen en persoonlijke beschikbaarheid weergeeft.
- `Luxafor Switch`: een draadloze, op afstand bediende beschikbaarheidsindicator die de beschikbaarheid van vergaderzalen en werkplekken in realtime weergeeft
- `Luxafor Cube`: een stand-alone LED beschikbaarheidsindicator die de beschikbaarheid van vergaderzalen weergeeft.
- `Luxafor Pomodoro-Timer`: een USB-gevoede LED-timer waarmee werk in kleinere sleuven kan worden verdeeld (zie [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro)).
- `Luxafor Orb`: een groothoek USB LED beschikbaarheidsindicator
- `Luxafor CO2 Monitor`: een sensor die de luchtkwaliteit van een ruimte analyseert en u waarschuwt wanneer deze geventileerd moet worden.
- `Luxafor Mute Button`: zet de microfoon aan/uit met één aanraking en geef aan of u beschikbaar bent met het rood/groen
- `Luxafor Colorblind Flag`: monochroom USB LED beschikbaarheidslicht elimineert afleidingen en verhoogt de productiviteit

### Integratie

Deze verschillende apparaten zijn ontworpen om handmatig ('mechanisch') aangestuurd te worden voor sommigen, semi-automatisch (handmatig aansturen via [software](https://luxaformanual.com)) / automatisch (integratie via [software](https://luxaformanual.com) met tools als Teams, Skype, Cisco, Zappier of via Webhook) voor anderen. 

## Presentatie van de bibliotheek

Deze bibliotheek is bedoeld om de integratie van USB LED-apparaten in uw interne toepassingen mogelijk te maken zonder de noodzaak om via de Luxafor-server (webhook) te gaan.

Ze is gebouwd voor `.NET Standard 2.0` en `.NET Framework 4.6.2` en is gebaseerd op de bibliotheek [HidLibrary](https://github.com/mikeobrien/HidLibrary) waarmee HID-compatibele USB-apparaten in .NET kunnen worden opgesomd en ermee kan worden gecommuniceerd.

> **Alleen Windows.** De apparaten worden aangestuurd via de HID-laag van Windows: het pakket kan op elk platform worden geïnstalleerd, maar de apparaten kunnen alleen onder Windows worden opgesomd en aangestuurd.

### Ondersteunde apparaten

De bibliotheek stuurt, via hun USB HID-protocol, de Luxafor-apparaten aan met vendor id `1240` (`0x04D8`) en product id `62322` (`0xF372`).

| Apparaat | Status |
| --- | --- |
| `Luxafor Orb` | **Getest**: het apparaat dat is gebruikt om de bibliotheek te ontwikkelen en te valideren (6 adresseerbare LED's). |
| `Luxafor Flag` | **Zou moeten werken, niet getest**: dezelfde identificatoren en hetzelfde verlichtingsprotocol (6 adresseerbare LED's). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Zouden moeten werken, niet getest**: de verlichtingscommando's zijn dezelfde; de opstelling van de LED's, hun aantal en de kleurweergave kunnen verschillen. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Niet ondersteund**: deze apparaten worden niet via dit USB HID-protocol aangestuurd. |

Feedback over een niet-getest apparaat is zeer welkom: [open gerust een issue](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

### Installatie

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

### Snelstart

De onderstaande code toont een voorbeeld van een basisgebruik van de bibliotheek om een [Luxafor Orb](https://luxafor.com/product/orb/) apparaat aan te sturen.

```csharp
[Fact]
public void french_sequence() {
    using ILuxaforDevice orb = Luxafor.GetDevices().First();
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

Regel 3 laat zien hoe u verbinding maakt met één enkele Orb die op de USB-poort van de machine is aangesloten. `ILuxaforDevice` implementeert `IDisposable`: de `using`-instructie geeft de handle van het apparaat aan het einde van het blok vrij.

### Een apparaat verkrijgen

```csharp
IEnumerable<ILuxaforDevice> GetDevices(); // Alle Luxafor-apparaten die op de USB-poorten zijn aangesloten (leeg als er geen is aangesloten)
ILuxaforDevice GetDevice(string devicePath); // Het Luxafor-apparaat op het opgegeven pad
```

`Luxafor.GetDevice` gooit een `LuxaforDeviceNotFoundException` wanneer er geen apparaat op het opgegeven pad wordt gevonden, wanneer het gevonden apparaat geen ondersteund Luxafor-apparaat is, of wanneer het niet meer is aangesloten.

```csharp
using ILuxaforDevice orb = Luxafor.GetDevice(@"\\?\hid#vid_04d8&pid_f372#...");
```

Ik zal snel alle mogelijke commando's doornemen die vanuit de `ILuxaforDevice` naar de apparaten kunnen worden gestuurd.

Elk commando geeft een `bool` terug: `true` wanneer het apparaat het commando heeft aanvaard, `false` wanneer het schrijven is mislukt (apparaat losgekoppeld, in gebruik door een andere toepassing, ...). Ongeldige argumenten gooien een uitzondering (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

### Zet uit

```csharp
bool TurnOff(); // Schakelt alle LEDs op het apparaat uit
bool TurnOff(TargetedLeds targetedLeds); // Schakel de doelapparaat-LED's uit.
```

### Stel een enkele kleur in

```csharp
bool SetColor(BrightColor color); // Schakelt de LED's van het apparaat in een aangepaste kleur in.
bool SetColor(TargetedLeds targetedLeds, BrightColor color); // Schakelt de doelapparaat-LED's in een aangepaste kleur in.
```

### Maak een overgang (fade)

```csharp
bool FadeColor(BrightColor color, FadeDuration duration); // Verandert alle LED's op het apparaat in een aangepaste kleur.
bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration); // Overgang van de doelapparaat-LED's naar een aangepaste kleur.
```

### Knipperen (stroboscoopeffect)

```csharp
bool Strobe(BrightColor color, Speed speed, Repeat repeat); // Alle LED's van het apparaat knipperen in een aangepaste kleur.
bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat); // De doelapparaat-LED's knipperen in een aangepaste kleur.
```

### Golven / Ingebouwde patronen

```csharp
bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat); // Start een golfpatroon dat zich richt op alle LED's op het apparaat op basis van een aangepaste kleur.
bool PlayPattern(BuiltInPattern pattern, Repeat repeat); // Start een ingebouwd patroon dat zich richt op alle LED's op het apparaat
```

### Stuur een commando

Het is mogelijk om aangepaste commando's te maken, `LightingCommand` genaamd, zodat ze in de code kunnen worden hergebruikt:

```csharp
var command = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

Met de methode `Send` kunt u deze commando's gebruiken.

```csharp
bool Send(LightingCommand command); // Stuur een commando naar het apparaat.
```

### Kleuren

```csharp
BrightColor.Red; // evenals Green, Blue, Yellow, Cyan, Magenta, White, Black
BrightColor.From("#0F11A8"); // Vanuit de hexadecimale weergave
BrightColor.From(15, 17, 168); // Vanuit de rode, groene en blauwe componenten
```

## De bibliotheek bouwen

```shell
dotnet build -c Release
dotnet test -c Release
dotnet pack Reefact.LuxaforLightingDeviceController -c Release -o artifacts
```

## Licentie

Deze bibliotheek wordt verspreid onder de [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE)-licentie.
