> Questa è una traduzione automatica delle note di rilascio complete. L'originale inglese è il riferimento.

DeckPilot 2.2.0 aggiunge la soppressione del rumore del microfono locale, mantenendo le clip della scheda audio intatte.

- RNNoise filtra il microfono fisico selezionato prima DeckPilot Le clip bypassano il filtro e rimangono chiare nel microfono virtuale.
- Scegli qualsiasi applicazione che utilizzi **CABLE Output** come il suo microfono—giochi e applicazioni voce-chat allo stesso modo ricevere la voce filtrata e mix soundboard. Apps utilizzando il microfono fisico bypass direttamente DeckPilotIl filtro.
- Abilitare o disabilitare il filtraggio e impostare la sua 0–100% di forza **Soundboard · micro virtuel**. L'elaborazione audio rimane su questo PC; non è necessario alcun flash firmware.
- La libreria nativa RNNoise è costruita per Windows x64 e incorporata in DeckPilot e il suo installatore. La sua licenza di terze parti in stile BSD è inclusa nel repository sorgente.

The 10 ms RNNoise frame size non garantisce la latenza end-to-end. VB-CABLE Anche influisce sul ritardo. Verificare la qualità del microfono e la latenza con la configurazione prevista.
