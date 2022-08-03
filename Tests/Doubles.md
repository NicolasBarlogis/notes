https://blog.cleancoder.com/uncle-bob/2014/05/14/TheLittleMocker.html

Nom des objets permettant de remplacer des éléments pour les tests
On distingue les  [[Dummy]], [[Mock]], [[Stub]], [[Fake]] et [[Spy]]

### Dummy / faux

Aucune logique. Sert juste à vérifier que l'appel se fait

### Stub / bouchon
![[Stub.png]]
données prédéfinies / statiques renvoyés

### Fake / faux
![[Fake.png]]
Relativement fonctionnel, simplifie l'implémentation. Typiquement via stockage en mémoire.
Il dispose généralement d'une connaissance du métier

### Spy / espion
Peut être un fake, un stub ou dummy. Ajout une logique de capture des actions faites. Par exemple pour vérifier qu'une méthode a bien été appelée ou compter le nombre de hits.

### Mock / contrefaçon
Classe compléte de substitution. Comportement paramétrable, il peut donc lever des exceptions ou autre. Utiliser pour les vérifications comportementales. Il procède lui même à des assertions.

Origine de ces patterns: http://xunitpatterns.com/Test%20Double.html

https://knplabs.com/fr/blog/mocks-fakes-stubs-dummy-et-spy-faire-la-difference

https://blog.pragmatists.com/test-doubles-fakes-mocks-and-stubs-1a7491dfa3da --> attention définition du mock