> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot 2.1.3 réduit la latence du microphone de la carte son et supprime les blocs de conversion d'une seconde du chemin audio en direct.

- Utilisez le rééchantillonnage à petite échelle par sortie pour le microphone et les clips sonores; les taux d'échantillonnage correspondants contournent entièrement le rééchantillonnage.
- Demander un événement 20 ms capture et 40 ms buffers de lecture au lieu de la précédente 100 paramètres ms. La latence réelle dépend de Windows, les appareils audio et VB-CABLE.
- Boufferon de microphone pour 100 ms et conserver des images complètes récentes après un décrochage de lecture au lieu de faire la file d'attente d'un vieux discours.
- Prise en charge des entrées PCM/float mono, stéréo, multicanaux et Windows extensibles sans la Fondation des médias une seconde de lecture à l'avance.
- Maintenir le traitement et le routage des discords inchangés. 1.0.11 et le firmware reste 1.1.1.

La validation comprend des contrôles de régression pour le rééchantillonnage de la taille des blocs, la conversion des canaux, les formats extensibles, la récupération d'underrun, la fraîcheur de débordement, la capture/playback simultanée et l'achèvement des clips, ainsi que les tests d'installation et d'application Windows existants.
