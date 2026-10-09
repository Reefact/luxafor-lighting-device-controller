_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG.md) - [Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-FR.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-NL.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-GR.md) - [Wersja polska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-PL.md)_

# Ändringslogg

Alla noterbara ändringar i det här projektet dokumenteras i den här filen.

Formatet bygger på [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) och projektet följer
[semantisk versionshantering](https://semver.org/lang/sv/spec/v2.0.0.html).

## [2.1.0]

Ett tillägg till det publika API:et. En mindre version, även om en sorts kod måste följa med: en klass som
själv implementerar `ILuxaforDevice` — typiskt en handskriven fake i ett testprojekt — kompilerar inte längre
förrän den också implementerar `IsConnected` (en redan kompilerad klass misslyckas att laddas med ett
`TypeLoadException`). Biblioteket riktar sig mot `netstandard2.0` och `net462`, som inte stöder
standardimplementationer i gränssnitt, så egenskapen kunde inte få någon. Kod som bara använder de enheter som
`Luxafor` lämnar ut, och mockar som genereras av ett bibliotek som Moq eller NSubstitute, påverkas inte.

### Tillagt

- `ILuxaforDevice.IsConnected` anger om enheten fortfarande är ansluten. Egenskapen frågar Windows om enhetens
  sökväg fortfarande finns bland de närvarande HID-enheterna, utan att öppna eller skriva något, så den kan
  avfrågas regelbundet, och en enhet som har försvunnit svarar `false` i stället för att kasta ett undantag —
  till skillnad från `Luxafor.GetDevice`, hittills det enda sättet att ta reda på det, som kastar
  `LuxaforDeviceNotFoundException`. En enhet som kopplas ur och sätts tillbaka i samma port svarar `true`
  igen, men handtaget som instansen håller överlever inte urkopplingen: enheten måste hämtas på nytt för att
  kunna styras.

## [2.0.1]

Enbart dokumentation, verktyg och CI: själva biblioteket är oförändrat sedan 2.0.0, och dess publika API
likaså.

### Tillagt

- Ett konsumtionstest av det byggda paketet, `build/Test-PackageConsumption.ps1`: ett engångsprojekt utanför
  förrådet installerar `.nupkg`-filen från ett lokalt flöde in i en privat paketcache, kompilerar de publika
  exemplen mot det för `net472` och `net10.0`, kontrollerar de upplösta tillgångarna (`lib/net462`,
  `lib/netstandard2.0`, XML-dokumentationen) och beroendet `hidlibrary`, och kör sedan resultatet under
  Windows. Att läsa arkivet kan inte fånga ett beroende som inte löses upp eller en tillgång som aldrig når
  konsumenten; det kan det här testet. Det körs i CI:n, och i release-arbetsflödet innan något publiceras.
- En mapp `samples` med de exempel som visas i dokumentationen, kompilerade tillsammans med testerna på varje
  målramverk, plus `build/Sync-Snippets.ps1` som kopierar dem till markdown-sidorna. CI:n kör det i
  kontrolläge, så ett exempel kan inte längre glida ifrån den kod det visar.
- En `CONTRIBUTING.md` som beskriver hur man bygger, vad CI:n kontrollerar och hur en version publiceras, samt
  en mall för pull requests.
- Den här ändringsloggen är nu översatt till README:ns sju språk. `CHANGELOG.md` förblir den engelska
  versionen — referensen som översättningarna följer — eftersom paketets release notes pekar på den och
  eftersom verktyg förväntar sig Keep a Changelog-rubrikerna på engelska; övriga språk får ett suffix
  (`CHANGELOG-FR.md`, `CHANGELOG-NL.md`, ...).
- Polska tillkommer bland dokumentationens språk: `README-PL.md`, `docs/api-PL.md`, `docs/luxafor-PL.md`
  och `CHANGELOG-PL.md`.
- Konsumtionstestet låser nu det testade paketet till det lokala flödet med NuGet Package Source Mapping,
  och läser tillbaka källan som noterats i `.nupkg.metadata` som bevis. Engångsprojektet har båda källorna
  konfigurerade: så snart en version publicerats kan återställningen leverera den i stället för den
  nybyggda, och fel paket skulle testas. Beroendet måste fortfarande komma från nuget.org.

### Ändrat

- Dokumentation: varje README ger nu först det en läsare söker — vad biblioteket är till för, hur man
  installerar det, en snabbstart, de kompatibla enheterna, funktionerna, och därefter var man läser vidare.
  Presentationen av företaget Luxafor och dess produktsortiment har flyttat till `docs/luxafor*.md`, och
  genomgången kommando för kommando har blivit en API-referens i `docs/api*.md`, båda på samma sju språk.
- `README.md` är nu den engelska versionen: det är vad GitHub visar på förrådets startsida och vad paketet
  levererar, och engelska är det språk dess internationella läsare förväntar sig. Den franska versionen
  har flyttat till `README-FR.md`, och de övriga sex behåller sitt suffix. Varje länk till `README-EN.md`
  måste uppdateras.
- Arbetsflödena går över till `actions/checkout@v5`, `actions/setup-dotnet@v5` och
  `actions/upload-artifact@v6`, för var och en den första major som kör på Node 24, då Node 20 har fasats ut
  på körarna.

### Rättat

- `docs/api.md` och `docs/luxafor.md` är de engelska sidorna; de franska flyttar till `docs/api-FR.md`
  och `docs/luxafor-FR.md`. Förrådet följer nu en enda regel — filen utan suffix är den engelska — där
  `docs/` fortfarande hade den franska sidan utan suffix. Länkar till `docs/api-EN.md` eller
  `docs/luxafor-EN.md` måste uppdateras.
- XML-dokumentationen för `TargetedLeds` lyder inte längre "the on/off or animation will also be sequential
  and could: it can cause a visual ripple effect".
- Metadatakontrollen i `build/Validate-Package.ps1` kombinerade sina två villkor med `-and`, vilket inget
  värde kunde uppfylla: en tom `<description>` slank igenom. Den rapporterar nu lika väl tomma, blanka och
  saknade element som element utan attribut.

## [2.0.0]

### Brytande ändringar

- `TargetedLeds.TabSide` och `TargetedLeds.BackSide` tänder äntligen den sida de heter efter. De var
  omkastade: `TabSide` skickade luxkoden 66 (`0x42`) och `BackSide` 65 (`0x41`), medan Luxafor-protokollet
  tilldelar 65 till fliksidan (lysdioderna nr 1, 2 och 3) och 66 till baksidan (lysdioderna nr 4, 5 och 6).
  API:et ändras inte och inget slutar kompilera, men **vilka lysdioder som tänds ändras**: kod skriven mot
  1.x styrde motsatt sida, så varje kringgående som bytte plats på de två måste tas bort. Verifierat på en
  enhet före ändringen.
- Enhetsgränssnittet byter namn från `LuxaforDevice` till `ILuxaforDevice`, vilket anpassar det publika
  API:et till den namngivningskonvention i .NET som resten av ekosystemet använder (`IDisposable`,
  `IEnumerable<T>`, ...). Konsumenter måste uppdatera typnamnet; medlemmarna själva är oförändrade, så
  migreringen är en omdöpning: `using ILuxaforDevice orb = Luxafor.GetDevices().First();`.
  Implementationerna fortsätter att namnge sin specialisering (`HidLuxaforDevice`), och `Luxafor`,
  `LuxaforDeviceLocator` och `LuxaforDeviceNotFoundException` behåller sina namn — de är inte gränssnitt.
- Värdeobjekten (`BrightColor`, `FadeDuration`, `LedIndex`, `LightingCommand`, `Repeat`, `Speed`,
  `TargetedLeds`) ärver inte längre från `Value.ValueType<T>`: de implementerar sin egen likhet (`Equals`,
  `GetHashCode`, `==`, `!=`, `IEquatable<T>`) med samma värdesemantik, och paketet `Value` är inte längre ett
  beroende. Källkodskompatibelt för all normal användning (jämförelser, nycklar i en ordbok,
  `IEquatable<T>`), men binärt inkompatibelt: kompilera om mot 2.0.0. Endast kod som uttryckligen refererar
  till bastypen `Value.ValueType<T>` (eller som åsidosätter `GetAllAttributesToBeUsedForEquality`) behöver
  anpassas.
- `Luxafor.GetDevice(devicePath)` rapporterar nu ogiltiga sökvägar uttryckligen i stället för att fallera med
  ett obegripligt `ArgumentNullException`: den kastar `ArgumentException` vid en tom sökväg och
  `LuxaforDeviceNotFoundException` när ingen enhet finns på sökvägen, när det inte är en Luxafor-enhet som
  stöds, eller när den inte längre är ansluten.
- Paketet levererar inte längre en `net46`-assembly i en `net462`-mapp: målet `net462` kompileras verkligen
  mot .NET Framework 4.6.2.

### Tillagt

- `LuxaforDeviceNotFoundException`, som bär med sig den felande `DevicePath`.
- `TargetedLeds.FromLedIndex(LedIndex)`, det namngivna alternativet till den befintliga implicita
  konverteringen.
- Source Link, ett symbolpaket (`.snupkg`) och deterministiska byggen.
- En GitHub Actions-CI (Windows) som bygger, testar, paketerar och validerar innehållet i `.nupkg`-filen.
- Ett GitHub Actions-arbetsflöde för release som publicerar till nuget.org när en `v*`-tagg pushas, efter
  kontroll av att taggen stämmer med projektets version, efter att testerna körts och paketet validerats. Det
  autentiserar via trusted publishing (OIDC): ingen långlivad API-nyckel lagras i förrådet.
- Tester som täcker vidarebefordran av skrivfel, `Dispose`, saknade enheter, ogiltiga HID-sökvägar,
  argumentkontroller, värdeobjektens likhet och det publika API:ets yta.

### Ändrat

- Ett enda projekt i SDK-format som riktar sig mot både `netstandard2.0` och `net462`, i stället för Shared
  Project, det icke-SDK-baserade .NET Framework-projektet och den handskrivna `.nuspec`-filen; paketet
  framställs nu av `dotnet pack -c Release` (från Release-binärer, där den tidigare `.nuspec`-filen plockade
  upp Debug-binärer).
- Nullbara referenstyper, varningar som fel och .NET-analysatorerna är påslagna.
- HidLibrary används genom en intern abstraktion (`IHidDeviceRegistry`, `IHidDeviceHandle`), vilket håller
  den utanför det publika API:et och gör styrlogiken testbar utan hårdvara.
- Interna omdöpningar, utan påverkan på det publika API:et: `LuxaforDeviceImp` blir `HidLuxaforDevice`,
  filerna och mapparna `Lightning*` blir `Lighting*` (efter typen `LightingCommand` som de innehåller), och
  det interna gränssnittet `LightingCommandFactory` blir `ILightingCommandFactory`.
- Dokumentation: README-filerna (7 språk) är synkroniserade med koden — föråldrade exempel med `BasicColor` /
  `SetBasicColor` ersatta av `BrightColor` / `SetColor`, `void`-signaturer rättade till `bool`, `using` visat
  på `ILuxaforDevice`, plus avsnitt om installation, uppslagning av enhet, felhantering, enheter som stöds
  och licens.

### Rättat

- Luxkoderna som skickades för `TargetedLeds.TabSide` och `TargetedLeds.BackSide` var omkastade, så båda
  tände enhetens motsatta sida. Se de brytande ändringarna ovan: den här är tyst, den ändrar beteendet utan
  att bryta bygget.
- `FadeColor`-kommandon beskriver sig inte längre med stavfelet "duration od" i `ToString()`.

## [1.2.0]

### Tillagt

- Enhetskommandon returnerar en `bool` för att ange om åtgärden lyckades.
- `LuxaforDevice` implementerar `IDisposable` för att möjliggöra korrekt frigöring av resurser.
