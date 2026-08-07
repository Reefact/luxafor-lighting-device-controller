_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-EN.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)_

# Contrôleur de périphériques Luxafor

Pilotez vos indicateurs de disponibilité [Luxafor](https://luxafor.com) depuis vos propres applications .NET, en parlant directement leur protocole USB HID : ni serveur Luxafor, ni webhook, ni logiciel tiers à installer.

- Une API directe : `SetColor`, `FadeColor`, `Strobe`, `PlayPattern`, jusqu'au pilotage LED par LED.
- `.NET Standard 2.0` et `.NET Framework 4.6.2`, pour une seule dépendance ([HidLibrary](https://github.com/mikeobrien/HidLibrary)).
- **Windows uniquement** : les périphériques sont pilotés via la couche HID de Windows. Le package s'installe sur toutes les plateformes, mais l'énumération et le pilotage ne fonctionnent que sous Windows.

## Installation

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

## Démarrage rapide

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

`Luxafor.GetDevices()` énumère les périphériques Luxafor branchés sur les ports USB de la machine ; l'énumération est vide lorsqu'aucun n'est branché. `ILuxaforDevice` implémente `IDisposable` : le `using` libère le handle du périphérique à la fin du bloc. Chaque commande retourne un `bool` : `true` lorsque le périphérique a accepté la commande, `false` lorsque l'écriture a échoué.

## Périphériques compatibles

La bibliothèque pilote, via leur protocole USB HID, les périphériques Luxafor identifiés par le vendor id `1240` (`0x04D8`) et le product id `62322` (`0xF372`).

| Périphérique | Statut |
| --- | --- |
| `Luxafor Orb` | **Testé** : le périphérique utilisé pour développer et valider la bibliothèque (6 LEDs adressables). |
| `Luxafor Flag` | **Devrait fonctionner, non testé** : mêmes identifiants et même protocole d'éclairage (6 LEDs adressables). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Devraient fonctionner, non testés** : les commandes d'éclairage sont les mêmes ; la disposition des LEDs, leur nombre et le rendu des couleurs peuvent différer. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Non supportés** : ces périphériques ne se pilotent pas via ce protocole USB HID. |

Tout retour concernant un périphérique non testé est le bienvenu : n'hésitez pas à [ouvrir une issue](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

## Fonctionnalités

- **Couleur fixe** (`SetColor`) à partir d'une couleur nommée, d'un code hexadécimal ou de composantes rouge / verte / bleue.
- **Fondu** (`FadeColor`) vers une couleur, sur une durée de transition choisie.
- **Clignotement** (`Strobe`) à une vitesse et pour un nombre de répétitions choisis.
- **Motifs** (`PlayPattern`) : vagues colorées, ou motifs intégrés au périphérique (`Police`, `Rainbow`, `TrafficLight`, ...).
- **Extinction** (`TurnOff`), de toutes les LEDs ou d'une partie seulement.
- **Ciblage des LEDs** : toutes, une face (`TabSide`, `BackSide`) ou une LED précise (`Led_1` à `Led_6`).
- **Commandes réutilisables** : `LightingCommand` décrit une commande une fois pour la rejouer avec `Send`.

## Documentation détaillée

- [Référence de l'API](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api.md) : toutes les commandes, leurs paramètres et leurs erreurs.
- [Luxafor, la société et ses périphériques](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor.md) : à quoi servent ces indicateurs et lesquelles cette bibliothèque pilote.
- [Journal des modifications](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG.md) et [guide de contribution](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CONTRIBUTING.md).

## Licence

Cette bibliothèque est distribuée sous licence [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE).
