> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 2.4.1 corrige las pestañas del panel de control para que sus contornos completos permanezcan visibles y sus anchos permanezcan alineados cuando la ventana se redimensione.

No se requiere actualización de firmware ni flash.

DeckPilot 2.4.0 reorganiza el panel de control y añade selección exacta de colores.

- Usar el **Acciones**, **Appareil** y **Réglages** pestañas para encontrar acciones clave, controles de dispositivo/audio y preferencias.
- Mantenga el diseño de teclado/editor de la mano izquierda existente, DeckPilot logo y monocromo vector pictogramas.
- Elige los colores exactos de fondo, panel y acento con **Palette...**, previsualizar sus valores y swatches, luego aplicarlos. El selector de Windows admite colores personalizados; no se requiere ningún tema preestablecido.
- Eliminar la integración de control OBS/Streamlabs. 2.3 Los perfiles siguen siendo legibles, y sus acciones retiradas se conservan pero no se pueden ejecutar. Reasignar las claves de la biblioteca de acción.
- Preserve otros perfiles, acciones y calibración física. 1.1.1 and Atelier remains 1.0.12; no se necesita un flash de firmware.

DeckPilot 2.3.0 añade los controles directos OBS Studio y Streamlabs Desktop desde los botones macropad.

- Conéctese a OBS a través de su servidor WebSocket y Streamlabs Desktop a través de control remoto. DeckPilot envía los comandos API de aplicación directamente en lugar de simular atajos de teclado.
- Configurar acciones para escenas, streaming en vivo, grabación, mute de audio, visibilidad de la fuente y amortiguación de reproducción. OBS también admite el modo Studio y los controles de cámara virtual.
- Las credenciales de streaming están protegidas para el usuario actual de Windows. Firmware existente macropad funciona como es; no se requiere actualización de firmware ni flash.

DeckPilot 2.2.0 añade una supresión de ruido de micrófono local ajustable mientras mantiene los clips de placa de sonido intactos.

- RNNoise filtra el micrófono físico seleccionado antes DeckPilot mezcla en clips de cartón. Clips pasa el filtro y permanece claro en el micrófono virtual.
- Elija cualquier aplicación que use **CABLE Output** como su micrófono —juegos y aplicaciones de la cadena de voz por igual reciben la mezcla de la voz filtrada y el tablero de sonido. DeckPilotEs filtro.
- Activar o desactivar el filtrado y establecer su 0–100% de fuerza en **Soundboard · micro virtuel**. El procesamiento de audio permanece en este PC; no se requiere flash de firmware.
- La biblioteca nativa RNNoise está construida para Windows x64 e incrustada en DeckPilot y su instalador. Su licencia de terceros tipo BSD está incluida en el repositorio de origen.

El 10 ms RNNoise tamaño del marco no garantiza la la latencia de extremo a extremo. Windows, los dispositivos de audio y VB-CABLE También afecta el retraso. Prueba la calidad del micrófono y la latencia con la configuración prevista.

DeckPilot 2.1.7 agrega acciones de volumen sólo de Spotify, una acción integrada para abrir DeckPilot, y notificaciones de perfil en pantalla.

- Los botones de volumen de Spotify ajustan la sesión de audio de Spotify de Windows en 5% pasos, sin cambiar el volumen maestro u otros jugadores.
- El buscador de aplicaciones y la búsqueda de acción siempre ofrecen DeckPilot, y su acción muestra la ventana existente desde el área de notificación.
- Los cambios de perfil muestran una sobreposición breve y no activada en la pantalla activa. Elige una posición de esquina o personalizada en preferencias y previsualízala allí. Juegos exclusivos de pantalla completa pueden ocultar los overlays estándar de Windows.

DeckPilot Atelier remains at 1.0.12 y restos de firmware 1.1.1. No se necesita un flash de firmware para estas acciones.

DeckPilot 2.1.6 y DeckPilot Atelier 1.0.12 hacer que el firmware de Atelier borra explícito y recuperable.

- Mueve la era destructiva del firmware en operaciones avanzadas desplomadas y requiere escribir `EFFACER` antes de escribir.
- Explique que el tablero deja de enumerar como un teclado después de la borrada, muestre los pasos exactos de recuperación RST/GND de doble ajuste, y mantenga la acción de recuperación de Atelier disponible después de la borrada o un flash interrumpido.
- Firmware sigue siendo 1.1.1; no DeckPilot Se requiere un cambio de firmware de hardware.

La validación incluye controles de firmware y aplicaciones, además de la automatización de Windows UI para el control de borrado avanzado, seguridad de no dispositivos y guía de recuperación RST/GND. El reajuste en sí mismo todavía requiere la junta física.

DeckPilot 2.1.5 suaviza el audio-dispositivo refresca y añade una pantalla de inicio de marca.

- Descubra micrófonos físicos y VB-CABLE Los registros automáticos comparten una instantánea reciente del dispositivo; el botón de actualización manual siempre realiza un escaneo fresco.
- Reduzca la encuesta repetida de Windows de audio-estado y agilice la ruta común de conversión de mono-micrófono.
- Mostrar el DeckPilot banner mientras la aplicación interactiva comienza. Silent startup con Windows permanece en el fondo.
- Atelier remains 1.0.11 y restos de firmware 1.1.1.

La validación incluye cheques de regresión por escaneo de fondo/cache, cheques de conversión de audio, Windows construye, actualización/desinstalación del instalador, salpicaduras/actualización nativas y controles de la aplicación. El uso real de CPU y la latencia todavía dependen de los dispositivos de PC y audio.

DeckPilot 2.1.4 Comienza el micrófono virtual configurado automáticamente con la aplicación, incluyendo la startup de fondo después del registro de Windows.

- La puesta en marcha automática del micrófono de placa sonora está habilitada por defecto para configuraciones nuevas y existentes. Utiliza el micrófono físico seleccionado y VB-CABLE; no se necesita activación manual después de reiniciar el PC cuando DeckPilot comienza con Windows.
- Si los dispositivos no están listos para iniciar sesión, o el audio se detiene después de una desconexión, DeckPilot se alimenta cada cinco segundos mientras que la activación automática está activada.
- Una parada explícita deshabilita y salva la preferencia de arranque automático; activarla de nuevo desde el Soundboard panel o el botón de activación. Los clips de audio nunca se reproducen automáticamente.
- Atelier remains 1.0.11 y restos de firmware 1.1.1.

La validación incluye ajustes de migración/ida y vuelta y un cheque nativo de Windows UI para la preferencia de arranque automático persistida, además de los cheques de audio e instalador existentes. El arranque/reconexión del dispositivo físico todavía depende del hardware del propietario.

DeckPilot 2.1.3 reduce la latencia del micrófono de la placa sonora y elimina los bloques de conversión de un segundo de la vía de audio en vivo.

- Utilice el muestreo de bajada de salida, pequeño bloque para el micrófono y clips de sonido; ajuste de las tasas de muestra de bypass completamente.
- Solicitud de evento impulsado 20 ms captura y 40 ms buffers de reproducción en lugar de la anterior 100 configuración ms. Latencia real depende de Windows, los dispositivos de audio y VB-CABLE.
- Bound micrófono buffering to 100 ms y conservar los marcos completos recientes después de un puesto de reproducción en lugar de colar el viejo discurso. Las subidas cortas agregan sólo el silencio solicitado por la lectura actual.
- Soporte mono, estéreo, multicanal y Windows entradas extensibles PCM/float sin la Fundación Medios un segundo lectura-ahead.
- Mantener el procesamiento de disco y el enrutamiento sin cambios. 1.0.11 y restos de firmware 1.1.1.

La validación incluye cheques de regresión para tamaños de bloques de muestreo, conversión de canales, formatos extensibles, recuperación de subida, frescura de desbordamiento, captura/jugador simultáneo y finalización de clips, junto con las pruebas de instalación y aplicación de Windows existentes. No se reclama cifra de latencia de hardware; verifique la mejora con su micrófono y controlador de audio.

DeckPilot 2.1.2 y DeckPilot Atelier 1.0.11 Fija la seguridad del taller, la fiabilidad de la placa de sonido y los sonidos de perfil portátil.

- Atelier puede preparar tablas Leonardo/Caterina que todavía no responden DeckPilot protocolo; fallas en serie todavía dejan de parpadear y la ubicación USB, bootloader y la firma MCU siguen siendo verificadas.
- Cada inspección comienza con resultados vacíos. Los informes conservan la tabla probada y el firmware, y los callbacks USB estancados no pueden afectar una nueva sesión.
- Cierre Atelier durante la escritura o verificación de firmware está bloqueado.
- Las exportaciones de perfiles y copias de seguridad de perfiles automáticos pueden retener archivos MP3/WAV de referencia (32 MiB límite de audio). Importaciones copiar clips incrustados en una carpeta gestionada y remapa referencias de acción corta/long. Las importaciones de perfiles de Legacy siguen soportadas; falta de audio de copia de seguridad se reporta explícitamente.
- El panel de sonido libera clips terminados, actualiza indicadores de reproducción e informa de flujos de audio interrumpidos. VB-CABLE endpoints are recognized through their adapter identity.
- Enlaces correctos traducidos README y preservar los nombres de los dispositivos técnicos durante la traducción. VB-CABLE requisito.

La validación incluye cheques de regresión núcleo, pruebas de protocolo de firmware, pruebas de traducción, Windows build, DeckPilot y los controles de instalación / actualización / desinstalación de Atelier, y la interfaz nativa / actualizaciones de los cheques. La primera pestaña física y el comportamiento de desconexión de audio todavía requieren el hardware del propietario. 1.1.1; no se requiere actualización de firmware.

DeckPilot 2.1.1 mejora la instalación y las actualizaciones:

- Comprobación automática VB-CABLE durante la primera instalación y actualizaciones, y descargar y lanzar el instalador oficial firmado cuando el controlador está desaparecido.
- Mostrar Inicio de Windows, cheques automáticos de actualización y preferencias de baja categoría durante la configuración y actualizaciones, con las opciones existentes preseleccionadas y conservadas.
- Mantenga el micrófono físico seleccionado y solicite la configuración del micrófono sólo cuando sea necesario.
- Arreglar inicialización del instalador y detección de instalaciones existentes.

Descargar **DeckPilot-Setup.exe** o aceptar la actualización en DeckPilot. Se requiere la aprobación del administrador de Windows. Se conservan los perfiles existentes y la calibración. DeckPilot Atelier remains at 1.0.10 and firmware remains at 1.1.1; ambos se incluyen en la versión compartida.

Validación: controles de núcleo y Windows build, actualización del instalador / desinstalación y pruebas de aplicaciones nativas se ejecutan antes de la publicación de la publicación.

DeckPilot 2.1.0 mantiene la profunda interfaz negra y colores personalizables, y recorre el tablero de sonido a través de VB-Audio VB-CABLE automáticamente. Los usuarios seleccionan sólo su micrófono físico; el punto final de reproducción virtual se fija por lo que los clips MP3/WAV no se pueden enviar accidentalmente a los altavoces. DeckPilot instalador puede descargar el oficial VB-CABLE paquete, verifique su editor de Authenticode, y lanzar la configuración del controlador del proveedor; el paquete no se redistribuye. En discordia o un juego, seleccione CABLE Output como el micrófono. Atelier permanece en versión 1.0.10.

DeckPilot 1.9.11 y DeckPilot Atelier 1.0.10 Agregue el hardware y el diagnóstico de firmware previos al combate, una comparación de versiones de firmware, una acción de recuperación guiada después de los escritos interrumpidos, el flash secuencial multi-board con un informe de lotes, y un cheque de diseño físico-posición-a-F13–F24 en cada informe de control clave.

DeckPilot 1.9.11 incluye la misma actualización de Atelier en la versión compartida del instalador.

DeckPilot 1.9.10 restaura las liberaciones compartidas que contienen ambos DeckPilot y DeckPilot Atelier 1.0.9. Ambas aplicaciones una vez más comprobar la misma última versión de GitHub.

Si instala Atelier 1.0.8 desde el canal de liberación separado, instalar DeckPilot-Atelier-Setup.exe de esta versión una vez para volver a las actualizaciones compartidas. DeckPilot puede utilizar su actualizador existente.

DeckPilot Atelier 1.0.7 añade una era confirmada del programa-área que aclara todo 28 KiB de flash de usuario, deja un bucle de reset seguro, y conserva Caterina y EEPROM. Reiniciar permanece desactivado hasta que se selecciona una tabla física.

DeckPilot 1.9.7 fija Atelier desinstalar mientras la aplicación está abierta. Atelier 1.0.6 también corrige su manifiesto de Windows DPI, que impidió que la aplicación instalada iniciara.

DeckPilot 1.9.6 fija el manifiesto DPI de Windows que previno DeckPilot Atelier desde la apertura después de la instalación.

DeckPilot 1.9.5 Incluye diagnósticos detallados de arranque Atelier en el cheque de liberación de Windows.

DeckPilot 1.9.4 mejora la prueba de humo de arranque de Atelier Windows para informar detalles de fallo de la aplicación.

DeckPilot 1.9.3 fija los metadatos de Atelier para leer el manifiesto de firmware del archivo de firmware agrupado.

DeckPilot 1.9.2 fija el paso de embalaje de liberación de Atelier y mejora su diagnóstico de construcción.

DeckPilot 1.9.1 mejora la puesta en marcha del instalador Atelier y añade controles automáticos de actualización de aplicaciones y firmware.

- El instalador lanza Atelier en la sesión de usuario de Windows firmada y detiene/restringe de forma segura para las actualizaciones.
- Atelier comprueba GitHub sobre la puesta en marcha y bajo demanda. Anuncia el nuevo Atelier construye o firmware agrupado, luego descarga un instalador verificado contra el publicado SHA-256 archivo.
- Firmware sólo se flashiza después de que el usuario elija la acción flash; una actualización de la aplicación nunca escribe a una tabla conectada por su cuenta.

El instalador Atelier también se lanza en la sesión de Windows del usuario firmado después de una instalación fresca. El flujo de trabajo de lanzamiento ahora abre la aplicación instalada, luego verifica la actualización y desinstalación mientras se está ejecutando.

DeckPilot 1.9.0 adiciones DeckPilot Atelier, una aplicación separada del taller de Windows para flashear nuevos macropados y comprobar todos 12 llaves.

- Instale el firmware incluido en las tablas Leonardo / Pro Micro ATmega32U4 con el cargador de arranque Caterina.
- Ejecute controles guiados de presión corta y larga presión para cada clave física, registre su cartografía F13–F24 y guarde un informe de control de calidad.
- Descargar **DeckPilot-Atelier-Setup.exe** de esta liberación, el original DeckPilot Configuración y firmware permanecen disponibles por separado.

DeckPilot 1.8.0 añade un espacio de trabajo refrescado y formas más rápidas de construir y utilizar sus perfiles.

- Interfaz de carbón/teal más clara, biblioteca de acción primero, nítida 3 × 4 claves, colores de acento clave y preferencia de movimiento reducido.
- Tres plantillas de perfil editables: Música y audio, Escritorio y productividad, y Creación.
- Acciones de perfil: cambiar a un perfil nombrado, el siguiente o el perfil anterior de una clave. Los conjuntos de perfil importado mantienen sus enlaces internos.
- Acciones de Windows: abrir ajustes comunes o Administrador de tareas, o bloquear explícitamente el PC.
- Copiar/cortar/paste/reestablecer teclas completas, incluyendo largas prensas y colores, con deshacer/redo y un portapapeles plano.
- Capturar un atajo de teclado directamente; Escapar cancela y las teclas macropadas permanecen reservadas durante la captura.
- Ctrl+K búsqueda de acción y una historia clara y sencilla de la última sesión 30 acciones.
- Arregla el impulso automático de firmware para aceptarlo abre el gestor de firmware. Los gestos pendientes se cancelan a través de los cambios de perfil.

Descargar **DeckPilot-Setup.exe** o actualización de la aplicación. Se conservan los perfiles existentes, las preferencias de calibración y puesta en marcha. **1.1.1**; no se requiere flash para estas características.

La validación incluye compatibilidad de núcleos/navigation/import/template checks e instalación nativa de Windows, actualización, picker, drag-and-drop, undo/redo, creación de plantillas, acciones de perfil, captura atajo, edición de portapapeles, diseño y minimizar/restore checks. El comportamiento de macropado físico y audio-dispositivo todavía depende del hardware Windows del propietario.
DeckPilot 2.1.8 añade un altavoz opcional de vuelta para Soundboard clips.

- In **Soundboard · micro virtuel**, habilita **Entendre aussi les son du soundboard**, elegir un altavoz físico o salida de auriculares, y establecer su independiente 0–100Volumen %. El retorno se apaga por defecto y se guarda a través de los lanzamientos.
- Sólo los clips sonoros se envían a esta segunda salida. El micrófono físico continúa yendo al micrófono virtual, y nunca se mezcla en la vuelta del altavoz. VB-CABLE en su volumen original.
- Cambiar el volumen de retorno no reinicia el micrófono virtual. Una salida de altavoz desconectada detiene sólo el retorno; el micrófono virtual sigue funcionando. Refresh dispositivos o elige otra salida para restaurarlo.

DeckPilot Atelier remains at 1.0.12 y restos de firmware 1.1.1. No se necesita un flash de firmware. El routing de altavoces real debe ser revisado en el PC Windows del propietario con sus dispositivos de audio.

DeckPilot 2.1.9 fija el monitoreo de altavoces en dispositivos cuyo formato de audio de Windows es extensible, incluyendo algunos auriculares USB, y añade una notificación de sobreimpresión de la placa de sonido.

- El altavoz devuelve el formato exacto de mezcla WASAPI de la salida seleccionada, convierte muestras de clip en su PCM o codificación flotante, y se retrae sin reproducción impulsada por el evento cuando un controlador rechaza ese modo.
- El inicio de un clip de cartón muestra su nombre en la sobrepuesta configurable en pantalla. La preferencia de notificación de perfil existente, la colocación y el control de ajuste de movimiento reducido ambos tipos de notificación.
- El retorno todavía contiene sólo clips de mesa de sonido; el micrófono físico se mantiene fuera de la mezcla de altavoces y continúa hasta el micrófono virtual.

DeckPilot Atelier remains at 1.0.12 y restos de firmware 1.1.1. No se necesita un flash de firmware. La compatibilidad de altavoces debe ser verificada con el dispositivo de audio Windows real del propietario.
