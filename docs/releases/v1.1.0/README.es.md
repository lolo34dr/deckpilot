> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot ahora puede instalar firmware en el macropad DIY Pro Micro directamente desde la aplicación.

- Nuevo **Firmware du macropad…** diálogo con la selección y confirmación del dispositivo antes de flashear.
- Firme de Bundled **1.0.0** conserva F13–F24 y añade un DeckPilot respuesta de identidad/versión en serie USB.
- Bundled AVRDUDE: no Arduino IDE instalación necesaria para actualizaciones normales.
- Comprobación de firmware validado y límites de memoria, conexión USB física emparejando, escribir verificación y verificación de la versión post-flash.
- Los perfiles de PC existentes y la calibración se conservan. Actualización de la aplicación nunca flashea hardware automáticamente.

**La primera prueba de hardware sigue siendo necesaria:** se confirma el procedimiento de subida Caterina del firmware original; el nuevo firmware/updater todavía debe ser probado en el macropado real. Para Pro Micro **ATmega32U4, 5 V / 16 MHz**, cableado **2–10, 14, 15, 16**, con Leonardo USB IDs y Caterina bootloader sólo. Mantenga el boceto original y el acceso RST/GND para la recuperación.

Windows 10/11 x64. Descargar **DeckPilot.exe** o **DeckPilot-win-x64.zip**. El firmware/herramientas están incrustados en la aplicación y también están disponibles en **DeckPilot-firmware.zip**; AVRDUDE tiene su propia licencia GPL y fuente de paquetes.
