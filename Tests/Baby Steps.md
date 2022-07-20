## Quel est l'intérêt des baby-steps ?
L'ajout de tests précise et contraint le code. En faisant une implémentation naïve, uniquement pour passer les tests, on s'assure d'avoir des [[Tests Unitaires]] qui ont du sens et qui correspondent à ce que l'on veut s'assurer fonctionnellement.

En codant les tests d'un seul coup, il peut être plus complexe de s'assurer de la pertinence de ces tests. Ce phénoméne est exacerbé quand un [[code coverage]]de 100% est exigé. Ce genre de tests obtient en général de mauvais score lorsque soumit à du [[mutation testing]].
--> Attention à la [[Loi de Goodhart]]

#### Sources
Session de TDD par l'oncle Bob (Robert Martin). [Voir](https://youtu.be/58jGpV2Cg50?t=1300)
