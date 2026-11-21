> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 2.1.2 y DeckPilot Atelier 1.0.11 Fija la seguridad del taller, la confiabilidad de la placa sonora y los sonidos de perfil portátil.

- Atelier puede preparar tablas Leonardo/Caterina que aún no responden a la DeckPilot protocolo; fallas en serie todavía dejan de parpadear y la ubicación USB, el cargador de arranque y la firma MCU siguen siendo verificadas.
- Cada inspección comienza con resultados vacíos. Los informes conservan la tabla probada y el firmware, y los callbacks USB estancados no pueden afectar una nueva sesión.
- Cerrar Atelier durante la escritura o verificación de firmware está bloqueado.
- Las exportaciones de perfiles y copias de seguridad de perfiles automáticos pueden retener archivos MP3/WAV de referencia (32 MiB límite de audio). Importaciones copiar clips incrustados en una carpeta gestionada y remapa referencias de acción cortas/longas. Las importaciones de perfiles de Legacy siguen soportadas; falta de audio de copia de seguridad se reporta explícitamente.
- La tabla de sonido libera clips terminados, actualiza indicadores de reproducción e informes interrumpidos flujos de audio. VB-CABLE endpoints are recognized through their adapter identity.
- Enlaces correctos traducidos README y preservar los nombres de los dispositivos técnicos durante la traducción. VB-CABLE requisito.

La validación incluye cheques de regresión núcleo, pruebas de protocolo de firmware, pruebas de traducción, Windows build, DeckPilot e instalación de Atelier / cheques de actualización / desinstalación, y comprobación de la interfaz nativa/actualización. Comportamiento físico de primera pestaña y desconexión de audio todavía requieren el hardware del propietario. 1.1.1; no se requiere actualización de firmware.
