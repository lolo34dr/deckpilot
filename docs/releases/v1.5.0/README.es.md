> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 1.5.0 añade controles específicos de Spotify, búsqueda de acciones y drag-and-drop, y repara el registro de arranque de Windows.

- **Spotify** play/pause, next and previous address only the Spotify desktop media session, keeping browser/YouTube reproducción separate. Abrir Spotify y comenzar la reproducción una vez. Spotify en un navegador no está soportado; no hay ningún retroceso de medios globales si Spotify está desaparecido o rechaza un comando.
- Buscar acciones y aplicaciones instaladas por nombre o palabras clave como volumen, skip, Spotify o discordia. Haga clic en los resultados o arrástrelos en una clave.
- Arrastre acciones de catálogo/búsqueda en claves, cambie las acciones de dos teclas sin cambiar la calibración, o suelte un enlace ejecutable, atajo, carpeta o HTTP(S).
- Startup mantiene una preferencia por usuario guardada, repara caminos perdidos/de cuento, detecta banderas deshabilitadas de Windows conocidas y apoya la reactivación explícita. Los lanzamientos de arranque de fondo y duplicado permanecen en silencio, incluso antes de la calibración.

La construcción requiere Windows 10 versión 2004 o más tarde, o Windows 11. Descargar **DeckPilot-Setup.exe** o actualización desde DeckPilot. Los perfiles y calibración se conservan; no es necesario un flash de firmware. Para reparar una startup previamente bloqueada, compruebe su estado bajo **Préférences de l’application** y habilitación **Lancer avec Windows** otra vez.

Validation incluye simulación de aislamiento de Spotify/browser, cheques de búsqueda y configuración, pruebas de reparación de instalación de Windows/startup, real mouse drag-and-drop y actualizaciones nativas/UI. La reproducción de Spotify junto con YouTube y un registro de Windows real todavía necesitan confirmación en el PC del usuario.
