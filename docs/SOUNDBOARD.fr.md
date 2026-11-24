# Soundboard et microphone

Le soundboard est optionnel : les fonctions principales du macropad marchent sans lui.

## Installation

Dans **Audio**, DeckPilot mélange des clips MP3/WAV avec le microphone choisi et envoie le mix vers **CABLE Input**. Dans un jeu ou une application vocale, sélectionne **CABLE Output** comme microphone.

Setup vérifie la présence du pilote signé VB-CABLE et installe le paquet du fournisseur s’il manque. Cela demande Internet et les droits administrateur Windows. DeckPilot vérifie l’éditeur du paquet ; le pilote n’est pas inclus dans l’application.

## Clips

Quand tu ajoutes ou remplaces un son dans **Audio**, DeckPilot en copie le fichier dans `%APPDATA%\DeckPilot\Sounds`. Le son reste donc disponible si tu déplaces ou supprimes l’original. Pour copier un ancien son, sélectionne de nouveau son fichier dans le menu **…**. Un clip peut aussi être affecté à une touche.

Les exports de profils peuvent inclure les clips utilisés, jusqu’à 32 Mio de données audio. Si un son ne tient pas dans la limite, il est signalé comme indisponible plutôt que remplacé sans prévenir.

## Filtre du microphone et retour casque

**Filtrer le bruit du microphone** applique RNNoise localement à la voix, avant le mixage des clips. Les clips ne sont pas filtrés. L’audio n’est ni enregistré ni envoyé sur Internet. Les trames de 10 ms ne garantissent pas une latence totale de 10 ms : Windows et les appareils audio ajoutent aussi leur délai.

Le retour casque optionnel diffuse les clips vers un casque ou des haut-parleurs en plus du microphone virtuel. Il ne renvoie pas le microphone physique vers les haut-parleurs. Un casque évite plus facilement que le micro ne capte les sons diffusés.

Le microphone virtuel peut démarrer avec DeckPilot si le micro choisi et VB-CABLE sont disponibles. Arrête-le explicitement pour désactiver le démarrage automatique ; tu peux le réactiver dans **Audio**.

Le traitement audio reste sur le PC et ne demande pas de mise à jour du firmware. Le soundboard utilise [NAudio sous licence MIT](../third-party/NAudio-LICENSE.txt) et [RNNoise sous une licence de type BSD](../third-party/RNNoise/NOTICE.md).
