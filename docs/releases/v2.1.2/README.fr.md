> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot 2.1.2 et DeckPilot Atelier 1.0.11 fixer la sécurité de l'atelier, la fiabilité du tableau de bord et les sons de profil portables.

- Atelier peut préparer des planches Leonardo/Caterina qui ne répondent pas encore DeckPilot protocole; les défaillances série arrêtent toujours le clignotement et l'emplacement USB, le chargeur d'amorçage et la signature MCU restent vérifiés.
- Chaque inspection commence par des résultats vides. Les rapports conservent la carte et le firmware testés, et les callbacks USB bloqués ne peuvent pas affecter une nouvelle session.
- Fermeture Atelier pendant l'écriture ou la vérification du firmware est bloqué.
- Les exportations de profils et les sauvegardes automatiques de profils peuvent conserver les fichiers MP3/WAV référencés (32 Importation de clips intégrés dans un dossier géré et remap des références d'action courte/longue. Les importations de profil historique restent prises en charge; l'audio de sauvegarde manquant est signalé explicitement.
- Le soundboard publie des clips finis, met à jour les indicateurs de lecture, et des rapports interrompus des flux audio. VB-CABLE Les paramètres sont reconnus par leur identité d'adaptateur.
- Corriger les liens README traduits et préserver les noms d'appareils techniques pendant la traduction. VB-CABLE obligatoire.

La validation comprend les contrôles de régression de base, les tests de protocole firmware, les tests de traduction, la construction de Windows, DeckPilot et Vérifications d'installation/mise à niveau/désinstallation de l'atelier, et vérifications de l'interface/mise à jour natives. Le comportement physique de déconnexion du premier flash et de l'audio nécessite toujours le matériel du propriétaire. 1.1.1; aucune mise à jour du firmware n'est requise.
