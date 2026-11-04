> Questa è una traduzione automatica delle note di rilascio complete. L'originale inglese è il riferimento.

DeckPilot può ora installare il firmware sul macropad DIY Pro Micro direttamente dall'app.

- Nuovo **Firmware du macropad…** finestra di dialogo con la selezione del dispositivo e la conferma prima di lampeggiare.
- firmware Bundled **1.0.0** conserva F13–F24 e aggiunge un DeckPilot risposta di identità/versione su seriale USB.
- AVRDUDE Bundled: nessuna installazione Arduino IDE necessaria per aggiornamenti normali.
- Controllo firmware convalidato e limiti di memoria, corrispondenza della connessione USB fisica, verifica della scrittura e controllo della versione post-flash.
- L'aggiornamento dell'app non lampeggia automaticamente l'hardware.

**Prima prova hardware ancora richiesto:** la procedura di caricamento del firmware originale di Caterina è confermata; il nuovo firmware/updater deve ancora essere provato sul macropad effettivo. **ATmega32U4 5 V / 16 MHz**, collegato a **2–10, 14, 15, 16**, con Leonardo USB ID e Caterina bootloader solo. Mantenere lo schizzo originale e l'accesso RST/GND per il recupero. Nessun backup firmware o rollback automatico.

Windows 10/11 x64. Scarica **DeckPilot.exe** o **DeckPilot-win-x64.zip**. I firmware/tools sono incorporati nell'app e disponibili anche in **DeckPilot-firmware.zip**; AVRDUDE ha una propria licenza GPL e una fonte in bundle.
