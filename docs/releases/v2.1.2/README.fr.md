> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot 2.1.2 et DeckPilot Atelier 1.0.11 fixer la sécurité de l'atelier, la fiabilité de la carte sonore et les sons de profil portables.

- Atelier peut préparer des planches Leonardo/Caterina qui ne répondent pas encore DeckPilot protocole; les défaillances série arrêtent toujours le clignotement et l'emplacement USB, le chargeur d'amorçage et la signature MCU restent vérifiés.
- Chaque inspection commence par des résultats vides. Les rapports conservent la carte et le firmware testés, et les callbacks USB inexistants ne peuvent pas affecter une nouvelle session.
- Fermeture Atelier pendant l'écriture ou la vérification du firmware est bloqué.
- Les exportations de profils et les sauvegardes de profils automatiques peuvent conserver les fichiers MP3/WAV référencés (32 Importation de clips intégrés dans un dossier géré et remap des références d'action courte/longue. Les importations de profil historique restent prises en charge; l'audio de sauvegarde manquant est signalé explicitement.
- Le soundboard publie des clips finis, met à jour les indicateurs de lecture, et des rapports interrompus des flux audio. VB-CABLE Les paramètres sont reconnus par leur identité d'adaptateur.
- Corriger les liens README traduits et conserver les noms d'appareils techniques pendant la traduction. VB-CABLE obligatoire.

La validation comprend les contrôles de régression de base, les tests de protocole firmware, les tests de traduction, la construction de Windows, DeckPilot et Vérifications d'installation/mise à niveau/désinstallation de l'atelier, et vérifications d'interface/mise à jour natives. Le comportement de déconnexion physique du premier flash et de l'audio nécessite toujours le matériel du propriétaire. 1.1.1; aucune mise à jour du firmware n'est requise.

DeckPilot 2.1.1 améliore l'installation et les mises à jour:

- Vérifier automatiquement VB-CABLE lors de la première installation et des mises à jour, et télécharger et lancer l'installateur officiel signé lorsque le pilote est absent.
- Afficher le démarrage de Windows, les vérifications automatiques de mise à jour et les préférences de réduction de mouvement pendant la configuration et les mises à jour, avec les choix existants présélectionnés et conservés.
- Conservez le microphone physique sélectionné et demandez l'installation du microphone seulement si nécessaire.
- Correction de l'initialisation et de la détection des installations existantes.

Télécharger **DeckPilot-Setup.exe** ou accepter la mise à jour dans DeckPilot. L'approbation de l'administrateur Windows est requise. Les profils existants et l'étalonnage sont conservés. DeckPilot Atelier reste à 1.0.10 et le firmware reste à 1.1.1; les deux sont inclus dans la version partagée.

Validation : contrôles de base et construction de Windows, mise à jour/désinstallation d'installateur et tests d'application native s'exécutent avant la publication de la publication.

DeckPilot 2.1.0 garde l'interface noire profonde et les couleurs personnalisables, et conduit le tableau de son à travers VB-Audio VB-CABLE automatiquement. Les utilisateurs ne sélectionnent que leur microphone physique; le paramètre de lecture virtuelle est corrigé de sorte que la voix et les clips MP3/WAV ne peuvent pas être accidentellement envoyés aux haut-parleurs. DeckPilot installateur peut télécharger le officiel VB-CABLE paquet, vérifier son éditeur Authenticode, et lancer la configuration du pilote du fournisseur; le paquet n'est pas redistribué. Dans Discord ou un jeu, sélectionnez CABLE Output comme le microphone. Atelier reste en version 1.0.10.

DeckPilot 1.9.11 et DeckPilot Atelier 1.0.10 ajouter des diagnostics de matériel et de firmware pré-flash, une comparaison de version firmware, une action de récupération guidée après des écritures interrompues, un clignotement séquentiel multi-board avec un rapport de lot, et une vérification de la mise en page physique-position-à-F13–F24 dans chaque rapport de contrôle de touche.

DeckPilot 1.9.11 inclut la même mise à jour Atelier dans la version d'installation partagée.

DeckPilot 1.9.10 restaure les versions partagées contenant les deux DeckPilot et DeckPilot Atelier 1.0.9. Les deux applications revérifient la même dernière version de GitHub.

Si vous avez installé Atelier 1.0.8 à partir du canal de libération séparé, installer DeckPilot-Atelier-Setup.exe de cette version une fois pour revenir aux mises à jour partagées. DeckPilot peut utiliser leur mise à jour existante.

DeckPilot Atelier 1.0.7 ajoute une zone de programme confirmée effacer qui efface tout 28 KiB de l'utilisateur flash, laisse une boucle de réinitialisation sécuritaire, et préserve Caterina et EEPROM. La réinitialisation reste désactivée jusqu'à ce qu'une carte physique soit sélectionnée.

DeckPilot 1.9.7 corrige Atelier désinstallant pendant que l'application est ouverte. Atelier 1.0.6 corrige également son manifeste DPI Windows, qui a empêché l'application installée de lancer.

DeckPilot 1.9.6 corrige le manifeste DPI de Windows qui a empêché DeckPilot Atelier d'ouverture après installation.

DeckPilot 1.9.5 comprend des diagnostics détaillés de démarrage d'Atelier dans la vérification de sortie de Windows.

DeckPilot 1.9.4 améliore le test de fumée de démarrage de l'Atelier Windows pour signaler les détails du plantage de l'application.

DeckPilot 1.9.3 corrige les métadonnées de l'Atelier pour lire le manifeste du firmware à partir de l'archive du firmware groupé.

DeckPilot 1.9.2 fixe l'étape de l'emballage de l'Atelier et améliore son diagnostic de construction.

DeckPilot 1.9.1 améliore le démarrage de l'installateur Atelier et ajoute des contrôles automatiques de mise à jour de l'application et du firmware.

- L'installateur lance Atelier dans la session utilisateur de Windows signée et s'arrête/restarts en toute sécurité pour les mises à niveau.
- Atelier vérifie GitHub au démarrage et à la demande. Il annonce un nouveau Atelier construit ou livré firmware, puis télécharge un installateur vérifié par rapport à la publication SHA-256 fichier.
- Le firmware n'est clignoté qu'après que l'utilisateur ait choisi l'action flash; une mise à jour de l'application n'écrit jamais à une carte connectée seule.

L'installateur Atelier lance également dans la session Windows de l'utilisateur signé après une nouvelle installation. Le flux de travail de sortie ouvre maintenant l'application installée, puis vérifie la mise à jour et désinstalle pendant qu'elle fonctionne.

DeckPilot 1.9.0 ajoute DeckPilot Atelier, une application d'atelier Windows séparée pour clignoter de nouveaux macropads et vérifier tous 12 les touches.

- Installez le firmware inclus sur les cartes Leonardo / Pro Micro ATmega32U4 avec le chargeur Caterina.
- Effectuez des vérifications guidées à courte pression et à longue pression pour chaque touche physique, enregistrez sa cartographie F13–F24 et enregistrez un rapport de contrôle de qualité.
- Télécharger **DeckPilot-Atelier-Setup.exe** de cette version. L'original DeckPilot configuration et firmware restent disponibles séparément.

DeckPilot 1.8.0 ajoute un espace de travail rafraîchi et des façons plus rapides de construire et d'utiliser vos profils.

- Interface charbon/thé, bibliothèque d'action d'abord, croustillante 3 × 4 les touches, les couleurs de l'accent touche et la préférence pour les mouvements réduits.
- Trois modèles de profil modifiables : Musique et audio, Bureau et productivité, et Création.
- Actions de profil : passer à un profil nommé, le profil suivant ou le profil précédent à partir d'une touche. Les ensembles de profil importés conservent leurs liens internes.
- Actions Windows : ouvrez les paramètres communs ou le Gestionnaire des tâches, ou verrouillez explicitement le PC.
- Copier/découper/coller/réinitialiser les touches entières, y compris les longues presses et les couleurs, avec un dédo/redo et un presse-papiers local.
- Capturer un raccourci clavier directement; Escape annule et les touches macropad restent réservées pendant la capture.
- Recherche d'action Ctrl+K et un historique clair et unique de la dernière session 30 les mesures prises.
- Correction de l'invite de firmware automatique pour l'accepter ouvre le gestionnaire de firmware. Les gestes en attente sont annulés à travers les changements de profil.

Télécharger **DeckPilot-Setup.exe** ou mise à jour depuis l'application. Les profils existants, les préférences de calibrage et de démarrage sont préservés. **1.1.1**; aucun flash n'est requis pour ces caractéristiques.

La validation inclut les contrôles de compatibilité/navigation/importation/template et l'installation native Windows, mise à jour, sélectionneur, glisser-déposer, défaire/refaire, création de modèles, actions de profil, capture de raccourcis, édition de presse-papiers, mise en page et contrôle de minimis/restauration.
