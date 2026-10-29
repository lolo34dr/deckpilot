> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot 1.6.4  corrige le remplacement des résultats de recherche pendant le glisser-déposer et stabilise la vérification de Windows.

-  Gardez les boutons de recherche existants lorsque le calendrier de débonflage ou le catalogue d'applications installées produit les mêmes résultats, de sorte qu'une source de glisser n'est pas détachée de façon inattendue.
- Reportez le remplacement de recherche-résultats pendant qu'une interaction de souris ou OLE drag est en cours.
- Les vérifications de l'interface utilisateur Windows utilisent les coordonnées du curseur physique, les commandes de focus avant de les mesurer, et scannent la gamme complète de défilement au lieu de supposer un nombre fixe d'accroissements de ligne.
- Les paramètres de Windows sauvent utilisent l'API de remplacement atomique dédiée afin qu'un lecteur existant puisse conserver son ancien instantané sans bloquer la prochaine sauvegarde. Une régression de base vérifie trois remplacements successifs avec un lecteur partagé tenu ouvert.
-  Paramètres lecteurs dans les tests Windows utiliser UTF-8 et permettre le remplacement de fichiers atomiques. 500 ms seulement pour Windows partage/verrouillage des violations pendant le remplacement; d'autres erreurs de lecture et des verrous persistants échouent le test. Trois cycles de désactivation/redo gardent également un ancien instantané ouvert pour vérifier que les sauvegardes réussissent sans modifier les lecteurs existants.
- Les vérifications de démarrage en arrière-plan attendent que WPF finisse de cacher sa fenêtre, puis exigent que le processus continue à fonctionner sans fenêtre principale visible.
-  Mettre à niveau les actions GitHub vers Node.js 24 versions et installer le même Arduino CLI 1.2.2 à partir de ses archives en amont vérifiées par le bilan de contrôle.

 Télécharger **DeckPilot-Setup.exe** ou mise à jour de l'application. Profils et calibrage sont conservés. Aucun flash firmware n'est nécessaire.
