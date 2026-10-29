> Esta es una traducción automática de la README completa. El original inglés es la referencia. Las etiquetas de interfaz de aplicación y los comandos se conservan exactamente.

<p align="center">
  <img src="../../deckpilotbanner.png#gh-light-mode-only" alt="DeckPilot banner" width="760">
  <img src="../../deckpilotlogo.png#gh-dark-mode-only" alt="DeckPilot logo" width="112">
</p>

<h1 align="center">DeckPilot</h1>

<p align="center">
  Un compañero de Windows para su 12- Macropado DIY.<br>
  Configura tus teclas, aplicaciones de lanzamiento, medios de control y perfiles de conmutación.
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

**No se requieren cambios adicionales de controlador o firmware.** La interfaz de aplicación está actualmente en francés. Elija un idioma anterior para un README traducido completo, incluyendo la referencia técnica y la historia de la versión. Las traducciones se generan automáticamente; este original en inglés es la referencia.

<a id="features"></a>
## Características

- **Calibración guiada:** Identificar todos 12 botones físicos sin cambiar el macropado.
- **Picker de aplicación instalado:** búsqueda Iniciar atajos de menú y el catálogo de aplicaciones de Windows, incluyendo entradas de Microsoft Store. La selección manual de archivos sigue disponible.
- **Acciones personalizadas:** aplicaciones de lanzamiento, carpetas abiertas o sitios web, enviar atajos y medios de control.
- **Teclas libres:** Mantenga F13–F24 disponible para atajos configurados directamente en un juego u otra aplicación.
- **Perfiles locales:** crear, duplicar, renombrar, eliminar y seleccionar perfiles. La calibración se comparte a través de perfiles.
- **Perfiles listos:** empezar con plantillas de música, productividad o creación, a continuación, personalizarlas. Cambiar a un perfil específico, próximo o anterior de una clave física.
- **Edición más rápida:** copy/cut/paste/reset teclas completas, atajos de teclado para grabar, elegir colores de acento clave, y deshacer cambios.
- **Herramientas de Windows:** abrir sonido, visualización, Bluetooth y configuración de red, Administrador de tareas, o bloquear la sesión.
- **Interfaz oscura compacta:** iconos vectoriales, iconos de aplicación donde esté disponible, y animaciones sutiles que respetan las preferencias de animación de Windows.
- **Operación de antecedentes:** Manténganse. DeckPilot en el área de notificación, con inicio en el registro de Windows habilitado por defecto (puede ser deshabilitado).
- **Actualizaciones automáticas:** aprobar una nueva versión, entonces dejar DeckPilot descargar, verificar, reemplazar y reiniciar en sí mismo.

<a id="getting-started"></a>
## Comienzo

<a id="requirements"></a>
### Necesidades

- Windows 10 o Windows 11, **x64**.
- Un macropado compatible con Arduino Leonardo enviando 12 teclas distintas **F13 a F24**.
- Una conexión de datos USB.

Las descargas publicadas incluyen el tiempo de ejecución .NET. **Usted no necesita instalar .NET para utilizar DeckPilot.**

<a id="install-and-launch"></a>
### Instalación y lanzamiento

1. Descargar **[DeckPilot-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** y ejecutar el instalador. Windows pide permiso de administrador para instalar **Program Files\DeckPilot**.
2. El instalador añade DeckPilot a Windows **Aplicaciones instaladas**, crea un atajo de menú Inicio, y selecciona las opciones de inicio de escritorio y Windows por defecto. Puede deseleccionar estas tareas opcionales.
3. Conecta tu macropado y abre DeckPilot de su atajo.
4. Siga la calibración de izquierda a derecha a través **cuatro columnas**, entonces la siguiente fila, para **tres filas**. Cada botón debe enviar una tecla F13-F24 diferente.

Al actualizarse de la primera 4- diseño de la hoja, DeckPilot conserva perfiles/acciones y solicita una nueva calibración para las posiciones corregidas. Instalar una configuración portátil existente también preserva la configuración en `%APPDATA%\DeckPilot`; renunciar a la copia vieja si la configuración le pide.

Un portátil [DeckPilot.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) y [ZIP](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip) permanecer disponible para la compatibilidad. Para el uso portátil, mantener el ejecutable en una carpeta personal writable.

Desinstalación de Windows **Aplicaciones instaladas**. La desinstalación elimina el programa y sus atajos, al tiempo que conserva los perfiles personales para que una reinstalación posterior pueda reutilizarlos.

El ejecutable de un solo fichero extrae sus dependencias agrupadas en la caché .NET del usuario en el primer lanzamiento, que puede tardar un poco más. Los ejecutables publicados están actualmente sin firmar, por lo que Windows puede mostrar una advertencia SmartScreen.

<a id="configure-a-key"></a>
### Configurar una clave

1. Seleccione una llave en la cuadrícula.
2. Elija una acción en la barra lateral derecha.
3. Configure su configuración debajo de la cuadrícula y ahorre. Las teclas de conmutación o los perfiles también guarda la configuración de la llave actual.

| Medida | Comportamiento |
| --- | --- |
| Tecla libre | Pasa F13–F24 a través de normalmente. Asignarlo directamente en su juego o aplicación. |
| Aplicación | Lanza una aplicación instalada o un programa seleccionado manualmente o atajo. |
| Folder | Abre una carpeta en Windows Explorer. |
| Sitio web | Abre una dirección HTTP o HTTPS en su navegador predeterminado. |
| Atajo de teclado | Envía una combinación de teclas a la ventana activa. |
| Medios | Controla la reproducción, las pistas, el volumen o muda. |

Para acciones de aplicación, abra el selector de aplicaciones y busque por nombre. El selector de archivos manual permanece disponible, con argumentos opcionales de programa bajo opciones avanzadas. Introduzca argumentos de acuerdo con la sintaxis de la aplicación de destino.

Sostener un botón activa una acción configurada sólo una vez hasta la liberación. En modo free-key, los eventos normales de repetición del teclado permanecen disponibles.

<a id="manage-profiles"></a>
### Gestionar perfiles

Abra la sección de gestión de perfiles en la barra lateral derecha para crear, duplicar, renombrar o eliminar un perfil. Utilice el selector por encima de la cuadrícula clave para elegir el perfil activo.

La calibración se aplica a todos los perfiles y puede repetirse desde la barra lateral.

<a id="background-operation"></a>
### Operación de antecedentes

Cierra la ventana oculta DeckPilot Haga doble clic en su icono de la zona de notificación cerca del reloj para reabrirlo, o haga clic con el botón derecho en el icono y elija el comando renunciar para detenerlo completamente.

El inicio de Windows está habilitado por defecto para el usuario actual. Puede deshabilitarlo con **Lancer avec Windows**; actualizaciones de aplicaciones posteriores preservan esa opción. Instalación y lanzamientos posteriores refrescan una entrada habilitada al camino ejecutable actual.

<a id="installed-applications"></a>
## Aplicaciones instaladas

El selector lee atajos de los menús de inicio del usuario actual y de todo el sistema, luego los complementa con `Get-StartApps` resultados del catálogo de aplicaciones de Windows.

Buscar ignora el caso y los acentos. Los nombres duplicados se agrupan, con las entradas del menú Inicio tomando prioridad. Aplicaciones y programas portátiles sin accesos directos registrados todavía se pueden seleccionar manualmente. Si el catálogo de Windows no está disponible, legible Atajos del menú Inicio siguen siendo utilizables.

Windows lanza los atajos seleccionados. `shell:AppsFolder`. Los argumentos opcionales se aplican a los programas basados en archivos y atajos; están ocultos para las entradas de AppsFolder.

<a id="automatic-updates"></a>
## Actualizaciones automáticas

Disponible desde **0.2.0**, cheques automáticos de actualización se ejecutan al inicio después de la calibración, luego cada seis horas. Pueden ser deshabilitados en la barra lateral.

Cuando se encuentra una nueva versión estable de GitHub, DeckPilot pregunta si actualizar. **Copias instaladas** descarga y SHA-256 verificar `DeckPilot-Setup.exe`, luego lanzar el instalador. Windows pide permiso de administrador para actualizar Archivos del programa. El instalador cierra DeckPilot Agraciadamente cuando esté listo, conserva perfiles y preferencias de inicio, instala la nueva versión y relanza la aplicación. Cancelando el aviso de permiso de Windows deja abierta la aplicación. Setup se niega a interrumpir una sesión de firmware abierto.

**Copias portátiles** conservar el mecanismo de actualización existente. Después de la confirmación, se:

1. Descargue el ejecutable con soporte de presentación de informes y cancelación de progreso.
2. Verifica su SHA-256 hash against `SHA256SUMS.txt` de la misma liberación de GitHub.
3. Ahorra tu configuración y prepara el ayudante de actualización.
4. Exits, replaces the executable, and restarts.
5. Mantiene el ejecutable anterior hasta que el nuevo proceso confirma la inicialización exitosa; si la startup falla, restaura y relanza la versión anterior.

Elegir para actualizar más adelante pospone los impulsos automáticos para 24 horas. bypass manual checks que posponen.

Las descargas utilizan HTTPS, con redirecciones restringidas a los hosts GitHub esperados. Los archivos temporales se almacenan en `%LOCALAPPDATA%\DeckPilot\Updates`; descargas mayores de siete días se limpian. La entrada de inicio de Windows continúa utilizando el mismo camino ejecutable después de una actualización.

Las copias portátiles requieren una carpeta personal de escritura. Las copias instaladas utilizan el instalador para directorios protegidos; DeckPilot app en sí sigue funcionando sin derechos de administrador.

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

Quit DeckPilot antes de copiar este archivo para respaldar o restaurar su configuración. Si un archivo de configuración no se puede leer, DeckPilot conserva una copia con una `.backup-<date>` sufijo antes de usar una configuración fresca.

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

La consola comprueba la salida con un estado no cero en la primera afirmación fallida. Cubre:

- Cartografía clave, repeticiones de teclas, versiones, conmutación de perfil y copia de acción.
- Configuración de validación, persistencia y migración de configuraciones antiguas.
- Búsqueda de aplicaciones y manejo de objetivos.
- Selección de lanzamiento, sumas de comprobación, descargas truncadas o corruptas, redirecciones y cancelación.
- Reemplazo ejecutable, preservación de copias de seguridad, y revolver.

El control de bloqueo de archivos se ejecuta sólo en Windows.

GitHub Actions también funciona `tests/windows-update-smoke.ps1` contra el ejecutable independiente. Comienza el ayudante de actualización, sale del proceso original, verifica la confirmación de sustitución y puesta en marcha, comprueba la preservación de la configuración, luego abre el selector de aplicaciones instalado a través de la Automatización de la UI y guarda una selección. También se ejecutan ejercicios renombrados ejecutables y caminos que contienen espacios. Este script está reservado para la cuenta de prueba de GitHub Actions des. `tests/windows-installer-smoke.ps1`: instalación de archivos de programa real, atajos de menú de escritorio/Iniciar, arranque predeterminado, actualización con la aplicación abierta, preservación de arranque deshabilitado, y desinstalación sin eliminar perfiles.

<a id="publish-a-standalone-windows-folder"></a>
### Publicar una carpeta independiente de Windows

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

Distribuir el archivo completo. El flujo de trabajo de construcción de Windows también produce este paquete.

<a id="publish-a-github-release"></a>
### Publish a GitHub release

Actualizar la versión de la aplicación y `.github/release-notes.md`, luego empujar un emparejado `v*` tag, tales como `v1.0.0`. El flujo de trabajo de lanzamiento comprueba que la etiqueta coincide con la versión del proyecto y ejecuta validación antes de publicar:

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

DeckPilot es liberado bajo **[Licencia MIT](../../LICENSE)**. Puede utilizar, copiar, modificar y redistribuirlo, incluso comercialmente, siempre y cuando retenga los avisos de copyright y licencia requeridos por la licencia.

Si construyes DeckPilot, un pequeño crédito visible y un enlace a este repositorio serían apreciados. Este crédito adicional es opcional; mantener los avisos del MIT todavía es necesario.

Crédito sugerido:

> Basado en [DeckPilot](https://github.com/lolo34dr/deckpilot) por [lolo34dr](https://github.com/lolo34dr).

<a id="macropad-firmware-updates"></a>
## Actualizaciones de firmware de Macropad

DeckPilot 1.1.0 incluye un **Firmware du macropad…** diálogo y firmware 1.0.0 Pro Micro ATmega32U4 **5 V / 16 MHz**, utilizando la configuración Leonardo/Caterina confirmada por el registro de carga del propietario. La herramienta de parpadeo de Windows agrupada no requiere Arduino IDE. La selección y confirmación de dispositivos son obligatorios; actualizaciones de aplicaciones nunca flashear silenciosamente el tablero.

**El propietario ha confirmado éxito en la aplicación flashing, firmware 1.0.0 identificación y todo 12 botones en la tabla de referencia.** Mantenga el boceto original y el acceso a RST/GND para la recuperación. [documentación de firmware](FIRMWARE.es.md) para compatibilidad, validación, recuperación, pasos de construcción y licencias de terceros. Los modos USB per-button y los controles de medios nativos no son parte de esta versión de firmware.

<a id="building-the-windows-installer"></a>
### Construcción del instalador de Windows

Después de producir `artifacts/standalone/DeckPilot.exe` con el paquete de firmware integrado, instalar Inno Setup 6 y correr `./installer/build.ps1` de la raíz del repositorio. `artifacts/installer/DeckPilot-Setup.exe`. CI construye y prueba este instalador antes de adjuntarlo a una versión. El instalador utiliza un ID de aplicación estable para que las versiones futuras actualicen la misma instalación.

<a id="connection-dashboard-and-firmware-checks-130"></a>
## Controles de panel de conexión y firmware (1.3.0)

El **Votre macropad** tarjeta muestra estado de conexión, el firmware detectado y cualquier actualización de paquetes disponibles. **Connexion et diagnostic** expone la selección de tableros cuando varios Leonardos están conectados, el estado de enlace USB en vivo, cuenta de botones físicos, preferencias de notificación de firmware y un resumen de diagnóstico copiable. Los diagnósticos no copian las acciones de perfil o identificadores de serie USB. El monitor vuelve a conectarse después de desconectar y deja su puerto serie automáticamente mientras el administrador de firmware está abierto. 1.0.0 y sin el vínculo de diagnóstico.

Las comprobaciones de firmware de inicio están activadas por defecto. Después de la actualización de la aplicación, el cheque y la calibración están fuera del camino, un firmware identificado más antiguo puede impulsar la instalación; enviar recordatorios de demoras para 24 horas. Los tableros desconocidos y múltiples candidatos no seleccionados no se destellan automáticamente. Los lanzamientos de firmware se agrupan con actualizaciones de aplicaciones de GitHub, en lugar de obtener de una alimentación independiente de firmware.

Firmware **1.1.1** añade identificación de sesión, respuestas de latidos cardíacos correlativos, mensajes encuadernados y eventos físicos de estado clave. Estos eventos animan los botones mapeados y actualizan el diagnóstico; nunca invocan acciones por segunda vez. Su primera validación de tablero físico todavía es necesaria, aunque el firmware anterior 1.0.0 y el procedimiento flash han sido confirmados por el propietario. [protocolo y documentación de compatibilidad](FIRMWARE.es.md).

<a id="keypad-interface-140"></a>
## Interfaz de teclado (interfaz de teclado)1.4.0)

El más grande 3 × 4 keypad sigue el recinto físico y las teclas negras. Cada clave muestra su icono principal, etiqueta, cartografía F13–F24 y un pequeño icono de tipo de acción; las puntas de las herramientas conservan la etiqueta completa y la posición. Pulsa retroalimentación, acciones de ejecución, envío exitoso y errores tienen apariencias distintas, incluyendo símbolos en lugar de color solo.

Elija una clave para editarla. **Options avancées · fichier et arguments** mantiene las trayectorias manuales de aplicación y los argumentos de lanzamiento colapsados por defecto, mientras que los destinos esenciales de carpetas y sitios web se siguen expandiendo. **Tester l’action** guarda y envía la acción seleccionada en el PC actual; un indicador de éxito confirma el envío, no la terminación dentro de otra aplicación. Atajos de teclado apuntan a la ventana activa. Las teclas F13-F24 libres no tienen ninguna acción de prueba.

Cuando el macropado está desconectado, **Hors ligne** y la indirecta acompañante explican que los perfiles todavía pueden ser editados y probados las acciones. La configuración se almacena en el PC y permanece disponible a través de desconexiones y reconexiones. Estas mejoras de interfaz funcionan con el firmware existente y no requieren flash adicional.

El menú de la zona de notificación ahora cambia los perfiles directamente. **Ctrl+S** Guarda la configuración actual del botón. Los archivos de configuración inválidos se respaldan antes de que se utilice una configuración fresca. Se mantienen la instalación de aplicaciones, iconos de menús de escritorio/Iniciar, preferencias de inicio y el actualizador automático existente.

Para cargas manuales de Arduino IDE, **Renuncio DeckPilot desde su menú de bandeja** para liberar el puerto de serie. Cerrar la ventana principal lo mantiene funcionando en el fondo.

<a id="spotify-action-search-and-drag-and-drop-150"></a>
## Spotify, búsqueda de medidas y drag-and-drop (1.5.0)

**Spotify** está separado de **Multimédia**. Its play/pause, next and previous commands address the Spotify desktop app's Windows media session, even while a browser is playing YouTube. Spotify must be open and have an available reproducción session; start a track once if necessary. Spotify in a browser is not supported. Missing or refusing Spotify sessions produce an error, with no fallback to global media keys or another player. No hay autorización de cuenta de Spotify, suscripción Premium o token es necesaria por DeckPilot. Windows 10 versión 2004 o más tarde, o Windows 11, es requerido por esta construcción.

Uso **Rechercher une action ou une application** para consultas tales como `volume`, `skip`, `Spotify` o el nombre de una aplicación instalada. El índice de aplicación instalada carga en la primera búsqueda y se encaje durante cinco minutos; la selección manual de archivos sigue disponible. Haga clic en un resultado para configurar la tecla seleccionada o arrastrela en otra clave.

Arrastre una acción del catálogo o los resultados de búsqueda en una clave para asignarlo. Arrastre una clave configurada en otra **cambiar sus acciones**; la calibración física permanece inalterada. `.exe`, `.lnk`, `.appref-ms`, carpeta o URL HTTP(S) en una clave. Dropping sólo configura la clave y nunca ejecuta la acción. Complete cualquier configuración que falta en el editor.

El inicio de Windows ahora mantiene una preferencia explícita por usuario, repara una entrada Stale o missing Run, e informa las banderas deshabilitadas de arranque de Windows conocidas. Los lanzamientos normales respetan el desactivamiento de Windows; explícitamente habilitante **Lancer avec Windows** despeja lo conocido DeckPilot bandera deshabilitada para el usuario actual. Los lanzamientos de fondo permanecen ocultos incluso antes de la calibración, y los lanzamientos de fondo duplicados salen tranquilamente. **Préférences de l’application**, comprobar el estado de inicio y reactivar la opción si Windows lo bloqueó. Un registro real / registro en el PC del usuario sigue siendo el cheque final para las políticas de Windows o restricciones específicas de la máquina.

<a id="sharp-keypad-rendering-profile-recovery-and-audio-controls-161"></a>
## Dotación de teclado Sharp, recuperación de perfiles y controles de audio (1.6.1)

El teclado ahora renderiza en tamaños nativos de WPF en lugar de escalar texto a través de un Viewbox. Efectos de sombra de pariente/key y animaciones de escala de texto fueron eliminados; ventanas más pequeñas pergamino en lugar de reducir el teclado. La aplicación declara conciencia DPI por monitor y utiliza iconos de shell más grandes para aplicaciones de escritorio.

**Annuler / Rétablir** restaura las ediciones de perfiles, incluyendo swaps de arrastrar y soltar, ajustes de acción, importaciones y creación de perfiles/deleción. 50 ediciones durante la sesión de la aplicación. Ctrl+Z / Ctrl+Y operan en perfiles fuera de los campos de texto; los campos de texto conservan su propio deshacer/redo. La calibración, las preferencias de inicio y las acciones ejecutadas no se deshacen.

Under **Gérer les profils**, exportar el perfil actual o todos los perfiles como `.deckpilot.json`, los perfiles de importación como copias nuevas, o restaurar una copia de seguridad de todos los perfiles. Importar nunca ejecuta acciones y nunca reemplaza la calibración del PC. Los perfiles contienen trayectorias de acción, argumentos y IDs de terminales de audio, que pueden necesitar ajustarse en otro PC. Se rechazan formatos desconocidos, archivos de tamaño excesivo y configuraciones de gesto malformadas.

Antes de guardar perfiles cambiados, DeckPilot mantiene los perfiles anteriores en `%APPDATA%\DeckPilot\Backups` (20 Versiones, deduplicadas por contenido). Los ahorros sólo de preferencia no crean copias de seguridad de perfil. Un fallo de copia de seguridad evita la sobreescritura de la configuración actual. Restaurar reemplaza los perfiles después de la confirmación y puede ser deshecho; la calibración permanece inalterada.

**Microphone Windows** toggles mute on all currently active Windows capture endpoints. La barra de estado persistente y la tarjeta Audio Windows muestran estado activo/muted/mixed/unavailable, refrescado cada dos segundos y después de los comandos de la aplicación. Una actualización multi-endpoint fallido intenta restaurar estados mudos anteriores. Estos indicadores describen el estado de Windows endpoint, no un interruptor de hardware o un botón mute de la aplicación.

**Changer de sortie audio** alterna entre dos dispositivos de salida seleccionados conectados, para la consola de Windows, multimedia y predeterminados de comunicaciones. Un objetivo perdido da un error sin elegir otro dispositivo. Las aplicaciones con su propia salida fija pueden no seguir el predeterminado de Windows. Los IDs de endpoint importados se muestran como indisponibles hasta que se adaptan al PC actual.

Para configurar **appui court / appui long**, primero elegir una acción configurada, habilitar la opción de dos acciones, seleccionar el gesto para editar y establecer un umbral desde 200 a 3000 ms. Sin este modo, las acciones conservan el comportamiento inmediato de arriba abajo. Con él, las acciones cortas se ejecutan en la liberación antes del umbral; las acciones largas se ejecutan una vez en el umbral (o en la liberación si el temporizador todavía no ha funcionado). Una acción larga nunca también corre el corto. Las teclas de paso F13–F24 no pueden usar el modo de doble acción.

Controles básicos cubren los límites de repetición / liberación, las bodegas simultáneas, cancelación, historial de perfiles/intercambios/backups y rollos de audio utilizando puntos finales falsos. Windows UI comprueba el ejercicio real drag undo/redo, configurando una acción larga y el umbral, las dimensiones de teclado nativo después del tamaño y el indicador de micrófono.

<a id="search-and-ci-reliability-fixes-164"></a>
## Fiabilidad de búsqueda y CI fija (1.6.4)

Los resultados de búsqueda Equivalente mantienen sus botones WPF existentes. El catálogo de la aplicación instalada y el reemplazo de los resultados de aplazamiento del temporizador de debounce mientras se captura un botón de búsqueda o se está progresando un arrastre.

Controles de humo de Windows continúan realizando arrastres de ratón nativos y validando asignaciones guardadas, swaps, undo/redo y ambos gestos de prensa. El cliente de prueba utiliza coordenadas del cursor físico para coincidir con los límites UI Automation independientemente de DPI, se centra antes de medir, y escanea todos los porcentajes de desplazamiento consecutivos.

Construir y soltar flujos de trabajo utilizan Node.js 24 acciones. El compilador Arduino permanece en CLI 1.2.2; CI descarga su archivo de Windows con un marcado SHA-256 checksum. El firmware y sus versiones de toolchain no han cambiado.

Ajustes leídos en las pruebas de Windows permiten explícitamente compartir lectura/escritura y parse UTF-8 Después de cerrar el lector. Esto evita que la prueba en sí misma niegue la sustitución de archivos atómicos de la aplicación. La prueba de historia mantiene una vieja instantánea abierta a través de tres ciclos de deshacer/redo y verifica que permanece intacta mientras los ajustes en vivo cambian.

En Windows, los archivos de configuración existentes se comprometen con `File.Replace` en lugar de sobreescribir-mode `File.Move`. Esto es compatible con los lectores que permiten la eliminación al tiempo que conservan una instantánea vieja completa. La suite central prueba tres reemplazos con un lector mantenido abierto; los controles de núcleo de Windows funcionan antes de la compilación de firmware y la publicación de manera que los fallos de persistencia se detectan temprano.

Un lector de instantáneas de prueba puede esperar hasta 500 ms para Windows compartir / bloquear las violaciones mientras `ReplaceFile` commits. La espera se aplica sólo para abrir la instantánea, nunca para ejecutar o volver a ejecutar una acción de aplicación. Otros errores, malformado JSON y bloqueos persistentes siguen siendo fallos.

<a id="runtime-and-editing-optimizations-170"></a>
## Optimizaciones de tiempo de ejecución y edición (1.7.0)

Ahorrar configuraciones idénticas comprueba el archivo actual pero salta el trabajo de reemplazo y copia de seguridad. Se siguen detectando ediciones externas/deleción y escritos fallidos. `ButtonAction` tiene igualdad de valor (incluyendo la acción larga); los controles sucios del editor evitan la serialización JSON. La historia mantiene instantáneas de modelo desprendidos, valida nuevos estados, y evita que JSON pare durante el deshacer/redo. 50-edit limit and 20 Las copias de seguridad del perfil permanecen.

Search and picker share an immutable installed-app catalog for five minutes ()15 segundos para un resultado parcial).Los consumidores concurrentes comparten un escaneo; cerrar un selector no cancela otro consumidor. Refresh pasa por el caché, y cada picker tiene su propio filtro/view. El escaneo y el pergamino del catálogo se ejecutan fuera del hilo UI; el filtrado del selector es desmontado por 120 ms. Action search rechecks this cache after its debounce and when returning from the picker, so a freshed catalog is available there too.

Actualizaciones de la interfaz de usuario de la Junta ahora siguen notificaciones de conexión en lugar de despertar cada 100 m. Las notificaciones de desconexión ordenadas todavía cancelan las bodegas pendientes. Ganchos de teclado cola trabajo visual, mientras que la supresión y el tiempo de gesto permanecen sincronizados. ventanas ocultas/minimizadas detienen animaciones de retroalimentación y utilizan un intervalo de diez segundos de encuesta de audio en lugar de dos segundos; reapertura o emisión de un audio pedido de audio congelado estado de inmediato.

Estos cambios reducen el trabajo especificado; no establecen un porcentaje de CPU/RAM dependiente de la máquina. La conmutación de audio del hardware nativo y el comportamiento de clave física todavía requieren validación en un PC de Windows con el macropado.

<a id="workspace-tools-and-refreshed-interface-180"></a>
## Herramientas de espacio de trabajo e interfaz refrescada (1.8.0)

La interfaz de carbón pone la biblioteca de acción primero, separa los controles de perfil del tamaño nativo nítido 3 × 4 keypad, y grupos configuración de acción en una tarjeta. Los acentos clave distinguen sus propias categorías; selección, enfoque de teclado y retroalimentación de acción permanecen separados. **Réduire les animations** en preferencias complementa el ajuste de animación de Windows. Las ventanas más pequeñas conservan el acceso de desplazamiento en lugar de reducir el texto clave.

- **Plantillas de perfil:** por debajo de los límites **Gérer les profils**, añadir Música y audio, Escritorio y productividad, o Creación. Cada plantilla crea un perfil editable separado y nunca ejecuta sus acciones. Se conservan perfiles existentes y calibración física. Los atajos de teclado utilizan la aplicación activa, cuyos atajos pueden diferir.
- **Claves de perfil:** c) **Changer de profil**, a continuación, seleccione un perfil nombrado o ciclo hacia adelante / hacia atrás. Referencias entre perfiles se remapped al importar un conjunto o duplicar un perfil de auto-referencia. Un destino perdido reporta un error y puede ser reemplazado en el editor.
- **Windows comandos:** uso **Commandes Windows** abrir configuraciones comunes o Administrador de tareas, o bloquear explícitamente el PC. Los comandos vienen de una lista fija, sin ningún script para entrar. La acción de bloqueo requiere iniciar sesión en Windows.
- **Portapapeles de tecla completa:** Haga clic con el botón derecho para copiar, cortar, pegar o restablecerlo, incluyendo la acción y el acento largos. Ctrl+C/X/V y Eliminar el trabajo mientras una tecla de cubierta tiene enfoque del teclado; los campos de texto ordinario mantienen sus atajos de edición. DeckPilot y se aclara en la salida. Estas ediciones soportan undo/redo.
- **Grabador atajo:** seleccione una acción del teclado y haga clic **Enregistrer un raccourci**. Presione una combinación soportada, a continuación, guardarla. Escapar cancela; F13–F24 está reservado para el macropado mientras se registra. Las combinaciones conservadas por Windows todavía pueden requerir los controles manuales.
- **Acceso rápido:** Ctrl+K o el botón Biblioteca centra la búsqueda de acción. **Activité récente** mantiene el último 30 Acciones enviadas/failed en memoria para esta sesión, sin guardar los argumentos del programa o las rutas de destino. Se puede limpiar en cualquier momento. Un envío exitoso no garantiza la terminación en la aplicación de destino.

El firmware sigue **1.1.1**. Aceptar una oferta automática de firmware ahora abre correctamente el gestor de firmware; el anterior guardia modal impidió que el handoff. La identidad de firmware existente, latido del corazón, la reconexión y los cheques flash permanecen en su lugar.

La validación cubre los ajustes heredados, validación de acento, navegación de perfiles y remapping de referencia, plantillas portátiles, configuración nativa de Windows plantilla/deshacer/redo/redo y conmutación de perfiles, grabación atajo, copia/paste/reset, persistencia de configuraciones, instalación/actualización, redimensionamiento de diseño y una captura de interfaz de usuario real.
