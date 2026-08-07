_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG.md) - [Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-FR.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-DE.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-GR.md) - [Wersja polska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-PL.md)_

# Registro de cambios

Todos los cambios notables de este proyecto quedan documentados en este archivo.

El formato se basa en [Keep a Changelog](https://keepachangelog.com/es/1.1.0/) y este proyecto sigue el
[versionado semántico](https://semver.org/lang/es/spec/v2.0.0.html).

## [2.0.1]

Sólo documentación, herramientas y CI: la biblioteca en sí no ha cambiado desde la 2.0.0, ni tampoco su API
pública.

### Añadido

- Una prueba de consumo del paquete producido, `build/Test-PackageConsumption.ps1`: un proyecto desechable
  creado fuera del repositorio instala el `.nupkg` desde un origen local en una caché de paquetes privada,
  compila los ejemplos públicos contra él para `net472` y `net10.0`, comprueba los recursos resueltos
  (`lib/net462`, `lib/netstandard2.0`, documentación XML) y la dependencia `hidlibrary`, y después ejecuta el
  resultado en Windows. Leer el archivo comprimido no permite detectar una dependencia que no se resuelve ni
  un recurso que nunca llega al consumidor; esta prueba sí. Se ejecuta en la CI y en el flujo de publicación
  antes de publicar nada.
- Una carpeta `samples` con los ejemplos que aparecen en la documentación, compilados junto con las pruebas
  en cada framework de destino, además de `build/Sync-Snippets.ps1`, que los copia a las páginas markdown. La
  CI lo ejecuta en modo comprobación, de modo que un ejemplo ya no puede desincronizarse del código que
  muestra.
- Un `CONTRIBUTING.md` que describe cómo compilar, qué comprueba la CI y cómo se publica una versión, además
  de una plantilla de pull request.
- Este registro de cambios está ahora traducido a los siete idiomas del README. `CHANGELOG.md` sigue siendo
  la versión inglesa — la referencia que siguen las traducciones — porque las notas de versión del paquete
  apuntan a ella y porque las herramientas esperan los encabezados de Keep a Changelog en inglés; los demás
  idiomas llevan sufijo (`CHANGELOG-FR.md`, `CHANGELOG-NL.md`, ...).
- El polaco se suma a los idiomas de la documentación: `README-PL.md`, `docs/api-PL.md`,
  `docs/luxafor-PL.md` y `CHANGELOG-PL.md`.

### Cambiado

- Documentación: cada README ofrece ahora primero lo que busca quien lo lee — para qué sirve la biblioteca,
  cómo instalarla, un inicio rápido, los dispositivos compatibles, las funcionalidades, y después dónde
  seguir leyendo. La presentación de la empresa Luxafor y su catálogo de productos se han trasladado a
  `docs/luxafor*.md`, y el recorrido comando por comando se ha convertido en una referencia de la API en
  `docs/api*.md`, ambos en los mismos siete idiomas.
- `README.md` es ahora la versión inglesa: es lo que GitHub muestra en la página principal del repositorio
  y lo que lleva el paquete, y el inglés es el idioma que esperan sus lectores internacionales. La versión
  francesa se ha trasladado a `README-FR.md`, y las otras seis conservan su sufijo. Hay que actualizar
  cualquier enlace que apunte a `README-EN.md`.
- Los flujos de trabajo pasan a `actions/checkout@v5`, `actions/setup-dotnet@v5` y
  `actions/upload-artifact@v6`, la primera versión mayor de cada una que se ejecuta sobre Node 24, ya que
  Node 20 está obsoleto en los agentes.

### Corregido

- La documentación XML de `TargetedLeds` ya no dice "the on/off or animation will also be sequential and
  could: it can cause a visual ripple effect".
- La comprobación de metadatos de `build/Validate-Package.ps1` combinaba sus dos condiciones con `-and`, algo
  que ningún valor podía satisfacer: una `<description>` vacía pasaba. Ahora señala por igual los elementos
  vacíos, en blanco, ausentes y sin atributos.

## [2.0.0]

### Cambios que rompen la compatibilidad

- `TargetedLeds.TabSide` y `TargetedLeds.BackSide` iluminan por fin la cara que nombran. Estaban invertidos:
  `TabSide` enviaba el código lux 66 (`0x42`) y `BackSide` el 65 (`0x41`), mientras que el protocolo Luxafor
  asigna el 65 a la cara de la pestaña (LEDs n.º 1, 2 y 3) y el 66 a la cara trasera (LEDs n.º 4, 5 y 6). La
  API no cambia y nada deja de compilar, pero **los LEDs que se encienden sí cambian**: el código escrito
  para la 1.x controlaba la cara opuesta, así que hay que retirar cualquier apaño que intercambiara ambas.
  Verificado en un dispositivo antes del cambio.
- La interfaz de dispositivo pasa de llamarse `LuxaforDevice` a `ILuxaforDevice`, lo que alinea la API
  pública con la convención de nomenclatura de .NET que usa el resto del ecosistema (`IDisposable`,
  `IEnumerable<T>`, ...). Los consumidores deben actualizar el nombre del tipo; los miembros no cambian, así
  que la migración se reduce a un renombrado:
  `using ILuxaforDevice orb = Luxafor.GetDevices().First();`. Las implementaciones siguen nombrando su
  especialización (`HidLuxaforDevice`), y `Luxafor`, `LuxaforDeviceLocator` y
  `LuxaforDeviceNotFoundException` conservan su nombre: no son interfaces.
- Los objetos de valor (`BrightColor`, `FadeDuration`, `LedIndex`, `LightingCommand`, `Repeat`, `Speed`,
  `TargetedLeds`) ya no derivan de `Value.ValueType<T>`: implementan su propia igualdad (`Equals`,
  `GetHashCode`, `==`, `!=`, `IEquatable<T>`) con la misma semántica de valor, y el paquete `Value` ha dejado
  de ser una dependencia. Compatible a nivel de código fuente para cualquier uso normal (comparaciones,
  claves de diccionario, `IEquatable<T>`), pero incompatible a nivel binario: recompile contra la 2.0.0. Sólo
  hay que adaptar el código que referencia explícitamente el tipo base `Value.ValueType<T>` (o que
  sobrescribe `GetAllAttributesToBeUsedForEquality`).
- `Luxafor.GetDevice(devicePath)` informa ahora explícitamente de las rutas no válidas en lugar de fallar con
  una oscura `ArgumentNullException`: lanza `ArgumentException` ante una ruta vacía y
  `LuxaforDeviceNotFoundException` cuando no hay ningún dispositivo en la ruta, cuando no es un dispositivo
  Luxafor compatible, o cuando ya no está conectado.
- El paquete ya no entrega un ensamblado `net46` dentro de una carpeta `net462`: el destino `net462` se
  compila realmente contra .NET Framework 4.6.2.

### Añadido

- `LuxaforDeviceNotFoundException`, que lleva consigo el `DevicePath` culpable.
- `TargetedLeds.FromLedIndex(LedIndex)`, la alternativa con nombre a la conversión implícita existente.
- Source Link, un paquete de símbolos (`.snupkg`) y compilaciones deterministas.
- Una CI de GitHub Actions (Windows) que compila, prueba, empaqueta y valida el contenido del `.nupkg`.
- Un flujo de trabajo de publicación de GitHub Actions que publica en nuget.org cuando se envía una etiqueta
  `v*`, tras comprobar que la etiqueta coincide con la versión del proyecto, ejecutar las pruebas y validar
  el paquete. Se autentica mediante trusted publishing (OIDC): no se guarda ninguna clave de API de larga
  duración en el repositorio.
- Pruebas que cubren la propagación de fallos de escritura, `Dispose`, los dispositivos ausentes, las rutas
  HID no válidas, las comprobaciones de argumentos, la igualdad de los objetos de valor y la superficie de la
  API pública.

### Cambiado

- Un único proyecto en formato SDK que apunta a la vez a `netstandard2.0` y `net462`, en sustitución del
  Shared Project, del proyecto .NET Framework no basado en SDK y del `.nuspec` escrito a mano; el paquete lo
  produce ahora `dotnet pack -c Release` (a partir de los binarios de Release, mientras que el `.nuspec`
  anterior recogía los de Debug).
- Están activados los tipos de referencia que admiten null, las advertencias como errores y los analizadores
  de .NET.
- HidLibrary se utiliza a través de una abstracción interna (`IHidDeviceRegistry`, `IHidDeviceHandle`), lo
  que la mantiene fuera de la API pública y hace que la lógica de control sea comprobable sin hardware.
- Renombrados internos, sin impacto en la API pública: `LuxaforDeviceImp` pasa a ser `HidLuxaforDevice`, los
  archivos y carpetas `Lightning*` pasan a ser `Lighting*` (como el tipo `LightingCommand` que contienen), y
  la interfaz interna `LightingCommandFactory` pasa a ser `ILightingCommandFactory`.
- Documentación: los README (7 idiomas) están sincronizados con el código — ejemplos obsoletos de
  `BasicColor` / `SetBasicColor` sustituidos por `BrightColor` / `SetColor`, firmas `void` corregidas a
  `bool`, `using` mostrado sobre `ILuxaforDevice`, más las secciones de instalación, búsqueda de
  dispositivo, gestión de errores, dispositivos compatibles y licencia.

### Corregido

- Los códigos lux enviados para `TargetedLeds.TabSide` y `TargetedLeds.BackSide` estaban invertidos, de modo
  que ambos iluminaban la cara opuesta del dispositivo. Véanse los cambios que rompen la compatibilidad más
  arriba: éste es silencioso, cambia el comportamiento sin romper la compilación.
- Los comandos `FadeColor` ya no se describen con la errata "duration od" en `ToString()`.

## [1.2.0]

### Añadido

- Los comandos de dispositivo devuelven un `bool` para indicar si la operación ha tenido éxito.
- `LuxaforDevice` implementa `IDisposable` para permitir una liberación correcta de los recursos.
