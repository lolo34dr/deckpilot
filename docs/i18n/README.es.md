> Esta es una traducción automática de la README completa. El original inglés es la referencia. Las etiquetas de interfaz de aplicación y los comandos se conservan exactamente.

<p align="center">
  <img src="../../deckpilotbanner.png#gh-light-mode-only" alt="DeckPilot banner" width="760">
  <img src="../../deckpilotlogo.png#gh-dark-mode-only" alt="DeckPilot logo" width="112">
</p>

<h1 align="center">DeckPilot</h1>

<p align="center">
  Un compañero de Windows para su 12- El macropado DIY.<br>
  Configura tus llaves, aplicaciones de lanzamiento, medios de control y perfiles de conmutación.
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest">Descargar la última versión</a>
   · 
  <a href="#getting-started">Comienzo</a>
   · 
  <a href="#development">Desarrollo</a>
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&color=78b7a0" alt="Última publicación"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&logo=windows" alt="Descargar para Windows x64"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Atelier-Setup.exe"><img src="https://img.shields.io/badge/Download-DeckPilot_Atelier-16856A?style=flat-square&logo=windows" alt="Descargar DeckPilot Atelier"></a>
  <a href="https://github.com/lolo34dr/deckpilot/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lolo34dr/deckpilot?style=flat-square" alt="Licencia MIT"></a>
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml"><img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg?branch=main" alt="estado de construcción de Windows"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases"><img src="https://img.shields.io/github/downloads/lolo34dr/deckpilot/total?style=flat-square&color=70a7e8" alt="Descarga de descargas de GitHub"></a>
</p>

<p align="center">
  <a href="../../README.md">🇬🇧 English</a> ·
  <a href="README.fr.md">🇫🇷 Français</a> ·
  <a href="README.de.md">🇩🇪 Deutsch</a> ·
  <a href="README.es.md">🇪🇸 Español</a> ·
  <a href="README.it.md">🇮🇹 Italiano</a> ·
  <a href="README.pt-BR.md">🇧🇷 Português brasileiro</a> ·
  <a href="README.nl.md">🇳🇱 Nederlands</a> ·
  <a href="README.pl.md">🇵🇱 Polski</a> ·
  <a href="README.ru.md">🇷🇺 Русский</a> ·
  <a href="README.ja.md">🇯🇵 日本語</a> ·
  <a href="README.ko.md">🇰🇷 한국어</a> ·
  <a href="README.zh-CN.md">🇨🇳 简体中文</a> ·
  <a href="README.ar.md">🇸🇦 العربية</a>
</p>

---

DeckPilot configura Arduino Leonardo-compatible macropads que ya envía **F13-F24** como entradas de teclado USB, dispuestas en **3 filas de 4 llaves**. El orden de clave física puede ser arbitrario: un mapa de calibración guiado cada botón a su posición.

**Las características del teclado F13-F24 no necesitan cambios adicionales de controlador o firmware. Soundboard mezcla requiere la firma VB-CABLE controlador de audio, instalado por Setup cuando falta.** La interfaz de aplicación está actualmente en francés. Elija un idioma anterior para un README traducido completo, incluyendo la referencia técnica y la historia de la versión. Las traducciones se generan automáticamente; este original en inglés es la referencia.

<a id="features"></a>
## Características

- **Calibración guiada:** Identificar todos 12 botones físicos sin cambiar el macropado.
- **Picker de aplicación instalado:** búsqueda Iniciar atajos de menú y el catálogo de aplicaciones de Windows, incluyendo entradas de Microsoft Store. La selección manual de archivos sigue disponible.
- **Acciones personalizadas:** aplicaciones de lanzamiento, carpetas abiertas o sitios web, enviar accesos directos y medios de control.
- **Teclas libres:** Mantenga F13–F24 disponible para atajos configurados directamente en un juego u otra aplicación.
- **Perfiles locales:** crear, duplicar, renombrar, eliminar y seleccionar perfiles. La calibración se comparte a través de perfiles.
- **Perfiles listos:** empezar con plantillas de música, productividad o creación, a continuación, personalizarlas. Cambiar a un perfil específico, próximo o anterior de una clave física.
- **Edición más rápida:** copy/cut/paste/reset teclas completas, atajos de teclado discográficos, elegir colores de acento clave, y deshacer cambios.
- **Herramientas de Windows:** abrir sonido, visualización, Bluetooth y configuración de red, Administrador de tareas, o bloquear la sesión.
- **Interfaz oscura compacta:** iconos vectoriales, iconos de aplicación donde esté disponible, y animaciones sutiles que respetan las preferencias de animación de Windows.
- **Operación de antecedentes:** Manténganse. DeckPilot en el área de notificación, con el inicio de sesión de Windows habilitado por defecto (puede ser deshabilitado).
- **Actualizaciones automáticas:** aprobar una nueva versión, entonces dejar DeckPilot descargar, verificar, reemplazar y reiniciar en sí mismo.

<a id="getting-started"></a>
## Comienzo

<a id="requirements"></a>
### Necesidades

- Windows 10 o Windows 11, **x64**.
- Un macropado compatible con Arduino Leonardo 12 teclas distintas de **F13 a F24**.
- Una conexión de datos USB.

Las descargas publicadas incluyen el tiempo de ejecución .NET. **Usted no necesita instalar .NET para utilizar DeckPilot.**

<a id="install-and-launch"></a>
### Instalación y lanzamiento

1. Descargar **[DeckPilot-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** y ejecutar el instalador. Windows pide permiso del administrador para instalar **Program Files\DeckPilot**.
2. El instalador le permite elegir si DeckPilot Comienza con Windows, comprueba las actualizaciones de la aplicación automáticamente y reduce las animaciones. Los cheques de inicio y actualización están por defecto; las animaciones reducidas están apagadas. VB-CABLE conductor requerido por el Soundboard e lo instala automáticamente si falta. Se requiere una conexión a Internet para ese cheque de controlador.
3. El instalador añade DeckPilot a Windows **Aplicaciones instaladas** y crea un atajo de menú Inicio. Un atajo de escritorio es seleccionado por defecto.
4. Conecta tu macropado y abre DeckPilot de su atajo.
5. Siga la calibración de izquierda a derecha a través **cuatro columnas**, entonces la siguiente fila, para **tres filas**. Cada botón debe enviar una tecla F13-F24 diferente.

Al actualizarse de la primera 4- diseño de flechas, DeckPilot conserva perfiles/acciones y solicita una nueva calibración para las posiciones corregidas. Instalar sobre una configuración portátil existente también preserva la configuración en `%APPDATA%\DeckPilot`; renunciar a la copia vieja si la configuración le pide.

Un portátil [DeckPilot.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) y [ZIP](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip) permanecer disponible para compatibilidad. Para el uso portátil, mantener el ejecutable en una carpeta personal de escritura.

Desinstalación de Windows **Aplicaciones instaladas**. La desinstalación elimina el programa y sus atajos, al tiempo que conserva los perfiles personales para que una reinstalación posterior pueda reutilizarlos.

El ejecutable de un solo fichero extrae sus dependencias agrupadas en la caché .NET del usuario en el primer lanzamiento, que puede tardar un poco más. Los ejecutables publicados están actualmente sin firmar, por lo que Windows puede mostrar una advertencia SmartScreen.

<a id="configure-a-key"></a>
### Configurar una clave

1. Seleccione una llave en la cuadrícula.
2. Elija una acción en la barra lateral derecha.
3. Configurar su configuración debajo de la cuadrícula y guardar. Cambiar las teclas o los perfiles también guarda la configuración actual de la llave.

| Medida | Comportamiento |
| --- | --- |
| Tecla libre | Pasa F13–F24 a través de normalmente. Asignarlo directamente en su juego o aplicación. |
| Aplicación | Lanza una aplicación instalada o un programa seleccionado manualmente o atajo. |
| Folder | Abre una carpeta en Windows Explorer. |
| Sitio web | Abre una dirección HTTP o HTTPS en tu navegador predeterminado. |
| Atajo de teclado | Envía una combinación de teclas a la ventana activa. |
| Medios | Controla la reproducción, las pistas, el volumen o muda. |
| Spotify | Controla la reproducción de Spotify y ajusta sólo el volumen de audio-sesión de Windows de Spotify. |
| Aplicación DeckPilot | Muestra el existente DeckPilot ventana, incluso cuando se ejecuta en el área de notificación. |

Para acciones de aplicación, abra el selector de aplicaciones y busque por nombre. El selector de archivos manual permanece disponible, con argumentos opcionales de programa bajo opciones avanzadas. Introduzca argumentos de acuerdo con la sintaxis de la aplicación de destino.

Sostener un botón activa una acción configurada sólo una vez hasta la liberación. En modo free-key, los eventos normales de repetición del teclado permanecen disponibles.

<a id="manage-profiles"></a>
### Gestionar perfiles

Abra la sección de gestión de perfiles en la barra lateral derecha para crear, duplicar, renombrar o eliminar un perfil. Utilice el selector por encima de la cuadrícula clave para elegir el perfil activo.

La calibración se aplica a todos los perfiles y puede repetirse desde la barra lateral.

<a id="background-operation"></a>
### Operación de antecedentes

Cierre la ventana oculta DeckPilot Haga doble clic en su icono de la zona de notificación cerca del reloj para reabrirlo, o haga clic con el botón derecho en el icono y elija el comando para dejar de hacerlo completamente.

El inicio de Windows está habilitado por defecto para el usuario actual. Puede deshabilitarlo con **Lancer avec Windows**; actualizaciones de aplicaciones posteriores preservan esa opción. Instalación y lanzamientos posteriores refrescan una entrada habilitada al camino ejecutable actual.

<a id="installed-applications"></a>
## Aplicaciones instaladas

El selector lee atajos de los menús de inicio del usuario actual y de todo el sistema, luego los complementa con `Get-StartApps` resultados del catálogo de aplicaciones de Windows.

Buscar ignora el caso y los acentos. Los nombres duplicados se agrupan, con las entradas del menú Inicio tomando prioridad. Aplicaciones y programas portátiles sin accesos directos registrados todavía se pueden seleccionar manualmente. Si el catálogo de Windows no está disponible, legible Atajos del menú Inicio siguen siendo utilizables.

Windows lanza los atajos seleccionados. `shell:AppsFolder`. Los argumentos opcionales se aplican a programas basados en archivos y atajos; están ocultos para las entradas de AppsFolder.

<a id="automatic-updates"></a>
## Actualizaciones automáticas

Disponible desde **0.2.0**, cheques automáticos de actualización se ejecutan al inicio después de la calibración, luego cada seis horas. Pueden ser deshabilitados en la barra lateral. Un cheque manual permanece disponible.

Cuando se encuentra una nueva versión estable de GitHub, DeckPilot pregunta si actualizar. **Copias instaladas** descarga y SHA-256 verificar `DeckPilot-Setup.exe`, luego lanzar el instalador con la puesta en marcha actual, las preferencias de actualización automática y reducción de las emociones preseleccionadas. Usted puede cambiar durante la actualización. Windows pide permiso de administrador para actualizar los archivos del programa. DeckPilot elegantemente cuando esté listo, conserva perfiles y configuración, comprueba e instala el firmado VB-CABLE controlador si falta, y relanza la aplicación. Cancelar el instalador deja la aplicación abierta. Setup se niega a interrumpir una sesión de firmware abierto.

**Copias portátiles** mantener el mecanismo de actualización existente. Después de la confirmación,:

1. Descargue el ejecutable con soporte de presentación de informes y cancelación de progreso.
2. Verifica su SHA-256 hash against `SHA256SUMS.txt` de la misma liberación de GitHub.
3. Ahorra tu configuración y prepara el ayudante de actualización.
4. Exits, replaces the executable, and restarts.
5. Mantiene el ejecutable anterior hasta que el nuevo proceso confirma la inicialización exitosa; si la startup falla, restaura y relanza la versión anterior.

Elegir para actualizar más adelante pospone los impulsos automáticos para 24 Horas, controles manuales de bypass que posponen.

Las descargas utilizan HTTPS, con redirecciones restringidas a los hosts GitHub esperados. Los archivos temporales se almacenan en `%LOCALAPPDATA%\DeckPilot\Updates`; descargas mayores de siete días se limpian. La entrada de inicio de Windows continúa utilizando el mismo camino ejecutable después de una actualización.

Las copias portátiles requieren una carpeta personal de escritura. Las copias instaladas utilizan el instalador para directorios protegidos; el DeckPilot app en sí sigue funcionando sin derechos de administrador.

**Actualización desde 0.1.x?** Dejar la versión antigua del área de notificación y reemplazar manualmente al ejecutable con la última versión una vez. Esas versiones anteriores no contienen el actualizador. Las actualizaciones posteriores se pueden instalar desde dentro. DeckPilot.

<a id="repository-rename-compatibility"></a>
### Compatibilidad de renombre de repositorio

Versión **1.0.1** prepara el movimiento de `lolo34dr/streamdeck-diy` a `lolo34dr/deckpilot`. El repositorio está ahora `lolo34dr/deckpilot`. Los cheques de actualización utilizan el nuevo nombre y conservan la compatibilidad con el nombre original. Las descargas de lanzamiento se validan contra estos dos caminos de repositorio exactos.

Uso **1.0.1 o más tarde** con el repositorio renombrado. 0.2.0 y 1.0.0 validar estrictamente las trayectorias de activos originales y puede necesitar una actualización manual. Descargue el ejecutable actual desde la página de versiones si una versión anterior no puede actualizarse.

<a id="settings-and-backups"></a>
## Ajustes y copias de seguridad

Los perfiles y calibración se almacenan fuera de la carpeta de aplicación:

```text
%APPDATA%\DeckPilot\settings.json
```

Quit DeckPilot antes de copiar este archivo para respaldar o restaurar su configuración. Si un archivo de configuración no se puede leer, DeckPilot conserva una copia con una `.backup-<date>` sufijo antes de usar una configuración nueva.

No se requiere cuenta. Actualizar cheques contacto GitHub y puede ser deshabilitado en la interfaz.

<a id="development"></a>
## Desarrollo

Instalar el **.NET 8 SDK**, versión 8.0.415 o una nueva versión compatible permitida por `global.json`. Visual Studio 2022 con el volumen de trabajo de desarrollo de escritorio .NET es opcional.

Ejecute estos comandos desde la raíz del repositorio:

```powershell
dotnet restore DeckPilot.sln
dotnet build DeckPilot.sln -c Release --no-restore
dotnet run --project tests/DeckPilot.Checks -c Release
dotnet run --project src/DeckPilot.Windows -c Release
```

La biblioteca central y las consolas también se ejecutan en Linux. La interfaz WPF y las API de entrada de Windows requieren Windows.

<a id="validation"></a>
### Validación

La consola revisa la salida con un estado no cero en la primera afirmación fallida. Cubre:

- Cartografía clave, repeticiones de teclas, versiones, conmutación de perfil y copia de acción.
- Configuración de validación, persistencia y migración de configuraciones antiguas.
- Búsqueda de aplicaciones y manejo de objetivos.
- Selección de lanzamiento, sumas de comprobación, descargas truncadas o corruptas, redirecciones y cancelación.
- Reemplazo ejecutable, preservación de copias de seguridad, y revolver.

El control de bloqueo de archivos se ejecuta sólo en Windows.

GitHub Actions también funciona `tests/windows-update-smoke.ps1` contra el ejecutable independiente. Comienza el ayudante de actualización, sale del proceso original, verifica la confirmación de sustitución y puesta en marcha, comprueba la preservación de la configuración, luego abre el selector de aplicaciones instalado a través de la automatización UI y ahorra una selección. También realiza ejercicios renombrados ejecutables y caminos que contienen espacios. Este script está reservado para la cuenta de prueba de GitHub Actions desechable. `tests/windows-installer-smoke.ps1`: instalación de archivos de programa real, atajos de menú de escritorio/Iniciar, arranque predeterminado, actualización con la aplicación abierta, preservación de arranque deshabilitado, y desinstalación sin borrar perfiles.

<a id="publish-a-standalone-windows-folder"></a>
### Publicar una carpeta independiente de Windows

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

Distribuir el archivo completo. El flujo de trabajo de construcción de Windows también produce este paquete.

<a id="publish-a-github-release"></a>
### Publish a GitHub release

Actualizar la versión de la aplicación y `.github/release-notes.md`, luego empujar una coincidencia `v*` tag, tales como `v1.0.0`. El flujo de trabajo de lanzamiento comprueba que la etiqueta coincide con la versión del proyecto y ejecuta validación antes de publicar:

| Descargar | Índice |
| --- | --- |
| `DeckPilot.exe` | Aplicación Windows x64 autocontenido, de un solo fichero. |
| `DeckPilot-win-x64.zip` | Carpeta completa de aplicación autocontenida. |
| `SHA256SUMS.txt` | SHA-256 hashes para ambas descargas. |

Para Windows ARM64, publicar por separado con `-r win-arm64` y validar en la máquina de destino.

<a id="hardware-checks-on-windows"></a>
## Controles de hardware en Windows

Con el Leonardo conectado:

- Calibrar todos 12 posiciones en un orden F13-F24 arbitrario. Confirme las teclas duplicadas son rechazadas y cancelando preserva el mapeo anterior.
- Asignar una llave gratuita en un juego y comprobar que el juego lo recibe normalmente.
- Inicie un programa configurado, luego mantenga el botón: debe lanzarse una vez hasta su lanzamiento. Incluya caminos que contengan espacios y argumentos opcionales.
- Pruebe una carpeta, un sitio web, un acceso directo a una aplicación ordinaria y controles multimedia.
- Enviar F13–F24 a través de una acción atajo y confirmar que no hay bucle de DeckPilot acciones.
- Crear dos perfiles con diferentes acciones, cambiar entre ellos, renunciar y relanzar. Persistirán los perfiles y la calibración.
- Cierre la ventana, active una acción, luego vuelva a abrir y dejar a través del icono de la zona de notificación. Una vez DeckPilot salidas, todas las llaves deben pasar normalmente.
- Activar el inicio y la prueba de Windows después de firmar de nuevo; deshabilitarlo después si no está deseada.

<a id="current-limitations"></a>
## Limitaciones actuales

- La escucha clave es global para la sesión de Windows. Las teclas F13–F24 de otro teclado siguen la misma configuración. Los eventos de teclado inyectados por software se ignoran para prevenir los bucles de acción.
- Algunos juegos, sistemas anti-queat, o programas que utilizan la entrada directa del dispositivo pueden recibir claves interceptadas. El modo Free-key sigue disponible. La pantalla de inicio de sesión, escritorio seguro y otras sesiones están fuera del control de la aplicación.
- Windows puede bloquear atajos enviados a aplicaciones que se ejecutan con privilegios de administrador. DeckPilot se ejecuta sin derechos de administrador y no evita esta restricción.
- Lanzamiento Ctrl, Alt, Shift y Windows antes de desencadenar una acción atajo. Combinaciones como Alt+Tab permanecen sujetas al comportamiento de Windows.
- Detección de desconexión, perfiles automáticos por juego y un instalador firmado son mejoras futuras. Los perfiles se seleccionan manualmente.

<a id="project-structure"></a>
## Estructura del proyecto

| Camino | Propósito |
| --- | --- |
| `src/DeckPilot.Core` | Ajustes, estado clave, búsqueda, validación de la liberación de GitHub, descargas verificadas y transacciones de reemplazo. |
| `src/DeckPilot.Windows` | Interfaz WPF, catálogo de aplicaciones, iconos, acciones de Windows, integración de área de notificación y ayudante de actualización. |
| `tests/DeckPilot.Checks` | Comprobaciones ejecutables sin un marco de prueba externo. |
| `tests/windows-update-smoke.ps1` | Actualización nativa de Windows y prueba de humo de picker aplicación utilizado por CI. |
| `deckpilotlogo.png` | Logo de aplicación. |
| `deckpilotbanner.png` | La bandera del proyecto. |

<a id="license-and-credit"></a>
## Licencia y crédito

DeckPilot es liberado bajo **[Licencia MIT](../../LICENSE)**. Usted puede utilizar, copiar, modificar y redistribuirlo, incluyendo comercialmente, siempre y cuando retenga los avisos de copyright y licencia requeridos por la licencia. El denoiser de audio RNNoise agrupado es un componente independiente de terceros bajo su licencia de estilo BSD; ver [`third-party/RNNoise/COPYING`](../../third-party/RNNoise/COPYING) y [`third-party/RNNoise/NOTICE.md`](../../third-party/RNNoise/NOTICE.md).

Si construyes DeckPilot, un pequeño crédito visible y un enlace a este repositorio serían apreciados. Este crédito adicional es opcional; mantener los avisos del MIT todavía es necesario.

Crédito sugerido:

> Basado en [DeckPilot](https://github.com/lolo34dr/deckpilot) por [lolo34dr](https://github.com/lolo34dr).

<a id="macropad-firmware-updates"></a>
## Actualizaciones de firmware de Macropad

Para la preparación del dispositivo y los controles de calidad del taller, instalar el separado [DeckPilot Atelier](../../docs/FACTORY.md) herramienta. Su instalador se publica como **[DeckPilot-Atelier-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Atelier-Setup.exe)** junto a la aplicación principal. Puede destellar el firmware incluido a una tabla Leonardo/Caterina, a continuación, comprobar prensas cortas y largas en todos 12 claves físicas y exportar un informe.

DeckPilot 1.1.0 incluye un **Firmware du macropad…** diálogo y firmware 1.0.0 Pro Micro ATmega32U4 **5 V / 16 MHz**, utilizando la configuración Leonardo/Caterina confirmada por el registro de carga del propietario. La herramienta de flashización de Windows agrupada no requiere Arduino IDE. La selección y confirmación del dispositivo son obligatorios; actualizaciones de la aplicación nunca flashear silenciosamente el tablero. Existing F13–F24 cartografías, perfiles y calibración siguen siendo compatibles.

**El propietario ha confirmado éxito en la aplicación flashing, firmware 1.0.0 identificación y todo 12 botones en la tabla de referencia.** Mantenga el boceto original y el acceso a RST/GND para la recuperación. [documentación de firmware](FIRMWARE.es.md) para compatibilidad, validación, recuperación, pasos de construcción y licencias de terceros. Los modos USB per-button y los controles de medios nativos no son parte de esta versión de firmware.

<a id="building-the-windows-installer"></a>
### Construcción del instalador de Windows

Después de producir `artifacts/standalone/DeckPilot.exe` con el paquete de firmware integrado, instalar Inno Setup 6 y correr `./installer/build.ps1` de la raíz del repositorio. `artifacts/installer/DeckPilot-Setup.exe`. CI construye y prueba este instalador antes de adjuntarlo a una versión. El instalador utiliza un ID de aplicación estable para que las versiones futuras actualicen la misma instalación.

<a id="connection-dashboard-and-firmware-checks-130"></a>
## Controles de panel de conexión y firmware (1.3.0)

El **Votre macropad** tarjeta muestra estado de conexión, el firmware detectado y cualquier actualización de paquetes disponibles. **Connexion et diagnostic** expone la selección de tableros cuando varios Leonardos están conectados, el estado de enlace USB en vivo, cuenta de botones físicos, preferencias de notificación de firmware y un resumen de diagnóstico copiable. Los diagnósticos no copian las acciones de perfil o identificadores de serie USB. El monitor vuelve a conectarse después de desconectar y deja su puerto serie automáticamente mientras el administrador de firmware está abierto. 1.0.0 y sin el vínculo de diagnóstico.

Las comprobaciones de firmware de inicio están activadas por defecto. Después de la actualización de la aplicación, el cheque y la calibración están fuera del camino, un firmware identificado más antiguo puede impulsar la instalación; enviar recordatorios de demoras para 24 horas. Los tableros desconocidos y múltiples candidatos no seleccionados no se destellan automáticamente. Los lanzamientos de firmware se agrupan con actualizaciones de aplicaciones de GitHub, en lugar de obtener de una alimentación independiente de firmware.

Firmware **1.1.1** añade identificación de sesión, respuestas correlativas correlativas, mensajes encuadernados y eventos físicos de estado clave. Estos eventos animan los botones mapeados y actualizan el diagnóstico; nunca invocan acciones por segunda vez. Su primera validación de tablero físico todavía es necesaria, aunque el firmware anterior 1.0.0 y el procedimiento flash han sido confirmados por el propietario. [protocolo y documentación de compatibilidad](FIRMWARE.es.md).

<a id="keypad-interface-140"></a>
## Interfaz de teclado (interfaz de teclado)1.4.0)

El más grande 3 × 4 keypad sigue el recinto físico y las teclas negras. Cada clave muestra su icono principal, etiqueta, cartografía F13–F24 y un pequeño icono de tipo de acción; las puntas de las herramientas conservan la etiqueta completa y la posición. Pulse la retroalimentación, las acciones de ejecución, el envío exitoso y los errores tienen apariencias distintas, incluyendo símbolos en lugar de color solo.

Elige una clave para editarla. **Options avancées · fichier et arguments** mantiene las trayectorias manuales de aplicación y los argumentos de lanzamiento colapsados por defecto, mientras que los destinos esenciales de carpetas y sitios web se siguen expandiendo. **Tester l’action** guarda y envía la acción seleccionada en el PC actual; un indicador de éxito confirma el envío, no la terminación dentro de otra aplicación. Atajos de teclado apuntan a la ventana activa. Las teclas F13-F24 libres no tienen ninguna acción de prueba.

Cuando el macropado está desconectado, **Hors ligne** y la indirecta acompañante explican que los perfiles todavía pueden ser editados y las acciones probadas. La configuración se almacena en el PC y permanece disponible a través de desconexiones y reconexiones. Estas mejoras de interfaz funcionan con el firmware existente y no requieren flash adicional.

El menú de la zona de notificación ahora cambia los perfiles directamente. **Ctrl+S** Guarda la configuración del botón actual. Los archivos de configuración inválidos se respaldan antes de que se utilice una configuración fresca. Se mantienen la instalación de aplicaciones, iconos de menús de escritorio/Iniciar, preferencias de inicio y el actualizador automático existente.

Para cargar manualmente Arduino IDE, **Renuncio DeckPilot desde su menú de bandeja** para liberar el puerto de serie. Cerrar la ventana principal lo mantiene funcionando en el fondo.

<a id="spotify-action-search-and-drag-and-drop-150"></a>
## Spotify, búsqueda de medidas y drag-and-drop (1.5.0)

**Spotify** está separado de **Multimédia**. Its play/pause, next and previous commands address the Spotify desktop app's Windows media session, even while a browser is playing YouTube. Spotify volume +/− ajusta la sesión de audio de Spotify en Windows 5% steps, leaving the master volume and other players unchanged. Spotify must be open and have an available reproducción session; start a track once if necessary. Spotify in a browser is not supported. Missing Spotify sessions produce an error, with no fallback to global media keys or another player. No hay autorización de cuenta de Spotify, suscripción Premium o token almacenada es necesaria por DeckPilot. Windows 10 versión 2004 o más tarde, o Windows 11, es requerido por esta construcción.

Uso **Rechercher une action ou une application** para consultas tales como `volume`, `skip`, `Spotify` o el nombre de una aplicación instalada. El índice de aplicación instalada carga en la primera búsqueda y está caché durante cinco minutos; la selección manual de archivos sigue disponible. Haga clic en un resultado para configurar la tecla seleccionada o arrástrela en otra clave.

<a id="control-panel-and-exact-colours-240"></a>
## Panel de control y colores exactos (2.4.0)

El panel derecho tiene tres pestañas: **Acciones** para asignar llaves, **Appareil** para el macropado y el audio, y **Réglages** para perfiles y preferencias. El teclado y editor a la izquierda conservan su diseño y los iconos vectoriales originales y DeckPilot logo.

Under **Réglages → Réglages de l’application**, uso **Palette...** para elegir un fondo exacto, panel o color de acento con el selector de color de Windows. `#RRGGBB` campo y swatch adyacente muestran el valor seleccionado. **Appliquer les couleurs** valida y ahorra los tres colores; cancelar el selector mantiene el valor actual.

Se ha eliminado la integración del control OBS/Streamlabs. 2.3 Los perfiles siguen siendo legibles y mantienen sus acciones almacenadas, pero las acciones de streaming retiradas no se ejecutan. Seleccione una acción de reemplazo de la biblioteca para reasignar esas teclas. Se conservan otras acciones, perfiles y calibración.

Arrastre una acción del catálogo o los resultados de búsqueda en una clave para asignarlo. Arrastre una tecla configurada en otra **cambiar sus acciones**; la calibración física permanece inalterada. `.exe`, `.lnk`, `.appref-ms`, carpeta o URL HTTP(S) en una clave. Dropping sólo configura la clave y nunca ejecuta la acción. Complete cualquier configuración que falta en el editor.

El inicio de Windows ahora mantiene una preferencia explícita por usuario, repara una entrada Stale o missing Run, e informa las banderas deshabilitadas de arranque de Windows conocidas. Los lanzamientos normales respetan el desactivamiento de Windows; explícitamente habilitante **Lancer avec Windows** despeja lo conocido DeckPilot bandera deshabilitada para el usuario actual. Los lanzamientos de fondo permanecen ocultos incluso antes de la calibración, y los lanzamientos de fondo duplicados salen tranquilamente. **Préférences de l’application**, comprobar el estado de inicio y reactivar la opción si Windows lo bloqueó. Un registro real / registro en el PC del usuario sigue siendo el cheque final para las políticas de Windows o restricciones específicas de la máquina.

<a id="sharp-keypad-rendering-profile-recovery-and-audio-controls-161"></a>
## Dotación de teclado Sharp, recuperación de perfiles y controles de audio (1.6.1)

El teclado ahora renderiza en tamaños nativos de WPF en lugar de escalar texto a través de un Viewbox. Efectos de sombra de pariente/key y animaciones de escala de texto fueron eliminados; ventanas más pequeñas pergamino en lugar de reducir el teclado. La aplicación declara conciencia DPI por monitor y utiliza iconos de shell más grandes para aplicaciones de escritorio.

**Annuler / Rétablir** restaura las ediciones de perfiles, incluyendo swaps de arrastrar y soltar, ajustes de acción, importaciones y creación de perfiles/deleción. 50 ediciones durante la sesión de la aplicación. Ctrl+Z / Ctrl+Y operan en perfiles fuera de los campos de texto; los campos de texto conservan su propio deshacer/redo. La calibración, las preferencias de inicio y las acciones ejecutadas no se deshacen.

Under **Gérer les profils**, exportar el perfil actual o todos los perfiles como `.deckpilot.json`, los perfiles de importación como copias nuevas, o restaurar una copia de seguridad de todos los perfiles. Importar nunca ejecuta acciones y nunca reemplaza la calibración del PC. Los perfiles contienen trayectorias de acción, argumentos y IDs de terminales de audio, que pueden necesitar ajustarse en otro PC. Se rechazan formatos desconocidos, archivos sobredimensionados y configuraciones de gesto malformados.

Antes de guardar perfiles cambiados, DeckPilot mantiene los perfiles anteriores en `%APPDATA%\DeckPilot\Backups` (20 Versiones, deduplicadas por contenido).Los ahorros sólo de preferencia no crean copias de seguridad de perfil. Un fallo de copia de seguridad evita la sobreescritura de la configuración actual. Restaurar reemplaza los perfiles después de la confirmación y se puede deshacer; la calibración permanece inalterada.

**Microphone Windows** toggles mute on all currently active Windows capture endpoints. La barra de estado persistente y la tarjeta Audio Windows muestran el estado activo/muted/mixed/unavailable, refrescado cada dos segundos y después de los comandos de la aplicación. Una actualización multi-endpoint fallido intenta restaurar estados mudos anteriores. Estos indicadores describen el estado de Windows endpoint, no un interruptor de hardware o un botón mute de la aplicación.

**Changer de sortie audio** alterna entre dos dispositivos de salida seleccionados, conectados, para la consola de Windows, multimedia y predeterminados de comunicaciones. Un objetivo perdido da un error sin elegir otro dispositivo. Las aplicaciones con su propia salida fija pueden no seguir el predeterminado de Windows. Los IDs de punto final importados se muestran como indisponibles hasta que se adaptan al PC actual.

Para configurar **appui court / appui long**, primero elegir una acción configurada, habilitar la opción de dos acciones, seleccionar el gesto para editar y establecer un umbral desde 200 a 3000 ms. Sin este modo, las acciones conservan el comportamiento inmediato de cierre de teclas. Con él, las acciones cortas se ejecutan en la liberación antes del umbral; las acciones largas se ejecutan una vez en el umbral (o en la liberación si el temporizador aún no ha funcionado). Una acción larga nunca también se ejecuta el corto. Las teclas de paso F13–F24 no pueden usar el modo de doble acción.

Controles básicos cubren límites de repetición / liberación, contenciones simultáneas, cancelación, historial de perfiles/intercambios/backups y retrocesos de audio utilizando puntos finales falsos. Windows UI verifica el ejercicio real drag undo/redo, configurando una acción larga y el umbral, las dimensiones de teclado nativo después del tamaño y el indicador de micrófono.

<a id="search-and-ci-reliability-fixes-164"></a>
## Fiabilidad de búsqueda y CI fija (1.6.4)

Los resultados de búsqueda Equivalente mantienen sus botones WPF existentes. El catálogo de la aplicación instalada y la sustitución de los resultados de retardo de debounce mientras se captura un botón de búsqueda o se está progresando un arrastre. Esto evita que se elimine un botón fuente durante una interacción.

Controles de humo de Windows continúan realizando arrastres de ratón nativos y validando asignaciones guardadas, swaps, undo/redo y ambos gestos de prensa. El cliente de prueba utiliza coordenadas del cursor físico para coincidir con los límites UI Automation independientemente de DPI, se centra antes de medir, y escanea todos los porcentajes de desplazamiento consecutivos.

Construir y soltar flujos de trabajo utilizan Node.js 24 acciones. El compilador Arduino permanece en CLI 1.2.2; CI descarga su archivo de Windows con un marcado SHA-256 checksum. El firmware y sus versiones de toolchain no han cambiado.

Ajustes leídos en las pruebas de Windows permiten explícitamente compartir lectura/escritura y parse UTF-8 Después de cerrar el lector. Esto evita que la prueba en sí misma niegue la sustitución de archivos atómicos de la aplicación. La prueba de historia mantiene una vieja instantánea abierta a través de tres ciclos de deshacer/redo y verifica que permanece intacta mientras los ajustes en vivo cambian.

En Windows, los archivos de configuración existentes se comprometen con `File.Replace` en lugar de overwrite-mode `File.Move`. Esto es compatible con lectores que permiten la eliminación mientras conservan una instantánea completa vieja. La suite central prueba tres reemplazos con un lector mantenido abierto; cheques de núcleo de Windows funcionan antes de la compilación de firmware y la publicación de manera que los fallos de persistencia se detectan temprano.

Un lector de instantáneas de prueba puede esperar hasta 500 ms para Windows compartir / bloquear violaciones mientras `ReplaceFile` commits. La espera se aplica sólo para abrir la instantánea, nunca para ejecutar o volver a ejecutar una acción de aplicación. Otros errores, malformado JSON y bloqueos persistentes siguen siendo fallos.

<a id="runtime-and-editing-optimizations-170"></a>
## Optimizaciones de tiempo de ejecución y edición (1.7.0)

Ahorrar configuraciones idénticas comprueba el archivo actual pero salta el trabajo de reemplazo y copia de seguridad. Se siguen detectando ediciones externas/deleción y escritos fallidos. `ButtonAction` tiene igualdad de valor (incluyendo la acción larga); los controles sucios del editor evitan la serialización de JSON. La historia mantiene instantáneas de modelos desprendidos, valida nuevos estados, y evita que JSON pare durante el deshacer/redo. 50-edit limit and 20 Las copias de seguridad del perfil permanecen.

Search and picker share an immutable installed-app catalog for five minutes ()15 segundos para un resultado parcial).Los consumidores concurrentes comparten un escaneo; cerrar un selector no cancela otro consumidor. Refresh bypasses the cache, y cada picker tiene su propio filtro/view. El escaneo y el pergamino del catálogo se ejecutan fuera del hilo UI; el filtrado del selector es desmontado por 120 ms. Action search rechecks this cache after its debounce and when returning from the picker, so a fresco catálogo is available there too.

Actualizaciones de la interfaz de usuario de la Junta ahora siguen notificaciones de conexión en lugar de despertar cada 100 ms. Las notificaciones de desconexión ordenadas todavía cancelan las bodegas pendientes. Ganchos de teclado trabajo visual cola, mientras que la supresión y el tiempo de gesto permanecen sincronizados. ventanas ocultas / minimizadas detienen animaciones de retroalimentación y utilizan un intervalo de diez segundos de encuesta de audio en lugar de dos segundos; reapertura o emisión de un comando de audio solicitudes de estado fresco inmediatamente.

Estos cambios reducen el trabajo especificado; no establecen un porcentaje de CPU/RAM dependiente de la máquina. La conmutación de audio del hardware nativo y el comportamiento de clave física todavía requieren validación en un PC de Windows con el macropado.

<a id="workspace-tools-and-refreshed-interface-180"></a>
## Herramientas de espacio de trabajo e interfaz refrescada (1.8.0)

La interfaz de carbón pone la biblioteca de acción primero, separa los controles de perfil del tamaño nativo nítido 3 × 4 keypad, y grupos configuración de acción en una tarjeta. Los acentos clave distinguen sus propias categorías; selección, enfoque del teclado y retroalimentación de acción permanecen separados. **Réduire les animations** en preferencias complementa el ajuste de animación de Windows. Las ventanas más pequeñas conservan el acceso de pergamino en lugar de reducir el texto clave.

- **Plantillas de perfil:** por debajo de los límites **Gérer les profils**, añadir Música y audio, Escritorio y productividad, o Creación. Cada plantilla crea un perfil editable separado y nunca ejecuta sus acciones. Se conservan perfiles existentes y calibración física. Los atajos de teclado utilizan la aplicación activa, cuyos atajos pueden diferir.
- **Claves de perfil:** c) **Changer de profil**, a continuación, seleccione un perfil nombrado o ciclo hacia adelante / hacia atrás. Referencias entre perfiles se remapped al importar un conjunto o duplicar un perfil de auto-referencia. Un destino perdido reporta un error y puede ser reemplazado en el editor.
- **Windows comandos:** uso **Commandes Windows** abrir configuraciones comunes o Administrador de tareas, o bloquear explícitamente el PC. Los comandos vienen de una lista fija, sin ningún script para entrar. La acción de bloqueo requiere iniciar sesión en Windows.
- **Portapapeles de tecla completa:** Haga clic con el botón derecho para copiar, cortar, pegar o restablecerlo, incluyendo la acción y el acento largos. Ctrl+C/X/V y Eliminar el trabajo mientras una tecla de cubierta tiene enfoque del teclado; los campos de texto ordinario mantienen sus atajos de edición. DeckPilot y se aclara en la salida. Estas ediciones soportan undo/redo.
- **Grabador atajo:** seleccione una acción del teclado y haga clic **Enregistrer un raccourci**. Presione una combinación compatible, a continuación, guardarlo. Escapar cancela; F13–F24 está reservado para el macropado durante la grabación. Las combinaciones conservadas por Windows todavía pueden requerir los controles manuales.
- **Acceso rápido:** Ctrl+K o el botón Biblioteca centra la búsqueda de acción. **Activité récente** mantiene el último 30 Acciones enviadas/failed en memoria para esta sesión, sin guardar los argumentos del programa o las rutas de destino. Se puede limpiar en cualquier momento. Un envío exitoso no garantiza la terminación en la aplicación de destino.

El firmware sigue **1.1.1**. Aceptar una oferta automática de firmware ahora abre correctamente el gestor de firmware; el anterior guardia modal impidió que la entrega. La identidad de firmware existente, el latido del corazón, la reconexión y las comprobaciones flash permanecen en su lugar.

<a id="microphone-noise-suppression-220"></a>
## Represión del ruido del micrófono (2.2.0)

**Soundboard · micro virtuel** ahora incluye la supresión de ruido local RNNoise para el micrófono físico seleccionado. **Filtrer le bruit du microphone** y ajuste **Intensité**. DeckPilot filtra la voz antes de mezclar en clips de mesa sonora, por lo que los clips permanecen inalterados y no se eliminan por el filtro de ruido. **CABLE Output** como su micrófono, incluyendo juegos y aplicaciones de la cadena de voz; los programas establecidos para utilizar el micrófono físico directamente no pasan a través DeckPilot.

Audio se procesa localmente en DeckPilot. No se registra o se envía a través de la red. RNNoise trabaja en 10 ms, 48 kHz mono frames; este tamaño de marco no es una garantía de latencia de extremo a extremo, que también depende de Windows, los dispositivos y VB-CABLE. Esta actualización no requiere un flash de firmware. El componente nativo de RNNoise está cubierto por su propia licencia de estilo BSD, señalada anteriormente.

<a id="soundboard-return-and-on-screen-sound-notifications-219"></a>
## Soundboard notificaciones de sonido de retorno y en pantalla (2.1.9)

In **Soundboard · micro virtuel**, habilita **Entendre aussi les son du soundboard** para escuchar clips a través de su auricular o altavoces, así como el micrófono virtual. Elija la salida física y establezca el volumen de retorno separado de 0 a 100%. Este retorno se apaga por defecto y se guarda en este PC. Juega **sólo clips de placa sonora**; su micrófono físico nunca se añade a la salida del altavoz. El micrófono virtual todavía envía su voz y clips a VB-CABLE.

Cambiar el volumen no interrumpe el micrófono virtual. Si el altavoz seleccionado se desconecta, el retorno se detiene mientras el micrófono virtual continúa. Reconectarlo, refrescar la lista de dispositivos o elegir otra salida. Los auriculares ayudan a evitar que el micrófono físico recoja clips acústicamente.

El altavoz devuelve ahora utiliza el formato de audio de Windows anunciado por el dispositivo seleccionado, incluyendo formatos extensibles utilizados por algunos auriculares USB, y retrocede de la reproducción impulsada por el evento si un controlador lo rechaza. Comenzar un clip de panel de sonido también muestra su nombre en el mismo overlay configurable, click-through como cambios de perfil.

DeckPilot Atelier remains at 1.0.12 y restos de firmware 1.1.1. No se necesita un flash de firmware. El routing de altavoz real todavía depende del dispositivo de audio de Windows y debe ser revisado en el PC del propietario.

<a id="soundboard-speaker-return-218"></a>
## Soundboard retorno del orador (2.1.8)

In **Soundboard · micro virtuel**, habilita **Entendre aussi les son du soundboard** para escuchar clips a través de su auricular o altavoces, así como el micrófono virtual. Elija la salida física y establezca el volumen de retorno separado de 0 a 100%. Este retorno se apaga por defecto y se guarda en este PC. Juega **sólo clips de placa sonora**; su micrófono físico nunca se añade a la salida del altavoz. El micrófono virtual todavía envía su voz y clips a VB-CABLE.

Cambiar el volumen no interrumpe el micrófono virtual. Si el altavoz seleccionado se desconecta, el retorno se detiene mientras el micrófono virtual continúa. Reconectarlo, refrescar la lista de dispositivos o elegir otra salida. Los auriculares ayudan a evitar que el micrófono físico recoja clips acústicamente.

<a id="open-deckpilot-and-profile-notifications-217"></a>
## Abierto DeckPilot y notificaciones de perfiles (2.1.7)

Under **Aplicación Ouvrir une**, elegir el incorporado **DeckPilot** entrada o haga clic **Afectador « Ouvrir DeckPilot »**. Esta acción trae lo existente DeckPilot ventana al frente; su objetivo se mantiene válido después de una actualización de la aplicación o reinstalación. También está disponible en la búsqueda de acción.

Los perfiles cambiantes muestran una breve notificación de click-through con el nombre de perfil activo. **Préférences de l’application**, usted puede deshabilitarlo, elegir cualquier rincón, o establecer una posición horizontal y vertical personalizada. **Enregistrer et prévisualiser** muestra el resultado inmediatamente. La notificación sigue la pantalla que contiene la aplicación del primer plano y también aparece mientras DeckPilot está oculto. Puede aparecer sobre juegos sin fronteras / ventana; los juegos exclusivos de pantalla completa pueden cubrir los overlays normales de Windows.

<a id="deckpilot-210-fixed-signed-virtual-microphone-routing"></a>
## DeckPilot 2.1.0: routing de micrófono virtual firmado fijo

DeckPilot ahora utiliza una interfaz negra profunda por defecto. **Préférences de l’application**, cambiar el fondo, panel y colores de acento utilizando `#RRGGBB` valores; su tema se guarda en este PC.

El **Soundboard · micro virtuel** panel le permite seleccionar su micrófono físico y clips MP3/WAV. DeckPilot ruta automáticamente la mezcla al VB-Audio VB-CABLE punto final de reproducción; en disco o un juego, elegir **`CABLE Output`** como micrófono. Durante la instalación y actualizaciones, DeckPilot cheques para VB-CABLE descarga el paquete oficial sobre HTTPS y verifica el editor de Authenticode antes de ejecutar el instalador silencioso del controlador. Esto requiere una conexión a Internet y un permiso de administrador. DeckPilot no empaqueta o redistribuye archivos VB-Audio. Si el controlador falta más tarde, puede iniciar el mismo instalador de la firma comprobada del Soundboard panel. El motor de audio utiliza NAudio (MIT; véase [notificación de terceros](../../third-party/NAudio-LICENSE.txt)). VB-CABLE es un controlador de Windows, firmado por separado, no un componente MIT.

<a id="deckpilot-atelier-190"></a>
## DeckPilot Atelier (Atelier)1.9.0)

Atelier comprueba ahora el tablero seleccionado y el firmware antes de parpadear, compara las versiones instaladas y agrupadas, y ofrece una reingresación verificada después de los escritos interrumpidos. **Mode atelier en série...** diagnostica y flashea todas las tablas detectadas secuencialmente y puede exportar un informe de lote. 12- La prueba de clave verifica el físico 01→12 diseño, registra la asignación F13–F24, e incluye resultados de firmware y per-key en el informe de calidad.


El separado **DeckPilot Atelier** El instalador de Windows prepara nuevos macropados DIY. Detecta tableros Leonardo/Caterina, valida y destella el firmware agrupado sin sobrescribir el cargador de arranque, luego guía un físico 3×4 chequeo de todos 12 botones. Cada botón debe pasar una pulsación corta y una sujeción de al menos 700 ms. Los registros de herramientas que la señal F13–F24 pertenece a cada posición física, marca señales múltiples o inconsistentes, y exporta un reporte atemporal. Descargar `DeckPilot-Atelier-Setup.exe` desde la página de lanzamientos. Un descargador de arranque Caterina ya debe estar presente; la herramienta no instala un cargador de arranque perdido.

DeckPilot Atelier comprueba GitHub para la aplicación y actualizaciones de firmware agrupadas en la puesta en marcha y bajo demanda. Las actualizaciones requieren confirmación y utilizar la versión SHA-256 manifest. Firmware nunca es flasheado por una actualización automáticamente; después de las actualizaciones de Atelier, elija la acción de firmware cuando esté listo para programar el tablero conectado.

Atelier 1.0.7 añade un confirmado **Effacer le programme DeckPilot** acción, aclara la 28 Área de aplicación KiB con una instrucción de parada segura verificada, por lo que el tablero deja de actuar como un teclado preservando a Caterina para la recuperación y el futuro flashing. Deja intactos los perfiles de bootloader, EEPROM y Windows.

Versión 1.9.1 fija el lanzamiento de Atelier después de la instalación al iniciarlo en la sesión de Windows firmada. Su actualización puede instalar un paquete nuevo Atelier o paquete de firmware, luego relanzar la aplicación. El oleoducto de lanzamiento de Windows comprueba el primer lanzamiento, actualización y desinstalación para el instalador Atelier.

Versión 1.9.2 mejora el paso de empaquetado Atelier Windows e informa su ubicación exacta de fallo en CI.

Versión 1.9.3 corrige cómo el paquete Windows lee la versión de firmware incluida, por lo que el cheque de actualización de Atelier rastrea el firmware enviado en el instalador.

El cheque de liberación de Atelier informa ahora diagnóstico de inicio si Windows no abre su ventana instalada.

DeckPilot 1.9.6 fija Atelier's Windows DPI manifiesto para que la aplicación instalada se inicie correctamente en Windows 10 y 11.

DeckPilot 1.9.7 soluciona desinstalar Atelier mientras se está ejecutando. La prueba de liberación de Windows también verifica el lanzamiento, actualización en el lugar y desinstalación.

DeckPilot 1.9.8 añade un restablecimiento explícito y confirmado de Atelier que reemplaza el programa de usuario del tablero con un boceto vacío verificado y conserva el cargador de arranque de Caterina.

La validación cubre los ajustes heredados, validación de acentos, navegación de perfiles y remapping de referencia, plantillas portátiles, conmutación nativa de Windows/undo/redo y perfil, grabación atajo, copia/paste/reset, persistencia de configuraciones, instalación/actualización, redimensionamiento de diseño y una captura real de interfaz de usuario.

<a id="profile-sound-files-and-workshop-safety"></a>
### Perfil de archivos de sonido y seguridad del taller

Las exportaciones de perfiles incluyen los clips MP3/WAV referenciados (hasta 32 MiB de audio por paquete). Importaciones copiar audio incrustado en DeckPilot’s managed Sounds folder and remap identifiers without replace existing clips. Las copias de seguridad de perfil automático utilizan el mismo formato; el audio no disponible se registra como desaparecido en lugar de reemplazar silenciosamente. Mantenga los archivos fuente si supera el límite de exportación. Los archivos de perfil de Legacy siguen soportados. Se bloquea la eliminación de un clip que todavía se asigna a una acción corta o larga.

Atelier acepta tablas que aún no responden DeckPilot protocolo, mientras que sigue comprobando la ubicación USB, Caterina bootloader y ATmega32U4 firma antes de la programación. Cada sesión de prueba comienza con resultados vacíos, y su informe se mantiene asociado con la tabla probada. Cerrar la aplicación durante un flash o reset se bloquea hasta el final de la escritura y verificación.

<a id="live-microphone-latency"></a>
### Latencia del micrófono en vivo

DeckPilot 2.1.3 procesos de audio en vivo en bloques pequeños, impulsados por la salida. 20 ms captura y captura de micrófono impulsado por eventos 40 ms buffers de reproducción. La cola del micrófono está ligada a 100 ms y descartes marcos completos antiguos si la reproducción se bloquea; esto evita construir segundos de retraso del habla. Estos son ajustes de amortiguación de software, no una garantía de latencia de extremo a extremo medido. Windows, el micrófono, VB-CABLE y la aplicación receptora también contribuyen a retrasar. Los ajustes de aislamiento de voz de disco son inalterados y pueden suprimir clips de placa de sonido porque recibe la señal mixta.

<a id="automatic-virtual-microphone-startup"></a>
### Inicio automático del micrófono virtual

DeckPilot 2.1.4 activa automáticamente el micrófono virtual cuando el micrófono físico seleccionado y VB-CABLE están disponibles. Esto también funciona en el fondo después de la entrada de Windows cuando **Empieza con Windows** está habilitado. Los dispositivos perdidos son retrigados cada cinco segundos. Detener el micrófono virtual ahorra explícitamente la puesta en marcha automática como deshabilitado; volver a hacerlo en el Soundboard Los clips sólo se reproducen cuando se solicita.
