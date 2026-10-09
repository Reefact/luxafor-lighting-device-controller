_[English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api.md) - [Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-FR.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-NL.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-SE.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-DE.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-GR.md) - [Polski](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-PL.md)_

[← Volver al README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md)

# Referencia de la API

Todo se encuentra en un único espacio de nombres, `Reefact.LuxaforLightingDeviceController`. La clase estática `Luxafor` es el punto de entrada: entrega instancias de `ILuxaforDevice`, a las que después se envían los comandos.

Los ejemplos de esta página se extraen de la carpeta [`samples`](https://github.com/Reefact/luxafor-lighting-device-controller/tree/main/samples), que la CI compila: no pueden desincronizarse del código.

## Obtener un dispositivo

| Firma | Descripción |
| --- | --- |
| `IEnumerable<ILuxaforDevice> Luxafor.GetDevices()` | Todos los dispositivos Luxafor conectados a los puertos USB. La enumeración está vacía cuando no hay ninguno conectado. |
| `ILuxaforDevice Luxafor.GetDevice(string devicePath)` | El dispositivo Luxafor situado en la ruta indicada. |

<!-- snippet: list-devices -->
```csharp
foreach (ILuxaforDevice device in Luxafor.GetDevices()) {
    using (device) {
        Console.Out.WriteLine(device.Description + " — " + device.Path);
    }
}
```
<!-- endSnippet -->

`ILuxaforDevice` implementa `IDisposable`: la instrucción `using` libera el descriptor del dispositivo al final del bloque. Un dispositivo también expone su ruta (`Path`) y su descripción (`Description`), tal como las informa Windows.

`Luxafor.GetDevice` lanza una `LuxaforDeviceNotFoundException` cuando no se encuentra ningún dispositivo en la ruta indicada, cuando el dispositivo encontrado no es un dispositivo Luxafor compatible o cuando ya no está conectado. La propiedad `DevicePath` de la excepción devuelve la ruta solicitada.

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

## Resultado de un comando

Cada comando devuelve un `bool`: `true` cuando el dispositivo ha aceptado el comando, `false` cuando la escritura ha fallado (dispositivo desconectado, ocupado por otra aplicación, ...). Los argumentos no válidos, en cambio, lanzan una excepción (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

<!-- snippet: command-result -->
```csharp
if (!device.SetColor(BrightColor.Red)) {
    Console.Error.WriteLine("The device refused the command: unplugged, or held by another application.");
}
```
<!-- endSnippet -->

## Comprobar que un dispositivo sigue conectado

`bool IsConnected` indica si el dispositivo sigue conectado. La propiedad pregunta a Windows si la ruta del dispositivo sigue figurando entre los dispositivos HID presentes: no se abre ni se escribe nada, así que se puede consultar periódicamente sin molestar al dispositivo, y un dispositivo que ha desaparecido responde `false` en lugar de lanzar una excepción, a diferencia de `Luxafor.GetDevice`.

<!-- snippet: is-connected -->
```csharp
if (!device.IsConnected) {
    Console.Error.WriteLine(device.Path + " has been unplugged.");
}
```
<!-- endSnippet -->

Un dispositivo desconectado y vuelto a conectar en el mismo puerto USB vuelve a estar presente, así que `IsConnected` vuelve a `true`, pero el descriptor que mantiene la instancia no sobrevive a la desconexión: sus comandos siguen devolviendo `false`. Para volver a controlar el dispositivo, hay que obtenerlo de nuevo con `Luxafor.GetDevice(device.Path)`.

## Apagar

| Firma | Descripción |
| --- | --- |
| `bool TurnOff()` | Apaga todos los LEDs del dispositivo. |
| `bool TurnOff(TargetedLeds targetedLeds)` | Apaga los LEDs seleccionados del dispositivo. |

<!-- snippet: turn-off -->
```csharp
device.TurnOff();
device.TurnOff(TargetedLeds.BackSide);
```
<!-- endSnippet -->

## Establecer un color

| Firma | Descripción |
| --- | --- |
| `bool SetColor(BrightColor color)` | Enciende todos los LEDs del dispositivo en un color. |
| `bool SetColor(TargetedLeds targetedLeds, BrightColor color)` | Enciende los LEDs seleccionados en un color. |

<!-- snippet: set-color -->
```csharp
device.SetColor(BrightColor.Green);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);
```
<!-- endSnippet -->

## Hacer una transición (fundido)

| Firma | Descripción |
| --- | --- |
| `bool FadeColor(BrightColor color, FadeDuration duration)` | Hace pasar todos los LEDs hacia un color. |
| `bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration)` | Hace pasar los LEDs seleccionados hacia un color. |

La duración se expresa en la unidad del dispositivo: `FadeDuration.From(byte)`, de `0` (inmediato) a `255` (el más lento).

<!-- snippet: fade-color -->
```csharp
device.FadeColor(BrightColor.Blue, FadeDuration.From(30));
device.FadeColor(TargetedLeds.BackSide, BrightColor.Blue, FadeDuration.From(30));
```
<!-- endSnippet -->

## Parpadeo (efecto estroboscópico)

| Firma | Descripción |
| --- | --- |
| `bool Strobe(BrightColor color, Speed speed, Repeat repeat)` | Hace parpadear todos los LEDs en un color. |
| `bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat)` | Hace parpadear los LEDs seleccionados en un color. |

La velocidad se expresa también en la unidad del dispositivo: `Speed.FromByte(byte)`. El número de repeticiones se declara con `Repeat.Once`, `Repeat.Twice` o `Repeat.Count(byte)`.

<!-- snippet: strobe -->
```csharp
device.Strobe(BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
device.Strobe(TargetedLeds.TabSide, BrightColor.Yellow, Speed.FromByte(20), Repeat.Twice);
```
<!-- endSnippet -->

## Ondas y patrones incorporados

| Firma | Descripción |
| --- | --- |
| `bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat)` | Inicia una onda de color sobre todos los LEDs del dispositivo. |
| `bool PlayPattern(BuiltInPattern pattern, Repeat repeat)` | Inicia un patrón incorporado en el dispositivo. |

Las ondas van de `WavePattern.Wave_1` a `WavePattern.Wave_5`. Los patrones incorporados son `BuiltInPattern.Pattern_1` a `Pattern_5`, además de `Rainbow`, `TrafficLight`, `Police` y `Off`.

<!-- snippet: play-pattern -->
```csharp
device.PlayPattern(WavePattern.Wave_1, BrightColor.Cyan, Speed.FromByte(10), Repeat.Count(5));
device.PlayPattern(BuiltInPattern.Police, Repeat.Twice);
```
<!-- endSnippet -->

## Seleccionar LEDs

`TargetedLeds` designa los LEDs que un comando enciende, apaga o anima simultáneamente.

| Valor | LEDs afectados |
| --- | --- |
| `TargetedLeds.All` | Los seis LEDs. |
| `TargetedLeds.TabSide` | Los LEDs n.º 1, 2 y 3. |
| `TargetedLeds.BackSide` | Los LEDs n.º 4, 5 y 6. |
| `TargetedLeds.Led_1` ... `TargetedLeds.Led_6` | Un solo LED. |

Un `LedIndex` (`LedIndex._1` a `LedIndex._6`, o `LedIndex.From(byte)`) se convierte implícitamente en `TargetedLeds`, de modo que se puede pasar un índice donde se espera un objetivo. Las demás combinaciones no son expresables: el dispositivo no las acepta. Entonces hay que encadenar varios comandos, al precio de un efecto de ondulación visible, ya que el encendido pasa a ser secuencial.

<!-- snippet: targeted-leds -->
```csharp
device.SetColor(TargetedLeds.All, BrightColor.Black);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);   // LEDs n° 1, 2 and 3
device.SetColor(TargetedLeds.BackSide, BrightColor.Red);    // LEDs n° 4, 5 and 6
device.SetColor(TargetedLeds.Led_1, BrightColor.Blue);
device.SetColor(LedIndex.From(6), BrightColor.Blue);
```
<!-- endSnippet -->

## Colores

`BrightColor` ofrece los colores `Red`, `Green`, `Blue`, `Yellow`, `Cyan`, `Magenta`, `White` y `Black`, o se construye a partir de una representación hexadecimal (`#RRGGBB`) o de tres componentes.

<!-- snippet: colors -->
```csharp
device.SetColor(BrightColor.Red);                    // Green, Blue, Yellow, Cyan, Magenta, White, Black
device.SetColor(BrightColor.From("#0F11A8"));        // from an hexadecimal representation
device.SetColor(BrightColor.From(15, 17, 168));      // from its red, green and blue components
```
<!-- endSnippet -->

`BrightColor.From(string)` lanza una `FormatException` cuando la cadena no es una representación hexadecimal válida.

## Comandos reutilizables

Un `LightingCommand` describe un comando de una vez por todas, para volver a lanzarlo después con `Send`.

| Fábrica | Equivale a |
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
