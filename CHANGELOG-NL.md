_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG.md) - [Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-FR.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-GR.md) - [Wersja polska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-PL.md)_

# Wijzigingslogboek

Alle noemenswaardige wijzigingen aan dit project worden in dit bestand vastgelegd.

De opmaak is gebaseerd op [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) en dit project volgt
[semantisch versiebeheer](https://semver.org/lang/nl/spec/v2.0.0.html).

## [2.1.0]

Eén toevoeging aan de publieke API. Een minor-versie, al moet één soort code volgen: een klasse die
`ILuxaforDevice` zelf implementeert — meestal een handgeschreven fake in een testproject — compileert niet
meer zolang ze ook `IsConnected` niet implementeert (een al gecompileerde klasse faalt bij het laden met een
`TypeLoadException`). De bibliotheek richt zich op `netstandard2.0` en `net462`, die geen
standaardimplementaties in interfaces ondersteunen, dus de eigenschap kon er geen meebrengen. Code die alleen
de apparaten gebruikt die `Luxafor` aanlevert, en mocks die door een bibliotheek als Moq of NSubstitute worden
gegenereerd, worden niet geraakt.

### Toegevoegd

- `ILuxaforDevice.IsConnected` geeft aan of het apparaat nog is aangesloten. De eigenschap vraagt Windows of
  het pad van het apparaat nog voorkomt tussen de aanwezige HID-apparaten, zonder iets te openen of te
  schrijven, zodat ze regelmatig kan worden opgevraagd, en een verdwenen apparaat antwoordt `false` in plaats
  van een uitzondering te gooien — anders dan `Luxafor.GetDevice`, tot nu toe de enige manier om het te weten,
  die `LuxaforDeviceNotFoundException` gooit. Een apparaat dat wordt losgekoppeld en weer in dezelfde poort
  wordt gestoken, antwoordt opnieuw `true`, maar de handle die de instantie vasthoudt overleeft het
  loskoppelen niet: het apparaat moet opnieuw worden opgehaald om het aan te sturen.

## [2.0.1]

Uitsluitend documentatie, gereedschap en CI: de bibliotheek zelf is sinds 2.0.0 onveranderd, en haar
publieke API eveneens.

### Toegevoegd

- Een consumptietest van het geproduceerde pakket, `build/Test-PackageConsumption.ps1`: een wegwerpproject
  buiten de repository installeert het `.nupkg` vanuit een lokale feed in een privé-pakketcache, compileert
  de publieke voorbeelden ertegen voor `net472` en `net10.0`, controleert de opgeloste assets (`lib/net462`,
  `lib/netstandard2.0`, XML-documentatie) en de `hidlibrary`-afhankelijkheid, en voert het resultaat
  vervolgens uit onder Windows. Het archief lezen kan een afhankelijkheid die niet oplost of een asset die de
  gebruiker nooit bereikt niet betrappen; deze test wel. Hij draait in de CI, en in de release-workflow
  voordat er ook maar iets wordt gepubliceerd.
- Een map `samples` met de voorbeelden die in de documentatie worden getoond, meegecompileerd met de tests op
  elk doelframework, plus `build/Sync-Snippets.ps1` dat ze naar de markdown-pagina's kopieert. De CI draait
  het in controlemodus, zodat een voorbeeld niet langer uit de pas kan lopen met de code die het toont.
- Een `CONTRIBUTING.md` die beschrijft hoe je bouwt, wat de CI controleert en hoe een release wordt
  gepubliceerd, en een pull request-sjabloon.
- Dit wijzigingslogboek is nu vertaald in de zeven talen van de README. `CHANGELOG.md` blijft de Engelse
  versie — de referentie die de vertalingen volgen — omdat de release notes van het pakket ernaar verwijzen
  en omdat gereedschap de Keep a Changelog-koppen in het Engels verwacht; de andere talen krijgen een
  achtervoegsel (`CHANGELOG-FR.md`, `CHANGELOG-NL.md`, ...).
- Het Pools voegt zich bij de talen van de documentatie: `README-PL.md`, `docs/api-PL.md`,
  `docs/luxafor-PL.md` en `CHANGELOG-PL.md`.
- De consumptietest zet het geteste pakket nu vast op de lokale feed met NuGet Package Source Mapping, en
  leest de in `.nupkg.metadata` vastgelegde bron terug als bewijs. Voor het wegwerpproject zijn beide
  bronnen ingesteld: zodra een versie is gepubliceerd kan de restore die serveren in plaats van de zojuist
  gebouwde, en dan zou de verkeerde worden getest. Van de afhankelijkheid wordt geëist dat ze nog steeds
  van nuget.org komt.

### Gewijzigd

- Documentatie: elke README geeft nu eerst wat een lezer zoekt — waar de bibliotheek voor dient, hoe je haar
  installeert, een snelstart, de compatibele apparaten, de functionaliteiten, en dan waar je verder kunt
  lezen. De presentatie van het bedrijf Luxafor en zijn productassortiment zijn verhuisd naar
  `docs/luxafor*.md`, en de bespreking commando per commando is een API-referentie geworden in
  `docs/api*.md`, beide in dezelfde zeven talen.
- `README.md` is nu de Engelse versie: dat is wat GitHub op de startpagina van de repository toont en wat
  het pakket meelevert, en Engels is de taal die de internationale lezers verwachten. De Franse versie is
  verhuisd naar `README-FR.md`, en de andere zes behouden hun achtervoegsel. Elke verwijzing naar
  `README-EN.md` moet worden bijgewerkt.
- De workflows gaan over op `actions/checkout@v5`, `actions/setup-dotnet@v5` en `actions/upload-artifact@v6`,
  van elk de eerste major die op Node 24 draait, aangezien Node 20 op de runners is afgeschaft.

### Opgelost

- `docs/api.md` en `docs/luxafor.md` zijn de Engelse pagina's; de Franse verhuizen naar `docs/api-FR.md`
  en `docs/luxafor-FR.md`. De repository volgt nu één regel — het bestand zonder achtervoegsel is het
  Engelse — terwijl `docs/` de Franse pagina nog zonder achtervoegsel had. Verwijzingen naar
  `docs/api-EN.md` of `docs/luxafor-EN.md` moeten worden bijgewerkt.
- De XML-documentatie van `TargetedLeds` leest niet langer "the on/off or animation will also be sequential
  and could: it can cause a visual ripple effect".
- De metadatacontrole van `build/Validate-Package.ps1` combineerde haar twee voorwaarden met `-and`, wat geen
  enkele waarde kon vervullen: een lege `<description>` kwam erdoor. Ze meldt nu zowel lege, blanco en
  ontbrekende elementen als elementen zonder attributen.

## [2.0.0]

### Brekende wijzigingen

- `TargetedLeds.TabSide` en `TargetedLeds.BackSide` verlichten eindelijk de zijde die ze benoemen. Ze waren
  omgedraaid: `TabSide` stuurde luxcode 66 (`0x42`) en `BackSide` 65 (`0x41`), terwijl het Luxafor-protocol
  65 aan de tabzijde toekent (LED's nr. 1, 2 en 3) en 66 aan de achterzijde (LED's nr. 4, 5 en 6). De API
  verandert niet en er stopt niets met compileren, maar **welke LED's oplichten verandert wél**: code
  geschreven voor 1.x stuurde de tegenovergestelde zijde aan, dus elke omweg die de twee verwisselde moet
  worden verwijderd. Vóór de wijziging op een apparaat geverifieerd.
- De apparaatinterface wordt hernoemd van `LuxaforDevice` naar `ILuxaforDevice`, waarmee de publieke API
  aansluit op de .NET-naamgevingsconventie die de rest van het ecosysteem hanteert (`IDisposable`,
  `IEnumerable<T>`, ...). Gebruikers moeten de typenaam bijwerken; de leden zelf zijn ongewijzigd, dus de
  migratie is een hernoeming: `using ILuxaforDevice orb = Luxafor.GetDevices().First();`. De implementaties
  blijven hun specialisatie benoemen (`HidLuxaforDevice`), en `Luxafor`, `LuxaforDeviceLocator` en
  `LuxaforDeviceNotFoundException` behouden hun naam — het zijn geen interfaces.
- De waardeobjecten (`BrightColor`, `FadeDuration`, `LedIndex`, `LightingCommand`, `Repeat`, `Speed`,
  `TargetedLeds`) erven niet langer van `Value.ValueType<T>`: ze implementeren hun eigen gelijkheid
  (`Equals`, `GetHashCode`, `==`, `!=`, `IEquatable<T>`) met dezelfde waardesemantiek, en het pakket `Value`
  is geen afhankelijkheid meer. Broncompatibel voor elk normaal gebruik (vergelijkingen, sleutels in een
  dictionary, `IEquatable<T>`), maar binair incompatibel: hercompileer tegen 2.0.0. Alleen code die
  uitdrukkelijk naar het basistype `Value.ValueType<T>` verwijst (of `GetAllAttributesToBeUsedForEquality`
  overschrijft) moet worden aangepast.
- `Luxafor.GetDevice(devicePath)` meldt ongeldige paden nu uitdrukkelijk in plaats van te falen met een
  obscure `ArgumentNullException`: ze gooit een `ArgumentException` bij een leeg pad en een
  `LuxaforDeviceNotFoundException` wanneer er geen apparaat op het pad staat, wanneer het geen ondersteund
  Luxafor-apparaat is, of wanneer het niet meer is aangesloten.
- Het pakket levert niet langer een `net46`-assembly in een `net462`-map: het `net462`-doel wordt
  daadwerkelijk tegen .NET Framework 4.6.2 gecompileerd.

### Toegevoegd

- `LuxaforDeviceNotFoundException`, met het betreffende `DevicePath`.
- `TargetedLeds.FromLedIndex(LedIndex)`, het benoemde alternatief voor de bestaande impliciete conversie.
- Source Link, een symboolpakket (`.snupkg`) en deterministische builds.
- Een GitHub Actions-CI (Windows) die bouwt, test, verpakt en de inhoud van het `.nupkg` valideert.
- Een GitHub Actions-releaseworkflow die naar nuget.org publiceert wanneer een `v*`-tag wordt gepusht, na
  controle dat de tag overeenkomt met de versie van het project, na het draaien van de tests en na validatie
  van het pakket. Hij authenticeert via trusted publishing (OIDC): er staat geen langlevende API-sleutel in
  de repository.
- Tests voor het doorgeven van schrijffouten, `Dispose`, ontbrekende apparaten, ongeldige HID-paden,
  argumentbewaking, gelijkheid van waardeobjecten en het oppervlak van de publieke API.

### Gewijzigd

- Eén enkel project in SDK-stijl dat zowel `netstandard2.0` als `net462` bedient, ter vervanging van het
  Shared Project, het niet-SDK .NET Framework-project en het handgeschreven `.nuspec`; het pakket wordt nu
  gemaakt door `dotnet pack -c Release` (vanuit Release-binaries, waar het vorige `.nuspec` die van Debug
  opraapte).
- Nullable referentietypes, waarschuwingen als fouten en de .NET-analyzers staan aan.
- HidLibrary wordt gebruikt via een interne abstractie (`IHidDeviceRegistry`, `IHidDeviceHandle`), wat haar
  buiten de publieke API houdt en de aansturingslogica testbaar maakt zonder hardware.
- Interne hernoemingen, zonder gevolgen voor de publieke API: `LuxaforDeviceImp` wordt `HidLuxaforDevice`, de
  bestanden en mappen `Lightning*` worden `Lighting*` (naar het type `LightingCommand` dat ze bevatten), en
  de interne interface `LightingCommandFactory` wordt `ILightingCommandFactory`.
- Documentatie: de README's (7 talen) zijn met de code gesynchroniseerd — verouderde voorbeelden
  `BasicColor` / `SetBasicColor` vervangen door `BrightColor` / `SetColor`, `void`-signaturen gecorrigeerd
  naar `bool`, `using` getoond op `ILuxaforDevice`, plus secties over installatie, het opzoeken van een
  apparaat, foutafhandeling, ondersteunde apparaten en licentie.

### Opgelost

- De luxcodes die voor `TargetedLeds.TabSide` en `TargetedLeds.BackSide` werden gestuurd, waren omgedraaid,
  zodat beide de tegenovergestelde zijde van het apparaat verlichtten. Zie de brekende wijzigingen hierboven:
  deze is stil, hij verandert het gedrag zonder de build te breken.
- `FadeColor`-commando's beschrijven zichzelf niet langer met een "duration od"-typefout in `ToString()`.

## [1.2.0]

### Toegevoegd

- Apparaatcommando's geven een `bool` terug om aan te geven of de bewerking is geslaagd.
- `LuxaforDevice` implementeert `IDisposable` om het correct vrijgeven van resources mogelijk te maken.
