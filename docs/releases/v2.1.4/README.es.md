> Esta es una traducción automática de las notas de liberación completas. El original inglés es la referencia.

DeckPilot 2.1.4 Comienza el micrófono virtual configurado automáticamente con la aplicación, incluyendo la startup de fondo después del registro de Windows.

- La puesta en marcha automática del micrófono de placa sonora está habilitada por defecto para configuraciones nuevas y existentes. Utiliza el micrófono físico seleccionado y VB-CABLE; no se necesita activación manual después de reiniciar el PC cuando DeckPilot comienza con Windows.
- Si los dispositivos no están listos para iniciar sesión, o el audio se detiene después de una desconexión, DeckPilot se alimenta cada cinco segundos mientras que la activación automática está activada.
- Una parada explícita deshabilita y ahorra la preferencia de arranque automático; activarla de nuevo desde el Soundboard panel o el botón de activación. Los clips de audio nunca se reproducen automáticamente.
- Atelier remains 1.0.11 y restos de firmware 1.1.1.

La validación incluye los controles de migración/ida y vuelta y un cheque UI nativo de Windows para la preferencia de arranque automático persistente, además de los cheques de audio e instalador existentes. El arranque/reconexión del dispositivo físico todavía depende del hardware del propietario.
