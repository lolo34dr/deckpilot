> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot 2.1.9 corrige la surveillance des haut-parleurs sur les appareils dont le format audio Windows est extensible, y compris certains casques USB, et ajoute une notification de superposition de tableau de son.

- Le retour de l'enceinte conserve le format exact de mélange WASAPI de la sortie sélectionnée, convertit les échantillons de clips en son encodage PCM ou flotteur, et se répète sans lecture par événement lorsqu'un pilote rejette ce mode.
- Le démarrage d'un clip de tableau de son affiche son nom dans la superposition à l'écran configurable. La préférence de notification de profil, le placement et le réglage de la vitesse réduite contrôlent les deux types de notification.
- Le retour ne contient toujours que des clips de tableau de son; le microphone physique reste hors du mix de haut-parleur et continue jusqu'au microphone virtuel.

DeckPilot Atelier reste à 1.0.12 et le firmware reste 1.1.1. Aucun flash de firmware n'est nécessaire. La compatibilité du haut-parleur doit toujours être vérifiée avec le périphérique audio réel Windows du propriétaire.
