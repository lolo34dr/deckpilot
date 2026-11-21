> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 2.1.6 y DeckPilot Atelier 1.0.12 hacer que el firmware de Atelier borra explícito y recuperable.

- Mover la era destructiva del firmware en operaciones avanzadas desplomadas y requerir la escritura `EFFACER` antes de escribir.
- Explique que el tablero deja de enumerar como un teclado después de la borrada, muestre los pasos exactos de recuperación RST/GND de doble reajuste, y mantenga la acción de recuperación de Atelier disponible después de la borrada o un flash interrumpido.
- Firmware sigue siendo 1.1.1; no DeckPilot Se requiere un cambio de firmware de hardware.

La validación incluye controles de firmware y aplicaciones, además de la automatización de Windows UI para el control de borrado avanzado, seguridad de no dispositivos y guía de recuperación RST/GND. El reseteo en sí mismo todavía requiere la junta física.
