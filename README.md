# DeckPilot

Application Windows pour configurer un clavier DIY Arduino Leonardo de **12 touches F13 à F24**, disposées en **4 lignes de 3**. Aucun pilote ni changement du programme du microcontrôleur n’est nécessaire.

## Utiliser l’application

Windows 10/11, PC x64. Le dossier publié autonome contient le runtime .NET : aucune installation de .NET n’est nécessaire pour l’utilisateur.

1. Depuis les [releases GitHub](https://github.com/lolo34dr/streamdeck-diy/releases), télécharger `DeckPilot.exe` et le placer dans un dossier permanent. Sinon, extraire entièrement `DeckPilot-win-x64.zip`, puis lancer le `DeckPilot.exe` contenu dans ce dossier.
2. Brancher le Leonardo. Au premier lancement, suivre le calibrage : appuyer sur les boutons physiques de gauche à droite, ligne par ligne. L’ordre des F13–F24 peut être quelconque, mais chaque bouton doit envoyer une touche distincte.
3. Cliquer sur une touche dans la grille, puis sur une action dans la colonne de droite. Pour **Ouvrir une application**, utiliser **Choisir une application** et chercher son nom ; **Choisir un fichier manuellement** reste disponible. Ses réglages apparaissent sous la grille ; cliquer sur **Enregistrer** après modification. Changer de touche ou de profil enregistre aussi les réglages en cours.
4. Pour un jeu, choisir **Touche libre**, puis attribuer la touche directement dans les paramètres du jeu.
5. Pour créer un profil, ouvrir **Gérer les profils** à droite, saisir son nom et cliquer sur **Créer**. La liste en haut permet de choisir le profil actif. Le calibrage est partagé entre tous les profils.

Actions disponibles : touche libre, programme avec arguments facultatifs, dossier, site HTTP(S), raccourci clavier et commandes multimédias. Un bouton maintenu déclenche une action une seule fois ; en mode libre, les répétitions du clavier restent disponibles. Les arguments sont transmis au programme choisi ; saisissez-les selon les règles de ce programme.

La croix de fermeture masque la fenêtre. Double-cliquer sur l’icône près de l’horloge pour la rouvrir, ou clic droit → **Quitter** pour arrêter réellement l’application. L’option **Lancer avec Windows** est facultative et agit seulement pour l’utilisateur Windows courant. Elle référence l’emplacement de `DeckPilot.exe` : désactiver cette option avant de déplacer le dossier, puis la réactiver.

Le calibrage et les profils sont enregistrés dans `%APPDATA%\DeckPilot\settings.json`, hors du dossier de l’application. Quitter DeckPilot avant de copier ce fichier pour le sauvegarder ou de le restaurer. Une configuration illisible est sauvegardée avec le suffixe `.backup-<date>` avant utilisation d’une nouvelle configuration. Aucun compte n’est nécessaire. La recherche de mises à jour contacte uniquement GitHub et peut être désactivée dans l’interface.

## Interface

La version 0.2.0 conserve la palette anthracite et la grille compacte, avec des pictogrammes vectoriels, les icônes des applications disponibles, des effets de survol et des transitions discrètes. Les animations respectent le réglage d’animation de Windows. Les actions sont à droite et les réglages sous la grille.

## Applications installées

Le sélecteur lit les raccourcis du menu Démarrer de l’utilisateur et du système, puis complète la liste avec `Get-StartApps` (applications Windows, dont Microsoft Store). La recherche ignore la casse et les accents. Les doublons de noms sont regroupés en privilégiant le menu Démarrer. Les applications portables ou dépourvues de raccourci peuvent être ajoutées manuellement. Une indisponibilité du catalogue Windows n’empêche pas d’utiliser les raccourcis lisibles.

Les raccourcis sont lancés par Windows ; les applications du catalogue Windows utilisent `shell:AppsFolder`. Les arguments facultatifs concernent les programmes et raccourcis choisis par fichier, et sont masqués pour les identifiants AppsFolder. La lecture de la liste ne lance aucun des programmes détectés.

## Mises à jour automatiques

À partir de **0.2.0**, DeckPilot recherche une release GitHub stable au démarrage (après le calibrage), puis toutes les six heures. Une fenêtre propose **Mettre à jour** ou **Plus tard**. Après confirmation : téléchargement avec progression et annulation possible, vérification SHA-256, enregistrement des réglages, fermeture, remplacement de l’exécutable et redémarrage. **Plus tard** reporte les invitations automatiques de 24 heures ; **Vérifier maintenant** reste disponible.

Le téléchargement est vérifié contre `SHA256SUMS.txt` de la même release GitHub, via HTTPS. Les redirections sont limitées aux hôtes GitHub prévus. L’ancien exécutable reste sauvegardé jusqu’à ce que le nouveau confirme son initialisation ; en cas d’échec de démarrage, il est restauré et relancé. Les réglages dans `%APPDATA%` ne sont pas remplacés. Les fichiers temporaires sont dans `%LOCALAPPDATA%\DeckPilot\Updates` et les téléchargements de plus de sept jours sont nettoyés.

Placez le programme dans un dossier personnel modifiable, par exemple `%LOCALAPPDATA%\Programs\DeckPilot`. Un emplacement protégé ou un verrouillage par un autre logiciel peut empêcher le remplacement ; DeckPilot n’élève pas ses droits. L’option de démarrage Windows continue à utiliser le même chemin après mise à jour.

**Depuis 0.1.x, télécharger et remplacer manuellement l’exécutable une fois pour obtenir 0.2.0.** Les versions précédentes ne contiennent pas encore ce mécanisme. Les mises à jour suivantes seront proposées par l’application.

## Développer et compiler

Installer le SDK .NET 8 (version 8.0.415 ou plus récente compatible avec `global.json`). Visual Studio 2022 avec les outils de développement Desktop .NET est facultatif. Depuis la racine du dépôt :

```powershell
dotnet restore DeckPilot.sln
dotnet build DeckPilot.sln -c Release --no-restore
dotnet run --project tests/DeckPilot.Checks -c Release
dotnet run --project src/DeckPilot.Windows -c Release
```

Les tests console retournent un code non nul dès la première assertion en échec. Ils couvrent la correspondance des touches, les répétitions et relâchements, les changements de profils, la copie des actions, la validation et la persistance. Le cœur et ces tests fonctionnent aussi sous Linux ; l’interface et les API clavier exigent Windows. Les nouvelles vérifications couvrent aussi le catalogue, la sélection des versions, les téléchargements tronqués/corrompus, les redirections, l’annulation, la migration des réglages et le retour à l’ancien exécutable. Le test de verrouillage de fichier s’exécute uniquement sous Windows.

GitHub Actions exécute également `tests/windows-update-smoke.ps1` avec l’exécutable autonome : lancement de l’assistant, fermeture du processus parent, remplacement, confirmation du démarrage, puis ouverture du sélecteur d’applications par UI Automation et sauvegarde d’un choix. Ce script est réservé au compte de test éphémère de GitHub Actions.

### Produire un dossier Windows autonome

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

Distribuer l’archive complète. Le workflow GitHub Actions compile, exécute les tests et produit aussi cette archive. Lors de la publication d’un tag `v*`, le workflow `release.yml` crée une release GitHub avec un exécutable autonome `DeckPilot.exe`, cette archive et leurs empreintes SHA-256. Le premier lancement de la version en fichier unique extrait ses dépendances dans le cache .NET de l’utilisateur ; il peut prendre un peu plus de temps. Les exécutables ne sont pas signés : Windows peut afficher un avertissement SmartScreen. Pour Windows ARM64, publier séparément avec `-r win-arm64` et valider sur la machine cible.

## Vérifications à faire avec le Leonardo sous Windows

- Calibrer les 12 positions dans un ordre F13–F24 aléatoire ; vérifier qu’une touche déjà utilisée est refusée et qu’**Annuler** conserve le précédent calibrage.
- Attribuer une touche libre dans un jeu : elle doit être reçue normalement.
- Configurer un programme, le lancer avec le bouton, puis maintenir le bouton : un seul lancement jusqu’au relâchement. Tester les chemins avec espaces et les arguments.
- Tester un dossier, un site, un raccourci dans une application ordinaire et les commandes multimédias.
- Envoyer F13–F24 via une action raccourci : aucune boucle d’actions DeckPilot ne doit se produire.
- Créer deux profils avec des actions différentes, changer de profil, quitter et relancer : profils et calibrage doivent être conservés.
- Fermer la fenêtre, déclencher une action, puis rouvrir et quitter depuis l’icône. Une fois l’application quittée, toutes les touches redeviennent libres.
- Activer le démarrage Windows et tester après reconnexion à la session ; désactiver ensuite si ce n’est pas souhaité.

## Limites de cette première version

- L’écoute est globale à la session Windows. Les F13–F24 provenant d’un autre clavier physique suivent les mêmes règles. Les événements clavier injectés par logiciel sont ignorés par DeckPilot pour éviter les boucles.
- Certains jeux, systèmes anti-triche ou logiciels qui lisent directement les périphériques peuvent recevoir les touches même si l’application les intercepte. Le mode libre reste disponible. L’écran de connexion, le bureau sécurisé et les autres sessions ne sont pas contrôlés.
- Windows peut empêcher un raccourci envoyé vers un programme lancé comme administrateur. L’application fonctionne sans droits administrateur ; elle ne contourne pas cette restriction.
- Relâcher Ctrl, Alt, Maj et Windows avant de déclencher une action raccourci. Un raccourci comme Alt+Tab reste dépendant des règles de Windows.
- La détection du débranchement, les profils automatiques par jeu et un installeur signé sont des évolutions futures. Les profils actuels sont choisis manuellement.

## Organisation

- `src/DeckPilot.Core` : configuration, état des touches, recherche, contrat GitHub, téléchargement vérifié et transaction de remplacement.
- `src/DeckPilot.Windows` : interface WPF, catalogue Windows, icônes, actions, zone de notification et assistant de mise à jour.
- `tests/DeckPilot.Checks` : vérifications exécutables sans framework de test externe.
