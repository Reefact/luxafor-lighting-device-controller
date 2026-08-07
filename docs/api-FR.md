_[English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-NL.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-SE.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-DE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-ES.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-GR.md) - [Polski](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-PL.md)_

[← Retour au README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-FR.md)

# Référence de l'API

Tout se trouve dans un seul namespace, `Reefact.LuxaforLightingDeviceController`. La classe statique `Luxafor` est le point d'entrée : elle donne des `ILuxaforDevice`, à qui l'on envoie ensuite des commandes.

Les exemples de cette page sont extraits du dossier [`samples`](https://github.com/Reefact/luxafor-lighting-device-controller/tree/main/samples), qui est compilé par la CI : ils ne peuvent pas se désynchroniser du code.

## Obtenir un périphérique

| Signature | Description |
| --- | --- |
| `IEnumerable<ILuxaforDevice> Luxafor.GetDevices()` | Tous les périphériques Luxafor branchés sur les ports USB. L'énumération est vide lorsqu'aucun n'est branché. |
| `ILuxaforDevice Luxafor.GetDevice(string devicePath)` | Le périphérique Luxafor situé au chemin indiqué. |

<!-- snippet: list-devices -->
```csharp
foreach (ILuxaforDevice device in Luxafor.GetDevices()) {
    using (device) {
        Console.Out.WriteLine(device.Description + " — " + device.Path);
    }
}
```
<!-- endSnippet -->

`ILuxaforDevice` implémente `IDisposable` : le `using` libère le handle du périphérique à la fin du bloc. Le périphérique expose également son chemin (`Path`) et sa description (`Description`), telle que rapportée par Windows.

`Luxafor.GetDevice` lève une `LuxaforDeviceNotFoundException` lorsqu'aucun périphérique ne se trouve au chemin indiqué, lorsque le périphérique trouvé n'est pas un périphérique Luxafor supporté, ou lorsqu'il n'est plus connecté. La propriété `DevicePath` de l'exception rappelle le chemin demandé.

<!-- snippet: get-device-by-path -->
```csharp
try {
    using ILuxaforDevice device = Luxafor.GetDevice(devicePath);

    device.SetColor(BrightColor.Red);
} catch (LuxaforDeviceNotFoundException exception) {
    Console.Error.WriteLine(exception.DevicePath + " is not a connected Luxafor device.");
}
```
<!-- endSnippet -->

## Résultat d'une commande

Chaque commande retourne un `bool` : `true` lorsque le périphérique a accepté la commande, `false` lorsque l'écriture a échoué (périphérique débranché, monopolisé par une autre application, ...). Les arguments invalides, eux, lèvent une exception (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

<!-- snippet: command-result -->
```csharp
if (!device.SetColor(BrightColor.Red)) {
    Console.Error.WriteLine("The device refused the command: unplugged, or held by another application.");
}
```
<!-- endSnippet -->

## Éteindre

| Signature | Description |
| --- | --- |
| `bool TurnOff()` | Éteint toutes les LEDs du périphérique. |
| `bool TurnOff(TargetedLeds targetedLeds)` | Éteint les LEDs ciblées du périphérique. |

<!-- snippet: turn-off -->
```csharp
device.TurnOff();
device.TurnOff(TargetedLeds.BackSide);
```
<!-- endSnippet -->

## Définir une couleur

| Signature | Description |
| --- | --- |
| `bool SetColor(BrightColor color)` | Allume toutes les LEDs du périphérique dans une couleur. |
| `bool SetColor(TargetedLeds targetedLeds, BrightColor color)` | Allume les LEDs ciblées dans une couleur. |

<!-- snippet: set-color -->
```csharp
device.SetColor(BrightColor.Green);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);
```
<!-- endSnippet -->

## Effectuer une transition (fondu)

| Signature | Description |
| --- | --- |
| `bool FadeColor(BrightColor color, FadeDuration duration)` | Fait passer toutes les LEDs vers une couleur. |
| `bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration)` | Fait passer les LEDs ciblées vers une couleur. |

La durée s'exprime dans l'unité du périphérique : `FadeDuration.From(byte)`, de `0` (instantané) à `255` (le plus lent).

<!-- snippet: fade-color -->
```csharp
device.FadeColor(BrightColor.Blue, FadeDuration.From(30));
device.FadeColor(TargetedLeds.BackSide, BrightColor.Blue, FadeDuration.From(30));
```
<!-- endSnippet -->

## Clignotement (effet stroboscopique)

| Signature | Description |
| --- | --- |
| `bool Strobe(BrightColor color, Speed speed, Repeat repeat)` | Fait clignoter toutes les LEDs dans une couleur. |
| `bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat)` | Fait clignoter les LEDs ciblées dans une couleur. |

La vitesse s'exprime elle aussi dans l'unité du périphérique : `Speed.FromByte(byte)`. Le nombre de répétitions se déclare avec `Repeat.Once`, `Repeat.Twice` ou `Repeat.Count(byte)`.

<!-- snippet: strobe -->
```csharp
device.Strobe(BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
device.Strobe(TargetedLeds.TabSide, BrightColor.Yellow, Speed.FromByte(20), Repeat.Twice);
```
<!-- endSnippet -->

## Vagues et motifs intégrés

| Signature | Description |
| --- | --- |
| `bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat)` | Démarre une vague colorée sur toutes les LEDs du périphérique. |
| `bool PlayPattern(BuiltInPattern pattern, Repeat repeat)` | Démarre un motif intégré au périphérique. |

Les vagues vont de `WavePattern.Wave_1` à `WavePattern.Wave_5`. Les motifs intégrés sont `BuiltInPattern.Pattern_1` à `Pattern_5`, plus `Rainbow`, `TrafficLight`, `Police` et `Off`.

<!-- snippet: play-pattern -->
```csharp
device.PlayPattern(WavePattern.Wave_1, BrightColor.Cyan, Speed.FromByte(10), Repeat.Count(5));
device.PlayPattern(BuiltInPattern.Police, Repeat.Twice);
```
<!-- endSnippet -->

## Cibler des LEDs

`TargetedLeds` désigne les LEDs qu'une commande allume, éteint ou anime simultanément.

| Valeur | LEDs concernées |
| --- | --- |
| `TargetedLeds.All` | Les six LEDs. |
| `TargetedLeds.TabSide` | Les LEDs n° 1, 2 et 3. |
| `TargetedLeds.BackSide` | Les LEDs n° 4, 5 et 6. |
| `TargetedLeds.Led_1` ... `TargetedLeds.Led_6` | Une seule LED. |

Un `LedIndex` (`LedIndex._1` à `LedIndex._6`, ou `LedIndex.From(byte)`) se convertit implicitement en `TargetedLeds`, ce qui permet de passer un index là où une cible est attendue. Les autres combinaisons ne sont pas exprimables : le périphérique ne les accepte pas. Il faut alors enchaîner plusieurs commandes, au prix d'un effet de vaguelette puisque l'allumage devient séquentiel.

<!-- snippet: targeted-leds -->
```csharp
device.SetColor(TargetedLeds.All, BrightColor.Black);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);   // LEDs n° 1, 2 and 3
device.SetColor(TargetedLeds.BackSide, BrightColor.Red);    // LEDs n° 4, 5 and 6
device.SetColor(TargetedLeds.Led_1, BrightColor.Blue);
device.SetColor(LedIndex.From(6), BrightColor.Blue);
```
<!-- endSnippet -->

## Couleurs

`BrightColor` propose les couleurs `Red`, `Green`, `Blue`, `Yellow`, `Cyan`, `Magenta`, `White` et `Black`, ou se construit à partir d'une représentation hexadécimale (`#RRGGBB`) ou de trois composantes.

<!-- snippet: colors -->
```csharp
device.SetColor(BrightColor.Red);                    // Green, Blue, Yellow, Cyan, Magenta, White, Black
device.SetColor(BrightColor.From("#0F11A8"));        // from an hexadecimal representation
device.SetColor(BrightColor.From(15, 17, 168));      // from its red, green and blue components
```
<!-- endSnippet -->

`BrightColor.From(string)` lève une `FormatException` lorsque la chaîne n'est pas une représentation hexadécimale valide.

## Commandes réutilisables

Une `LightingCommand` décrit une commande une fois pour toutes, pour la rejouer ensuite avec `Send`.

| Fabrique | Équivalent |
| --- | --- |
| `LightingCommand.CreateTurnOffCommand()` | `TurnOff()` |
| `LightingCommand.CreateTurnOffCommand(TargetedLeds)` | `TurnOff(TargetedLeds)` |
| `LightingCommand.CreateSetColorCommand(BrightColor)` | `SetColor(BrightColor)` |
| `LightingCommand.CreateSetColorCommand(TargetedLeds, BrightColor)` | `SetColor(TargetedLeds, BrightColor)` |
| `LightingCommand.CreateFadeColorCommand(TargetedLeds, BrightColor, FadeDuration)` | `FadeColor(TargetedLeds, BrightColor, FadeDuration)` |
| `LightingCommand.CreateStrobeCommand(TargetedLeds, BrightColor, Speed, Repeat)` | `Strobe(TargetedLeds, BrightColor, Speed, Repeat)` |
| `LightingCommand.CreatePlayWavePatternCommand(WavePattern, BrightColor, Speed, Repeat)` | `PlayPattern(WavePattern, BrightColor, Speed, Repeat)` |
| `LightingCommand.CreatePlayBuiltInPatternCommand(BuiltInPattern, Repeat)` | `PlayPattern(BuiltInPattern, Repeat)` |

<!-- snippet: reusable-commands -->
```csharp
LightingCommand alert = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
LightingCommand calm  = LightingCommand.CreateSetColorCommand(BrightColor.Green);

device.Send(alert);
device.Send(calm);
```
<!-- endSnippet -->
