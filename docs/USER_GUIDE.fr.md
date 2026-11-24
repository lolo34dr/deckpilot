# Guide utilisateur

La fenêtre de DeckPilot est actuellement en français. Ce guide couvre l’usage courant ; pour le soundboard et le filtre du microphone, consulte le [guide audio](SOUNDBOARD.fr.md). La [version anglaise détaillée](USER_GUIDE.md) contient les informations de dépannage et de validation.

[Retour au README](../README.md) · [Firmware](../firmware/README.md) · [DeckPilot Atelier](FACTORY.md)

## Prérequis

- Windows 10 ou 11 en 64 bits.
- Un macropad compatible Leonardo à 12 touches qui envoie F13 à F24.
- Un câble USB qui transporte les données.

Le runtime .NET est inclus dans les téléchargements.

## Installer et démarrer

1. Télécharge **[DeckPilot-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** et lance-le. Windows demande les droits administrateur pour installer l’application dans **Program Files**.
2. Dans l’installeur, tu peux régler le démarrage avec Windows, la recherche de mises à jour et les animations réduites. Setup vérifie aussi VB-CABLE et installe son pilote signé s’il manque. Cela demande Internet et les droits administrateur.
3. Branche le macropad et ouvre DeckPilot depuis le menu Démarrer.

## Configurer les touches

1. Ouvre **Macropads**, puis choisis la carte à régler.
2. Clique sur une touche et choisis une action : application, dossier, site web, raccourci clavier ou contrôle multimédia.
3. Si nécessaire, complète la destination ou les options de l’action dans l’éditeur.
4. Si les touches affichées ne correspondent pas aux boutons physiques, lance **Calibrer les touches** depuis la carte du macropad.

Avec le firmware **1.2.0**, DeckPilot peut configurer deux cartes en même temps. Chaque carte garde son profil et son calibrage. Au premier choix d’action, DeckPilot active son mode pour la carte sélectionnée : les touches libres ne transmettent alors plus F13–F24. Repasse en mode clavier pour utiliser ces touches directement dans Windows.

## Profils et fonctionnement en arrière-plan

Les profils permettent de garder plusieurs dispositions. Tu peux en créer, dupliquer, renommer ou supprimer ; il doit en rester au moins un. Chaque macropad peut avoir son propre profil.

Fermer la fenêtre masque DeckPilot dans la zone de notification, près de l’horloge. Double-clique sur son icône pour la rouvrir. Pour arrêter complètement l’application, utilise sa commande de fermeture dans le menu de l’icône.

## Mises à jour et sauvegarde

DeckPilot vérifie les versions stables publiées sur GitHub et demande confirmation avant une mise à jour. Les téléchargements sont contrôlés avec leur somme SHA-256. L’installeur conserve les profils et les préférences.

Les profils et calibrages sont stockés dans `%APPDATA%\DeckPilot\settings.json`. Ferme DeckPilot avant de copier ce fichier. Si les paramètres sont illisibles, une copie de sauvegarde est conservée avant la création d’une configuration vierge.

## Limites connues

- L’écoute F13–F24 est globale à la session Windows ; les touches F13–F24 d’un autre clavier suivent donc la même configuration.
- Certains jeux, anti-triches et programmes qui lisent directement les périphériques peuvent recevoir les touches interceptées. Les touches libres permettent de conserver l’usage clavier normal.
- Windows peut bloquer l’envoi d’un raccourci vers une application lancée en administrateur. DeckPilot ne contourne pas cette restriction.

Pour les schémas, la récupération du firmware et le test de deux appareils, consulte la [documentation firmware](../firmware/README.md). Pour préparer une carte ou vérifier ses 12 touches, consulte le guide [Atelier](FACTORY.md).
