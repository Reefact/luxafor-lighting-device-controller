_[Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-EN.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)_

# Controlador de dispositivos Luxafor

Controle sus indicadores de disponibilidad [Luxafor](https://luxafor.com) directamente desde sus propias aplicaciones .NET, hablando su protocolo USB HID: sin servidor Luxafor, sin webhook, sin software de terceros que instalar.

- Una API directa: `SetColor`, `FadeColor`, `Strobe`, `PlayPattern`, hasta controlar un LED a la vez.
- `.NET Standard 2.0` y `.NET Framework 4.6.2`, con una única dependencia ([HidLibrary](https://github.com/mikeobrien/HidLibrary)).
- **Solo Windows**: los dispositivos se controlan a través de la capa HID de Windows. El paquete se instala en cualquier plataforma, pero los dispositivos sólo pueden enumerarse y controlarse en Windows.

## Instalación

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

## Inicio rápido

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

`Luxafor.GetDevices()` enumera los dispositivos Luxafor conectados a los puertos USB de la máquina; la enumeración está vacía cuando no hay ninguno conectado. `ILuxaforDevice` implementa `IDisposable`: la instrucción `using` libera el descriptor del dispositivo al final del bloque. Cada comando devuelve un `bool`: `true` cuando el dispositivo ha aceptado el comando, `false` cuando la escritura ha fallado.

## Dispositivos compatibles

La biblioteca controla, mediante su protocolo USB HID, los dispositivos Luxafor identificados por el vendor id `1240` (`0x04D8`) y el product id `62322` (`0xF372`).

| Dispositivo | Estado |
| --- | --- |
| `Luxafor Orb` | **Probado**: el dispositivo utilizado para desarrollar y validar la biblioteca (6 LEDs direccionables). |
| `Luxafor Flag` | **Debería funcionar, no probado**: mismos identificadores y mismo protocolo de iluminación (6 LEDs direccionables). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Deberían funcionar, no probados**: los comandos de iluminación son los mismos; la disposición de los LEDs, su número y la reproducción de los colores pueden diferir. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **No compatibles**: estos dispositivos no se controlan mediante este protocolo USB HID. |

Cualquier comentario sobre un dispositivo no probado es bienvenido: no dude en [abrir una incidencia](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

## Funcionalidades

- **Color fijo** (`SetColor`) a partir de un color con nombre, un código hexadecimal o componentes roja / verde / azul.
- **Fundido** (`FadeColor`) hacia un color, durante una duración de transición elegida.
- **Parpadeo** (`Strobe`) a una velocidad y durante un número de repeticiones elegidos.
- **Patrones** (`PlayPattern`): ondas de color, o patrones incorporados en el dispositivo (`Police`, `Rainbow`, `TrafficLight`, ...).
- **Apagado** (`TurnOff`), de todos los LEDs o sólo de una parte.
- **Selección de LEDs**: todos, una cara (`TabSide`, `BackSide`) o un LED concreto (`Led_1` a `Led_6`).
- **Comandos reutilizables**: `LightingCommand` describe un comando una vez, para volver a lanzarlo con `Send`.

## Documentación detallada

- [Referencia de la API](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-ES.md): todos los comandos, sus parámetros y sus errores.
- [Luxafor, la empresa y sus dispositivos](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-ES.md): para qué sirven estos indicadores y cuáles controla esta biblioteca.
- [Registro de cambios](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG.md) y [guía de contribución](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CONTRIBUTING.md).

## Licencia

Esta biblioteca se distribuye bajo la licencia [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE).
