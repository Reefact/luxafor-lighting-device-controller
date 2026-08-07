_[English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor.md) - [Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-FR.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-NL.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-SE.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-DE.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-GR.md) - [Polski](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-PL.md)_

[← Volver al README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md)

# Luxafor, la empresa y sus dispositivos

Esta página aporta contexto: describe el hardware, no la biblioteca. Para utilizar la biblioteca bastan el [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md) y la [referencia de la API](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-ES.md).

## La empresa

[Luxafor](https://luxafor.com) diseña y vende productos para la productividad en la oficina, como indicadores de disponibilidad y herramientas de notificación.

Su producto estrella es un [indicador LED de disponibilidad](https://luxafor.com/product/flag) que puede programarse para mostrar distintos colores en función del estado de disponibilidad del usuario.

El objetivo de Luxafor es proporcionar a los usuarios una forma sencilla y eficaz de señalar su disponibilidad a los compañeros de trabajo y mejorar la comunicación y la colaboración en el lugar de trabajo.

## El catálogo

He aquí una lista no exhaustiva de [dispositivos Luxafor](https://luxafor.com/products):

- `Luxafor Flag`: un indicador LED de disponibilidad que muestra la disponibilidad personal
- `Luxafor Bluetooth`: un indicador LED de disponibilidad inalámbrico y controlado por software que muestra las notificaciones y la disponibilidad personal
- `Luxafor Switch`: un indicador de disponibilidad inalámbrico y teledirigido que muestra en tiempo real la disponibilidad de las salas de reuniones y los puestos de trabajo
- `Luxafor Cube`: un indicador LED de disponibilidad autónomo que muestra la disponibilidad de las salas de reuniones
- `Luxafor Pomodoro-Timer`: un temporizador LED alimentado por USB que permite dividir el trabajo en espacios más pequeños (véase [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: un indicador LED de disponibilidad USB de gran ángulo
- `Luxafor CO2 Monitor`: un sensor que analiza la calidad del aire de una habitación y le avisa cuando es necesario ventilarla
- `Luxafor Mute Button`: enciende/apaga el micrófono con un solo toque e indica si estás disponible con el rojo/verde
- `Luxafor Colorblind Flag`: luz monocroma USB LED de disponibilidad que elimina las distracciones y aumenta la productividad

## Cómo se controlan

Estos diferentes dispositivos están diseñados para ser accionados manualmente ("mecánicamente") en algunos casos, semiautomáticamente (accionamiento manual vía [software](https://luxaformanual.com)) o automáticamente (integración vía [software](https://luxaformanual.com) con herramientas como Teams, Skype, Cisco, Zappier, o vía Webhook) en otros.

Ese es precisamente el hueco que llena esta biblioteca: controlar los dispositivos LED USB desde sus propias aplicaciones, sin pasar por el servidor Luxafor (webhook) ni por software de terceros. Para ello se apoya en [HidLibrary](https://github.com/mikeobrien/HidLibrary), que permite enumerar y comunicarse con dispositivos USB compatibles con HID en .NET.

## Qué controla la biblioteca

Sólo afecta a los dispositivos que exponen el protocolo USB HID de Luxafor, identificados por el vendor id `1240` (`0x04D8`) y el product id `62322` (`0xF372`). La tabla de compatibilidad está en el [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md#dispositivos-compatibles).
