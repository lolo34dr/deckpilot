> Questa è una traduzione automatica delle note di rilascio complete. L'originale inglese è il riferimento.

DeckPilot 2.1.2 e DeckPilot Atelier 1.0.11 fissare la sicurezza dell'officina, l'affidabilità del suono e i suoni del profilo portatile.

- Atelier può preparare tavole Leonardo/Caterina che non rispondono ancora DeckPilot protocollo; i guasti seriali fermano ancora il flashing e la posizione USB, il bootloader e la firma MCU rimangono verificati.
- Ogni ispezione inizia con i risultati vuoti. I report mantengono la scheda e il firmware testati, e i callback USB stanti non possono influenzare una nuova sessione.
- Chiusura Atelier durante la scrittura del firmware o la verifica è bloccata.
- Le esportazioni dei profili e i backup automatici dei profili possono conservare i file MP3/WAV di riferimento (32 MiB audio limit). Importa copia clip incorporati in una cartella gestita e rimostra riferimenti di azione brevi / lunghi. Le importazioni del profilo Legacy rimangono supportate; l'audio di backup mancante viene segnalato esplicitamente.
- La scheda audio rilascia clip finiti, aggiorna gli indicatori di riproduzione e segnala i flussi audio interrotti. VB-CABLE gli endpoint sono riconosciuti attraverso la loro identità di adattatore.
- Corretto tradotto link README e conservare i nomi dei dispositivi tecnici durante la traduzione. VB-CABLE requisito.

La convalida include i controlli di regressione del nucleo, i test del protocollo del firmware, i test di traduzione, la costruzione di Windows, DeckPilot e Atelier installazione / upgrade / disinstallazione controlli, e nativo interfaccia / aggiornamento controlli. Il primo-flash fisico e comportamento di disconnessione audio richiedono ancora l'hardware del proprietario. 1.1.1; non è necessario alcun aggiornamento del firmware.
