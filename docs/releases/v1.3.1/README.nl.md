>  Dit is een automatische vertaling van de volledige release notes. Het Engelse origineel is de referentie.

DeckPilot 1.3.1  verbetert de USB-verbinding en controleert macropad firmware bij het opstarten.

- Verbindingsdashboard met automatische herverbinding, gedetecteerde firmware, live-knopstatus/USB-diagnostiek en expliciete boardselectie wanneer verschillende apparaten aanwezig zijn.
- Start firmware controles en bijwerken van voorstellen, met een 24-uurherinnering vertraging en een optie om firmware notificaties uit te schakelen. Installatie nog steeds moet worden bevestigd.
-  Nieuwe firmware **1.1.0**: begrensde protocolberichten, verse sessie tokens, correlated heartbeat replies en key-state events. De F13
- Firmware manager geeft automatisch de seriële poort van de monitor vrij voordat identificatie/flitsen en opnieuw aansluiten daarna. Firmware 1.0.0 en oudere identiteitsvragen blijven ondersteund.
-  Profielen kunnen worden geselecteerd uit het tray-menu; Ctrl+S slaat wijzigingen op; verbindingsdiagnostiek kan worden gekopieerd voor ondersteuning.

Firmware updates worden geleverd in app releases; opstartcontroles vergelijken de gedetecteerde versie met de meegeleverde bundel. Geen hardware wordt geruisloos knipperd. **Firmware 1.1.0 Er is nog steeds een eerste fysieke test nodig:** firmware 1.0.0 en de eerdere flash procedure werden gevalideerd door de eigenaar, terwijl dit nieuwe protocol wordt bestreken door host tests en AVR compilatie.

 Downloaden **DeckPilot-Setup.exe** of update van de app. Profielen, kalibratie en opstartvoorkeuren blijven behouden. Afsluiten DeckPilot voordat u Arduino IDE of een andere seriële monitor gebruikt; het openen van de in-app firmware manager geeft de verbinding automatisch vrij.
