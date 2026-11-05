> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot 1.6.1 corrige le rendu de clavier souple et ajoute la récupération de profil, les commandes audio Windows et les presses courtes/longues.

- Texte de clavier de taille native : suppression de l'échelle de la couche entière et des effets d'ombre de contenu, sensibilisation par moniteur DPI et icônes d'application de bureau plus grandes.
- Annuler/refaire pour les modifications de profil, y compris glisser-déposer (50 modifications par session d'application); les champs de texte conservent Ctrl+Z/Ctrl+Y natif.
- Profil import/export et 20 les sauvegardes automatiques de version précédente. Importations ajouter de nouvelles copies; restaurer et défaire préserver l'étalonnage. Les actions importées ne sont jamais exécutées automatiquement.
- Global Windows microphone bascule avec un indicateur de status-bar persistant et l'état des paramètres en direct. Tous les paramètres de capture active sont mutés/non mutés ensemble.
- Basculer entre deux périphériques de sortie connectés choisis, pour tous les rôles audio par défaut de Windows. Les périphériques manquants échouent sans choisir silencieusement une autre sortie.
- Deux actions configurées par touche avec une 200–3000 Seuil ms. Courts tours à la sortie, long remplace court et court une fois. Les actions immédiates existantes et les touches F13-F24 libres gardent leur comportement. Pas de flash firmware requis.

Télécharger **DeckPilot-Setup.exe** ou mise à jour de l'application. Les profils et l'étalonnage sont préservés. Les choix de paramètres audio peuvent devoir être ajustés après l'importation de profils sur un autre PC. L'état des paramètres de Windows diffère de celui d'une application ou d'un commutateur de mute matériel; les applications à sortie fixe ne peuvent pas suivre les changements de sorties par défaut.

Validation: core profile/gesture/audio tests (simulation de paramètres audio), Windows installator/update/UI tests, native souris glisser undo/redo et l'édition de longue action.
