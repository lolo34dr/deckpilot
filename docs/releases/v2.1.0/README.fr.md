> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot 2.1.0 conserve l'interface noire profonde et les couleurs personnalisables, et conduit le tableau de son à travers VB-Audio VB-CABLE automatiquement. Les utilisateurs ne sélectionnent que leur microphone physique; le paramètre de lecture virtuelle est corrigé de sorte que voix et clips MP3/WAV ne peuvent pas être accidentellement envoyés aux haut-parleurs. DeckPilot installateur peut télécharger le paquet VB-CABLE officiel, vérifier son éditeur Authenticode, et lancer la configuration du pilote du fournisseur; le paquet n'est pas redistribué. Dans Discord ou un jeu, sélectionnez CABLE Output comme microphone. Atelier reste à la version 1.0.10.

DeckPilot 1.9.11 et DeckPilot Atelier 1.0.10 ajouter des diagnostics de matériel et de firmware pré-flash, une comparaison de version firmware, une action de récupération guidée après des écritures interrompues, un clignotement séquentiel multi-boards avec un rapport de lot, et une vérification de la mise en page physique-position-à-F13–F24 dans chaque rapport de contrôle de touche.

DeckPilot 1.9.11 inclut la même mise à jour Atelier dans la version partagée de l'installateur.

DeckPilot 1.9.10 restaure les versions partagées contenant les deux DeckPilot et DeckPilot Atelier 1.0.9. Les deux applications revérifient la même dernière version de GitHub.

Si vous avez installé Atelier 1.0.8 à partir du canal de libération séparé, installer DeckPilot-Atelier-Setup.exe de cette version une fois pour revenir aux mises à jour partagées. DeckPilot peut utiliser leur mise à jour existante.

DeckPilot Atelier 1.0.7 ajoute une zone de programme confirmée effacer qui efface tout 28 KiB de l'utilisateur flash, laisse une boucle de réinitialisation sécuritaire, et préserve Caterina et EEPROM. La réinitialisation reste désactivée jusqu'à ce qu'une carte physique soit sélectionnée.

DeckPilot 1.9.7 corrige Atelier désinstallant pendant que l'application est ouverte. Atelier 1.0.6 corrige également son manifeste DPI Windows, qui a empêché l'application installée de lancer.

DeckPilot 1.9.6 corrige le manifeste DPI de Windows qui a empêché DeckPilot Atelier d'ouverture après installation.

DeckPilot 1.9.5 comprend des diagnostics de démarrage d'Atelier détaillés dans la vérification de sortie de Windows.

DeckPilot 1.9.4 améliore le test de fumée de démarrage de l'Atelier Windows pour signaler les détails du plantage de l'application.

DeckPilot 1.9.3 corrige les métadonnées de l'Atelier pour lire le manifeste du firmware à partir de l'archive du firmware groupé.

DeckPilot 1.9.2 corrige l'étape d'emballage de l'Atelier et améliore son diagnostic de construction.

DeckPilot 1.9.1 améliore le démarrage de l'installateur Atelier et ajoute des contrôles automatiques de mise à jour de l'application et du firmware.

- L'installateur lance Atelier dans la session utilisateur de Windows signée et s'arrête/restarts en toute sécurité pour les mises à niveau.
- Atelier vérifie GitHub sur le démarrage et à la demande. Il annonce nouveau Atelier construit ou firmware groupé, puis télécharge un installateur vérifié par le SHA publié-256 fichier.
- Le firmware n'est clignoté qu'après que l'utilisateur ait choisi l'action flash; une mise à jour de l'application n'écrit jamais à une carte connectée seule.

L'installateur Atelier lance également la session Windows de l'utilisateur signé après une nouvelle installation. Le flux de travail de sortie ouvre maintenant l'application installée, puis vérifie la mise à niveau et désinstalle pendant qu'elle est en cours d'exécution.

DeckPilot 1.9.0 ajoute DeckPilot Atelier, une application d'atelier Windows séparée pour clignoter de nouveaux macropads et vérifier tous 12 les touches.

- Installez le firmware inclus sur les cartes Leonardo / Pro Micro ATmega32U4 avec le chargeur d'amorçage Caterina.
- Effectuez des vérifications guidées à courte pression et à longue pression pour chaque touche physique, enregistrez sa cartographie F13–F24 et enregistrez un rapport de contrôle de la qualité.
- Télécharger **DeckPilot- Atelier-Setup.exe** de cette version. L'original DeckPilot configuration et firmware restent disponibles séparément.

DeckPilot 1.8.0 ajoute un espace de travail rafraîchi et des façons plus rapides de construire et d'utiliser vos profils.

- Interface charbon/thé, bibliothèque d'action d'abord, croustillante 3 × 4 les touches, les couleurs de l'accent touche et la préférence pour les mouvements réduits.
- Trois modèles de profil modifiables : Musique et audio, Bureau et productivité, et Création.
- Actions du profil : passez à un profil nommé, le profil suivant ou le profil précédent à partir d'une touche. Les ensembles de profils importés conservent leurs liens internes.
- Actions de Windows : ouvrez les paramètres communs ou le Gestionnaire des tâches, ou verrouillez explicitement le PC.
- Copier/découper/coller/réinitialiser les touches entières, y compris les longues presses et les couleurs, avec un dédo/redo et un presse-papiers local.
- Capturer un raccourci clavier directement; Escape annule et les touches macropad restent réservées pendant la capture.
- Ctrl+K recherche d'action et un historique clair, session seule de la dernière 30 et mesures à prendre.
- Correction de l'invite de firmware automatique pour l'accepter ouvre le gestionnaire de firmware. Les gestes en attente sont annulés à travers les changements de profil.

Télécharger **DeckPilot-Setup.exe** ou mise à jour à partir de l'application. Les profils existants, les préférences de calibrage et de démarrage sont préservés. **1.1.1**; aucun flash n'est requis pour ces caractéristiques.

La validation inclut la compatibilité de base/navigation/importation/template checks et l'installation native de Windows, mise à jour, sélectionneur, glisser-déposer, défaire/refaire, création de modèles, actions de profil, capture de raccourcis, édition de presse, mise en page et de minimiser/restaurer les vérifications.
