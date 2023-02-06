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

Het is ontwikkeld in .Net Core en is gebaseerd op de bibliotheek [HidLibrairy](https://github.com/mikeobrien/HidLibrary) waarmee HID-compatibele USB-apparaten in .NET kunnen worden opgesomd en ermee kan worden gecommuniceerd.

De onderstaande code toont een voorbeeld van een basisgebruik van de bibliotheek om een [Luxafor Orb](https://luxafor.com/product/orb/) apparaat aan te sturen.

```csharp
[Fact]
public void french_sequence() {
    LuxaforDevice orb = Luxafor.GetDevices().First();
    for (var i = 0; i < 3; i++) {
        orb.SetBasicColor(BasicColor.Blue);
        Thread.Sleep(500);
        orb.SetBasicColor(BasicColor.White);
        Thread.Sleep(500);
        orb.SetBasicColor(BasicColor.Red);
        Thread.Sleep(500);
        orb.SetBasicColor(BasicColor.Off);
        Thread.Sleep(1000);
    }
}
```

Regel 3 laat zien hoe een verbinding wordt gemaakt met een enkele Orb die is aangesloten op de USB-poort van de machine.

Ik zal snel de reeks mogelijke commando's overlopen die vanuit de `LuxaforDevice` naar apparaten kunnen worden gestuurd.

### Zet uit

```csharp
void TurnOff(); // Schakelt alle LEDs op het apparaat uit
void TurnOff(TargetedLeds targetedLeds); // Schakel de doelapparaat-LED's uit.
```

### Stel een enkele kleur in

```csharp
void SetColor(BrightColor color); // Schakelt de LED's van het apparaat in een aangepaste kleur in.
void SetColor(TargetedLeds targetedLeds, BrightColor color); // Schakelt de doelapparaat-LED's in een aangepaste kleur in.
```

### Maak een overgang (fade)

```csharp
void FadeColor(BrightColor color, FadeDuration duration); // Verandert alle LED's op het apparaat in een aangepaste kleur.
void FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration); // Overgang van de doelapparaat-LED's naar een aangepaste kleur.
```

### Knipperen (stroboscoop effect)

```csharp
void Strobe(BrightColor color, Speed speed, Repeat repeat); // Alle LED's van het apparaat knipperen in een aangepaste kleur.
void Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat); // De doelapparaat-LED's knipperen in een aangepaste kleur.
```

### Golven / Ingebouwde patronen

```csharp
void PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat); // Start een golfpatroon dat zich richt op alle LED's op het apparaat op basis van een aangepaste kleur.
void PlayPattern(BuiltInPattern, Repeat repeat); // Start een ingebouwd patroon dat zich richt op alle LED's op het apparaat
```

### Stuur een commando

Het is mogelijk om aangepaste commando's te maken met de naam `LightingCommand`, zodat ze in de code kunnen worden hergebruikt:

```csharp
var commando = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

Met de methode `Zend` kunt u deze commando's gebruiken.

```csharp
void Send(LightingCommand command); // Stuur een commando naar het apparaat.
```
