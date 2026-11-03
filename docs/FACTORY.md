# DeckPilot Atelier

DeckPilot Atelier est l’application Windows de préparation et de contrôle des macropads DIY. Elle est distincte de DeckPilot, l’application utilisée au quotidien.

## Utilisation

1. Installe **DeckPilot-Atelier-Setup.exe**, puis branche un Leonardo ou Pro Micro ATmega32U4 en USB.
2. Dans **Installer le firmware**, vérifie que la carte ou son bootloader Caterina apparaît puis lance le flash. L’outil vérifie l’image, conserve le bootloader et contrôle la version après le redémarrage. Ne débranche pas la carte pendant l’écriture.
3. Ouvre **Tester les touches**, démarre la liaison USB puis suis l’ordre de la grille : pour chacune des 12 touches, fais un appui bref puis un maintien d’au moins 700 ms.
4. À la fin, enregistre le rapport de contrôle si tu veux conserver les durées et les associations entre emplacement physique et touche F13–F24.

Le test lit le masque des touches via le protocole série du firmware. Il ne dépend pas de la fenêtre active et signale les appuis multiples ou les associations incohérentes. Les touches du macropad peuvent tout de même émettre leurs touches F13–F24 comme un clavier USB normal.

## Récupération

Si l’écriture a été interrompue, rebranche la carte. Caterina reste intact et permet de relancer le flash depuis le bootloader. L’application enregistre le journal AVRDUDE dans `%LOCALAPPDATA%\DeckPilotFactory\last-flash.log`.

Le programme nécessite Windows 10 ou 11 64 bits, un câble USB de données et une carte équipée du bootloader Caterina. Il ne peut pas installer un bootloader absent ou endommagé.

## Mises à jour

Atelier vérifie les nouvelles versions de l’application et du firmware inclus au démarrage, et le bouton **Vérifier les mises à jour** lance une recherche manuelle. Après confirmation, l’installeur est téléchargé puis validé avec le SHA-256 publié. Le programme se ferme proprement, s’installe dans son dossier existant et redémarre. Si une nouvelle version du firmware est incluse, elle peut ensuite être flashée depuis l’étape 1 ; l’application ne programme jamais une carte automatiquement.
