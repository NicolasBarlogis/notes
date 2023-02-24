# CSharp
## Optimisation
Plusieurs solutions pour réduire la durée des tests de mutations:
### 1 - Limiter les fichiers mutés
Il est possible de ne lancer les tests en ne mutant qu'une partie du projet via l'option [mutate](https://stryker-mutator.io/docs/stryker-net/configuration/#mutate-glob):
```shell
# mutate only Domain files
dotnet stryker -m '**\Domain\*'

# mutate all but Domain files
dotnet stryker -m '!**\Domain\*'
```

### 2 - Ne mutter que les fichiers modifiés 
[Since](https://stryker-mutator.io/docs/stryker-net/configuration/#since-flag-committish) permet de ne lancer les tests que sur les mutants ayant changé par rapport au commit précisé
```shell
# depuis un commit particulier
dotnet stryker --since:c6e1c9990d1add08bb9dfa76fd74c5322459e31d

# depuis le précédent commit (shell style)
dotnet stryker --since:`git rev-parse HEAD^`
```