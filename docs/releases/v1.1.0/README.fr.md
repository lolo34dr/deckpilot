> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot peut maintenant installer le firmware sur le macropad DIY Pro Micro directement depuis l'application.

- Nouveau **Firmware du macropad…** dialogue avec la sélection et la confirmation du périphérique avant de clignoter.
- Logiciels firmware groupés **1.0.0** conserve F13–F24 et ajoute une DeckPilot réponse d'identité/version sur la série USB.
- AVRDUDE groupé: pas d'installation Arduino IDE nécessaire pour les mises à jour normales.
- Validated firmware checksum et les limites de la mémoire, connexion USB physique correspondant, vérification d'écriture et de la version après flash.
- Les profils PC existants et le calibrage sont conservés. La mise à jour de l'application ne clignote jamais le matériel automatiquement.

**Premier essai matériel encore requis:** la procédure de téléchargement Caterina du firmware original est confirmée; le nouveau firmware/updater doit encore être essayé sur le macropad réel. **L'annexe I est modifiée comme suit: 5 V/ 16 MHz**, sur écoute **2–10, 14, 15, 16**, avec Leonardo USB IDs et Caterina bootloader seulement. Gardez le croquis original et l'accès RST/GND pour la récupération. Pas de sauvegarde du firmware ou de retour automatique.

Fenêtres 10/11 x64. Télécharger **DeckPilot.exe** ou **DeckPilot-win-x64.zip**. Le firmware / outils sont intégrés dans l'application et sont également disponibles dans **DeckPilot-logiciel.zip**; AVRDUDE possède sa propre licence GPL et sa source groupée.
