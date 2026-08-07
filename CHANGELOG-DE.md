_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG.md) - [Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-FR.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-SE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-GR.md) - [Wersja polska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-PL.md)_

# Änderungsprotokoll

Alle nennenswerten Änderungen an diesem Projekt werden in dieser Datei festgehalten.

Das Format orientiert sich an [Keep a Changelog](https://keepachangelog.com/de/1.1.0/), und dieses Projekt
folgt der [semantischen Versionierung](https://semver.org/lang/de/spec/v2.0.0.html).

## [2.0.1]

Nur Dokumentation, Werkzeuge und CI: die Bibliothek selbst ist seit 2.0.0 unverändert, ihre öffentliche API
ebenfalls.

### Hinzugefügt

- Ein Konsumtest des erzeugten Pakets, `build/Test-PackageConsumption.ps1`: ein Wegwerfprojekt außerhalb des
  Repositorys installiert das `.nupkg` aus einer lokalen Quelle in einen privaten Paketcache, kompiliert die
  öffentlichen Beispiele dagegen für `net472` und `net10.0`, prüft die aufgelösten Assets (`lib/net462`,
  `lib/netstandard2.0`, XML-Dokumentation) und die Abhängigkeit `hidlibrary` und führt das Ergebnis
  anschließend unter Windows aus. Das Archiv zu lesen kann eine Abhängigkeit, die sich nicht auflöst, oder
  ein Asset, das den Konsumenten nie erreicht, nicht aufdecken; dieser Test schon. Er läuft in der CI und im
  Release-Workflow, bevor irgendetwas veröffentlicht wird.
- Ein Ordner `samples` mit den in der Dokumentation abgedruckten Beispielen, die zusammen mit den Tests auf
  jedem Zielframework kompiliert werden, dazu `build/Sync-Snippets.ps1`, das sie in die Markdown-Seiten
  kopiert. Die CI führt es im Prüfmodus aus, sodass ein Beispiel nicht mehr von dem Code abweichen kann, den
  es zeigt.
- Eine `CONTRIBUTING.md`, die beschreibt, wie gebaut wird, was die CI prüft und wie eine Version
  veröffentlicht wird, sowie eine Pull-Request-Vorlage.
- Dieses Änderungsprotokoll ist nun in die sieben Sprachen der README übersetzt. `CHANGELOG.md` bleibt die
  englische Fassung — die Referenz, der die Übersetzungen folgen — weil die Release Notes des Pakets darauf
  verweisen und weil Werkzeuge die Keep-a-Changelog-Überschriften auf Englisch erwarten; die übrigen Sprachen
  erhalten ein Suffix (`CHANGELOG-FR.md`, `CHANGELOG-NL.md`, ...).
- Polnisch kommt zu den Sprachen der Dokumentation hinzu: `README-PL.md`, `docs/api-PL.md`,
  `docs/luxafor-PL.md` und `CHANGELOG-PL.md`.

### Geändert

- Dokumentation: Jede README bringt jetzt zuerst das, was eine Leserin oder ein Leser sucht — wozu die
  Bibliothek dient, wie man sie installiert, ein Schnellstart, die kompatiblen Geräte, die Funktionen und
  dann, wo es weitergeht. Die Vorstellung des Unternehmens Luxafor und sein Produktsortiment sind nach
  `docs/luxafor*.md` gewandert, und der Durchgang Befehl für Befehl ist zu einer API-Referenz in
  `docs/api*.md` geworden, beides in denselben sieben Sprachen.
- `README.md` ist jetzt die englische Fassung: das ist, was GitHub auf der Startseite des Repositorys
  anzeigt und was das Paket mitliefert, und Englisch ist die Sprache, die seine internationalen Lesenden
  erwarten. Die französische Fassung ist nach `README-FR.md` gewandert, die anderen sechs behalten ihr
  Suffix. Jeder Link auf `README-EN.md` muss angepasst werden.
- Die Workflows wechseln zu `actions/checkout@v5`, `actions/setup-dotnet@v5` und
  `actions/upload-artifact@v6`, jeweils der ersten Hauptversion, die auf Node 24 läuft, da Node 20 auf den
  Runnern abgekündigt ist.

### Behoben

- Die XML-Dokumentation von `TargetedLeds` lautet nicht mehr "the on/off or animation will also be sequential
  and could: it can cause a visual ripple effect".
- Die Metadatenprüfung in `build/Validate-Package.ps1` verknüpfte ihre beiden Bedingungen mit `-and`, was
  kein Wert erfüllen konnte: eine leere `<description>` kam durch. Sie meldet nun leere, nur aus Leerzeichen
  bestehende, fehlende und attributlose Elemente gleichermaßen.

## [2.0.0]

### Brechende Änderungen

- `TargetedLeds.TabSide` und `TargetedLeds.BackSide` beleuchten endlich die Seite, die sie benennen. Sie
  waren vertauscht: `TabSide` sendete den Lux-Code 66 (`0x42`) und `BackSide` 65 (`0x41`), während das
  Luxafor-Protokoll 65 der Laschenseite (LEDs Nr. 1, 2 und 3) und 66 der Rückseite (LEDs Nr. 4, 5 und 6)
  zuordnet. Die API ändert sich nicht und nichts hört auf zu kompilieren, aber **welche LEDs aufleuchten,
  ändert sich sehr wohl**: gegen 1.x geschriebener Code steuerte die gegenüberliegende Seite an, jeder
  Workaround, der die beiden vertauschte, muss also entfernt werden. Vor der Änderung an einem Gerät
  verifiziert.
- Die Geräteschnittstelle wird von `LuxaforDevice` in `ILuxaforDevice` umbenannt und richtet die öffentliche
  API damit an der .NET-Namenskonvention aus, die der Rest des Ökosystems verwendet (`IDisposable`,
  `IEnumerable<T>`, ...). Konsumenten müssen den Typnamen anpassen; die Mitglieder selbst sind unverändert,
  die Migration ist also eine Umbenennung: `using ILuxaforDevice orb = Luxafor.GetDevices().First();`. Die
  Implementierungen benennen weiterhin ihre Spezialisierung (`HidLuxaforDevice`), und `Luxafor`,
  `LuxaforDeviceLocator` und `LuxaforDeviceNotFoundException` behalten ihre Namen — sie sind keine
  Schnittstellen.
- Die Wertobjekte (`BrightColor`, `FadeDuration`, `LedIndex`, `LightingCommand`, `Repeat`, `Speed`,
  `TargetedLeds`) leiten nicht mehr von `Value.ValueType<T>` ab: sie implementieren ihre eigene Gleichheit
  (`Equals`, `GetHashCode`, `==`, `!=`, `IEquatable<T>`) mit derselben Wertsemantik, und das Paket `Value`
  ist keine Abhängigkeit mehr. Quellkompatibel für jede normale Verwendung (Vergleiche, Schlüssel in einem
  Dictionary, `IEquatable<T>`), aber binär inkompatibel: gegen 2.0.0 neu kompilieren. Nur Code, der
  ausdrücklich auf den Basistyp `Value.ValueType<T>` verweist (oder
  `GetAllAttributesToBeUsedForEquality` überschreibt), muss angepasst werden.
- `Luxafor.GetDevice(devicePath)` meldet ungültige Pfade nun ausdrücklich, statt mit einer undurchsichtigen
  `ArgumentNullException` zu scheitern: sie löst bei einem leeren Pfad eine `ArgumentException` aus und eine
  `LuxaforDeviceNotFoundException`, wenn unter dem Pfad kein Gerät liegt, wenn es kein unterstütztes
  Luxafor-Gerät ist oder wenn es nicht mehr angeschlossen ist.
- Das Paket liefert keine `net46`-Assembly mehr in einem `net462`-Ordner: das Ziel `net462` wird wirklich
  gegen .NET Framework 4.6.2 kompiliert.

### Hinzugefügt

- `LuxaforDeviceNotFoundException`, die den betroffenen `DevicePath` mitführt.
- `TargetedLeds.FromLedIndex(LedIndex)`, die benannte Alternative zur bestehenden impliziten Konvertierung.
- Source Link, ein Symbolpaket (`.snupkg`) und deterministische Builds.
- Eine GitHub-Actions-CI (Windows), die baut, testet, packt und den Inhalt des `.nupkg` validiert.
- Ein GitHub-Actions-Release-Workflow, der bei einem gepushten `v*`-Tag nach nuget.org veröffentlicht,
  nachdem geprüft wurde, dass der Tag zur Version des Projekts passt, die Tests gelaufen sind und das Paket
  validiert wurde. Er authentifiziert sich über Trusted Publishing (OIDC): kein langlebiger API-Schlüssel
  liegt im Repository.
- Tests für die Weitergabe von Schreibfehlern, `Dispose`, fehlende Geräte, ungültige HID-Pfade,
  Argumentprüfungen, die Gleichheit der Wertobjekte und die Oberfläche der öffentlichen API.

### Geändert

- Ein einziges Projekt im SDK-Stil, das sowohl `netstandard2.0` als auch `net462` bedient, anstelle des
  Shared Project, des nicht SDK-basierten .NET-Framework-Projekts und der handgeschriebenen `.nuspec`; das
  Paket entsteht nun durch `dotnet pack -c Release` (aus Release-Binärdateien, wo die frühere `.nuspec` die
  aus Debug aufgriff).
- Nullable-Referenztypen, Warnungen als Fehler und die .NET-Analyzer sind aktiviert.
- HidLibrary wird über eine interne Abstraktion (`IHidDeviceRegistry`, `IHidDeviceHandle`) genutzt, was sie
  aus der öffentlichen API heraushält und die Ansteuerungslogik ohne Hardware testbar macht.
- Interne Umbenennungen, ohne Auswirkung auf die öffentliche API: `LuxaforDeviceImp` wird
  `HidLuxaforDevice`, die Dateien und Ordner `Lightning*` werden `Lighting*` (passend zum enthaltenen Typ
  `LightingCommand`), und die interne Schnittstelle `LightingCommandFactory` wird `ILightingCommandFactory`.
- Dokumentation: Die READMEs (7 Sprachen) sind mit dem Code synchronisiert — veraltete Beispiele mit
  `BasicColor` / `SetBasicColor` durch `BrightColor` / `SetColor` ersetzt, `void`-Signaturen zu `bool`
  korrigiert, `using` an `ILuxaforDevice` gezeigt, dazu Abschnitte zu Installation, Gerätesuche,
  Fehlerbehandlung, unterstützten Geräten und Lizenz.

### Behoben

- Die für `TargetedLeds.TabSide` und `TargetedLeds.BackSide` gesendeten Lux-Codes waren vertauscht, sodass
  beide die gegenüberliegende Seite des Geräts beleuchteten. Siehe die brechenden Änderungen oben: diese hier
  ist still, sie ändert das Verhalten, ohne den Build zu brechen.
- `FadeColor`-Befehle beschreiben sich nicht mehr mit dem Tippfehler "duration od" in `ToString()`.

## [1.2.0]

### Hinzugefügt

- Gerätebefehle geben einen `bool` zurück, der angibt, ob der Vorgang erfolgreich war.
- `LuxaforDevice` implementiert `IDisposable`, um eine saubere Freigabe der Ressourcen zu ermöglichen.
