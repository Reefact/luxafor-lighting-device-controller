_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG.md) - [Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-FR.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-GR.md)_

# Dziennik zmian

Wszystkie istotne zmiany w tym projekcie są odnotowywane w tym pliku.

Format opiera się na [Keep a Changelog](https://keepachangelog.com/pl/1.1.0/), a projekt stosuje
[wersjonowanie semantyczne](https://semver.org/lang/pl/spec/v2.0.0.html).

## [2.0.1]

Wyłącznie dokumentacja, narzędzia i CI: sama biblioteka nie zmieniła się od 2.0.0, jej publiczne API również.

### Dodane

- Test konsumpcji wytworzonego pakietu, `build/Test-PackageConsumption.ps1`: jednorazowy projekt utworzony
  poza repozytorium instaluje `.nupkg` z lokalnego źródła do prywatnej pamięci podręcznej pakietów,
  kompiluje względem niego publiczne przykłady dla `net472` i `net10.0`, sprawdza rozwiązane zasoby
  (`lib/net462`, `lib/netstandard2.0`, dokumentacja XML) oraz zależność `hidlibrary`, a następnie uruchamia
  wynik w systemie Windows. Czytanie archiwum nie wykryje zależności, która się nie rozwiązuje, ani zasobu,
  który nigdy nie dociera do odbiorcy; ten test tak. Działa w CI oraz w przepływie publikacji, zanim
  cokolwiek zostanie opublikowane.
- Katalog `samples` z przykładami pokazywanymi w dokumentacji, kompilowanymi razem z testami na każdym
  docelowym frameworku, oraz `build/Sync-Snippets.ps1`, który kopiuje je do stron markdown. CI uruchamia go
  w trybie sprawdzania, więc przykład nie może już rozejść się z kodem, który pokazuje.
- Plik `CONTRIBUTING.md` opisujący, jak budować, co sprawdza CI i jak publikowana jest wersja, a także
  szablon pull requesta.
- Ten dziennik zmian jest teraz przetłumaczony na języki README. `CHANGELOG.md` pozostaje wersją angielską —
  odniesieniem, za którym podążają tłumaczenia — ponieważ wskazują na nią informacje o wydaniu pakietu i
  ponieważ narzędzia oczekują nagłówków Keep a Changelog po angielsku; pozostałe języki mają przyrostek
  (`CHANGELOG-FR.md`, `CHANGELOG-NL.md`, ...).
- Polski dołącza do języków dokumentacji: `README-PL.md`, `docs/api-PL.md`, `docs/luxafor-PL.md` i ten plik.

### Zmienione

- Dokumentacja: każdy README podaje teraz najpierw to, czego szuka czytelnik — do czego służy biblioteka, jak
  ją zainstalować, szybki start, zgodne urządzenia, funkcje, a potem gdzie czytać dalej. Prezentacja firmy
  Luxafor i jej katalog produktów przeniosły się do `docs/luxafor*.md`, a omówienie polecenie po poleceniu
  stało się dokumentacją API w `docs/api*.md` — jedno i drugie w tych samych językach.
- `README.md` jest teraz wersją angielską: to ją GitHub pokazuje na stronie głównej repozytorium i to ją
  dostarcza pakiet, a angielski jest językiem, którego oczekują jego międzynarodowi czytelnicy. Wersja
  francuska przeniosła się do `README-FR.md`, a pozostałe zachowują swój przyrostek. Każdy odnośnik do
  `README-EN.md` trzeba zaktualizować.
- Przepływy pracy przechodzą na `actions/checkout@v5`, `actions/setup-dotnet@v5` i
  `actions/upload-artifact@v6` — dla każdej z nich pierwszą wersję główną działającą na Node 24, ponieważ
  Node 20 został wycofany na maszynach CI.

### Naprawione

- Dokumentacja XML `TargetedLeds` nie brzmi już "the on/off or animation will also be sequential and could:
  it can cause a visual ripple effect".
- Sprawdzenie metadanych w `build/Validate-Package.ps1` łączyło swoje dwa warunki operatorem `-and`, czego
  żadna wartość nie mogła spełnić: pusty `<description>` przechodził. Teraz zgłasza tak samo elementy puste,
  złożone z białych znaków, nieobecne oraz pozbawione atrybutów.

## [2.0.0]

### Zmiany łamiące zgodność

- `TargetedLeds.TabSide` i `TargetedLeds.BackSide` oświetlają wreszcie tę stronę, którą nazywają. Były
  zamienione: `TabSide` wysyłał kod lux 66 (`0x42`), a `BackSide` 65 (`0x41`), podczas gdy protokół Luxafor
  przypisuje 65 stronie zakładki (diody nr 1, 2 i 3), a 66 stronie tylnej (diody nr 4, 5 i 6). API się nie
  zmienia i nic nie przestaje się kompilować, ale **zmienia się to, które diody się zapalają**: kod pisany
  pod 1.x sterował przeciwną stroną, więc każde obejście zamieniające obie strony trzeba usunąć.
  Zweryfikowane na urządzeniu przed wprowadzeniem zmiany.
- Interfejs urządzenia zmienia nazwę z `LuxaforDevice` na `ILuxaforDevice`, co dostosowuje publiczne API do
  konwencji nazewnictwa .NET stosowanej przez resztę ekosystemu (`IDisposable`, `IEnumerable<T>`, ...).
  Odbiorcy muszą zaktualizować nazwę typu; same składowe pozostają bez zmian, więc migracja sprowadza się do
  zmiany nazwy: `using ILuxaforDevice orb = Luxafor.GetDevices().First();`. Implementacje nadal nazywają
  swoją specjalizację (`HidLuxaforDevice`), a `Luxafor`, `LuxaforDeviceLocator` i
  `LuxaforDeviceNotFoundException` zachowują nazwy — nie są interfejsami.
- Obiekty wartości (`BrightColor`, `FadeDuration`, `LedIndex`, `LightingCommand`, `Repeat`, `Speed`,
  `TargetedLeds`) nie dziedziczą już po `Value.ValueType<T>`: implementują własną równość (`Equals`,
  `GetHashCode`, `==`, `!=`, `IEquatable<T>`) o tej samej semantyce wartości, a pakiet `Value` przestał być
  zależnością. Zgodne na poziomie źródeł przy każdym normalnym użyciu (porównania, klucze słownika,
  `IEquatable<T>`), ale niezgodne binarnie: przekompiluj względem 2.0.0. Dostosowania wymaga tylko kod, który
  jawnie odwołuje się do typu bazowego `Value.ValueType<T>` (albo przesłania
  `GetAllAttributesToBeUsedForEquality`).
- `Luxafor.GetDevice(devicePath)` zgłasza teraz nieprawidłowe ścieżki wprost, zamiast kończyć się niejasnym
  `ArgumentNullException`: rzuca `ArgumentException` przy pustej ścieżce oraz
  `LuxaforDeviceNotFoundException`, gdy pod ścieżką nie ma urządzenia, gdy nie jest to obsługiwane urządzenie
  Luxafor albo gdy nie jest już podłączone.
- Pakiet nie dostarcza już zestawu `net46` w katalogu `net462`: cel `net462` jest naprawdę kompilowany
  względem .NET Framework 4.6.2.

### Dodane

- `LuxaforDeviceNotFoundException`, niosący problematyczną `DevicePath`.
- `TargetedLeds.FromLedIndex(LedIndex)`, nazwana alternatywa dla istniejącej konwersji niejawnej.
- Source Link, pakiet symboli (`.snupkg`) i deterministyczne kompilacje.
- CI w GitHub Actions (Windows), które buduje, testuje, pakuje i waliduje zawartość `.nupkg`.
- Przepływ wydania w GitHub Actions publikujący na nuget.org po wypchnięciu tagu `v*`, po sprawdzeniu, że tag
  zgadza się z wersją projektu, po uruchomieniu testów i walidacji pakietu. Uwierzytelnia się przez trusted
  publishing (OIDC): w repozytorium nie jest przechowywany żaden długowieczny klucz API.
- Testy obejmujące propagację błędów zapisu, `Dispose`, brakujące urządzenia, nieprawidłowe ścieżki HID,
  kontrolę argumentów, równość obiektów wartości i powierzchnię publicznego API.

### Zmienione

- Jeden projekt w stylu SDK celujący jednocześnie w `netstandard2.0` i `net462`, w miejsce Shared Project,
  projektu .NET Framework spoza SDK i ręcznie pisanego `.nuspec`; pakiet powstaje teraz przez
  `dotnet pack -c Release` (z plików binarnych Release, podczas gdy poprzedni `.nuspec` zabierał te z Debug).
- Włączone są typy referencyjne dopuszczające wartość null, ostrzeżenia traktowane jak błędy i analizatory
  .NET.
- HidLibrary jest używana przez wewnętrzną abstrakcję (`IHidDeviceRegistry`, `IHidDeviceHandle`), co trzyma ją
  poza publicznym API i pozwala testować logikę sterowania bez sprzętu.
- Wewnętrzne zmiany nazw, bez wpływu na publiczne API: `LuxaforDeviceImp` staje się `HidLuxaforDevice`, pliki
  i katalogi `Lightning*` stają się `Lighting*` (zgodnie z typem `LightingCommand`, który zawierają), a
  wewnętrzny interfejs `LightingCommandFactory` staje się `ILightingCommandFactory`.
- Dokumentacja: pliki README są zsynchronizowane z kodem — przestarzałe przykłady `BasicColor` /
  `SetBasicColor` zastąpione przez `BrightColor` / `SetColor`, sygnatury `void` poprawione na `bool`, `using`
  pokazany na `ILuxaforDevice`, a do tego sekcje o instalacji, wyszukiwaniu urządzenia, obsłudze błędów,
  obsługiwanych urządzeniach i licencji.

### Naprawione

- Kody lux wysyłane dla `TargetedLeds.TabSide` i `TargetedLeds.BackSide` były zamienione, więc oba
  oświetlały przeciwną stronę urządzenia. Zobacz zmiany łamiące zgodność powyżej: ta jest cicha, zmienia
  zachowanie, nie psując kompilacji.
- Polecenia `FadeColor` nie opisują się już literówką "duration od" w `ToString()`.

## [1.2.0]

### Dodane

- Polecenia urządzenia zwracają `bool`, wskazując, czy operacja się powiodła.
- `LuxaforDevice` implementuje `IDisposable`, aby umożliwić poprawne zwalnianie zasobów.
