### Idées de titre 
* Quand l'objet devient fonctionnel, le meilleur de deux mondes
* Utiliser la programmation fonctionnelle en production ? Les langages objet s'adaptent
* Utiliser la programmation fonctionnelle en production ? Les langages s'adaptent, pourquoi pas vous ?
* Faire du fonctionnel sans changer de langage, c'est possible !

## Introduction - L'industrie n'est pas prête pour le tout fonctionnel (5min)
* Peu de projets en full Scala / Haskell / Elixir...
* Mais de plus en plus d'inclusion des paradigmes fonctionnels dans les langages populaires
* Apparition de conceptions hybrides (functionnal core / imperative shell)
--> Comment des principes fonctionnels peuvent améliorer la qualité / réduire la compléxité de notre code

## La programmation fonctionnelle (10min)
### Fonctions pures, immutabilité
et autres principes fondamentaux (higher order functions, lambda, ...)
### Map, filter, reduce
Introduction via les listes, connu des dévs OO, pour glisser vers le fait d'appliquer ce fonctionnement sur des valeurs encapsulées --> introduction du seul principe des monades, les functors / applicative ajoutent juste du bruit à ce niveau

## Les monades pour améliorer notre code
### Option (Maybe / Optionnal) (10min)
Meilleure gestion de la nullité
--> introduction de bind

### Try (10min)
Supprimer les side effect (Throw)
Désencombrer le code des try/catch verbeux et mieux mettre en avant la logique appliquée

### Either (5min)
Try, en continuant à exploiter les exceptions/erreurs

## Conclusion (5min)
Beaucoup de bonnes idées à prendre du fonctionnel, plus simple que de se lancer directement dans un langage fonctionnel.
De plus en plus simple à faire via les intégrations dans les langages, natives ou lib (java.util.Optional, vavr, fp-ts, ...)


# Sources
* [Explications illustrées ](https://adit.io/posts/2013-04-17-functors,_applicatives,_and_monads_in_pictures.html)
* [Reader/Writer/State Monads](https://adit.io/posts/2013-06-10-three-useful-monads.html)
* [Map/Filter/Reduce](https://web.mit.edu/6.005/www/fa15/classes/25-map-filter-reduce/#summary)
* [Opérations fonctionnelles en Java natif](https://adit.io/posts/2013-06-10-three-useful-monads.html)
* [Vavr (anciennementsJavaSlang), lib fonctionnelle pour Java](https://www.vavr.io/)
* [fp-ts, lib fonctionnelle pour TS](https://gcanti.github.io/fp-ts/)
* [F# for Fun and Profit](https://fsharpforfunandprofit.com/)
* [Language-ext, lib fonctionnelle pour C#](https://yoan-thirion.gitbook.io/knowledge-base/software-craftsmanship/functional-programming-made-easy-in-c-with-language-ext)
* https://belief-driven-design.com/functional-programming-with-java-introduction-43481e645ee/ --> https://belief-driven-design.com/book/


https://dev.to/glebec/four-ways-to-immutability-in-javascript-3b3l
https://docs.oracle.com/javase/tutorial/essential/concurrency/immutable.html
