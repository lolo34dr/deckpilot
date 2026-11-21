> Dit is een automatische vertaling van de volledige release notes. Het Engelse origineel is de referentie.

DeckPilot 2.1.3 vermindert soundboard microfoon latency en verwijdert de een-seconde conversie blokken van de live audio pad.

- Gebruik output-gedreven, klein blok resampling voor de microfoon en geluidsclips; matching sample rates bypass resampling volledig.
- Verzoek gebeurtenisgestuurd 20 ms capture en 40 ms afspeelbuffers in plaats van vorige 100 ms instellingen. De werkelijke latentie hangt af van Windows, de audio-apparaten en VB-CABLE.
- Bound microfoonbuffering naar 100 ms en recente volledige frames behouden na een afspeelkraam in plaats van een oude toespraak in de wachtrij te zetten. Korte underruns voegen alleen de stilte toe die door de huidige lezing wordt gevraagd.
- Ondersteuning mono, stereo, multichannel en Windows uitbreidbare PCM/float ingangen zonder de Media Foundation een-seconde read-ahead.
- Houd Discord processing en routing ongewijzigd. Atelier blijft 1.0.11 en firmware blijft 1.1.1.

Validatie omvat regressie controles voor resampling blok groottes, kanaal conversie, uitbreidbare formaten, underrun recovery, overflow versheid, gelijktijdige opname/weergave en clip voltooiing, naast de bestaande Windows-installer en toepassing testen. Geen hardware latency cijfer wordt beweerd; controleer de verbetering met uw microfoon en audio driver.
