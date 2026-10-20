# DeckPilot

Application Windows pour configurer un clavier DIY Arduino Leonardo de **12 touches F13 à F24**, disposées en **4 lignes de 3**. Aucun pilote ni changement du programme du microcontrôleur n’est nécessaire.

## Utiliser l’application

Windows 10/11, PC x64. Le dossier publié autonome contient le runtime .NET : aucune installation de .NET n’est nécessaire pour l’utilisateur.

1. Depuis les [releases GitHub](https://github.com/lolo34dr/streamdeck-diy/releases), télécharger `DeckPilot.exe` et le placer dans un dossier permanent. Sinon, extraire entièrement `DeckPilot-win-x64.zip`, puis lancer le `DeckPilot.exe` contenu dans ce dossier.
2. Brancher le Leonardo. Au premier lancement, suivre le calibrage : appuyer sur les boutons physiques de gauche à droite, ligne par ligne. L’ordre des F13–F24 peut être quelconque, mais chaque bouton doit envoyer une touche distincte.
3. Cliquer sur une touche dans la grille, puis sur une action dans la colonne de droite. Ses réglages apparaissent sous la grille ; cliquer sur **Enregistrer** après modification. Changer de touche ou de profil enregistre aussi les réglages en cours.
4. Pour un jeu, choisir **Touche libre**, puis attribuer la touche directement dans les paramètres du jeu.
5. Pour créer un profil, ouvrir **Gérer les profils** à droite, saisir son nom et cliquer sur **Créer**. La liste en haut permet de choisir le profil actif. Le calibrage est partagé entre tous les profils.

Actions disponibles : touche libre, programme avec arguments facultatifs, dossier, site HTTP(S), raccourci clavier et commandes multimédias. Un bouton maintenu déclenche une action une seule fois ; en mode libre, les répétitions du clavier restent disponibles. Les arguments sont transmis au programme choisi ; saisissez-les selon les règles de ce programme.

La croix de fermeture masque la fenêtre. Double-cliquer sur l’icône près de l’horloge pour la rouvrir, ou clic droit → **Quitter** pour arrêter réellement l’application. L’option **Lancer avec Windows** est facultative et agit seulement pour l’utilisateur Windows courant. Elle référence l’emplacement de `DeckPilot.exe` : désactiver cette option avant de déplacer le dossier, puis la réactiver.

Le calibrage et les profils sont enregistrés dans `%APPDATA%\DeckPilot\settings.json`, hors du dossier de l’application. Quitter DeckPilot avant de copier ce fichier pour le sauvegarder ou de le restaurer. Une configuration illisible est sauvegardée avec le suffixe `.backup-<date>` avant utilisation d’une nouvelle configuration. Aucun compte, service réseau ou système de mise à jour automatique dans cette version.

## Interface

La version 0.1.1 utilise un fond anthracite, des touches compactes avec pictogrammes, une liste d’actions à droite et un éditeur sous la grille. La sélection est signalée par une bordure vert discret. Les profils et le calibrage de la version précédente sont conservés.

## Développer et compiler

Installer le SDK .NET 8 (version 8.0.415 ou plus récente compatible avec `global.json`). Visual Studio 2022 avec les outils de développement Desktop .NET est facultatif. Depuis la racine du dépôt :

```powershell
dotnet restore DeckPilot.sln
dotnet build DeckPilot.sln -c Release --no-restore
dotnet run --project tests/DeckPilot.Checks -c Release
dotnet run --project src/DeckPilot.Windows -c Release
```

Les tests console retournent un code non nul dès la première assertion en échec. Ils couvrent la correspondance des touches, les répétitions et relâchements, les changements de profils, la copie des actions, la validation et la persistance. Le cœur et ces tests fonctionnent aussi sous Linux ; l’interface et les API clavier exigent Windows.

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
- La détection du débranchement, les profils automatiques par jeu, un installeur signé et les mises à jour sont des évolutions futures. Les profils actuels sont choisis manuellement.

## Organisation

- `src/DeckPilot.Core` : configuration, validation, persistance JSON et état des touches.
- `src/DeckPilot.Windows` : interface WPF, écoute clavier, actions Windows et zone de notification.
- `tests/DeckPilot.Checks` : vérifications exécutables sans framework de test externe.
