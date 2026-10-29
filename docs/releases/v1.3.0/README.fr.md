> Il s'agit d'une▁traduction▁automatique des notes de▁sortie▁complètes. L'original▁anglais est la▁référence.

DeckPilot 1.3.0 ▁améliore la▁connexion USB et▁vérifie le firmware macropad au▁démarrage.

- ▁Tableau de▁bord de▁connexion avec reconnexion▁automatique, micrologiciel▁détecté, diagnostic de l'état des▁boutons en direct/USB et▁sélection▁explicite du jury▁lorsque▁plusieurs▁appareils▁sont▁présents.
- ▁Vérifications et▁mise à▁jour des▁propositions du firmware de▁démarrage, avec 24-un▁délai de▁rappel d'heure et une option pour▁désactiver les notifications de firmware. L'installation a▁encore▁besoin de confirmation.
- Nouveau firmware **1.1.0**: messages de▁protocole▁délimités, jetons de session,▁réponses de▁battements▁cardiaques corrélées et▁événements à l'état▁clé. La▁cartographie F13-F24▁reste▁inchangée, y▁compris l'utilisation▁autonome.
- Le▁gestionnaire de firmware▁libère▁automatiquement le port▁série de l'écran▁avant l'identification/flashing et se reconnecte▁ensuite. 1.0.0 et les questions d'identité▁héritées▁restent▁prises en charge.
- Les▁profils▁peuvent▁être▁sélectionnés dans le menu du plateau; Ctrl+S▁enregistre les modifications; les diagnostics de▁connexion▁peuvent▁être▁copiés pour▁être▁pris en charge.

Les▁mises à▁jour firmware▁sont▁livrées dans les versions de l'application ; les▁vérifications de▁démarrage comparent la version▁détectée au▁paquet▁inclus.▁Aucun▁matériel n'est clignoté▁silencieusement. **Firmware 1.1.0 a▁besoin d'un premier test▁physique:** firmware 1.0.0 et la▁procédure flash▁antérieure▁ont▁été▁validées par le▁propriétaire,▁tandis que ce▁nouveau▁protocole est▁couvert par les tests d'hôte et la compilation AVR.

▁Télécharger **DeckPilot-Setup.exe** ou▁mise à▁jour▁depuis l'application.▁Profils, calibration et▁préférences de▁démarrage▁sont▁conservés. Quitter DeckPilot ▁avant d'utiliser Arduino IDE ou un▁autre▁moniteur▁série ; l'ouverture du▁gestionnaire de firmware in-app▁libère▁automatiquement la▁connexion.
