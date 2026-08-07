_[English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-EN.md) - [Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-SE.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-DE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-ES.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-GR.md) - [Polski](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-PL.md)_

[← Terug naar de README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md)

# Luxafor, het bedrijf en zijn apparaten

Deze pagina biedt context: ze beschrijft de hardware, niet de bibliotheek. Om de bibliotheek te gebruiken volstaan de [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) en de [API-referentie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-NL.md).

## Het bedrijf

[Luxafor](https://luxafor.com) ontwerpt en verkoopt producten voor kantoorproductiviteit, zoals beschikbaarheidsindicatoren en notificatiehulpmiddelen.

Hun paradepaardje is een [LED beschikbaarheidsindicator](https://luxafor.com/product/flag) die kan worden geprogrammeerd om verschillende kleuren weer te geven, afhankelijk van de beschikbaarheidsstatus van de gebruiker.

Het doel van Luxafor is om gebruikers een eenvoudige en effectieve manier te bieden om hun beschikbaarheid aan collega's kenbaar te maken en de communicatie en samenwerking op de werkplek te verbeteren.

## Het assortiment

Hier is een niet-limitatieve lijst van [Luxafor-apparaten](https://luxafor.com/products):

- `Luxafor Flag`: een LED beschikbaarheidsindicator die de persoonlijke beschikbaarheid weergeeft
- `Luxafor Bluetooth`: een draadloze, softwaregestuurde LED beschikbaarheidsindicator die meldingen en persoonlijke beschikbaarheid weergeeft
- `Luxafor Switch`: een draadloze, op afstand bediende beschikbaarheidsindicator die de beschikbaarheid van vergaderzalen en werkplekken in realtime weergeeft
- `Luxafor Cube`: een stand-alone LED beschikbaarheidsindicator die de beschikbaarheid van vergaderzalen weergeeft
- `Luxafor Pomodoro-Timer`: een USB-gevoede LED-timer waarmee werk in kleinere sleuven kan worden verdeeld (zie [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: een groothoek USB LED beschikbaarheidsindicator
- `Luxafor CO2 Monitor`: een sensor die de luchtkwaliteit van een ruimte analyseert en u waarschuwt wanneer deze geventileerd moet worden
- `Luxafor Mute Button`: zet de microfoon aan/uit met één aanraking en geef aan of u beschikbaar bent met het rood/groen
- `Luxafor Colorblind Flag`: monochroom USB LED beschikbaarheidslicht elimineert afleidingen en verhoogt de productiviteit

## Hoe ze worden aangestuurd

Deze verschillende apparaten zijn ontworpen om voor sommige handmatig ('mechanisch') aangestuurd te worden, voor andere semi-automatisch (handmatig aansturen via [software](https://luxaformanual.com)) of automatisch (integratie via [software](https://luxaformanual.com) met tools als Teams, Skype, Cisco, Zappier, of via Webhook).

Precies dat gat vult deze bibliotheek: de USB LED-apparaten aansturen vanuit uw eigen toepassingen, zonder via de Luxafor-server (webhook) of via software van derden te gaan. Ze steunt daarvoor op [HidLibrary](https://github.com/mikeobrien/HidLibrary), waarmee HID-compatibele USB-apparaten in .NET kunnen worden opgesomd en aangesproken.

## Wat de bibliotheek aanstuurt

Alleen de apparaten die het USB HID-protocol van Luxafor aanbieden, herkenbaar aan vendor id `1240` (`0x04D8`) en product id `62322` (`0xF372`), komen in aanmerking. De compatibiliteitstabel staat in de [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md#compatibele-apparaten).
