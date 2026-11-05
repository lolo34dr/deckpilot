> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 1.3.2 mejora la conexión USB y comprueba el firmware macropad al inicio.

- Panel de conexión con reconexión automática, firmware detectado, diagnóstico de estado de botón en vivo/USB y selección explícita de tableros cuando varios dispositivos están presentes.
- Comprobaciones de firmware de inicio y propuestas de actualización, con un 24- retraso de recordatorio de horas y una opción para desactivar notificaciones de firmware. La instalación todavía necesita confirmación.
- Nuevo firmware **1.1.1**: mensajes de protocolo consolidados, fichas de sesión frescas, respuestas de latidos cardíacos correlacionados y eventos de estado clave. La cartografía F13-F24 sigue siendo invariable, incluyendo el uso independiente.
- Firmware manager libera automáticamente el puerto serial del monitor antes de la identificación/flashing y vuelve a conectarse después. Firmware 1.0.0 y las consultas de identidad heredadas siguen siendo apoyadas.
- Los perfiles se pueden seleccionar en el menú de bandejas; Ctrl+S guarda cambios; el diagnóstico de conexión se puede copiar para el soporte.

Las actualizaciones de firmware se entregan en versiones de aplicaciones; cheques de arranque comparan la versión detectada contra el paquete incluido. Ningún hardware se flashea silenciosamente. **Firmware 1.1.1 todavía necesita una primera prueba de tablero físico:** firmware 1.0.0 y el procedimiento flash anterior fue validado por el propietario, mientras que este nuevo protocolo está cubierto por pruebas de host y compilación AVR.

Descargar **DeckPilot-Setup.exe** o actualización de la aplicación. Se conservan los perfiles, las preferencias de calibración y puesta en marcha. DeckPilot antes de utilizar Arduino IDE u otro monitor de serie; abrir el gestor de firmware de la aplicación libera la conexión automáticamente.
