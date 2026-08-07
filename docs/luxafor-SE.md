_[English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor.md) - [Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-FR.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-NL.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-DE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-ES.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-GR.md) - [Polski](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-PL.md)_

[← Tillbaka till README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md)

# Luxafor, företaget och dess enheter

Den här sidan ger sammanhang: den beskriver hårdvaran, inte biblioteket. För att använda biblioteket räcker [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) och [API-referensen](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-SE.md).

## Företaget

[Luxafor](https://luxafor.com) utformar och säljer produkter för kontorsproduktivitet, t.ex. tillgänglighetsindikatorer och notifieringsverktyg.

Deras flaggskeppsprodukt är en [LED-tillgänglighetsindikator](https://luxafor.com/product/flag) som kan programmeras för att visa olika färger beroende på användarens tillgänglighetsstatus.

Luxafors mål är att ge användarna ett enkelt och effektivt sätt att signalera att de är tillgängliga för sina kollegor och förbättra kommunikationen och samarbetet på arbetsplatsen.

## Sortimentet

Här är en icke uttömmande förteckning över [Luxafor-apparater](https://luxafor.com/products):

- `Luxafor Flag`: en LED-indikator för tillgänglighet som visar personlig tillgänglighet
- `Luxafor Bluetooth`: en trådlös, mjukvarustyrd LED-indikator för tillgänglighet som visar meddelanden och personlig tillgänglighet
- `Luxafor Switch`: en trådlös, fjärrstyrd tillgänglighetsindikator som visar tillgängligheten för mötesrum och arbetsstationer i realtid
- `Luxafor Cube`: en fristående LED-indikator som visar om mötesrummen är tillgängliga
- `Luxafor Pomodoro-Timer`: en USB-driven LED-timer som gör det möjligt att dela upp arbetet i mindre delar (se [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: en LED-indikator för USB-tillgänglighet med bred vinkel
- `Luxafor CO2 Monitor`: en sensor som analyserar luftkvaliteten i ett rum och varnar dig när det behöver ventileras
- `Luxafor Mute Button`: slå på/av mikrofonen med en enda beröring och indikera om du är tillgänglig med den röda/gröna
- `Luxafor Colorblind Flag`: monokrom USB LED-tillgänglighetslampa som eliminerar distraktioner och ökar produktiviteten

## Hur de styrs

Dessa olika enheter är utformade för att kunna drivas manuellt ("mekaniskt") för vissa, halvautomatiskt (manuell styrning via [programvara](https://luxaformanual.com)) eller automatiskt (integration via [programvara](https://luxaformanual.com) med verktyg som Teams, Skype, Cisco, Zappier, eller via Webhook) för andra.

Det är precis den luckan som det här biblioteket fyller: att styra USB LED-enheterna från dina egna program, utan att gå via Luxafor-servern (webhook) eller via programvara från tredje part. Det bygger för det på [HidLibrary](https://github.com/mikeobrien/HidLibrary), som gör det möjligt att räkna upp och kommunicera med HID-kompatibla USB-enheter i .NET.

## Vad biblioteket styr

Endast de enheter som exponerar Luxafors USB HID-protokoll, identifierade av vendor id `1240` (`0x04D8`) och product id `62322` (`0xF372`), berörs. Kompatibilitetstabellen finns i [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md#kompatibla-enheter).
