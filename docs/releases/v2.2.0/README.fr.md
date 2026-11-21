> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot 2.2.0 ajoute la suppression du bruit du microphone local réglable tout en maintenant les clips de tableau sonore intacts.

- RNNoise filtre le microphone physique sélectionné avant DeckPilot Les clips contournent le filtre et restent clairs dans le microphone virtuel.
- Choisissez une application qui utilise **CABLE Output** comme son microphone – jeux et applications de chat vocal reçoivent aussi bien la voix filtrée et le mélange de la carte son. Apps utilisant le micro physique directement contour DeckPilotLe filtre.
- Activer ou désactiver le filtrage et définir son 0–100% de résistance en **Soundboard · micro virtuel**. Le traitement audio reste sur ce PC; aucun flash firmware n'est nécessaire.
- La bibliothèque native RNNoise est construite pour Windows x64 et intégrée dans DeckPilot et son installateur. Sa licence de type BSD est incluse dans le dépôt source.

Les 10 ms RNNoise frame size ne garantit pas la latence de bout en bout. Windows, les appareils audio et VB-CABLE La qualité du microphone et la latence d'essai avec la configuration prévue.
