> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot 2.1.4 démarre automatiquement le microphone virtuel configuré avec l'application, y compris le démarrage de l'arrière-plan après la connexion de Windows.

- Le démarrage automatique du microphone est activé par défaut pour les paramètres existants et nouveaux. Il utilise le microphone physique sélectionné et VB-CABLE; aucune activation manuelle n'est nécessaire après le redémarrage du PC DeckPilot commence par Windows.
- Si les appareils ne sont pas prêts à la connexion, ou si l'audio s'arrête après une déconnexion, DeckPilot réessayer toutes les cinq secondes pendant que l'activation automatique est activée.
- Un arrêt explicite désactive et enregistre la préférence de démarrage automatique ; activez-le à nouveau depuis le Soundboard le panneau ou le bouton d'activation. Les clips audio ne sont jamais joués automatiquement.
- Atelier restant 1.0.11 et le firmware reste 1.1.1.

La validation inclut les contrôles de migration/aller-retour de paramètres et un contrôle d'interface utilisateur Windows natif pour la préférence de démarrage automatique persistante, plus les contrôles audio et d'installation existants.
