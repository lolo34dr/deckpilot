> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 2.2.0 añade una supresión de ruido de micrófono local ajustable mientras mantiene los clips de placa de sonido intactos.

- RNNoise filtra el micrófono físico seleccionado antes DeckPilot mezclas en clips de mesa sonora. Clips pasa el filtro y permanece claro en el micrófono virtual.
- Elija cualquier aplicación que use **CABLE Output** como su micrófono – juegos y aplicaciones de la cadena de voz por igual reciben la mezcla de la voz filtrada y el tablero de sonido. DeckPilotEs filtro.
- Activar o deshabilitar el filtrado y establecer su 0–100% de fuerza en **Soundboard · micro virtuel**. El procesamiento de audio permanece en este PC; no se requiere flash de firmware.
- La biblioteca nativa RNNoise se construye para Windows x64 y se incrusta en DeckPilot y su instalador. Su licencia de terceros tipo BSD está incluida en el repositorio fuente.

El 10 ms RNNoise tamaño del marco no garantiza la latencia de extremo a extremo. Windows, los dispositivos de audio y VB-CABLE También afecta el retraso. Prueba la calidad del micrófono y la latencia con la configuración prevista.
