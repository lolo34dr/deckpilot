> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 1.7.0 reduce el trabajo repetido al editar perfiles, encontrar aplicaciones y correr en el fondo.

- Ahorrar configuraciones no cambiadas ya no reemplaza el archivo o crea copias de seguridad. El contenido del disco actual sigue siendo revisado, por lo que no se ocultan ediciones externas, eliminaciones y ahorros fallidos.
- La historia del perfil mantiene instantáneas de modelo desprendidos. ediciones sin cambios y deshacer/redo evitan ida y vueltas JSON; edición de acciones utiliza comparaciones de valor en lugar de serialización repetida.
- La búsqueda de aplicaciones y el picker comparten un caché de catálogo de cinco minutos y una tarea de descubrimiento. Discovery corre fuera de WPF; Refresh todavía fuerza un nuevo escaneo. Cada picker posee su filtro y mantiene los resultados actuales utilizables durante el refresco.
- Sustitúyase el permanente 100 ms board UI encuesta con notificaciones de conexión ordenadas. Keyboard-hook callbacks cola trabajo visual en lugar de renderizar doce teclas sincronizadamente.
- Las encuestas de estado de audio cada dos segundos, visibles y cada diez segundos cuando se ocultan o minimizan, refrescante inmediatamente en la reaparición y después de los comandos de audio.
- Las ventanas ocultas detienen los temporizadores de retroalimentación visual y saltan las esperas de renderización cosmética. Los pinceles de UI congelados se reutilizan y lanzan envoltorios de proceso de aplicación.

Descargar **DeckPilot-Setup.exe** o actualización de la aplicación. Se conservan perfiles, preferencias de calibración y puesta en marcha. No se requiere un flash de firmware.

La validación incluye configuraciones/history/cache regressions e instalación de Windows, actualización, picker, drag-and-drop, edición de acción corta/long y minimizar / restaurar cheques. Estas son optimizaciones estructurales, no una reclamación medida de un CPU particular o RAM ahorro en cada PC.
