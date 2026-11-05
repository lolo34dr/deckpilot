> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 1.6.1 fija la renderización de teclado suave y añade recuperación de perfiles, controles de audio de Windows y prensas cortas / largas.

- Texto del teclado de tamaño nativo: eliminar el escalado de cubierta completa y los efectos de sombra de contenido, agregar la conciencia de DPI por monitor y los iconos de aplicación de escritorio más grandes.
- Deshacer/redo para ediciones de perfiles, incluyendo arrastrar y soltar (50 cambios por sesión de la aplicación); los campos de texto mantienen a los nativos Ctrl+Z/Ctrl+Y.
- Importación/exportación de perfiles 20 copias de seguridad automáticas de versiones anteriores. Importaciones añadir nuevas copias; restaurar y deshacer la calibración de conservación. Las acciones importadas nunca se ejecutan automáticamente.
- Micrófono global de Windows con un indicador de barra de estado persistente y estado de punto final en vivo. Todos los puntos finales de captura activos se mutilan/sinmutan juntos.
- Interruptor entre dos dispositivos de salida conectados elegidos, para todos los roles de audio predeterminados de Windows. Los dispositivos perdidos fallan sin elegir silenciosamente otra salida.
- Dos acciones configuradas por clave 200–3000 Límite ms. Corridas cortas en liberación, largas sustituyes cortas y corres una vez. Las acciones inmediatas existentes y las teclas F13-F24 libres mantienen su comportamiento.

Descargar **DeckPilot-Setup.exe** o actualización de la aplicación. Se conservan perfiles y calibración. Las opciones de punto final de audio pueden necesitar ajustarse después de importar perfiles en otro PC. El estado de punto final de Windows difiere de la muda propia de la aplicación o un interruptor de muda de hardware; las aplicaciones de salida fija pueden no seguir cambios de salida predeterminados.

Validación: pruebas de perfil de núcleo/gesture/audio (audio endpoints simulados), pruebas de instalación/actualización/UI de Windows, arrastre de ratón nativo deshacer/redo y edición de larga acción. Mantiene la llave física, controladores de micrófono reales y conmutador de auriculares / altavoces todavía necesitan confirmación en el hardware del propietario.
