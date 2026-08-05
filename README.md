_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-EN.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)_

# Contrôleur de périphériques Luxafor

Une bibliothèque .Net qui fournit une API simple pour contrôler les périphériques Luxafor.

## Luxafor

### Présentation de la société

[Luxafor](https://luxafor.com) est une entreprise qui conçoit et vend des produits pour la productivité de bureau, tels que des indicateurs de disponibilité et des outils de notification. 

Leur produit phare est un [indicateur de disponibilité LED](https://luxafor.com/product/flag) qui peut être programmé pour afficher différentes couleurs en fonction de l'état de disponibilité de l'utilisateur. 

L'objectif de Luxafor est de fournir aux utilisateurs un moyen simple et efficace de signaler leur disponibilité aux collègues de travail et d'améliorer la communication et la collaboration en entreprise.

### Présentation rapide des périphériques

Voici une liste non-exhaustive des [périphériques Luxafor](https://luxafor.com/products):

- `Luxafor Flag`: un indicateur de disponibilité par LED qui affiche la disponibilité personnelle
- `Luxafor Bluetooth`: un indicateur de disponibilité LED sans fil et contrôlé par logiciel qui affiche les notifications et la disponibilité personnelle
- `Luxafor Switch`: un indicateur de disponibilité sans fil et télécommandé qui affiche la disponibilité des salles de réunion et des postes de travail en temps réel
- `Luxafor Cube`: un indicateur de disponibilité LED autonome qui affiche la disponibilité des salles de réunion
- `Luxafor Pomodoro-Timer`: un minuteur à affichage LED alimenté par USB, qui permet de répartir le travail en petits créneaux (voir [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: un indicateur de disponibilité LED USB grand angle
- `Luxafor CO2 Monitor`: un capteur qui analyse la qualité de l'air d'une pièce et vous avertit lorsqu'il faut la ventiler
- `Luxafor Mute Button`: allumez/éteignez le micro d'une simple pression et indiquez si vous êtes disponible avec le rouge/vert
- `Luxafor Colorblind Flag`: lumière de disponibilité - d'occupation LED USB monochrome qui élimine les distractions et stimule la productivité

### Intégration

Ces différents périphériques sont conçus pour être pilotés manuellement ('mécanique') pour certains, de façon semi-automatique (pilotage manuel via [logiciel](https://luxaformanual.com)) / automatique (intégration via [logiciels](https://luxaformanual.com) à des outils comme Teams, Skype, Cisco, Zappier ou via Webhook) pour d'autres. 

## Présentation de la librairie

Cette librairie à pour but de permettre l'intégration des périphériques USB à LED à vos applications in-house sans avoir besoin de passer par le serveur Luxafor (webhook).

Elle cible `.NET Standard 2.0` et `.NET Framework 4.6.2`, et se base sur la librairie [HidLibrary](https://github.com/mikeobrien/HidLibrary) qui permet d'énumérer et de communiquer avec des périphériques USB compatibles HID en .NET.

> **Windows uniquement.** Les périphériques sont pilotés via la couche HID de Windows : le package s'installe sur toutes les plateformes, mais l'énumération et le pilotage des périphériques ne fonctionnent que sous Windows.

### Périphériques supportés

La librairie parle le protocole USB HID des périphériques Luxafor identifiés par le vendor id `1240` (`0x04D8`) et le product id `62322` (`0xF372`).

| Périphérique | Statut |
| --- | --- |
| `Luxafor Orb` | **Testé** : le périphérique utilisé pour développer et valider la librairie (6 LEDs adressables). |
| `Luxafor Flag` | **Devrait fonctionner, non testé** : mêmes identifiants et même protocole d'éclairage (6 LEDs adressables). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Devraient fonctionner, non testés** : les commandes d'éclairage sont les mêmes ; la disposition des LEDs, leur nombre et le rendu des couleurs peuvent différer. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Non supportés** : ces périphériques ne se pilotent pas via ce protocole USB HID. |

Tout retour concernant un périphérique non testé est le bienvenu : n'hésitez pas à [ouvrir une issue](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

### Installation

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

### Démarrage rapide

Le code ci-dessous présente un exemple d'utilisation basique de la librairie pour le pilotage d'un périphérique [Luxafor Orb](https://luxafor.com/product/orb/).

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

La ligne 3 montre comment se connecter à un unique Orb connecté au port USB de la machine. `ILuxaforDevice` implémente `IDisposable` : le `using` libère le handle du périphérique à la fin du bloc.

### Obtenir un périphérique

```csharp
IEnumerable<ILuxaforDevice> GetDevices(); // Tous les périphériques Luxafor connectés aux ports USB (énumération vide si aucun n'est branché)
ILuxaforDevice GetDevice(string devicePath); // Le périphérique Luxafor situé au chemin indiqué
```

`Luxafor.GetDevice` lève une `LuxaforDeviceNotFoundException` lorsqu'aucun périphérique ne se trouve au chemin indiqué, lorsque le périphérique trouvé n'est pas un périphérique Luxafor supporté, ou lorsqu'il n'est plus connecté.

```csharp
using ILuxaforDevice orb = Luxafor.GetDevice(@"\\?\hid#vid_04d8&pid_f372#...");
```

Je vais présenter rapidement l'ensemble des commandes possibles à envoyer aux périphériques à partir de l'`ILuxaforDevice`.

Chaque commande retourne un `bool` : `true` lorsque le périphérique a accepté la commande, `false` lorsque l'écriture a échoué (périphérique débranché, monopolisé par une autre application, ...). Les arguments invalides lèvent une exception (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

### Eteindre

```csharp
bool TurnOff(); // Eteint toutes les LEDs du périphérique
bool TurnOff(TargetedLeds targetedLeds); // Eteint les LEDs du périphérique ciblées
```

### Définir une couleur unique

```csharp
bool SetColor(BrightColor color); // Allume les LEDs du périphérique dans une couleur personnalisée.
bool SetColor(TargetedLeds targetedLeds, BrightColor color); // Allume les LEDs du périphérique ciblées dans une couleur personnalisée.
```

### Effectuer une transition (fondu)

```csharp
bool FadeColor(BrightColor color, FadeDuration duration); // Effectue une transition de toutes les LEDs du périphérique vers une couleur personnalisée
bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration); // Effectue une transition des LEDs du périphérique ciblées vers une couleur personnalisée
```

### Clignotement (effet stroboscopique)

```csharp
bool Strobe(BrightColor color, Speed speed, Repeat repeat); // Fait clignoter toutes les LEDs du périphérique dans une couleur personnalisée
bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat); // Fait clignoter les LEDs du périphérique ciblées dans une couleur personnalisée
```

### Vagues et autres motifs intégrés

```csharp
bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat); // Démarre un motif de type "vague" qui cible toutes les LEDs du périphérique basé sur une couleur personnalisée
bool PlayPattern(BuiltInPattern pattern, Repeat repeat); // Démarre un motif intégré qui cible toutes les LEDs du périphérique
```

### Envoyer une commande

Il est possible de créer des commandes personnalisées appelées `LightingCommand` afin de pouvoir les réutiliser dans le code:

```csharp
var command = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

La méthode `Send` permet d'utiliser ces commandes.

```csharp
bool Send(LightingCommand command); // Envoie une commande au périphérique
```

### Couleurs

```csharp
BrightColor.Red; // ainsi que Green, Blue, Yellow, Cyan, Magenta, White, Black
BrightColor.From("#0F11A8"); // A partir de sa représentation hexadécimale
BrightColor.From(15, 17, 168); // A partir de ses composantes rouge, verte et bleue
```

## Compiler la librairie

```shell
dotnet build -c Release
dotnet test -c Release
dotnet pack Reefact.LuxaforLightingDeviceController -c Release -o artifacts
```

## Licence

Cette librairie est distribuée sous licence [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE).
