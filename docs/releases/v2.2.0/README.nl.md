> Dit is een automatische vertaling van de volledige release notes. Het Engelse origineel is de referentie.

DeckPilot 2.2.0 voegt verstelbare, lokale microfoon lawaai onderdrukking terwijl het houden van soundboard clips onaangeroerd.

- RNNoise filtert de geselecteerde fysieke microfoon voor DeckPilot mixt in soundboard clips. Clips omzeilen het filter en blijven duidelijk in de virtuele microfoon.
- Kies een toepassing die gebruikt **CABLE Output** als de microfoon-games en spraak-chat toepassingen ontvangen zowel de gefilterde stem en soundboard mix. Apps met behulp van de fysieke microfoon direct bypass DeckPilotHet filter.
- Filteren inschakelen of uitschakelen en de instelling ervan instellen 0–100% sterkte in **Soundboard · micro virtuel**. Audio verwerking blijft op deze PC; geen firmware flits is vereist.
- De RNNoise native bibliotheek is gebouwd voor Windows x64 en ingebed in DeckPilot en zijn installatie. De BSD-stijl licentie van derden is opgenomen in de bron repository.

De 10 ms RNNoise frame grootte garandeert geen end-to-end latency. Windows, de audio-apparaten en VB-CABLE ook invloed op vertraging. Test microfoonkwaliteit en latentie met de beoogde setup.
