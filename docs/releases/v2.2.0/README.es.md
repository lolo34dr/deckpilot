> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 2.2.0 añade una supresión de ruido de micrófono local ajustable mientras mantiene los clips de la placa de sonido intactos.

- RNNoise filtra el micrófono físico seleccionado antes DeckPilot mezcla en clips de mesa de sonido. Clips pasa el filtro y permanece claro en el micrófono virtual.
- Elija cualquier aplicación que use **CABLE Output** como su micrófono —juegos y aplicaciones de la cadena de voz por igual reciben la mezcla de la voz filtrada y el tablero de sonido. DeckPilotEs filtro.
- Activar o deshabilitar el filtrado y establecer su 0–100% de fuerza en **Soundboard · micro virtuel**. El procesamiento de audio permanece en este PC; no se requiere flash de firmware.
- La biblioteca nativa RNNoise está construida para Windows x64 e incrustada en DeckPilot y su instalador. Su licencia de terceros tipo BSD está incluida en el repositorio de origen.

El 10 ms RNNoise tamaño del marco no garantiza la latencia final a fin. Windows, los dispositivos de audio y VB-CABLE También afecta el retraso. Prueba la calidad del micrófono y la latencia con la configuración prevista.
