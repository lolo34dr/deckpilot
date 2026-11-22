> Dit is een automatische vertaling van de volledige release notes. Het Engelse origineel is de referentie.

DeckPilot 2.1.5 glad maakt audio-apparaat verfrist en voegt een branded opstartscherm.

- Ontdek fysieke microfoons en VB-CABLE van de UI thread. Automatische herhalingen delen een recente apparaat snapshot; de handmatige verversen knop voert altijd een nieuwe scan uit.
- Verminder herhaalde Windows audio-state polling en stroomlijn de gemeenschappelijke mono-microfoon conversie pad.
- Toon het DeckPilot banner terwijl de interactieve app begint. Stille opstart met Windows blijft op de achtergrond.
- Atelier blijft 1.0.11 en firmware blijft 1.1.1.

Validatie omvat achtergrondscan / cache regressie controles, audio conversie controles, Windows builds, installer upgrade / uninstall, native splash / update en applicatie-picker controles. Werkelijke audio CPU gebruik en latency nog steeds afhankelijk van de PC en audio-apparaten.
