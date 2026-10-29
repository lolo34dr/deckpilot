<p align="center"><img src="../../deckpilotlogo.png" alt="DeckPilot" width="96"></p>

<h1 align="center">DeckPilot</h1>

<p align="center">Il compagno Windows per il tuo macropad a 12 tasti.</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&amp;color=78b7a0" alt="Latest release"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&amp;logo=windows" alt="Download for Windows x64"></a>
  <a href="https://github.com/lolo34dr/deckpilot/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lolo34dr/deckpilot?style=flat-square" alt="MIT license"></a>
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml"><img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg?branch=main" alt="Windows build status"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases"><img src="https://img.shields.io/github/downloads/lolo34dr/deckpilot/total?style=flat-square&amp;color=70a7e8" alt="GitHub release downloads"></a>
</p>

<p align="center">
  <a href="../../README.md">🇬🇧 English</a> ·
  <a href="README.fr.md">🇫🇷 Français</a> ·
  <a href="README.de.md">🇩🇪 Deutsch</a> ·
  <a href="README.es.md">🇪🇸 Español</a> ·
  <a href="README.it.md">🇮🇹 Italiano</a> ·
  <a href="README.pt-BR.md">🇧🇷 Português brasileiro</a> ·
  <a href="README.nl.md">🇳🇱 Nederlands</a> ·
  <a href="README.pl.md">🇵🇱 Polski</a> ·
  <a href="README.ru.md">🇷🇺 Русский</a> ·
  <a href="README.ja.md">🇯🇵 日本語</a> ·
  <a href="README.ko.md">🇰🇷 한국어</a> ·
  <a href="README.zh-CN.md">🇨🇳 简体中文</a> ·
  <a href="README.ar.md">🇸🇦 العربية</a>
</p>

DeckPilot configura F13–F24 sui macropad compatibili con Arduino Leonardo, disposti su 3 righe da 4 tasti. La calibrazione guidata riconosce le posizioni anche se il cablaggio segue un ordine diverso.

## Funzionalità

- Aprire applicazioni installate, cartelle e siti web o inviare scorciatoie da tastiera.
- Controllare Spotify separatamente dai comandi multimediali Windows, disattivare i microfoni e cambiare uscita audio.
- Creare profili e usare modelli Musica, Produttività o Creazione; cambiare profilo con un tasto.
- Configurare pressione breve e lunga, scegliere colori e registrare combinazioni di tasti.
- Trascinamento, copia/taglia/incolla, annulla/ripeti, importazione/esportazione e backup automatici.

## Installazione

Servono Windows 10 versione 2004 o successiva oppure Windows 11, x64, e un cavo USB dati. Il runtime .NET è incluso.

1. Scarica DeckPilot-Setup.exe dal collegamento in alto e installalo. Windows richiede i privilegi amministrativi per Program Files. Collegamenti Desktop/Start e avvio con Windows sono proposti per impostazione predefinita.
2. Collega il macropad e calibra da sinistra a destra: quattro tasti per riga, tre righe. Ogni tasto deve inviare un F13–F24 diverso.

## Utilizzo

Seleziona un tasto, scegli un’azione dalla libreria, configura le opzioni e salva. Puoi modificare e provare azioni anche senza la scheda collegata. I tasti liberi conservano F13–F24 per giochi e altre applicazioni.

Chiudendo la finestra DeckPilot rimane attivo nell’area di notifica. Usa il menu dell’icona per uscire completamente. Spotify richiede l’app Windows aperta con una sessione di riproduzione; non controlla YouTube come alternativa.

## Aggiornamenti e firmware

Gli aggiornamenti vengono cercati all’avvio e ogni sei ore. Dopo il consenso vengono scaricati e verificati. Il programma di installazione può richiedere autorizzazione Windows. Profili e calibrazione vengono conservati.

Non servono altri driver né un nuovo firmware se il macropad invia già F13–F24. Il firmware DeckPilot compatibile aggiunge identificazione e diagnostica; consulta la guida hardware prima del flash.

## Documentazione e licenza

Questa pagina traduce la guida utente. L’interfaccia è attualmente in francese; riferimento tecnico e cronologia sono in inglese.

[English technical reference](../../README.md#development) · [Firmware & hardware](../../firmware/README.md) · [Release notes](https://github.com/lolo34dr/deckpilot/releases)

Licenza MIT: puoi utilizzare, copiare, modificare e ridistribuire il software mantenendo gli avvisi di copyright e licenza.

[MIT LICENSE](../../LICENSE)
