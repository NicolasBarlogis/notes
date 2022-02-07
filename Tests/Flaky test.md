Un résultat de test est "_flaky_" lorsque le test peut à la fois passer et être un échec sur le même code

Sources possibles du problème:
-   Les environnements de test instables / non maitrisés. Un bon exemple : le réseau
-   Les données de test (Test Data) non maitrisées. Exemple : données d'entrée aléatoires ou changeantes
-   L'utilisation de threads, les exécutions parallèles, l'asynchronicité
-   Les dépendances logicielles ou produits tiers non maitrisés. Exemple : des versions qui peuvent changer d'une exécution à l'autre
- ...
https://eh-bien-testez-maintenant.github.io/2018/09/02/test-automatique-flakiness/
