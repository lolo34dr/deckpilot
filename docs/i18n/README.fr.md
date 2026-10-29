> Il s'agit d'une traduction automatique du README complet. L'original anglais est la référence. Les étiquettes et les commandes de l'interface d'application sont conservées exactement.

<p align="center">
  <img src="../../deckpilotbanner.png#gh-light-mode-only" alt="Bannière DeckPilot" width="760">
  <img src="../../deckpilotlogo.png#gh-dark-mode-only" alt="Logo DeckPilot" width="112">
</p>

<h1 align="center">DeckPilot</h1>

<p align="center">
  Un compagnon Windows pour votre 12- Le bricolage de macropad.<br>
  Configurez vos touches, vos applications de lancement, vos supports de contrôle et vos profils de commutation.
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"> Télécharger la dernière version</a>
   · 
  <a href="#getting-started">Premiers pas</a>
   · 
  <a href="#development"> Développement</a>
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&color=78b7a0" alt=" Dernière sortie"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&logo=windows" alt=" Télécharger pour Windows x64"></a>
  <a href="https://github.com/lolo34dr/deckpilot/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lolo34dr/deckpilot?style=flat-square" alt="Licence MIT"></a>
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml"><img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg?branch=main" alt=" État de construction de Windows"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases"><img src="https://img.shields.io/github/downloads/lolo34dr/deckpilot/total?style=flat-square&color=70a7e8" alt=" Téléchargement de la version GitHub"></a>
</p>

<p align="center">
  <a href="../../README.md">🇬🇧 English</a> ·
  <a href="README.fr.md">🇫🇷 Français</a> ·
  <a href="README.de.md">🇩🇪 Deutsch</a> ·
  <a href="README.es.md">🇪🇸 Español</a> ·
  <a href="README.it.md">🇮🇹 Italiano</a> ·
  <a href="README.pt-BR.md">🇧🇷 Português brasileiro</a> ·
  <a href="README.nl.md">🇳🇱 Nederlands</a> ·
  <a href="README.pl.md">🇵🇱 Polski</a> ·
  <a href="README.ru.md">🇷🇺 Русский</a> ·
  <a href="README.ja.md">🇯🇵 日本語</a> ·
  <a href="README.ko.md">🇰🇷 한국어</a> ·
  <a href="README.zh-CN.md">🇨🇳 简体中文</a> ·
  <a href="README.ar.md">🇸🇦 العربية</a>
</p>

---

DeckPilot configure les macropads compatibles Arduino Leonardo qui envoient déjà **F13–F24**  comme entrées de clavier USB, disposées en **3  rangées de 4  touches**. L'ordre de la touche physique peut être arbitraire : un calibrage guidé cartographie chaque bouton à sa position.

**Aucun changement supplémentaire de pilote ou de firmware n'est nécessaire.** L'interface de l'application est actuellement en français. Choisissez une langue ci-dessus pour un README complet traduit, y compris la référence technique et l'historique de la version. Les traductions sont générées automatiquement; cet original en anglais est la référence.

<a id="features"></a>
## Caractéristiques

- **Étalonnage guidé:**  tout identifier 12 des boutons physiques sans recâbler votre macropad.
- **Sélecteur d’applications installées :**  Rechercher Démarrer les raccourcis de menu et le catalogue d'applications Windows, y compris les entrées Microsoft Store. La sélection manuelle des fichiers reste disponible.
- **Actions personnalisées  :**  lancer des applications, ouvrir des dossiers ou des sites Web, envoyer des raccourcis et contrôler les médias.
- **Touches libres:**  Gardez F13–F24 disponible pour les raccourcis configurés directement dans un jeu ou une autre application.
- **Profils locaux:**  créer, dupliquer, renommer, supprimer et sélectionner les profils. L'étalonnage est partagé entre les profils.
- **Profils prêts à l'emploi:**  Commencez par les modèles Musique, Productivité ou Création, puis personnalisez-les. Passez à un profil spécifique, suivant ou précédent à partir d'une touche physique.
- **Édition plus rapide :**  copier/découper/coller/réinitialiser les touches complètes, enregistrer les raccourcis clavier, choisir les couleurs de l'accent touche et annuler les modifications.
- **Outils Windows  :**  ouvrir le son, l'affichage, les paramètres Bluetooth et réseau, le gestionnaire des tâches ou verrouiller la session.
- **Interface noire compacte:**  icônes vectorielles, icônes d'application là où disponibles, et animations subtiles qui respectent les préférences d'animation de Windows.
- **Fonctionnement en arrière-plan:**  garder DeckPilot dans la zone de notification, le démarrage de Windows étant activé par défaut (peut être désactivé).
- **Mises à jour automatiques :**  approuver une nouvelle version, puis laisser DeckPilot  télécharger, vérifier, remplacer et redémarrer lui-même.

<a id="getting-started"></a>
## Premiers pas

<a id="requirements"></a>
### Exigences

- Windows 10 ou Windows 11, **x64**.
- Un macropad compatible Arduino Leonardo 12  touches distinctes de **F13 à F24**.
-  Une connexion de données USB.

Les téléchargements publiés incluent le .NET runtime. **Vous n'avez pas besoin d'installer .NET à utiliser DeckPilot.**

<a id="install-and-launch"></a>
### Installer et lancer

1.  Télécharger **[DeckPilot-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** et exécutez l'installateur. Windows demande la permission de l'administrateur d'installer dans **Program Files\DeckPilot**.
2. L'installateur ajoute DeckPilot  vers Windows **Installé des applications**, crée un raccourci de menu Démarrer et sélectionne le raccourci de bureau et les options de démarrage de Windows par défaut. Vous pouvez désélectionner ces tâches optionnelles.
3.  Connectez votre macropad et ouvrez DeckPilot de son raccourci.
4.  Suivre le calibrage de gauche à droite **quatre colonnes**, puis la prochaine rangée, pour **trois lignes**. Chaque bouton doit envoyer une autre touche F13–F24.

 Dans le cadre de la mise à niveau 4-la disposition des lignes, DeckPilot Préserve les profils/actions et demande un nouveau calibrage pour les positions corrigées. L'installation sur une configuration portable existante préserve également les paramètres dans `%APPDATA%\DeckPilot`; quitter l'ancienne copie si l'installation vous le demande.

Un portable [DeckPilot.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) et [ZIP](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip) Pour une utilisation portable, conservez l'exécutable dans un dossier personnel enregistrable.

 Désinstaller depuis Windows **Installé des applications**. Désinstallation supprime le programme et ses raccourcis, tout en conservant des profils personnels afin qu'une réinstallation ultérieure puisse les réutiliser.

L'exécutable à un seul fichier extrait ses dépendances groupées dans le cache .NET de l'utilisateur au premier lancement, qui peut prendre un peu plus de temps. Les exécutables publiés sont actuellement non signés, de sorte que Windows peut afficher un avertissement SmartScreen.

<a id="configure-a-key"></a>
### Configurer une touche

1.  Sélectionnez une touche dans la grille.
2.  Choisissez une action dans la barre latérale droite.
3. Configurez ses paramètres sous la grille et enregistrez. La commutation des touches ou des profils enregistre également les paramètres de la touche actuelle.

|  Décision | Comportement |
| --- | --- |
| Touche libre |  Passez F13–F24 à travers normalement. Assignez-le directement dans votre jeu ou application. |
|  Demande |  Lance une application installée ou un programme ou un raccourci sélectionné manuellement. |
|  Dossier |  Ouvre un dossier dans l'Explorateur Windows. |
| Site Web |  Ouvre une adresse HTTP ou HTTPS dans votre navigateur par défaut. |
|  Raccourci clavier |  Envoie une combinaison de touches à la fenêtre active. |
|  Médias |  Contrôle la lecture, les pistes, le volume ou la mute. |

Pour les actions de l'application, ouvrez le sélecteur d'application et recherchez par nom. Le sélecteur de fichier manuel reste disponible, avec des arguments optionnels de programme sous des options avancées. Entrez des arguments selon la syntaxe de l'application cible.

La tenue d'un bouton ne déclenche une action configurée qu'une seule fois jusqu'à la sortie. En mode libre-touche, les événements normaux de répétition du clavier restent disponibles.

<a id="manage-profiles"></a>
### Gérer les profils

 Ouvrez la section de gestion de profil dans la barre latérale droite pour créer, dupliquer, renommer ou supprimer un profil. Utilisez le sélecteur au-dessus de la grille des touches pour choisir le profil actif. Au moins un profil doit rester.

L'étalonnage s'applique à tous les profils et peut être répété à partir de la barre latérale.

<a id="background-operation"></a>
### Fonctionnement de base

 Fermeture des cache-fenêtres DeckPilot Double-cliquez sur l'icône de la zone de notification près de l'horloge pour la rouvrir, ou faites un clic droit sur l'icône et choisissez la commande de quitter pour l'arrêter complètement.

Le démarrage de Windows est activé par défaut pour l'utilisateur actuel. Vous pouvez le désactiver avec **Lancer avec Windows**; les mises à jour ultérieures de l'application préservent ce choix. L'installation et les lancements suivants rafraîchissent une entrée activée sur le chemin exécutable actuel.

<a id="installed-applications"></a>
## Applications installées

Le sélectionneur lit les raccourcis à partir des menus de l'utilisateur actuel et de l'ensemble du système, puis les complète avec `Get-StartApps`  résultats du catalogue d'applications Windows.

La recherche ignore le cas et les accents. Les noms des duplicata sont groupés, les entrées de menu Démarrer prenant la priorité. Les applications et les programmes portables sans raccourcis enregistrés peuvent encore être sélectionnés manuellement. Si le catalogue Windows n'est pas disponible, les raccourcis de menu Démarrer sont lisibles.

Windows lance les raccourcis sélectionnés. Utilisation des entrées du catalogue `shell:AppsFolder`. Les arguments optionnels s'appliquent aux programmes et raccourcis basés sur des fichiers; ils sont cachés pour les entrées AppsFolder. Listing applications ne les lance pas.

<a id="automatic-updates"></a>
## Mises à jour automatiques

 Disponible depuis **0.2.0**, les contrôles automatiques de mise à jour s'effectuent au démarrage après calibrage, puis toutes les six heures. Ils peuvent être désactivés dans la barre latérale.

 Quand une nouvelle version stable de GitHub est trouvée, DeckPilot  demande s'il faut mettre à jour. **Copies installées**  téléchargement et SHA-256  vérifier `DeckPilot-Setup.exe`, puis lancez l'installateur. Windows demande la permission de l'administrateur de mettre à jour les fichiers du programme. DeckPilot  gracieusement quand prêt, conserve les profils et les préférences de démarrage, installe la nouvelle version et relance l'application. Annuler l'invite d'autorisation Windows laisse l'application ouverte. Configuration refuse d'interrompre une session firmware ouverte.

**Copies portatives**  conserver le mécanisme de mise à jour existant. Après confirmation, il:

1. Télécharge l'exécutable avec le reporting de progression et le support d'annulation.
2. Vérifie son SHA-256 Hash contre `SHA256SUMS.txt` de la même sortie GitHub.
3. Enregistre vos paramètres et prépare l'aide à la mise à jour.
4. Quitte, remplace l'exécutable et redémarre.
5. Conserve l'exécutable précédent jusqu'à ce que le nouveau processus confirme l'initialisation réussie ; si le démarrage échoue, il restaure et relance la version précédente.

Le choix de mettre à jour plus tard reporte les invites automatiques pour 24 Les vérifications manuelles contournent ce report.

Les téléchargements utilisent HTTPS, avec des redirections limitées aux hôtes GitHub attendus. `%LOCALAPPDATA%\DeckPilot\Updates`; les téléchargements de plus de sept jours sont nettoyés. L'entrée de démarrage de Windows continue d'utiliser le même chemin exécutable après une mise à jour.

Les copies portables nécessitent un dossier personnel enregistrable. Les copies installées utilisent l'installateur pour les répertoires protégés; DeckPilot app elle-même continue à fonctionner sans droits d'administrateur.

**Mise à niveau de 0.1- Oui ?** Quitter l'ancienne version de la zone de notification et remplacer manuellement l'exécutable par la dernière version une fois. Ces anciennes versions ne contiennent pas la mise à jour. DeckPilot.

<a id="repository-rename-compatibility"></a>
### Compatibilité du répertoire

Version **1.0.1**  prépare le déménagement de `lolo34dr/streamdeck-diy` à `lolo34dr/deckpilot`. Le dépôt est maintenant `lolo34dr/deckpilot`. Les vérifications de mise à jour utilisent le nouveau nom et conservent la compatibilité avec le nom original. Les téléchargements de publication sont validés en fonction de ces deux chemins de dépôt exacts.

 Utilisation **1.0.1 ou plus tard** avec le dépôt renommé. 0.2.0 et 1.0.0  valider strictement les chemins d'actifs originaux et peut avoir besoin d'une mise à jour manuelle. Télécharger l'exécutable courant à partir de la page des versions si une version plus ancienne ne peut pas se mettre à jour.

<a id="settings-and-backups"></a>
## Paramètres et sauvegardes

 Profils et calibrage sont stockés à l'extérieur du dossier d'application:

```text
%APPDATA%\DeckPilot\settings.json
```

Quitter DeckPilot  avant de copier ce fichier pour sauvegarder ou restaurer votre configuration. Si un fichier de paramètres ne peut pas être lu, DeckPilot conserve une copie avec une `.backup-<date>`  suffixe avant d'utiliser une configuration fraîche.

 Aucun compte n'est nécessaire. La mise à jour vérifie de contacter GitHub et peut être désactivée dans l'interface.

<a id="development"></a>
## Développement

Installez le **.NET 8 SDK**, version 8.0.415 ou une version plus récente compatible autorisée par `global.json`. Studio visuel 2022 avec la charge de travail de développement de bureau .NET est optionnelle.

Exécutez ces commandes depuis la racine du dépôt :

```powershell
dotnet restore DeckPilot.sln
dotnet build DeckPilot.sln -c Release --no-restore
dotnet run --project tests/DeckPilot.Checks -c Release
dotnet run --project src/DeckPilot.Windows -c Release
```

La bibliothèque centrale et la console vérifient également sous Linux. L'interface WPF et les API d'entrée Windows nécessitent Windows.

<a id="validation"></a>
### Validation

La console vérifie la sortie avec un statut non zéro lors de la première affirmation ratée.

- Cartographie des touches, répétitions des touches retenues, sorties, changement de profil et copie d'action.
- Validation des paramètres, persistance et migration à partir de configurations plus anciennes.
- Recherche d'applications et traitement des cibles.
-  Sélection de la version, comptes de contrôle, téléchargements tronqués ou corrompus, redirections et annulation.
- Remplacement exécutable, conservation de sauvegarde et retour en arrière.

La vérification du verrouillage des fichiers ne s'exécute que sous Windows.

GitHub Actions fonctionne également `tests/windows-update-smoke.ps1`  contre l'exécutable autonome. Il démarre l'aide à la mise à jour, quitte le processus d'origine, vérifie la confirmation de remplacement et de démarrage, vérifie la conservation des paramètres, puis ouvre le sélecteur d'application installé via l'automatisation d'interface et enregistre une sélection. Il exerce également des exécutables renommés et des chemins contenant des espaces. Ce script est réservé au compte de test GitHub Actions jetable. `tests/windows-installer-smoke.ps1`: installation de vrais fichiers de programme, raccourcis de menu de bureau/démarrage, démarrage par défaut, mise à jour avec l'application ouverte, conservation désactivée et désinstallation sans suppression de profils.

<a id="publish-a-standalone-windows-folder"></a>
### Publier un dossier Windows autonome

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

Distribuez l'archive complète. Le workflow de construction de Windows produit également ce paquet.

<a id="publish-a-github-release"></a>
### Publier une version GitHub

 Mettre à jour la version de l'application et `.github/release-notes.md`, puis pousser une correspondance `v*` tag, comme `v1.0.0`. Le flux de travail de publication vérifie que la balise correspond à la version du projet et exécute la validation avant la publication:

|  Télécharger | Table des matières |
| --- | --- |
| `DeckPilot.exe` | Auto-contenu, un seul fichier application Windows x64. |
| `DeckPilot-win-x64.zip` | Remplir un dossier d'application autonome. |
| `SHA256SUMS.txt` | SHA-256 Des haches pour les deux téléchargements. |

Pour Windows ARM64, publier séparément avec `-r win-arm64` et valider sur la machine cible.

<a id="hardware-checks-on-windows"></a>
## Contrôle du matériel sous Windows

 Avec le Leonardo connecté:

-  Étalonnage tous 12 les positions dans une commande F13–F24 arbitraire. Confirmer que les touches dupliquées sont rejetées et annuler préserve la cartographie précédente.
-  Assignez une touche libre dans un jeu et vérifiez que le jeu le reçoit normalement.
-  Lancez un programme configuré, puis maintenez le bouton: il devrait lancer une fois jusqu'à la sortie. Inclure des chemins contenant des espaces et des arguments optionnels.
- Tester un dossier, un site Web, un raccourci dans une application ordinaire, et les contrôles des médias.
-  Envoyer F13–F24 par une action de raccourci et confirmer qu'il n'y a pas de boucle de DeckPilot et mesures à prendre.
-  Créer deux profils avec des actions différentes, basculer entre eux, quitter et relancer. Les profils et l'étalonnage doivent persister.
-  Fermez la fenêtre, déclenchez une action, puis rouvrez et quittez l'icône de la zone de notification. DeckPilot les sorties, toutes les touches doivent passer normalement.
- Activer le démarrage et le test de Windows après s'être connecté à nouveau; désactiver ensuite si non désiré.

<a id="current-limitations"></a>
## Limites actuelles

- Les touches F13–F24 physiques d'un autre clavier suivent la même configuration. Les événements de clavier injectés par le logiciel sont ignorés pour empêcher les boucles d'action.
-  Certains jeux, systèmes anti-chaleur ou programmes utilisant l'entrée directe d'un appareil peuvent encore recevoir des touches interceptées. Le mode touche libre reste disponible. L'écran d'enregistrement, le bureau sécurisé et d'autres sessions sont hors du contrôle de l'application.
- Windows peut bloquer les raccourcis envoyés aux applications fonctionnant avec les privilèges de l'administrateur. DeckPilot  fonctionne sans droits d'administrateur et ne contourne pas cette restriction.
- Release Ctrl, Alt, Shift et Windows avant de déclencher une action de raccourci. Les combinaisons telles que Alt+Tab restent soumises au comportement de Windows.
- Déconnecter la détection, les profils automatiques par jeu et un installateur signé sont des améliorations futures. Les profils sont actuellement sélectionnés manuellement.

<a id="project-structure"></a>
## Structure du projet

|  Voie |  Objet |
| --- | --- |
| `src/DeckPilot.Core` |  Paramètres, état de la touche, recherche, validation de la version GitHub, téléchargements vérifiés et transactions de remplacement. |
| `src/DeckPilot.Windows` | Interface WPF, catalogue d'applications, icônes, actions Windows, intégration des zones de notification et helper de mise à jour. |
| `tests/DeckPilot.Checks` |  Contrôles exécutables sans cadre d'essai externe. |
| `tests/windows-update-smoke.ps1` | Native Windows mise à jour et application-picker test de fumée utilisé par CI. |
| `deckpilotlogo.png` | Logo de l'application. |
| `deckpilotbanner.png` |  Une bannière de projet. |

<a id="license-and-credit"></a>
## Licence et crédit

DeckPilot est libéré sous la **[Licence MIT](../../LICENSE)**. Vous pouvez l'utiliser, copier, modifier et redistribuer, y compris commercialement, tant que vous conservez les avis de droits d'auteur et de licence exigés par la licence.

Si vous construisez sur DeckPilot, un petit crédit visible et un lien vers ce dépôt seraient appréciés. Ce crédit supplémentaire est facultatif; la conservation des avis de MIT est toujours nécessaire.

 Crédit suggéré :

> Sur la base [DeckPilot](https://github.com/lolo34dr/deckpilot) par [lolo34dr](https://github.com/lolo34dr).

<a id="macropad-firmware-updates"></a>
## Mises à jour du firmware Macropad

DeckPilot 1.1.0  comprend: **Firmware du macropad…** dialogue et firmware 1.0.0 pour le Pro Micro ATmega32U4 **5 V/ 16 MHz**, à l'aide de la configuration Leonardo/Caterina confirmée par le journal de téléchargement du propriétaire. L'outil de flashage Windows livré ne nécessite pas Arduino IDE. La sélection et la confirmation des appareils sont obligatoires; les mises à jour de l'application ne clignotent jamais silencieusement le panneau.

**Le propriétaire a confirmé le succès dans l'application flashing, firmware 1.0.0 identification et tous 12  boutons sur le tableau de référence.**  Gardez le croquis original et l'accès à RST/GND pour la récupération. Le clignotement du firmware ne peut pas être retourné automatiquement. Voir [firmware documentation](FIRMWARE.fr.md) pour la compatibilité, la validation, la récupération, les étapes de construction et les licences tierces. Les modes USB par bouton et les contrôles des médias natifs ne font pas partie de cette version du firmware.

<a id="building-the-windows-installer"></a>
### Construire l'installateur Windows

 Après production `artifacts/standalone/DeckPilot.exe` avec le paquet de firmware intégré, installer Inno Setup 6 et courir `./installer/build.ps1`  depuis la racine du dépôt. La sortie est `artifacts/installer/DeckPilot-Setup.exe`. CI construit et teste cet installateur avant de l'attacher à une version. L'installateur utilise un ID d'application stable afin que les versions futures mettent à niveau la même installation.

<a id="connection-dashboard-and-firmware-checks-130"></a>
## Contrôles du tableau de bord et du firmware (1.3.0)

Les **Votre macropad**  carte affiche l'état de connexion, le firmware détecté et toute mise à jour groupée disponible. **Connexion et diagnostic** expose la sélection du jury lorsque plusieurs Leonardos sont connectés, l'état de la liaison USB en direct, le nombre de boutons physiques, les préférences de firmware-notification et un résumé diagnostique copiable. Diagnostics ne copie pas les actions de profil ou les identifiants de série USB. Le moniteur se reconnecte après le débranchement et abandonne automatiquement son port série pendant que le gestionnaire de firmware est ouvert. 1.0.0 et sans lien de diagnostic.

Les vérifications du firmware de démarrage sont activées par défaut. Après la vérification de la mise à jour de l'application et l'étalonnage sont hors du chemin, un firmware identifié plus ancien peut inviter à ouvrir l'installation; report des rappels de retard pour 24 Les versions firmware sont groupées avec les mises à jour d'applications de GitHub, plutôt que récupérées à partir d'un flux de firmware indépendant.

Firmware **1.1.1**  ajoute l'identification de session, les réponses corrélées aux battements de coeur, les messages limités et les événements physiques à l'état touche. Ces événements animent les boutons mappés et mettent à jour les diagnostics; ils n'invoquent jamais les actions une seconde fois. 1.0.0 et la procédure flash ont été confirmées par le propriétaire. [ protocole et documentation de compatibilité](FIRMWARE.fr.md).

<a id="keypad-interface-140"></a>
## Interface clavier (1.4.0)

La plus grande 3 × 4  Chaque touche affiche son icône principale, son étiquette, la cartographie F13–F24 et une petite icône de type action ; les tooltips conservent l'étiquette et la position complète. Appuyez sur les commentaires, les actions en cours, l'expédition réussie et les erreurs ont des apparences distinctes, y compris des symboles plutôt que de la couleur seule.

 Choisissez une touche pour la modifier. **Options avancées · fichier et arguments**  garde les chemins d'applications manuelles et les arguments de lancement effondrés par défaut, tandis que les destinations essentielles du dossier et du site Web restent élargies. **Tester l’action**  enregistre et envoie l'action sélectionnée sur le PC en cours; un indicateur de succès confirme l'expédition, pas la réalisation dans une autre application. Les raccourcis clavier ciblent la fenêtre active. Les touches F13–F24 libres n'ont aucune action de test.

 Lorsque le macropad est déconnecté, **Hors ligne** La configuration est stockée sur le PC et reste disponible tout au long des déconnexions et des reconnects. Ces améliorations d'interface fonctionnent avec le firmware existant et ne nécessitent aucun flash supplémentaire.

Le menu zone de notification commute maintenant les profils directement. **Ctrl+S** Les fichiers de configuration non valides sont sauvegardés avant qu'une nouvelle configuration ne soit utilisée. L'installation de l'application, les icônes de menu de bureau/démarrage, les préférences de démarrage et l'actualisation automatique existante sont conservées.

Pour les téléchargements IDE manuel Arduino, **arrêter DeckPilot à partir de son menu plateau** pour libérer le port série. La fermeture de la fenêtre principale la maintient en arrière-plan.

<a id="spotify-action-search-and-drag-and-drop-150"></a>
## Spotify, recherche d'action et glisser-déposer (1.5.0)

**Spotify** est séparé de **Multimédia**. Sa lecture/pause, les commandes suivantes et précédentes adressent la session Windows de l'application de bureau Spotify, même pendant qu'un navigateur est en train de jouer sur YouTube. Spotify doit être ouvert et avoir une session de lecture disponible; démarrer une piste une fois si nécessaire. Spotify dans un navigateur n'est pas pris en charge. DeckPilot. Windows 10 version 2004 ou plus tard, ou Windows 11, est requis par cette construction.

 Utilisation **Rechercher une action ou une application** pour des questions telles que `volume`, `skip`, `Spotify` ou le nom d'une application installée. L'index de l'application installée se charge de la première recherche et est mis en cache pendant cinq minutes; la sélection manuelle des fichiers reste disponible. Cliquez sur un résultat pour configurer la touche sélectionnée ou la glisser sur une autre touche.

 Faites glisser une action à partir du catalogue ou des résultats de recherche sur une touche pour l'assigner. Faites glisser une touche configurée sur une autre pour **échange de leurs actions**; le calibrage physique reste inchangé. `.exe`, `.lnk`, `.appref-ms`, dossier ou URL HTTP(S) sur une touche. Dropping ne configure que la touche et n'exécute jamais l'action. Compléter les paramètres manquants dans l'éditeur.

Windows startup conserve maintenant une préférence explicite par utilisateur, répare une entrée d'exécution ou manquante, et signale que Windows startup connu désactiver les drapeaux. **Lancer avec Windows**  efface le connu DeckPilot  désactiver le drapeau pour l'utilisateur actuel. Les lancements d'arrière-plan restent cachés même avant l'étalonnage, et les lancements d'arrière-plan dupliqués sortent tranquillement. **Préférences de l’application**, vérifiez l'état de démarrage et réactivez l'option si Windows l'a bloquée. Une vraie connexion sur le PC de l'utilisateur reste la dernière vérification des politiques de Windows ou des restrictions spécifiques à la machine.

<a id="sharp-keypad-rendering-profile-recovery-and-audio-controls-161"></a>
## Le rendu de clavier pointu, la récupération de profil et les commandes audio (1.6.1)

Le clavier rend maintenant aux tailles WPF natives au lieu de mettre le texte à l'échelle via une Viewbox. Les effets d'ombre et les animations d'échelle de texte ont été supprimés; les fenêtres plus petites défilent plutôt que de rétrécir le clavier. L'application déclare la sensibilisation par moniteur DPI et utilise des icônes de shell Windows plus grandes pour les applications de bureau.

**Annuler / Rétablir**  restaure les modifications de profil, y compris les swaps de glisser-déposer, les paramètres d'action, les importations et la création/suppression de profil. 50 édits pendant la session de l'application. Ctrl+Z / Ctrl+Y fonctionnent sur des profils en dehors des champs de texte; les champs de texte conservent leur propre undo/redo. L'étalonnage, les préférences de démarrage et les actions exécutées ne sont pas annulés.

 Sous **Gérer les profils**, exporter le profil actuel ou tous les profils comme `.deckpilot.json`, importer des profils en tant que nouvelles copies, ou restaurer une sauvegarde de tous les profils. Importer n'exécute jamais les actions et ne remplace jamais l'étalonnage du PC. Les profils contiennent des chemins d'action, des arguments et des identifiants d'extrémité audio, qui peuvent nécessiter un ajustement sur un autre PC.

Avant d'enregistrer les profils modifiés, DeckPilot conserve les profils précédents dans `%APPDATA%\DeckPilot\Backups` (20 Les sauvegardes de préférences ne créent pas de sauvegardes de profil. Une défaillance de sauvegarde empêche d'écraser les paramètres actuels. Restaurer remplace les profils après confirmation et peut elle-même être annulée; l'étalonnage reste inchangé.

**Microphone Windows** La barre d'état persistante et la carte Audio Windows montrent l'état actif/muté/mixé/indisponible, mis à jour toutes les deux secondes et après les commandes de l'application. Une mise à jour multi-endpoint échouée tente de restaurer les états de mute précédents. Ces indicateurs décrivent l'état de fin de Windows, pas un commutateur matériel ou le bouton de mute propre d'une application.

**Changer de sortie audio**  Une cible manquante donne une erreur sans choisir un autre périphérique. Les applications avec leur propre sortie fixe peuvent ne pas suivre la valeur par défaut de Windows. Les identifiants de fin de série importés sont affichés comme indisponibles jusqu'à ce qu'ils soient adaptés au PC actuel. Les changements de routage échoués tentent de restaurer les valeurs par défaut précédentes.

Pour configurer **appui court / appui long**, d'abord choisir une action configurée, activer l'option à deux actions, sélectionner le geste à modifier et définir un seuil à partir de 200 à 3000 ms. Sans ce mode, les actions conservent le comportement immédiat de la touche vers le bas. Avec elle, les actions courtes s'exécutent à la libération avant le seuil; les actions longues s'exécutent une fois au seuil (ou à la libération si le minuteur n'a pas encore exécuté). Une action longue n'exécute jamais aussi la courte. Les touches de passage libres F13–F24 ne peuvent pas utiliser le mode de double action.

Les contrôles de base couvrent les limites de répétition/release, les cales simultanées, l'annulation, l'historique du profil, l'échange/les sauvegardes et le retour audio à l'aide de faux paramètres. L'interface utilisateur Windows vérifie l'exercice réel de drag undo/redo, configurant une longue action et un seuil, les dimensions du clavier natif après le redimensionnement et l'indicateur du microphone.

<a id="search-and-ci-reliability-fixes-164"></a>
## Corrections de la fiabilité de la recherche et de l'IC (1.6.4)

Les résultats de recherche équivalents conservent leurs boutons WPF existants. Le catalogue de l'application installée et débunce timer reporter le résultat de remplacement pendant qu'un bouton de recherche est capturé ou une traînée est en cours. Cela empêche un bouton source d'être supprimé pendant une interaction.

Le client de test utilise des coordonnées de curseur physiques pour correspondre aux limites d'automatisation de l'interface, indépendamment du DPI, se concentre avant de mesurer et scanne tous les pourcentages de défilement. Une vérification de régression vérifie également que les recherches équivalentes conservent le même bouton accessible. Les affirmations de démarrage de fond attendent le gestionnaire chargé de WPF et nécessitent cinq observations consécutives de fenêtre cachée pendant que l'application reste en vie. Les défaillances rapportent la phase, les cibles de curseur, le DPI et la géométrie du dernier défilement.

 Construire et libérer les workflows utilisent Node.js 24 actions. Le compilateur Arduino reste à CLI 1.2.2; CI télécharge son archive Windows avec un SHA-256 checksum. Le firmware et ses versions de la chaîne d'outils sont inchangés.

Les paramètres lit dans les tests Windows permettent explicitement le partage de lecture/écriture/effacement et l'analyse UTF-8  Après la fermeture du lecteur. Cela empêche le test lui-même de refuser le remplacement du fichier atomique apps. Le test d'historique maintient un ancien instantané ouvert sur trois cycles d'annulation/redo et vérifie qu'il reste intact pendant que les paramètres en direct changent.

 Sous Windows, les fichiers de paramètres existants sont engagés avec `File.Replace`  plutôt que d'écraser-mode `File.Move`. Cela prend en charge les lecteurs qui permettent la suppression tout en conservant un vieux instantané complet. La suite de base teste trois remplacements avec un lecteur tenu ouvert; Windows core vérifications s'exécutent avant la compilation du firmware et la publication de sorte que les échecs de persistance sont détectés tôt.

Un lecteur d'instantané de test peut attendre jusqu'à 500 ms pour Windows partage/verrouillage des violations pendant `ReplaceFile` commits. L'attente ne s'applique qu'à l'ouverture de l'instantané, jamais à l'exécution ou à la réexécution d'une action d'application.

<a id="runtime-and-editing-optimizations-170"></a>
## Optimisations du temps d'exécution et de l'édition (1.7.0)

 Enregistrer les paramètres identiques vérifie le fichier actuel mais saute le travail de remplacement et de sauvegarde. Les modifications externes/suppression et les écritures échouées sont toujours détectées. `ButtonAction` a l'égalité de valeur (y compris la longue action); les contrôles sales de l'éditeur évitent la sérialisation de JSON. L'histoire conserve des instantanés de modèle détachés, valide de nouveaux états et évite l'analyse de JSON pendant undo/redo. 50- limite d'édition et 20 Les sauvegardes de profil restent.

La recherche et le ramassage partagent un catalogue immuable d'applications installées pendant cinq minutes (15 Les consommateurs simultanés partagent un scan; la fermeture d'un sélectionneur n'annule pas un autre consommateur. Refresh contourne le cache, et chaque sélectionneur a son propre filtre/vue. La numérisation et l'analyse du catalogue s'effectuent hors du thread de l'interface utilisateur; le filtrage des sélectionneurs est débloqué par 120 ms. La recherche d'action revérifie ce cache après sa déconcentration et lors de son retour depuis le récupérateur, donc un catalogue rafraîchi y est également disponible.

Les mises à jour de l'interface utilisateur suivent maintenant les notifications de connexion au lieu de se réveiller chaque 100 Les fenêtres cachées/minimisées arrêtent les animations de rétroaction et utilisent un intervalle de scrutin audio de dix secondes au lieu de deux secondes; la réouverture ou l'émission d'une commande audio demande un nouvel état immédiatement. En attendant, les mises à jour audio partagent une boucle et incluent les demandes arrivant lors d'une lecture existante. Les instantanés audio non modifiés et les pinceaux gelés évitent les allocations répétées de l'interface utilisateur. Les enveloppes de processus retournées en lançant une application sont éliminées sans fermer le programme lancé.

 Ces changements réduisent le travail spécifié; ils n'établissent pas un pourcentage de CPU/RAM indépendant de la machine. La commutation audio matérielle native et le comportement de touche physique nécessitent toujours une validation sur un PC Windows avec le macropad.

<a id="workspace-tools-and-refreshed-interface-180"></a>
## Outils d'espace de travail et interface rafraîchie (1.8.0)

L'interface de charbon place la bibliothèque d'action en premier, sépare les contrôles de profil de la taille native croustillante 3 × 4  clavier, et les paramètres d'action de groupes dans une carte. Les accents touches distinguent vos propres catégories; la sélection, la focalisation du clavier et la rétroaction d'action restent séparés. **Réduire les animations** dans les préférences complète le paramètre d'animation Windows. Les petites fenêtres conservent l'accès au défilement au lieu de réduire le texte de la touche.

- **Modèles de profil :**  sous **Gérer les profils** Chaque modèle crée un profil modifiable distinct et n'exécute jamais ses actions. Les profils existants et l'étalonnage physique sont conservés. Les raccourcis clavier utilisent l'application active, dont les liaisons peuvent différer.
- **touches du profil :**  attribuer **Changer de profil**, puis sélectionnez un profil nommé ou un cycle vers l'avant/vers l'arrière. Les références entre les profils sont recaptées lors de l'importation d'un ensemble ou duplication d'un profil auto-référencé. Une destination manquante signale une erreur et peut être remplacée dans l'éditeur.
- **Commandes Windows  :** utilisation **Commandes Windows** pour ouvrir les paramètres communs ou le gestionnaire des tâches, ou verrouiller explicitement le PC. Les commandes proviennent d'une liste fixe, sans script à saisir. L'action de verrouillage nécessite de se connecter à Windows.
- **Clipboard à touches complètes:**  clic droit sur une touche pour copier, couper, coller ou réinitialiser, y compris la longue action et l'accent. Ctrl+C/X/V et Supprimer le travail alors qu'une touche de pont a une mise au point du clavier; les champs de texte ordinaires gardent leurs raccourcis d'édition. DeckPilot et est effacé à la sortie. Ces modifications supportent undo/redo.
- **Enregistreur raccourci :**  sélectionnez une action du clavier et cliquez **Enregistrer un raccourci**. Appuyez sur une combinaison prise en charge, puis enregistrez-la. Escape annule; F13–F24 est réservé au macropad pendant l'enregistrement. Les combinaisons réservées à Windows peuvent encore nécessiter les commandes manuelles.
- **Accès rapide :** Ctrl+K ou le bouton Bibliothèque focalise la recherche d'action. **Activité récente**  garde le dernier 30 les actions expédiées/écheces en mémoire pour cette session, sans stocker les arguments du programme ou les chemins cibles. Il peut être effacé à tout moment. Une expédition réussie ne garantit pas l'achèvement dans l'application cible.

Le firmware reste **1.1.1**. Accepter une offre de firmware automatique ouvre maintenant correctement le gestionnaire de firmware; le garde modal précédent a empêché que la main off. L'identité du firmware existant, battement du cœur, reconnect et les contrôles flash restent en place.

La validation couvre les paramètres hérités, la validation de l'accent, la navigation de profil et le remappage de référence, les modèles portables, le template/undo/redo et la commutation de profil natif Windows, l'enregistrement de raccourci, la copie/colle/reset, la persistance des paramètres, l'installation/mise à jour, le redimensionnement de la mise en page et une capture réelle de l'interface utilisateur.
