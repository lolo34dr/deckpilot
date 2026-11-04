> Il s'agit d'une traduction automatique des notes de sortie complètes. L'original anglais est la référence.

DeckPilot 1.7.0 réduit le travail répété pendant l'édition des profils, la recherche d'applications et l'exécution en arrière-plan.

- Enregistrer les paramètres inchangés ne remplace plus le fichier ou crée des sauvegardes. Le contenu réel du disque est toujours vérifié, de sorte que les modifications externes, les suppressions et les sauvegardes échouées ne sont pas cachés.
- L'historique du profil conserve des instantanés de modèle détachés. Les modifications non modifiées et les annulations évitent les allers-retours JSON; l'édition d'action utilise des comparaisons de valeurs au lieu de la sérialisation répétée.
- La recherche d'applications et le sélectionneur partagent un cache de catalogue de cinq minutes et une tâche de découverte. Découverte fonctionne à l'extérieur de WPF; Refresh force toujours un nouveau scan. Chaque sélectionneur possède son filtre et garde les résultats actuels utilisables pendant le rafraîchissement.
- Remplacer le permanent 100 ms board UI sondage avec des notifications de connexion ordonnées. Cellboard-hook callbacks file d'attente travail visuel au lieu de rendre douze touches synchronement.
- Les sondages d'état audio toutes les deux secondes, visibles et toutes les dix secondes lorsqu'ils sont cachés/minimisés, rafraîchissant immédiatement lors de la réouverture et après les commandes audio.
- Les fenêtres cachées arrêtent les minuteurs de rétroaction visuelle et sautent les attentes de rendu cosmétique. Les pinceaux UI congelés sont réutilisés et les enveloppes de processus d'application lancées sont éliminées.

Télécharger **DeckPilot-Setup.exe** ou mise à jour depuis l'application. Profils, calibrage et préférences de démarrage sont préservés. Aucun flash firmware n'est requis.

La validation comprend les paramètres/historique/régression de cache et l'installation de Windows, la mise à jour, le sélecteur, le glisser-déposer, l'édition d'action courte/longe et les vérifications de minimis/restauration. Il s'agit d'optimisations structurelles, et non d'une revendication mesurée d'un CPU ou d'une RAM en particulier sur chaque PC.
