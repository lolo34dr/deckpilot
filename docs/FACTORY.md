# DeckPilot Atelier

DeckPilot Atelier est l’application Windows de préparation et de contrôle des macropads DIY. Elle est distincte de DeckPilot, l’application utilisée au quotidien.

## Utilisation

1. Installe **DeckPilot-Atelier-Setup.exe**, puis branche un Leonardo ou Pro Micro ATmega32U4 en USB.
2. Lance **Diagnostic et comparaison du firmware** avant le flash. Atelier valide l’emplacement USB, l’image et son empreinte, identifie le bootloader et compare les versions installée et incluse. Une version plus récente déjà installée bloque le flash.
3. Dans **Installer le firmware**, lance le flash. L’outil conserve le bootloader et contrôle la version après le redémarrage. Ne débranche pas la carte pendant l’écriture. En cas d’échec pendant une écriture, utilise **Récupérer / relancer le flash…** après avoir rebranché ou sélectionné le port bootloader.
   - **Effacer le programme DeckPilot…** efface les 28 Kio de mémoire programme et laisse une instruction d’arrêt sûre après une seconde confirmation. La carte n’émet alors plus F13–F24 ; Caterina reste intact pour un futur flash. L’action ne touche ni au bootloader, ni à l’EEPROM, ni aux profils PC.
4. Ouvre **Tester les touches**, démarre la liaison USB puis suis la disposition affichée 01→12 : pour chacune, fais un appui bref puis un maintien d’au moins 700 ms. Atelier détecte les doublons et consigne les associations emplacement physique ↔ F13–F24.
5. Enregistre le rapport de contrôle avec la version testée, le résultat de chaque touche et la disposition relevée.

Pour préparer plusieurs cartes déjà branchées, **Mode atelier en série…** diagnostique et flashe chacune à tour de rôle, puis propose un rapport récapitulatif par port.

Le test lit le masque des touches via le protocole série du firmware. Il ne dépend pas de la fenêtre active et signale les appuis multiples ou les associations incohérentes. Les touches du macropad peuvent tout de même émettre leurs touches F13–F24 comme un clavier USB normal.

## Récupération

Si l’écriture a été interrompue, rebranche la carte, sélectionne le port Caterina puis lance **Récupérer / relancer le flash…**. Caterina reste intact et permet de reprogrammer l’image validée. L’application enregistre le journal AVRDUDE dans `%LOCALAPPDATA%\DeckPilotFactory\last-flash.log`.

Le programme nécessite Windows 10 ou 11 64 bits, un câble USB de données et une carte équipée du bootloader Caterina. Il ne peut pas installer un bootloader absent ou endommagé.

## Mises à jour

Atelier vérifie les nouvelles versions de l’application et du firmware inclus au démarrage, et le bouton **Vérifier les mises à jour** lance une recherche manuelle. Après confirmation, l’installeur est téléchargé puis validé avec le SHA-256 publié. Le programme se ferme proprement, s’installe dans son dossier existant et redémarre. Si une nouvelle version du firmware est incluse, elle peut ensuite être flashée depuis l’étape 1 ; l’application ne programme jamais une carte automatiquement.
