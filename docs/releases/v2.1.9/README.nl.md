> Dit is een automatische vertaling van de volledige release notes. Het Engelse origineel is de referentie.

DeckPilot 2.1.9 fixes speaker monitoring op apparaten waarvan Windows audio-formaat is uitbreidbaar, met inbegrip van een aantal USB-headsets, en voegt een soundboard overlay kennisgeving.

- De speaker terugkeer houdt de geselecteerde output's exacte WASAPI mix formaat, converteert clip samples naar zijn PCM of float codering, en opnieuw zonder event-driven afspelen wanneer een driver weigert die modus.
- Het starten van een soundboard clip toont zijn naam in de configureerbare on-screen overlay. De bestaande profiel notificatie voorkeur, plaatsing en reduced-motion instelling controle beide notificatie types.
- De terugkeer bevat nog steeds alleen soundboard clips; de fysieke microfoon blijft uit de speakermix en gaat verder naar de virtuele microfoon.

DeckPilot Atelier blijft op 1.0.12 en firmware blijft 1.1.1. Geen firmware flash is nodig. Speaker compatibiliteit moet nog steeds worden gecontroleerd met de eigenaar van de werkelijke Windows-audio-apparaat.
