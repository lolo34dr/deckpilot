> Questa è una traduzione automatica delle note di rilascio complete. L'originale inglese è il riferimento.

DeckPilot 2.1.3 riduce la latenza del microfono della scheda audio e rimuove i blocchi di conversione di un secondo dal percorso audio dal vivo.

- Utilizzare l'output-driven, piccolo-blocco di risampling per il microfono e clip audio; i tassi di campionamento corrispondenti bypassa completamente il campionamento.
- Richiedi un evento guidato 20 ms cattura e 40 ms buffer di riproduzione invece del precedente 100 impostazioni ms. Latenza effettiva dipende da Windows, i dispositivi audio e VB-CABLE.
- Bound microfono buffering a 100 ms e conservare i recenti frame completi dopo uno stallo di riproduzione invece di mettere in coda il vecchio discorso.
- Supporta ingressi PCM/float mono, stereo, multicanale e Windows estensibili senza la Media Foundation un secondo di lettura.
- Mantenere l'elaborazione Discord e il routing invariato. Atelier rimane 1.0.11 e il firmware rimane 1.1.1.

La validazione include controlli di regressione per il campionamento di dimensioni, la conversione dei canali, i formati estensibili, il recupero in esecuzione, la freschezza di troppopieno, la cattura / il completamento concomitante di acquisizione e clip, accanto ai test di installazione e applicazione Windows esistenti.
