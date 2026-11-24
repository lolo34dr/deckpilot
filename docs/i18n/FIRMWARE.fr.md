<p align="center"><a href="../../firmware/README.md">English · Documentation technique complète</a></p>

# Firmware DeckPilot

Voici les informations matérielles que je peux confirmer et les grandes étapes pour configurer une ou deux cartes. La [documentation anglaise complète](../../firmware/README.md) décrit le protocole USB, la construction, les limites mémoire et la récupération détaillée.

## Carte de référence testée

J’ai vérifié le téléversement et les 12 touches sur un **Pro Micro ATmega32U4, 5 V / 16 MHz**, avec le profil **Arduino Leonardo** et le bootloader **Caterina / AVR109**. Le test physique documenté porte sur l’identification du firmware 1.0.0. Je n’ai pas validé tous les modèles compatibles Leonardo ni le fonctionnement simultané de deux cartes physiques.

Chaque contact de touche relie une broche à **GND**. Le firmware active les résistances pull-up internes.

| Broche | Touche envoyée |
| --- | --- |
| 2–10 | F13–F21, dans l’ordre |
| 14–16 | F22–F24, dans l’ordre |

La broche **1** maintenue à GND au démarrage désactive l’émission clavier et sert de sécurité. Ne relie jamais **VCC** à GND. USB-C décrit seulement la prise : cela ne suffit pas à confirmer la compatibilité d’une carte.

## Une ou deux cartes

Les deux cartes utilisent le même firmware **1.2.0**. Dans **Macropads**, clique sur chaque carte puis affecte une action pour activer le mode DeckPilot de cet appareil. Chaque macropad garde son profil et son calibrage ; la première configuration reprend le profil clavier existant, puis DeckPilot crée des profils distincts pour les autres cartes.

En mode clavier, les touches physiques envoient F13–F24. En mode DeckPilot, l’application reçoit les appuis de la carte par USB série et les touches libres ne sont plus envoyées à Windows. Si deux cartes identiques n’ont pas de numéro de série USB unique, garde-les sur leurs ports USB habituels et vérifie l’affectation après tout déplacement.

## Mise à jour et récupération

Le firmware est fourni avec les mises à jour de DeckPilot, mais l’application ne le programme pas automatiquement. Chaque flash demande de choisir la carte et de confirmer. Garde le sketch d’origine et un accès physique à **RST** et **GND** pour la récupération.

Pour flasher une nouvelle carte et contrôler ses 12 boutons, utilise [DeckPilot Atelier](../FACTORY.md). Pour les procédures détaillées de récupération, de construction et les licences, consulte la [documentation technique anglaise](../../firmware/README.md).
