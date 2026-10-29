> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot  peut maintenant installer le micrologiciel sur le macropad Bricolage Pro Micro directement depuis l'application.

- Nouveau **Firmware du macropad…** dialogue avec la sélection et la confirmation du périphérique avant de clignoter.
-  Logiciels firmware groupés **1.0.0** conserve F13–F24 et ajoute une DeckPilot  réponse d'identité/version sur la série USB.
- AVRDUDE groupé: pas d'installation Arduino IDE nécessaire pour les mises à jour normales.
- Validé firmware checksum et les limites de la mémoire, connexion USB physique correspondant, vérification d'écriture et la vérification de version après flash.
- Les profils PC existants et le calibrage sont préservés. La mise à jour de l'application ne clignote jamais automatiquement le matériel.

**Premier essai matériel encore requis:** la procédure de téléchargement Caterina du firmware original est confirmée; le nouveau firmware/updater doit encore être essayé sur le macropad réel. **Groupe de travail de la sécurité et de l'environnement 5 V/ 16 MHz**, sur écoute **2–10, 14, 15, 16**, avec Leonardo USB IDs et Caterina bootloader seulement. Gardez le croquis original et l'accès RST/GND pour la récupération. Pas de sauvegarde du firmware ou de retour automatique.

Windows 10/11 x64. Télécharger **DeckPilot.exe** ou **DeckPilot-win-x64.zip**. Le firmware / outils sont intégrés dans l'application et également disponibles dans **DeckPilot-logiciel.zip**; AVRDUDE possède sa propre licence GPL et sa source groupée.
