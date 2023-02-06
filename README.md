_[English Version](README-EN.md) - [Nederlandse versie](README-NL.md) - [Svensk version](README-SE.md) - [Deutsche Version](README-DE.md) - [Versión española](README-ES.md) - [Ελληνική έκδοση](README-GR.md)_

# Contrôleur de périphériques Luxafor

Une bibliothèque .Net qui fournit une API simple pour contrôler les périphériques Luxafor.

## Luxafor

### Présentation de la société

[Luxafor](https://luxafor.com) est une entreprise qui conçoit et vend des produits pour la productivité de bureau, tels que des indicateurs de disponibilité et des outils de notification. 

Leur produit phare est un [indicateur de disponibilité LED](https://luxafor.com/product/flag) qui peut être programmé pour afficher différentes couleurs en fonction de l'état de disponibilité de l'utilisateur. 

L'objectif de Luxafor est de fournir aux utilisateurs un moyen simple et efficace de signaler leur disponibilité aux collègues de travail et d'améliorer la communication et la collaboration en entrepris

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

Elle est développée en .Net Core et se base sur la librairie [HidLibrairy](https://github.com/mikeobrien/HidLibrary) qui permet d'énumérer et de communiquer avec des périphériques USB compatibles HID en .NET.

Le code ci-dessous présente un exemple d'utilisation basique de la librairie pour le pilotage d'un périphérique [Luxafor Orb](https://luxafor.com/product/orb/).

```csharp
[Fact]
public void french_sequence() {
    LuxaforDevice orb = Luxafor.GetDevices().First();
    for (var i = 0; i < 3; i++) {
        orb.SetBasicColor(BasicColor.Blue);
        Thread.Sleep(500);
        orb.SetBasicColor(BasicColor.White);
        Thread.Sleep(500);
        orb.SetBasicColor(BasicColor.Red);
        Thread.Sleep(500);
        orb.SetBasicColor(BasicColor.Off);
        Thread.Sleep(1000);
    }
}
```

La ligne 3 montre comment se connecter à un unique Orb connecté au port USB de la machine.

Je vais présenter rapidement l'ensemble des commandes possibles à envoyer aux périphériques à partir du `LuxaforDevice`.

### Eteindre

```csharp
void TurnOff(); // Eteint toutes les LEDs du périphérique
void TurnOff(TargetedLeds targetedLeds); // Eteint les LEDs du périphérique ciblées
```

### Définir une couleur unique

```csharp
void SetColor(BrightColor color); // Allume les LEDs du périphérique dans une couleur personnalisée.
void SetColor(TargetedLeds targetedLeds, BrightColor color); // Allume les LEDs du périphérique ciblées dans une couleur personnalisée.
```

### Effectuer une transition (fondu)

```csharp
void FadeColor(BrightColor color, FadeDuration duration); // Effectue une transition de toutes les LEDs du périphérique vers une couleur personnalisée
void FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration); // Effectue une transition des LEDs du périphérique ciblées vers une couleur personnalisée
```

### Clignotement (effet stroboscopique)

```csharp
void Strobe(BrightColor color, Speed speed, Repeat repeat); // Fait clignoter toutes les LEDs du périphérique dans une couleur personnalisée
void Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat); // Fait clignoter les LEDs du périphérique ciblées dans une couleur personnalisée
```

### Vagues et autres motifs intégrés

```csharp
void PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat); // Démarre un motif de type "vague" qui cible toutes les LEDs du périphérique basé sur une couleur personnalisée
void PlayPattern(BuiltInPattern pattern, Repeat repeat); // Démarre un motif intégré qui cible toutes les LEDs du périphérique
```

### Envoyer une commande

Il est possible de créer des commandes personnalisées appelées `LightingCommand` afin de pouvoir les réutiliser dans le code:

```csharp
var command = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

La méthode `Send` permet d'utiliser ces commandes.

```csharp
void Send(LightingCommand command); // Envoye une commande au périphérique
```
