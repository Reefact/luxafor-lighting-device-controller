_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-FR.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)_

# Kontroler urządzeń Luxafor

Steruj swoimi wskaźnikami dostępności [Luxafor](https://luxafor.com) bezpośrednio z własnych aplikacji .NET, rozmawiając wprost ich protokołem USB HID: bez serwera Luxafor, bez webhooka, bez instalowania oprogramowania firm trzecich.

- Bezpośrednie API: `SetColor`, `FadeColor`, `Strobe`, `PlayPattern`, aż po sterowanie pojedynczą diodą.
- `.NET Standard 2.0` i `.NET Framework 4.6.2`, przy jednej jedynej zależności ([HidLibrary](https://github.com/mikeobrien/HidLibrary)).
- **Tylko Windows**: urządzenia są sterowane przez warstwę HID systemu Windows. Pakiet instaluje się na każdej platformie, ale wyliczanie urządzeń i sterowanie nimi działa wyłącznie w systemie Windows.

## Instalacja

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

## Szybki start

<!-- snippet: quick-start -->
```csharp
using System.Linq;

using Reefact.LuxaforLightingDeviceController;

namespace MyApplication {

    public static class BuildStatusLight {

        public static void Show(bool buildSucceeded) {
            using ILuxaforDevice device = Luxafor.GetDevices().First();

            if (buildSucceeded) {
                device.SetColor(BrightColor.Green);
            } else {
                device.Strobe(BrightColor.Red, Speed.FromByte(20), Repeat.Count(3));
            }
        }

    }

}
```
<!-- endSnippet -->

`Luxafor.GetDevices()` wylicza urządzenia Luxafor podłączone do portów USB maszyny; wyliczenie jest puste, gdy żadne nie jest podłączone. `ILuxaforDevice` implementuje `IDisposable`: instrukcja `using` zwalnia uchwyt urządzenia na końcu bloku. Każde polecenie zwraca `bool`: `true`, gdy urządzenie je przyjęło, `false`, gdy zapis się nie powiódł.

## Zgodne urządzenia

Biblioteka steruje — poprzez ich protokół USB HID — urządzeniami Luxafor rozpoznawanymi po vendor id `1240` (`0x04D8`) i product id `62322` (`0xF372`).

| Urządzenie | Stan |
| --- | --- |
| `Luxafor Orb` | **Przetestowane**: urządzenie użyte do rozwoju i walidacji biblioteki (6 adresowalnych diod). |
| `Luxafor Flag` | **Powinno działać, nieprzetestowane**: te same identyfikatory i ten sam protokół oświetlenia (6 adresowalnych diod). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Powinny działać, nieprzetestowane**: polecenia oświetlenia są takie same; rozmieszczenie diod, ich liczba i odwzorowanie kolorów mogą się różnić. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Nieobsługiwane**: te urządzenia nie są sterowane tym protokołem USB HID. |

Wszelkie uwagi na temat nieprzetestowanego urządzenia są mile widziane: [zgłoś issue](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

## Funkcje

- **Stały kolor** (`SetColor`) z koloru nazwanego, kodu szesnastkowego albo składowych czerwonej / zielonej / niebieskiej.
- **Przejście** (`FadeColor`) do koloru, w wybranym czasie trwania.
- **Miganie** (`Strobe`) z wybraną prędkością i wybraną liczbą powtórzeń.
- **Wzory** (`PlayPattern`): kolorowe fale albo wzory wbudowane w urządzenie (`Police`, `Rainbow`, `TrafficLight`, ...).
- **Wygaszanie** (`TurnOff`) wszystkich diod albo tylko ich części.
- **Wskazywanie diod**: wszystkie, jedna strona (`TabSide`, `BackSide`) albo pojedyncza dioda (`Led_1` do `Led_6`).
- **Polecenia wielokrotnego użytku**: `LightingCommand` opisuje polecenie raz, aby odtworzyć je później przez `Send`.

## Szczegółowa dokumentacja

- [Dokumentacja API](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-PL.md): wszystkie polecenia, ich parametry i błędy.
- [Luxafor, firma i jej urządzenia](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-PL.md): do czego służą te wskaźniki i którymi steruje ta biblioteka.
- [Dziennik zmian](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-PL.md) i [przewodnik dla współtwórców](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CONTRIBUTING.md).

## Licencja

Ta biblioteka jest rozpowszechniana na licencji [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE).
