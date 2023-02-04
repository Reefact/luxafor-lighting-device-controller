# Contrôleur de périphériques Luxafor

Une bibliothèque .Net qui fournit une API simple pour contrôler les périphériques Luxafor.

## Luxafor

### Présentation de la Société

[Luxafor](https://luxafor.com) est une entreprise qui conçoit et vend des produits pour la productivité de bureau, tels que des indicateurs de disponibilité et des outils de notification. 

Leur produit phare est un [indicateur de disponibilité LED](https://luxafor.com/product/flag) qui peut être programmé pour afficher différentes couleurs en fonction de l'état de disponibilité de l'utilisateur. 

L'objectif de Luxafor est de fournir aux utilisateurs un moyen simple et efficace de signaler leur disponibilité aux collègues de travail et d'améliorer la communication et la collaboration en entrepris

### Présentation Rapide des Périphériques

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

## Présentation de la Librairie

Cette librairie à pour but de permettre l'intégration des périphériques USB à LED à vos applications in-house sans avoir besoin de passer par le serveur Luxafor (webhook).

Elle est développée en .Net Core et se base sur la librairie [HidLibrairy](https://github.com/mikeobrien/HidLibrary) qui permet d'énumérer et de communiquer avec des périphériques USB compatibles HID en .NET.

Le code ci-dessous présente un exemple d'utilisation basique de la librairie pour le pilotage d'un périphérique [Luxafor Orb](https://luxafor.com/product/orb/).

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController.UnitTests/UsageExamples.cs#L20-L32

La librairie permet d'exploiter l'ensemble des fonctionnalités proposées par ces périphériques à LED.

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController/LuxaforDevice.cs#L42
Permet d'éteindre le périphérique.

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController/LuxaforDevice.cs#L47
Permet d'allumer toutes les LEDs du périphérique dans une des couleurs de base.

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController/LuxaforDevice.cs#L52
Permet d'allumer la/les LEDs sélectionnées du périphérique dans une couleur personnalisée.

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController/LuxaforDevice.cs#L52
Permet d'effectuer un fondu pour la/les LEDs sélectionnées du périphérique dans une couleur personnalisée.

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController/LuxaforDevice.cs#L69
Permet d'activer le mode `wave` en sélectionnant certains paramètres.

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController/LuxaforDevice.cs#L78
Permet d'activer le mode `strobe` en sélectionnant certains paramètres.

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController/LuxaforDevice.cs#L88
Permet d'activer le mode `built-in pattern` en sélectionnant certains paramètres.
