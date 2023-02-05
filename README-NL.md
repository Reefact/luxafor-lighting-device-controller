# Luxafor Device Controller

Een .Net-bibliotheek die een eenvoudige API biedt om Luxafor-apparaten aan te sturen.

## Luxafor

### Bedrijfsoverzicht

[Luxafor](https://luxafor.com) is een bedrijf dat producten ontwerpt en verkoopt voor kantoorproductiviteit, zoals beschikbaarheidsindicatoren en notificatiehulpmiddelen. 

Hun paradepaardje is een [LED beschikbaarheidsindicator] (https://luxafor.com/product/flag) die kan worden geprogrammeerd om verschillende kleuren weer te geven, afhankelijk van de beschikbaarheidsstatus van de gebruiker. 

Het doel van Luxafor is om gebruikers een eenvoudige en effectieve manier te bieden om hun beschikbaarheid aan collega's kenbaar te maken en de communicatie en samenwerking op de werkplek te verbeteren.

### Snel overzicht van de apparaten

Hier is een niet-limitatieve lijst van [Luxafor-apparaten] (https://luxafor.com/products):

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

Het is ontwikkeld in .Net Core en is gebaseerd op de [HidLibrairy] bibliotheek (https://github.com/mikeobrien/HidLibrary) waarmee HID-compatibele USB-apparaten in .NET kunnen worden opgesomd en ermee kan worden gecommuniceerd.

De onderstaande code toont een voorbeeld van een basisgebruik van de bibliotheek om een [Luxafor Orb](https://luxafor.com/product/orb/) apparaat aan te sturen.

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController.UnitTests/UsageExamples.cs#L20-L32

Regel 21 laat zien hoe een verbinding wordt gemaakt met een enkele Orb die is aangesloten op de USB-poort van de machine.

Ik zal snel de reeks mogelijke commando's overlopen die vanuit de `LuxaforDevice` naar apparaten kunnen worden gestuurd.

### Zet uit

```csharp
void TurnOff(); // Schakelt alle LEDs op het apparaat uit
void TurnOff(TargetedLeds targetedLeds); // Schakel de doelapparaat-LED's uit.
```

### Stel een enkele kleur in

```csharp
void SetColor(BasicColor basicColor); // Schakelt alle apparaat-LED's in een basiskleur in.
void SetColor(CustomColor customColor); // Schakelt de LED's van het apparaat in een aangepaste kleur in.
void SetColor(TargetedLeds targetedLeds, BasicColor basicColor); // Schakelt alle targetedLeds van het apparaat in een basiskleur in.
void SetColor(TargetedLeds targetedLeds, CustomColor color); // Schakelt de doelapparaat-LED's in een aangepaste kleur in.
```

### Maak een overgang (fade)

```csharp
void FadeColor(BasicColor basicColor, FadeDuration duration); // Overgang van alle LED's op het apparaat naar een basiskleur.
void FadeColor(CustomColor color, FadeDuration duration); // Verandert alle LED's op het apparaat in een aangepaste kleur.
void FadeColor(TargetedLeds targetedLeds, BasicColor basicColor, FadeDuration duration); // Overgang van de targetedLeds van het apparaat naar een basiskleur
void FadeColor(TargetedLeds targetedLeds, CustomColor color, FadeDuration duration); // Overgang van de doelapparaat-LED's naar een aangepaste kleur.
```

### Knipperen (stroboscoop effect)

```csharp
void Strobe(BasicColor basicColor, Speed speed, Repeat repeat); // Alle apparaat-LED's knipperen in een basiskleur.
void Strobe(CustomColor customColor, Speed speed, Repeat repeat); // Alle LED's van het apparaat knipperen in een aangepaste kleur.
void Strobe(TargetedLeds targetedLeds, BasicColor basicColor, Speed speed, Repeat repeat); // De targetedLeds van het apparaat knipperen in een basiskleur.
void Strobe(TargetedLeds targetedLeds, CustomColor customColor, Speed speed, Repeat repeat); // De doelapparaat-LED's knipperen in een aangepaste kleur.
```

### Golven

```csharp
void Wave(WaveType waveType, BasicColor basicColor, Speed speed, Repeat repeat); // Start een "golf"-patroon dat zich richt op alle LED's op het apparaat op basis van een basiskleur
void Wave(WaveType waveType, CustomColor customColor, Speed speed, Repeat repeat); // Start een golfpatroon dat zich richt op alle LED's op het apparaat op basis van een aangepaste kleur.
```

### Ingebouwde patronen

```csharp
void PlayPattern(BuiltInPattern, Repeat repeat); // Start een ingebouwd patroon dat zich richt op alle LED's op het apparaat
```

### Stuur een commando

Het is mogelijk om aangepaste commando's te maken met de naam `LightningCommand`, zodat ze in de code kunnen worden hergebruikt:

```csharp
var commando = LightningCommand.CreateStrobeCommand(TargetedLeds.All, BasicColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

Met de methode `Zend` kunt u deze commando's gebruiken.

```csharp
void Send(LightningCommand command); // Stuur een commando naar het apparaat.
```
