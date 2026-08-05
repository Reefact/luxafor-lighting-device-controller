_[Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-EN.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)_

# Luxafor Device Controller

Ett .Net-bibliotek som tillhandahåller ett enkelt API för att styra Luxafor-enheter.

## Luxafor

### Översikt över företaget

[Luxafor](https://luxafor.com) är ett företag som utformar och säljer produkter för kontorsproduktivitet, t.ex. tillgänglighetsindikatorer och notifieringsverktyg. 

Deras flaggskeppsprodukt är en [LED availability indicator](https://luxafor.com/product/flag) som kan programmeras för att visa olika färger beroende på användarens tillgänglighetsstatus. 

Luxafors mål är att ge användarna ett enkelt och effektivt sätt att signalera att de är tillgängliga för sina kollegor och förbättra kommunikationen och samarbetet på arbetsplatsen.

### Snabb översikt över enheterna

Här är en icke uttömmande förteckning över [Luxafor-apparater](https://luxafor.com/products):

- `Luxafor Flag`: en LED-indikator för tillgänglighet som visar personlig tillgänglighet.
- `Luxafor Bluetooth`: en trådlös, mjukvarustyrd LED-indikator för tillgänglighet som visar meddelanden och personlig tillgänglighet.
- `Luxafor Switch`: en trådlös, fjärrstyrd tillgänglighetsindikator som visar tillgängligheten för mötesrum och arbetsstationer i realtid.
- `Luxafor Cube`: en fristående LED-indikator som visar om mötesrummen är tillgängliga.
- `Luxafor Pomodoro-Timer`: en USB-driven LED-timer som gör det möjligt att dela upp arbetet i mindre delar (se [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))).
- `Luxafor Orb`: en LED-indikator för USB-tillgänglighet med bred vinkel.
- `Luxafor CO2 Monitor`: en sensor som analyserar luftkvaliteten i ett rum och varnar dig när det behöver ventileras.
- `Luxafor Mute-knapp`: Slå på/av mikrofonen med en enda beröring och indikera om du är tillgänglig med den röda/gröna
- `Luxafor Colorblind Flag`: monokrom USB LED-tillgänglighetslampa som eliminerar distraktioner och ökar produktiviteten.

### Integration

Dessa olika enheter är utformade för att kunna drivas manuellt ("mekaniskt") för vissa, halvautomatiskt (manuell styrning via [programvara](https://luxaformanual.com)) eller automatiskt (integration via [programvara](https://luxaformanual.com) med verktyg som Teams, Skype, Cisco, Zappier eller via Webhook) för andra. 

## Presentation av biblioteket

Det här biblioteket syftar till att göra det möjligt att integrera USB LED-enheter i dina interna applikationer utan att behöva gå via Luxafor-servern (webhook).

Det riktar sig till `.NET Standard 2.0` och `.NET Framework 4.6.2` och bygger på biblioteket [HidLibrary](https://github.com/mikeobrien/HidLibrary) som gör det möjligt att räkna upp och kommunicera med HID-kompatibla USB-enheter i .NET.

> **Endast Windows.** Enheterna styrs via Windows HID-lager: paketet kan installeras på alla plattformar, men enheterna kan endast räknas upp och styras under Windows.

### Enheter som stöds

Biblioteket talar USB HID-protokollet för de Luxafor-enheter som identifieras av vendor id `1240` (`0x04D8`) och product id `62322` (`0xF372`).

| Enhet | Status |
| --- | --- |
| `Luxafor Orb` | **Testad**: den enhet som användes för att utveckla och validera biblioteket (6 adresserbara lysdioder). |
| `Luxafor Flag` | **Bör fungera, inte testad**: samma identifierare och samma belysningsprotokoll (6 adresserbara lysdioder). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Bör fungera, inte testade**: belysningskommandona är desamma; lysdiodernas placering, antal och färgåtergivning kan skilja sig åt. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Stöds inte**: dessa enheter styrs inte via detta USB HID-protokoll. |

Återkoppling om en enhet som inte testats är mycket välkommen: [öppna gärna ett ärende](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

### Installation

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

### Snabbstart

Koden nedan visar ett exempel på en grundläggande användning av biblioteket för att styra en [Luxafor Orb](https://luxafor.com/product/orb/) enhet.

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

Rad 3 visar hur man ansluter till en enda Orb som är ansluten till maskinens USB-port. `ILuxaforDevice` implementerar `IDisposable`: `using`-satsen frigör enhetens handtag i slutet av blocket.

### Hämta en enhet

```csharp
IEnumerable<ILuxaforDevice> GetDevices(); // Alla Luxafor-enheter som är anslutna till USB-portarna (tom om ingen är ansluten)
ILuxaforDevice GetDevice(string devicePath); // Luxafor-enheten på den angivna sökvägen
```

`Luxafor.GetDevice` kastar ett `LuxaforDeviceNotFoundException` när ingen enhet hittas på den angivna sökvägen, när enheten som hittas där inte är en Luxafor-enhet som stöds, eller när den inte längre är ansluten.

```csharp
using ILuxaforDevice orb = Luxafor.GetDevice(@"\\?\hid#vid_04d8&pid_f372#...");
```

Jag går snabbt igenom alla kommandon som kan skickas till enheterna från `ILuxaforDevice`.

Varje kommando returnerar en `bool`: `true` när enheten har accepterat kommandot, `false` när skrivningen misslyckades (enheten är urkopplad, upptagen av ett annat program, ...). Ogiltiga argument kastar ett undantag (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

### Stäng av

```csharp
bool TurnOff(); // Stänger av alla lysdioder på enheten.
bool TurnOff(TargetedLeds targetedLeds); // Stänger av den målinriktade enhetens lysdioder
```

### Ange en enda färg

```csharp
bool SetColor(BrightColor color); // Tänder enhetens lysdioder i en egen färg.
bool SetColor(TargetedLeds targetedLeds, BrightColor color); // Slår på de riktade enheternas lysdioder i en anpassad färg.
```

### Gör en övergång (blekning)

```csharp
bool FadeColor(BrightColor color, FadeDuration duration); // Övergår alla lysdioder på enheten till en anpassad färg.
bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration); // Övergång av de riktade enhetens lysdioder till en anpassad färg.
```

### Blink (stroboskopeffekt)

```csharp
bool Strobe(BrightColor color, Speed speed, Repeat repeat); // Blinkar alla lysdioder på enheten i en egen färg.
bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat); // Blinkar för de LED-lampor som är målinriktade i en anpassad färg.
```

### Vågor / Inbyggda mönster

```csharp
bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat); // Startar ett vågmönster som riktar sig till alla lysdioder på enheten baserat på en anpassad färg.
bool PlayPattern(BuiltInPattern pattern, Repeat repeat); // Starta ett inbyggt mönster som riktar sig till alla lysdioder på enheten.
```

### Skicka ett kommando

Det är möjligt att skapa egna kommandon som kallas `LightingCommand` så att de kan återanvändas i koden:

```csharp
var command = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

Med metoden `Send` kan du använda dessa kommandon.

```csharp
bool Send(LightingCommand command); // Skicka ett kommando till enheten
```

### Färger

```csharp
BrightColor.Red; // samt Green, Blue, Yellow, Cyan, Magenta, White, Black
BrightColor.From("#0F11A8"); // Från dess hexadecimala representation
BrightColor.From(15, 17, 168); // Från dess röda, gröna och blå komponenter
```

## Bygga biblioteket

```shell
dotnet build -c Release
dotnet test -c Release
dotnet pack Reefact.LuxaforLightingDeviceController -c Release -o artifacts
```

## Licens

Detta bibliotek distribueras under licensen [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE).
