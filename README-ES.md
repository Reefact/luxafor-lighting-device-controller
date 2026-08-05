_[Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-EN.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)_

# Controlador de dispositivos Luxafor

Una librería .Net que proporciona una API simple para controlar los dispositivos Luxafor.

## Luxafor

### Presentación de la empresa

[Luxafor](https://luxafor.com) es una empresa que diseña y vende productos para la productividad en la oficina, como indicadores de disponibilidad y herramientas de notificación. 

Su producto estrella es un [indicador LED de disponibilidad](https://luxafor.com/product/flag) que puede programarse para mostrar distintos colores en función del estado de disponibilidad del usuario. 

El objetivo de Luxafor es proporcionar a los usuarios una forma sencilla y eficaz de señalar su disponibilidad a los compañeros de trabajo y mejorar la comunicación y la colaboración en el lugar de trabajo.

### Vista rápida de los dispositivos

He aquí una lista no exhaustiva de [dispositivos Luxafor](https://luxafor.com/products):

- `Luxafor Flag`: un indicador LED de disponibilidad que muestra la disponibilidad personal
- `Luxafor Bluetooth`: un indicador LED de disponibilidad inalámbrico y controlado por software que muestra las notificaciones y la disponibilidad personal.
- `Luxafor Switch`: un indicador de disponibilidad inalámbrico y teledirigido que muestra en tiempo real la disponibilidad de las salas de reuniones y los puestos de trabajo.
- `Luxafor Cube`: un indicador LED de disponibilidad autónomo que muestra la disponibilidad de las salas de reuniones
- `Luxafor Pomodoro-Timer`: un temporizador LED alimentado por USB que permite dividir el trabajo en espacios más pequeños (véase [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: un indicador LED de disponibilidad USB de gran ángulo
- `Luxafor CO2 Monitor`: un sensor que analiza la calidad del aire de una habitación y le avisa cuando es necesario ventilarla.
- `Botón Luxafor Mute`: enciende/apaga el micrófono con un solo toque e indica si estás disponible con el rojo/verde
- `Luxafor Colorblind Flag`: luz monocroma USB LED de disponibilidad elimina las distracciones y aumenta la productividad

### Integración

Estos diferentes dispositivos están diseñados para ser accionados manualmente ("mecánicamente") para algunos, semiautomáticamente (accionamiento manual vía [software](https://luxaformanual.com)) / automáticamente (integración vía [software](https://luxaformanual.com) con herramientas como Teams, Skype, Cisco, Zappier o vía Webhook) para otros. 

## Presentación de la biblioteca

Esta librería tiene como objetivo permitir la integración de dispositivos LED USB a sus aplicaciones internas sin necesidad de pasar por el servidor Luxafor (webhook).

Tiene como destino `.NET Standard 2.0` y `.NET Framework 4.6.2`, y se basa en la librería [HidLibrary](https://github.com/mikeobrien/HidLibrary) que permite enumerar y comunicarse con dispositivos USB compatibles con HID en .NET.

> **Sólo Windows.** Los dispositivos se controlan a través de la capa HID de Windows: el paquete se instala en cualquier plataforma, pero los dispositivos sólo pueden enumerarse y controlarse en Windows.

### Dispositivos compatibles

La biblioteca habla el protocolo USB HID de los dispositivos Luxafor identificados por el vendor id `1240` (`0x04D8`) y el product id `62322` (`0xF372`).

| Dispositivo | Estado |
| --- | --- |
| `Luxafor Orb` | **Probado**: el dispositivo utilizado para desarrollar y validar la biblioteca (6 LEDs direccionables). |
| `Luxafor Flag` | **Debería funcionar, no probado**: mismos identificadores y mismo protocolo de iluminación (6 LEDs direccionables). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Deberían funcionar, no probados**: los comandos de iluminación son los mismos; la disposición de los LEDs, su número y la reproducción de los colores pueden diferir. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **No compatibles**: estos dispositivos no se controlan mediante este protocolo USB HID. |

Cualquier comentario sobre un dispositivo no probado es bienvenido: no dude en [abrir una incidencia](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

### Instalación

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

### Inicio rápido

El siguiente código muestra un ejemplo de uso básico de la biblioteca para controlar un dispositivo [Luxafor Orb](https://luxafor.com/product/orb/).

```csharp
[Fact]
public void french_sequence() {
    using LuxaforDevice orb = Luxafor.GetDevices().First();
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

La línea 3 muestra cómo conectarse a un único Orb conectado al puerto USB de la máquina. `LuxaforDevice` implementa `IDisposable`: la instrucción `using` libera el descriptor del dispositivo al final del bloque.

### Obtener un dispositivo

```csharp
IEnumerable<LuxaforDevice> GetDevices(); // Todos los dispositivos Luxafor conectados a los puertos USB (vacío si no hay ninguno)
LuxaforDevice GetDevice(string devicePath); // El dispositivo Luxafor situado en la ruta indicada
```

`Luxafor.GetDevice` lanza una `LuxaforDeviceNotFoundException` cuando no se encuentra ningún dispositivo en la ruta indicada, cuando el dispositivo encontrado no es un dispositivo Luxafor compatible o cuando ya no está conectado.

```csharp
using LuxaforDevice orb = Luxafor.GetDevice(@"\\?\hid#vid_04d8&pid_f372#...");
```

Repasaré rápidamente todos los comandos que se pueden enviar a los dispositivos desde el `LuxaforDevice`.

Cada comando devuelve un `bool`: `true` cuando el dispositivo ha aceptado el comando, `false` cuando la escritura ha fallado (dispositivo desconectado, ocupado por otra aplicación, ...). Los argumentos no válidos lanzan una excepción (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

### Apagar

```csharp
bool TurnOff(); // Apaga todos los LEDs del dispositivo
bool TurnOff(TargetedLeds targetedLeds); // Apagar los LEDs del dispositivo apuntado
```

### Establecer un solo color

```csharp
bool SetColor(BrightColor color); // Enciende los LEDs del dispositivo en un color personalizado.
bool SetColor(TargetedLeds targetedLeds, BrightColor color); // Enciende los LEDs del dispositivo targeted en un color personalizado.
```

### Hacer una transición (fundido)

```csharp
bool FadeColor(BrightColor color, FadeDuration duration); // Cambia todos los LEDs del dispositivo a un color personalizado
bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration); // Transición de los LEDs del dispositivo a un color personalizado
```

### Parpadeo (efecto estroboscópico)

```csharp
bool Strobe(BrightColor color, Speed speed, Repeat repeat); // Parpadea todos los LEDs del dispositivo en un color personalizado.
bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat); // Hace parpadear los LEDs del dispositivo objetivo en un color personalizado
```

### Ondas / Patrones incorporados

```csharp
bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat); // Inicia un patrón de onda que se dirige a todos los LEDs del dispositivo basándose en un color personalizado.
bool PlayPattern(BuiltInPattern pattern, Repeat repeat); // Iniciar un patrón incorporado que apunte a todos los LEDs del dispositivo.
```

### Enviar un comando

Es posible crear comandos personalizados llamados `LightingCommand` para que puedan ser reutilizados en el código:

```csharp
var command = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

El método `Send` le permite utilizar estos comandos.

```csharp
bool Send(LightingCommand command); // Enviar un comando al dispositivo
```

### Colores

```csharp
BrightColor.Red; // así como Green, Blue, Yellow, Cyan, Magenta, White, Black
BrightColor.From("#0F11A8"); // A partir de su representación hexadecimal
BrightColor.From(15, 17, 168); // A partir de sus componentes roja, verde y azul
```

## Compilar la biblioteca

```shell
dotnet build -c Release
dotnet test -c Release
dotnet pack Reefact.LuxaforLightingDeviceController -c Release -o artifacts
```

## Licencia

Esta biblioteca se distribuye bajo la licencia [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE).
