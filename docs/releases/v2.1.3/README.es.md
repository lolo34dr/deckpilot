> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 2.1.3 reduce la latencia del micrófono de la placa sonora y elimina los bloques de conversión de un segundo de la vía de audio en vivo.

- Utilice el muestreo de bajada de salida, pequeño bloque para el micrófono y clips de sonido; ajuste de las tasas de muestra de bypass por completo.
- Solicitud de evento 20 ms captura y 40 ms buffers de reproducción en lugar de la anterior 100 configuración ms. Latencia real depende de Windows, los dispositivos de audio y VB-CABLE.
- Bound micrófono buffering to 100 ms y conservar los marcos completos recientes después de un puesto de reproducción en lugar de colar el viejo discurso. Las subidas cortas agregan sólo el silencio solicitado por la lectura actual.
- Soporte mono, estéreo, multicanal y Windows entradas extensibles PCM/float sin la Fundación Medios un segundo lectura-ahead.
- Mantener el procesamiento de disco y el enrutamiento sin cambios. 1.0.11 y restos de firmware 1.1.1.

La validación incluye cheques de regresión para tamaños de bloques de muestreo, conversión de canales, formatos extensibles, recuperación de subida, frescura desbordamiento, captura/jugador simultáneo y finalización de clips, junto con las pruebas de instalación y aplicación de Windows existentes. No se reclama una cifra de latencia de hardware; verifique la mejora con su micrófono y controlador de audio.
