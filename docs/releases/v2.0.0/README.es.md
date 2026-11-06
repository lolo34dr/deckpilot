> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 2.0.0 introduce una profunda interfaz negra con fondo guardado, panel y colores de acento. La nueva tabla de sonido acepta clips MP3/WAV, permite a los usuarios elegir un micrófono físico y una salida de cable virtual, y mezcla la voz con clips para su uso como un micrófono en juegos o llamadas. Los clips se pueden iniciar desde la aplicación o se asigna a una tecla macropad. Un cable de audio virtual como VB-CABLE debe instalarse por separado en Windows; Atelier permanece en versión 1.0.10.

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
- Atelier comprueba GitHub sobre la puesta en marcha y bajo demanda. anuncia el nuevo Atelier construye o firmware empaquetado, luego descarga un instalador verificado contra el publicado SHA-256 archivo.
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

La validación incluye compatibilidad de núcleo/avigación/import/template checks e instalación nativa de Windows, actualización, picker, drag-and-drop, undo/redo, creación de plantillas, acciones de perfil, captura atajo, edición de portapapeles, diseño y minimizar/restore checks. El comportamiento de macropado físico y audio-dispositivo todavía depende del hardware Windows del propietario.
