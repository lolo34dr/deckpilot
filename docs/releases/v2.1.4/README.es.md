> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 2.1.4 Comienza el micrófono virtual configurado automáticamente con la aplicación, incluyendo la startup de fondo después del registro de Windows.

- La puesta en marcha automática del micrófono de placa sonora está habilitada por defecto para configuraciones nuevas y existentes. Utiliza el micrófono físico seleccionado y VB-CABLE; no se necesita activación manual después de reiniciar el PC cuando DeckPilot comienza con Windows.
- Si los dispositivos no están listos para iniciar sesión, o el audio se detiene después de una desconexión, DeckPilot vuelve cada cinco segundos mientras que la activación automática está activada.
- Una parada explícita deshabilita y ahorra la preferencia de arranque automático; activarla de nuevo desde el Soundboard panel o el botón de activación. Los clips de audio nunca se reproducen automáticamente.
- Atelier remains 1.0.11 y restos de firmware 1.1.1.

La validación incluye los controles de migración/ida y vuelta y un cheque UI nativo de Windows para la preferencia de arranque automático persistente, además de los cheques de audio e instalador existentes. El arranque/reconexión del dispositivo físico todavía depende del hardware del propietario.

DeckPilot 2.1.3 reduce la latencia del micrófono de la placa sonora y elimina los bloques de conversión de un segundo de la vía de audio en vivo.

- Utilice el muestreo de bajada de salida, pequeño bloque para el micrófono y clips de sonido; ajuste de las tasas de muestra de bypass por completo.
- Solicitud de evento 20 ms captura y 40 ms buffers de reproducción en lugar de la anterior 100 configuración ms. Latencia real depende de Windows, los dispositivos de audio y VB-CABLE.
- Bound micrófono buffering to 100 ms y conservar los marcos completos recientes después de un puesto de reproducción en lugar de colar el viejo discurso. Las subidas cortas agregan sólo el silencio solicitado por la lectura actual.
- Soporte mono, estéreo, multicanal y Windows entradas extensibles PCM/float sin la Fundación Medios un segundo lectura-ahead.
- Mantener el procesamiento de disco y el enrutamiento sin cambios. 1.0.11 y restos de firmware 1.1.1.

La validación incluye cheques de regresión para tamaños de bloques de muestreo, conversión de canales, formatos extensibles, recuperación de subida, frescura desbordamiento, captura/jugador simultáneo y finalización de clips, junto con las pruebas de instalación y aplicación de Windows existentes. No se reclama una cifra de latencia de hardware; verifique la mejora con su micrófono y controlador de audio.

DeckPilot 2.1.2 y DeckPilot Atelier 1.0.11 fijar seguridad del taller, fiabilidad de la placa de sonido y sonidos de perfil portátil.

- Atelier puede preparar tablas Leonardo/Caterina que aún no contestan DeckPilot protocolo; los fallos de serie todavía dejan de parpadear y la ubicación USB, el cargador de arranque y la firma MCU siguen siendo verificados.
- Cada inspección comienza con resultados vacíos. Los informes conservan la tabla probada y el firmware, y los callbacks USB estancados no pueden afectar una nueva sesión.
- Cerrar Atelier durante la escritura o verificación de firmware está bloqueado.
- Las exportaciones de perfiles y copias de seguridad de perfiles automáticos pueden retener archivos MP3/WAV de referencia (32 MiB límite de audio). Importaciones copiar clips incrustados en una carpeta gestionada y remapa referencias de acción cortas/longas. Las importaciones de perfiles de Legacy siguen soportadas; falta de audio de copia de seguridad se reporta explícitamente.
- La tabla de sonido libera clips terminados, actualiza indicadores de reproducción e informes interrumpidos flujos de audio. VB-CABLE endpoints are recognized through their adapter identity.
- Enlaces correctos traducidos README y preservar los nombres de los dispositivos técnicos durante la traducción. VB-CABLE requisito.

La validación incluye cheques de regresión núcleo, pruebas de protocolo de firmware, pruebas de traducción, Windows build, DeckPilot y los controles de instalación / actualización / desinstalación de Atelier, y la interfaz nativa / actualizaciones de los cheques. El comportamiento de primera y desconexión de audio física todavía requieren el hardware del propietario. 1.1.1; no se requiere actualización de firmware.

DeckPilot 2.1.1 mejora la instalación y actualizaciones:

- Comprobación automática para VB-CABLE durante la primera instalación y actualizaciones, y descargar y lanzar el instalador oficial firmado cuando el controlador está desaparecido.
- Mostrar Inicio de Windows, cheques automáticos de actualización y preferencias de baja categoría durante la configuración y actualizaciones, con las opciones existentes preseleccionadas y conservadas.
- Mantenga el micrófono físico seleccionado y solicite la configuración del micrófono sólo cuando sea necesario.
- Arreglar inicialización y detección de instalaciones existentes.

Descargar **DeckPilot-Setup.exe** o aceptar la actualización en DeckPilot. Se requiere la aprobación del administrador de Windows. Se conservan los perfiles existentes y la calibración. DeckPilot Atelier remains at 1.0.10 y el firmware sigue siendo 1.1.1; ambos se incluyen en la versión compartida.

Validación: controles de núcleo y Windows build, actualización del instalador / desinstalación y pruebas de aplicación nativa se ejecutan antes de la publicación de la publicación. El comportamiento del hardware de audio todavía requiere pruebas en el PC del usuario.

DeckPilot 2.1.0 mantiene la profunda interfaz negra y colores personalizables, y recorre la tabla de sonar a través de VB-Audio VB-CABLE automáticamente. Los usuarios seleccionan sólo su micrófono físico; el punto final de reproducción virtual se fija por lo que los clips MP3/WAV no se pueden enviar accidentalmente a los altavoces. DeckPilot instalador puede descargar el oficial VB-CABLE paquete, verificar su editor de Authenticode, y lanzar la configuración del controlador del proveedor; el paquete no se redistribuye. En Discord o un juego, seleccione CABLE Output como el micrófono. Atelier permanece en versión 1.0.10.

DeckPilot 1.9.11 y DeckPilot Atelier 1.0.10 Agregue el hardware y el diagnóstico de firmware previos al combate, una comparación de la versión de firmware, una acción de recuperación guiada después de los escritos interrumpidos, el flash secuencial multi-board con un informe de lote, y un control físico-posición-a-F13–F24 en cada informe de control clave.

DeckPilot 1.9.11 incluye la misma actualización de Atelier en la versión compartida del instalador.

DeckPilot 1.9.10 restaura las liberaciones compartidas que contienen ambos DeckPilot y DeckPilot Atelier 1.0.9. Ambas aplicaciones una vez más comprobar la misma última versión de GitHub.

Si instalas Atelier 1.0.8 desde el canal de liberación separado, instalar DeckPilot-Atelier-Setup.exe de esta versión una vez para volver a las actualizaciones compartidas. DeckPilot puede utilizar su actualizador existente.

DeckPilot Atelier 1.0.7 añade una era confirmada del programa-área que aclara todo 28 KiB de flash de usuario, deja un bucle de reset seguro, y conserva Caterina y EEPROM. Reiniciar permanece deshabilitado hasta que se selecciona una tabla física.

DeckPilot 1.9.7 fija Atelier desinstalar mientras la aplicación está abierta. Atelier 1.0.6 también corrige su manifiesto de Windows DPI, que impidió que la aplicación instalada iniciara.

DeckPilot 1.9.6 fija el manifiesto DPI de Windows que previno DeckPilot Atelier desde la apertura después de la instalación.

DeckPilot 1.9.5 Incluye diagnósticos detallados de arranque Atelier en el cheque de liberación de Windows.

DeckPilot 1.9.4 mejora la prueba de humo de arranque de Atelier Windows para informar detalles de fallo de la aplicación.

DeckPilot 1.9.3 fija los metadatos de Atelier para leer el manifiesto de firmware del archivo de firmware agrupado.

DeckPilot 1.9.2 arregla el paso de embalaje de liberación de Atelier y mejora su diagnóstico de construcción.

DeckPilot 1.9.1 mejora la puesta en marcha del instalador Atelier y añade controles automáticos de actualización de aplicaciones y firmware.

- El instalador lanza Atelier en la sesión de usuario de Windows firmada y detiene/restringe de forma segura para las actualizaciones.
- Atelier comprueba GitHub en la puesta en marcha y bajo demanda. anuncia el nuevo Atelier construye o firmware empaquetado, luego descarga un instalador verificado contra el publicado SHA-256 archivo.
- Firmware sólo se flashiza después de que el usuario elija la acción flash; una actualización de la aplicación nunca escribe a una tabla conectada por su cuenta.

El instalador Atelier también lanza en la sesión de Windows del usuario firmado después de una instalación fresca. El flujo de trabajo de lanzamiento ahora abre la aplicación instalada, luego verifica la actualización y desinstalación mientras se está ejecutando.

DeckPilot 1.9.0 adiciones DeckPilot Atelier, una aplicación separada del taller de Windows para flashear nuevos macropados y comprobar todos 12 llaves.

- Instale el firmware incluido en las tablas Leonardo / Pro Micro ATmega32U4 con el cargador de arranque Caterina.
- Ejecute controles guiados de presión corta y larga presión para cada clave física, registre su cartografía F13–F24 y guarde un informe de control de calidad.
- Descargar **DeckPilot-Atelier-Setup.exe** de esta liberación, el original DeckPilot Configuración y firmware permanecen disponibles por separado.

DeckPilot 1.8.0 añade un espacio de trabajo refrescado y formas más rápidas de construir y utilizar sus perfiles.

- Interfaz de carbón/teal más clara, biblioteca de acción primero, nítida 3 × 4 claves, colores de acento clave y preferencia de movimiento reducido.
- Tres plantillas de perfiles editables: Música y audio, Escritorio y productividad, y Creación.
- Medidas de perfil: cambiar a un perfil nombrado, el siguiente o el perfil anterior de una clave. Los conjuntos de perfil importado mantienen sus enlaces internos.
- Acciones de Windows: abrir ajustes comunes o Administrador de tareas, o bloquear explícitamente el PC.
- Copiar/cortar/paste/reestablecer teclas completas, incluyendo largas prensas y colores, con deshacer/redo y un portapapeles plano.
- Capturar un atajo de teclado directamente; Escapar cancela y las teclas macropadas permanecen reservadas durante la captura.
- Ctrl+K búsqueda de acción y una historia clara y sencilla de la última sesión 30 acciones.
- Arregla el impulso automático de firmware para aceptarlo abre el gestor de firmware. Los gestos pendientes se cancelan en los cambios de perfil.

Descargar **DeckPilot-Setup.exe** o actualización de la aplicación. Se conservan los perfiles existentes, las preferencias de calibración y puesta en marcha. **1.1.1**; no se requiere flash para estas características.

La validación incluye compatibilidad de núcleo/avigación/import/template checks e instalación nativa de Windows, actualización, picker, drag-and-drop, undo/redo, creación de plantillas, acciones de perfil, captura atajo, edición de portapapeles, diseño y minimizar/restore checks. El comportamiento de macropado físico y audio-dispositivo todavía depende del hardware Windows del propietario.
