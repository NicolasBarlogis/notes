# Domain Driven Design
## [[Ubiquitous Language]]
## [[Domain]]
## [[Strategic Design]]
## [[Tactical Design]]
[[Functioning Core, Reactive Shell]]
[[Clean Code/Supple design]]

[Domain-Driven Design Reference](https://www.domainlanguage.com/wp-content/uploads/2016/05/DDD_Reference_2015-03.pdf)
--> Résumé du DDD fait par Eric Evans en 2015

[About Team Topologies and Context Mapping](https://blog.avanscoperta.it/2021/04/22/about-team-topologies-and-context-mapping/) Comparaisson entre les concepts DDD de context mapping et ceux de team topologies

# Modélisation
[[Event storming]]

# Mise en place
[[Bubble context]]

---
## Les objets ou blocs d’objets préconisés par le DDD

Pour aider à l’élaboration du domain model, DDD préconise un certain nombre d’objets, toutefois elle n’impose pas des les utiliser absolument. Comme dans la plupart des cas, on est libre d’envisager une architecture n’utilisant ces différents objets.

### Entité

Les objets _entités_ contiennent une identité:

-   L’identité est identique durant tous les états du logiciel
-   La référence à l’objet est de préférence unique pour assure une certaine cohérence.
-   Il ne devrait pas exister 2 _entités_ avec la même identité sous peine d’avoir un logiciel dans un état incohérent.
-   L’identité peut être un identifiant unique ou une combinaison de plusieurs membres de l’entité.

### Value-object

Ce sont des objets n’ayant pas d’identité:

-   Les _value-objects_ n’ont pas d’identité car ils sont utilisés principalement pour les valeurs de leurs membres.
-   Ces objets peuvent facilement créés ou supprimés car il n’y a pas de nécessité de maintenir une identité.
-   L’absence d’identité permet d’éviter de dégrader les performances durant la création et l’utilisation de ces objets par rapport aux _entités_.
-   Les _value-objects_ peuvent être partagés
-   Dans le cas de partage de _value-objects_, il faut qu’ils soient immuables c’est-à-dire qu’on ne puisse pas les modifier durant toute leur durée de vie.
-   Les _value-objects_ peuvent contenir d’autres _value-objects_.

### Service

Lorsqu’on définit l’_ubiquitous language_, le nom des concept-clés permettent de definir les objets qui seront utilisés. Les verbes utilisés qui sont associés aux noms permettront de définir les comportements de l’objet. Ces comportements seront implémentés directement dans l’objet.

Ainsi, lorsque des comportements ne peuvent être associés à un objet, ils doivent être implémentés en dehors de tout objet, dans un service:

-   L’opération dans le service fait référence à un concept du _domaine_ qui n’appartient pas à une entité ou à un _value-object_.
-   Un service peut effectuer un traitement sur plusieurs _entités_ ou _value-objects_.
-   Les opérations n’ont pas d’états.
-   Les services ne doivent pas casser la séparation en couche, ainsi un service doit être spécifique à une couche.

### Module

Permet de regrouper les classes pour assurer une cohésion:

-   dans les relations entre les objets
-   dans les fonctionnalités gérées par ces objets.

L’intérêt est d’avoir une vue d’ensemble en regardant les modules, on peut ensuite s’intéresser aux relations entre les modules.

Les modules doivent:

-   former un ensemble de concepts cohérents, de façon à réduire le couplage entre les modules.
-   Le couplage faible permet de réduire la complexité et d’avoir des modules sur lesquels on peut réfléchir indépendamment.
-   Etre capable d’évoluer durant la durée de vie du logiciel.
-   Etre nommés suivant des termes de l’_ubiquitous language_.

### Aggregate

Les objets du modèle ont une durée de vie:

-   Ils peuvent être créés, placés en mémoire pour être utilisés puis détruits ensuite.
-   Ils peuvent aussi être persistés en mémoire ou dans une base de données.

La gestion de cette durée de vie n’est pas facile car:

-   Les objets peuvent avoir des relations entre eux : 1 à plusieurs, plusieurs à plusieurs.
-   Il peut exister des contraintes entres les objets au niveau de leur relation : par exemple unidirectionnel ou bidirectionnel.
-   Il peut être nécessaire de maintenir des invariants c’est-à-dire des règles qui sont maintenues même si les données changent.
-   Il faut assurer une cohésion du modèle même dans le cas d’association complexe.

Une méthode est d’utiliser un groupe d’objets comme les _agrégats_ (i.e. “aggregate”). Les _agrégats_ sont des groupes d’objets associés qui sont considérés comme un tout unique vis-à-vis des modifications des données, ainsi:

-   Une frontière sépare l’_agrégat_ du reste des objets du modèle,
-   Chaque _agrégat_ a une racine qui est une entité qui sera le lien entre les objets à l’intérieur et les objets à l’extérieur de l’_agrégat_.
-   Seule la racine possède une référence vers les autres objets de l’_agrégat_.
-   L’identité des _entités_ à l’intérieur de l’_agrégat_ doivent être locale et non visible de l’extérieur.
-   La durée de vie des objets de l’_agrégat_ est liée à celle de la racine.
-   La gestion des invariants est plus facile car c’est la racine qui le fait.
-   La racine utilise des références éphémères si elle doit passer des références d’objets internes à des objets externes. L’intégrité de l’_agrégat_ est, ainsi, maintenue.
-   On peut utiliser des copies des _value-objects_.

### Factory

Les fabriques sont inspirées du “design pattern” pour créer des objets complexes:

-   Elles permettent d’éviter que toute la logique de création des objets ne se trouve dans l’_agrégat_.
-   Permet d’éviter de dupliquer la logique de règles s’appliquant aux relations des objets.
-   Il est plus facile de déléguer à une fabrique la création d’une _agrégat_ de façon atomique.
-   La gestion des identités des _entités_ n’est pas forcément triviale car des objets peuvent être créés à partir de rien, ils peuvent aussi avoir déjà existé (il faut être sûr qu’il n’existe pas encore une autre entité avec le même identifiant) ou il peut être nécessaire d’effectuer des traitements pour récupérer les données de l’entité en base de données par exemple.

L’utilisation de fabriques n’est pas indispensables, on peut privilégier un constructeur simple quand:

-   La construction n’est pas compliquée : pas d’invariants, de contraintes, de relations avec d’autres objets.
-   La création n’implique pas la création d’autres objets et que toutes les données membres soient passées par le constructeur.
-   Il n’y a pas de nécessité de choisir parmi plusieurs implémentations concrètes.

### Repository

Dans le cas d’utilisation d’_agrégats_, si un objet externe veut avoir une référence vers un objet à l’intérieur, il doit passer par la racine et ainsi, avoir une référence vers la racine de l’_agrégat_, ainsi:

-   Maintenir une liste de références vers des racines d’agrégat peut s’avérer compliqué dans le cas où beaucoup d’objets sont utilisés. Une mise à jour de la référence de la racine auprès de plusieurs objets peut s’avérer couteux.
-   L’accès à des objets de persistance se fait dans la couche infrastructure, les implémentations permettant d’y accéder peuvent se trouver dans plusieurs objets et ainsi être dupliquées.
-   Un objet du modèle ne doit contenir que des logiques du modèle et non les logiques permettant d’accéder à une base de persistance.

Utiliser un _repository_ permet:

-   D’encapsuler la logique permettant d’obtenir des références d’objets.
-   Stocker des objets
-   D’utiliser une stratégie particulière à appliquer pour accéder à un objet.

L’implémentation d’un _repository_ peut se faire dans la couche infrastructure toutefois l’interface de ce _repository_ fait partie du modèle.

Le _repository_ et la fabrique permettent, tout deux, de gérer le cycle de vie des objets du _domaine_:

-   La fabrique permet de créer les objets
-   Le _repository_ se charge de gérer des objets déjà existants.