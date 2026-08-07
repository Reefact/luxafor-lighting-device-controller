_[English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor.md) - [Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-FR.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-NL.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-SE.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-DE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-ES.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-GR.md)_

[← Powrót do README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-PL.md)

# Luxafor, firma i jej urządzenia

Ta strona daje kontekst: opisuje sprzęt, a nie bibliotekę. Do korzystania z biblioteki wystarczą
[README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-PL.md) i
[dokumentacja API](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-PL.md).

## Firma

[Luxafor](https://luxafor.com) projektuje i sprzedaje produkty zwiększające produktywność w biurze, takie jak wskaźniki dostępności i narzędzia powiadomień.

Ich sztandarowym produktem jest [diodowy wskaźnik dostępności](https://luxafor.com/product/flag), który można zaprogramować tak, aby wyświetlał różne kolory w zależności od statusu dostępności użytkownika.

Celem Luxafor jest danie użytkownikom prostego i skutecznego sposobu sygnalizowania swojej dostępności współpracownikom oraz poprawa komunikacji i współpracy w miejscu pracy.

## Katalog

Oto niewyczerpująca lista [urządzeń Luxafor](https://luxafor.com/products):

- `Luxafor Flag`: diodowy wskaźnik dostępności pokazujący dostępność osobistą
- `Luxafor Bluetooth`: bezprzewodowy, sterowany programowo diodowy wskaźnik dostępności, pokazujący powiadomienia i dostępność osobistą
- `Luxafor Switch`: bezprzewodowy wskaźnik dostępności sterowany zdalnie, pokazujący w czasie rzeczywistym dostępność sal konferencyjnych i stanowisk pracy
- `Luxafor Cube`: samodzielny diodowy wskaźnik dostępności pokazujący dostępność sal konferencyjnych
- `Luxafor Pomodoro-Timer`: zasilany przez USB minutnik diodowy, pozwalający dzielić pracę na krótsze odcinki (zob. [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: szerokokątny diodowy wskaźnik dostępności na USB
- `Luxafor CO2 Monitor`: czujnik analizujący jakość powietrza w pomieszczeniu i ostrzegający, kiedy trzeba je przewietrzyć
- `Luxafor Mute Button`: włącz/wyłącz mikrofon jednym dotknięciem i pokaż kolorem czerwonym lub zielonym, czy jesteś dostępny
- `Luxafor Colorblind Flag`: monochromatyczne diodowe światło dostępności na USB, które eliminuje rozpraszacze i podnosi produktywność

## Jak się nimi steruje

Te różne urządzenia zaprojektowano tak, by jednymi sterować ręcznie („mechanicznie”), a innymi półautomatycznie (sterowanie ręczne przez [oprogramowanie](https://luxaformanual.com)) lub automatycznie (integracja przez [oprogramowanie](https://luxaformanual.com) z narzędziami takimi jak Teams, Skype, Cisco, Zappier, albo przez Webhook).

Właśnie tę lukę wypełnia ta biblioteka: pozwala sterować diodowymi urządzeniami USB z własnych aplikacji, bez przechodzenia przez serwer Luxafor (webhook) ani przez oprogramowanie firm trzecich. Opiera się przy tym na [HidLibrary](https://github.com/mikeobrien/HidLibrary), która umożliwia wyliczanie urządzeń USB zgodnych z HID i komunikację z nimi w .NET.

## Czym steruje biblioteka

Dotyczy to wyłącznie urządzeń udostępniających protokół USB HID firmy Luxafor, rozpoznawanych po vendor id `1240` (`0x04D8`) i product id `62322` (`0xF372`). Tabela zgodności znajduje się w [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-PL.md#zgodne-urządzenia).
