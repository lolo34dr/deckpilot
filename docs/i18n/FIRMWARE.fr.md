> Il s'agit d'une▁traduction▁automatique de la documentation▁complète du firmware. L'original▁anglais est la▁référence. Les▁commandes et les▁identifiants▁sont▁conservés▁exactement.

<a id="deckpilot-firmware"></a>
# DeckPilot firmware

Pour la configuration▁matérielle▁testée▁uniquement: **Pro Micro ATmega32U4, 5 V/ 16 MHz**, en▁utilisant **Arduino Leonardo** ▁profil de la▁planche et un **Caterina / AVR109** bootloader. USB-C▁décrit le▁connecteur,▁pas la▁compatibilité de la▁carte. Le▁croquis original du▁propriétaire a▁été▁téléchargé avec▁succès avec le 1200- réinitialisation de baud et AVR109 à 57600 baud. Le▁propriétaire a▁également▁confirmé le▁succès dans l'application flashing, firmware 1.0.0 identification et▁fonctionnement de▁tous 12 ▁boutons sur le▁tableau de▁référence.

<a id="wiring-and-behavior"></a>
## Câblage et▁comportement

▁Chaque interrupteur▁relie une▁broche au GND. Les pull-ups▁internes▁sont▁activées. **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** ▁envoyer **F13 à F24** dans▁cet▁ordre.▁Une écurie 30 Le▁comportement des▁claviers, les▁profils▁côté Windows et le calibrage▁restent▁compatibles ; le▁renversement des▁claviers USB▁reste▁limité par la▁bibliothèque Keyboard d'Arduino. Le firmware n'introduit▁pas▁encore de modes USB par▁bouton ni de▁contrôles des▁médias natifs.

▁Épingle **1** ▁tenue à GND au▁démarrage▁désactive l'opération du▁clavier, en▁préservant la▁sécurité d'origine. Le▁noyau USB▁peut▁encore▁prendre en charge l'entrée de bootloader. Ne▁pas▁connecter VCC à GND.

L'interface▁série USB CDC▁répond `DECKPILOT?\n` à 115200 baud avec:

```
DECKPILOT|1|promicro-32u4-16mhz|1.1.1
```

▁Aucune▁connexion▁série n'est▁nécessaire pour le▁fonctionnement normal du▁clavier. L'identité est une▁vérification de▁compatibilité,▁pas une authentification▁matérielle▁cryptographique. Windows▁signale▁toujours les▁identifiants USB Arduino Leonardo;▁beaucoup d'autres▁cartes▁partagent▁ces▁identifiants.

<a id="install-from-deckpilot"></a>
## Installer▁depuis DeckPilot

1. ▁Conserver le▁croquis de▁récupération▁fourni (`firmware/Original/Original.ino`) et une configuration de▁téléchargement Arduino de travail▁comme une option de▁récupération.▁Gardez RST et GND▁physiquement accessible.
2. ▁Fermer Arduino IDE, Serial Monitor et d'autres▁outils▁série. Déconnecter d'autres▁cartes Leonardo.
3. ▁Ouvrir **Firmware du macropad…**,▁choisissez le bon▁appareil et▁confirmez le▁panneau et le▁câblage. Un▁croquis original non▁identifié▁peut▁être▁installé sur▁seulement▁après confirmation▁explicite.
4. Confirmer l'installation et▁laisser le▁câble USB▁connecté▁jusqu'à la fin. DeckPilot ▁alors que la▁boîte de dialogue du firmware est▁ouverte; les▁événements F13–F24▁ordinaires▁atteignent▁toujours Windows.

Le firmware et Windows AVRDUDE 8.0 ▁sont▁groupés à l'intérieur de l'application▁publiée.▁Aucun Arduino IDE ou l'installation▁séparée du▁pilote est▁effectuée par DeckPilot. Windows▁doit▁déjà▁exposer le port▁série USB de la▁carte,▁comme il l'a▁fait pour le▁téléchargement▁réussi du▁propriétaire. De▁nouvelles versions de firmware▁sont▁livrées avec les▁mises à▁jour de l'application de GitHub; la▁mise à▁jour de l'application ne **▁pas** ▁Chaque flash▁nécessite une confirmation.

DeckPilot ▁contrôle SHA-256, les▁comptes d'enregistrement Intel HEX, la▁présence de réinitialisateurs, les▁chevauchements et le **28672-octet** ▁limite d'application▁avant la réinitialisation. Il correspond à l'emplacement USB▁physique à travers les ports d'exécution et de▁démarrage,▁utilise AVR109 à 57600 baud,▁nécessite la▁vérification▁écrite d'AVRDUDE,▁puis▁vérifie l'identité et la version du firmware en▁cours d'exécution. `2341:8036` / `2A03:8036` et Caterina `2341:0036` / `2A03:0036` Si Windows ne▁peut▁pas▁fournir une▁connexion▁physique▁correspondante, clignote est▁bloqué▁plutôt que de▁deviner un port.

Flashing n'est▁pas▁atomique : un▁déconnexion de▁câble ou une▁perte de▁puissance▁peut▁laisser le firmware d'application▁incomplet. Il n'y a▁pas de▁sauvegarde ou de▁retour▁automatique du firmware. **Arduino Leonardo**.▁Selon le▁chargeur d'amorçage de la▁carte,▁connecter▁brièvement RST à GND▁deux▁fois▁peut▁exposer un port d'amorçage▁temporaire;▁télécharger pendant que ce port est▁disponible. Un▁chargeur d'amorçage▁endommagé▁nécessite du▁matériel ISP et ne▁peut▁pas▁être▁réparé par▁cette▁mise à▁jour USB. N'écrivez▁pas une▁autre▁variante de▁carte ou d'horloge.

Le▁dernier journal de▁processus AVRDUDE▁complété est stocké à `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`.

<a id="build"></a>
## ▁Construire

▁Sous Windows,▁depuis la▁racine du▁dépôt, installer Arduino CLI **1.2.2** et .NET SDK 8,▁puis▁lancez:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

Le script pins Arduino AVR▁noyau **1.8.6**, Clavier **1.0.6**, et AVRDUDE en▁amont **8.0**. Les▁paquets▁gérés par CLI▁utilisent la validation de l'ensemble de▁contrôle d'Arduino ; les archives▁binaires/sources AVRDUDE▁ont▁explicitement SHA-256 des pins dans le script. `artifacts/firmware/DeckPilot-firmware.zip` ▁contient l'outil HEX,▁manifeste, Windows, licence et l'archive source AVRDUDE▁correspondante en▁amont. Publishing CI▁nécessite le bundle; un▁développeur▁ordinaire sans il▁peut▁exécuter l'application▁mais ne▁peut▁pas flasher.

L'original 12- un▁croquis de▁bouton a▁été▁adapté de David Madison **Stream▁pas▁cher** ▁projet, droit d'auteur 2018, MIT. Son▁avis▁reste dans `DeckPilot.ino`. DeckPilot Les▁ajouts▁utilisent la licence MIT du▁dépôt. **AVRDUDE est un▁logiciel▁séparé▁sous▁GPL-2.0-ou plus▁tard**, avec son▁avis/licence et sa source▁groupée ; la licence MIT du▁dépôt ne▁remplace▁pas▁cette licence.▁Voir https://github.com/avrdudes/avrdude/tree/v8.0 pour les instructions de construction en▁amont et les▁avis de▁dépendance.

<a id="firmware-111-and-usb-protocol-2"></a>
## Firmware 1.1.1 et le▁protocole USB 2

Firmware 1.1.1 ▁ajoute un▁lien de diagnostic en lecture▁seule▁tout en▁conservant le▁même▁clavier F13–F24 mapping et pin-1 startup fallsafe. **Le test▁physique du▁propriétaire a▁couvert le firmware 1.0.0. Firmware 1.1.1 a des tests de▁protocole▁automatisés et une construction AVR,▁mais a▁encore▁besoin de son premier test sur le▁tableau▁réel.** Les▁profils et l'étalonnage▁restent sur le PC et n'ont▁pas▁besoin d'être recréés pour▁cette▁mise à▁jour du firmware.

La▁nouvelle application▁identifie la▁carte▁connectée,▁maintient son port▁série▁ouvert, se reconnecte▁après le débranchement, et▁vérifie sa version firmware au▁démarrage et▁après la reconnection. Les▁mises à▁jour firmware▁sont▁livrées avec les versions app : les▁vérifications de▁démarrage comparent le firmware▁connecté avec le▁paquet▁validé▁inclus dans l'application▁installée.

Un firmware plus▁ancien▁détecté invite une▁fois par session d'application, avec un 24-délai de▁rappel de l'heure en▁cas de report. Les notifications▁peuvent▁être▁désactivées **Connexion et diagnostic**. Des▁croquis Arduino▁inconnus, des▁dispositifs déconnectés et des▁sélections ambiguës de▁cartes▁multiples ne▁déclenchent▁pas▁automatiquement des▁appels clignotants ou des▁mises à▁jour. L'installation du firmware▁nécessite▁toujours une confirmation.

**Pour un▁téléchargement▁manuel à▁partir d'Arduino IDE,▁quitter DeckPilot de son▁icône de plateau d'abord.** La▁fermeture de la▁fenêtre▁principale ne la cache que, de▁sorte▁qu'elle continue à▁posséder la▁connexion▁série. DeckPilot et un▁autre▁moniteur▁série ne▁peut▁pas▁utiliser le▁même port▁simultanément. Si un▁autre▁outil▁possède▁déjà le port, l'application▁signale le▁problème et retries sans▁affecter les▁touches du▁clavier▁ordinaire.

La▁nouvelle `DeckPilotProtocol.h` est la▁même▁mise en▁œuvre▁exercée par `tests/firmware-protocol.cpp`. Il▁utilise des▁tampons de▁taille▁fixe, le▁traitement d'entrée▁limité et l'espace de transmission USB▁disponible; la▁numérisation des▁boutons n'est▁jamais▁délibérément▁retardée en attendant un▁hôte. Le▁dernier▁masque▁bouton en▁attente est▁fusionné▁sous▁contre-pression. Il n'y a▁pas de▁commandes pour▁exécuter des macros,▁écrire EEPROM,▁désactiver le▁clavier,▁modifier les▁profils ou le firmware flash sur ce▁protocole de diagnostic.

▁À 115200 baud, les▁lignes se▁terminent par LF (CRLF est▁accepté). Les jetons de session▁contiennent▁exactement▁huit▁caractères hexadécimaux▁majuscules.▁Tous les nombres▁sont décimal non▁signés.

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` ▁Annonces 12 ▁touches et▁bits de▁capacité: 1 =▁rythme▁cardiaque/instantané d'état, 2 =▁événements à l'état▁clé. `PONG` ▁fait▁écho au▁compteur de▁requête,▁puis▁rend▁compte en millisecondes,▁ordre d'état et le 12- Un▁masque à▁boutons. `KEYS` ▁rapports▁séquence et▁masque; bit 0 est F13 et bit 11 est F24,▁indépendamment de l'étalonnage▁physique. Les sessions et les▁compteurs de▁demande▁rejettent les▁réponses▁statiques. 32-bit wraparound. Les▁clés ne▁restent que des▁données de diagnostic : les actions de l'application s'exécutent▁encore une▁fois sur le▁chemin du▁clavier▁existant, de▁sorte▁qu'un▁événement▁série ne▁peut▁pas▁exécuter la▁même action▁deux▁fois.

Les▁requêtes Heartbeat▁sont▁normalement▁envoyées▁toutes les▁deux▁secondes. L'application se reconnecte si une▁réponse▁correspondante est▁manquante pour 1.5 ▁secondes; le firmware▁arrête les diagnostics non▁sollicités▁après six▁secondes sans▁battement▁cardiaque ou▁lorsque la▁connexion▁série se▁ferme. 1.0.0 ▁reste▁pris en charge par la▁requête d'identité de l'héritage, sans diagnostics▁clés en direct. DeckPilot les applications▁peuvent▁encore▁lire l'identité▁héritée du firmware 1.1.1.

La version source dans `DeckPilotProtocol.h` ▁fournit la version de▁manifeste de▁libération. Les▁paquets de firmware▁publiés▁contiennent▁maintenant▁aussi le▁croquis/en-tête▁actuel▁sous `source/DeckPilot`, le▁croquis de▁récupération et le DeckPilot Licence MIT.

Le▁lien▁diagnostique▁utilise▁délibérément `Serial.dtr()` ▁plutôt que `bool(Serial)`:▁noyau Arduino AVR 1.8.6 ▁insère a 10 ms retard dans `Serial_::operator bool()`. La lecture du DTR▁empêche▁directement le▁chemin de diagnostic d'ajouter▁cette pause à▁chaque cycle▁clé-scan. Le test du▁protocole▁hôte▁supprime la conversion▁implicite du bool de▁sorte que ce motif de▁blocage ne▁peut▁pas▁être réintroduit dans l'implémentation du▁protocole▁partagé sans▁être▁remarqué.
