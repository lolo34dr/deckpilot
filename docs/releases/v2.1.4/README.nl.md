> Dit is een automatische vertaling van de volledige release notes. Het Engelse origineel is de referentie.

DeckPilot 2.1.4 start de geconfigureerde virtuele microfoon automatisch met de app, inclusief achtergrond opstarten na Windows-aanmelding.

- Automatische soundboard microfoon opstarten is standaard ingeschakeld voor bestaande en nieuwe instellingen. Het maakt gebruik van de geselecteerde fysieke microfoon en VB-CABLE; geen handmatige activering is nodig na het herstarten van de PC wanneer DeckPilot begint met Windows.
- Als de apparaten niet klaar zijn bij het aanmelden, of als de audio stopt na het verbreken van de verbinding, DeckPilot herstart elke vijf seconden terwijl automatische activering is ingeschakeld.
- Een expliciete stop schakelt uit en slaat de automatische start-voorkeur op; activeer het opnieuw van de Soundboard paneel of de activeringsknop. Audioclips worden nooit automatisch afgespeeld.
- Atelier blijft 1.0.11 en firmware blijft 1.1.1.

Validatie omvat instellingen migratie / round-trip controles en een native Windows UI check voor de aanhoudende automatische start voorkeur, plus de bestaande audio en installateur controles. Fysiek apparaat opstarten / opnieuw aansluiten is nog steeds afhankelijk van de hardware van de eigenaar.
