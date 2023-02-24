# Sunny Tech 2023  
* Nicolas Barlogis  
* Coach software craftsmanship  
* Société Inside Group  

## Présentation
Actuellement coach craft chez CDiscount via mon ESN Inside Group, je suis passé par beaucoup de postes différents, d'expert système à scrum master en passant par lead dév.

J'ai une énorme envie d'apprendre, c'est d'ailleurs ce qui m'a amené à faire un aussi grand écart dans mon parcours, et j'ai aussi beaucoup partager ces connaissances et l'expérience que j'ai pu avoir, d'où le fait que mes dernières missions aient été sur de l'accompagnement et du coaching, qu'il soit agile ou craft. 

Je commence à me pencher sur des participations à des forums tels que le Sunny Tech et de la contribution à des projets libres, par volonté de pouvoir donner un peu aux communautés qui m'ont tant apporté durant des années.

---

## Propositions  

### #1 - Quicky: Des tests plus expressifs avec les Test Data Builders  
* format: Quicky  
* catégorie: Architecture (par défaut pour pratiques de code, potentiellement Conception pour BDD/TDD)
* niveau: débutant

#### Objectif  
TLDR: Montrer comment le pattern Test Data Builder permet de faciliter la création de données de tests et d'augmenter l'expressivité des tests.

Les TU sont malheureusement encore trop souvent développés pour faire plaisir au code coverage de SonarQube sans réelle réflexion sur l'intérêt de chaque TU.
Pour encourager des pratiques mettant plus en avant le rôle fonctionnel des tests (TDD,BDD ou DDD), introduire le pattern des Test Data Builder permet de faire un premier pas dans l'expressivité des tests.
En rendant la création de données plus métier, on fait un premier pas vers la documentation as code, la sensibilisation au caractère pas juste techniques des TU et l'importance de l'ubiquitous language pour maintenir une base de test utile et pas juste technique/subie.
Bien sûr il n'est pas possible de toutes ces notions dans un quicky mais c'est pour ces raisons que ce pattern est intéressant à connaître.

#### Plan  
**Introduction**  
Comparaison d'un exemple de création classique avec une création d'objet  
utilisant des test data builder chaînés  

```csharp  
Country country = new Country("South Africa",Language.English);  
Author author = new Author("J.R.R. Tolkien", country);  
Book englishBook = new Book("The Lord of the Rings", author, 1954, Style.Fantasy, Language.English)  
Book translatedhBook = new Book("Le Seigneur des Anneaux", author, 1954, Style.Fantasy, Language.French)  
```

```csharp  
BookBuilder lordOfTheRingsBuilder = BookBuilder
	.AFantasyBook()
	.writtenBy(AuthorBuilder
	.AnAuthor()
		.Named("J.R.R. Tolkien")                 
		.From(CountryBuilder.SouthAfrica())
	.publishedIn(1954);

Book lordOfTheRings = lordOfTheRingsBuilder  
        .titled("The Lord of the Rings")
        .inLanguage(Language.English)
        .Build();
Book leSeigneurDesAnneaux = lordOfTheRingsBuilder
  .titled("Le Seigneur des Anneaux")
  .inLanguage(Language.French)
  .Build();  
```

**Présentation**  
Partir de la problématique de création de jeux de données de tests classiques  
--> réutilisabilité et expressivité  
Présenter les patterns [Object Mother](https://martinfowler.com/bliki/ObjectMother.html)  (réutilisabilité) et [Builder](https://refactoring.guru/design-patterns/builder)  (expressivité)  
Introduire le test data builder qui combine les deux approches  
Démonstration de la mise en place du chainage de test data builder pour générer des objets complexes  

Conclure en présentant quelques outils permettant la génération automatique de builder, ce qui permet de limiter le coût de développement des test data builders.  

---  

### #2 - Conférence:  L'objet fonctionnel, ou comment améliorer l'OOP avec des concepts de Functional Programming
  * format: conférence
  * catégorie: Backend (pour Paradigme de programmation, mais s'applique aux langages front)
  * niveau: confirmé

#### Objectif
TLDR: Montrer aux développeurs OOP comment tirer partie des fonctionnalités fonctionnelles intégrées dans la plupart des langages objets actuels.

Des concepts de programmation fonctionnelle sont aujourd'hui intégrés dans la plupart des langages objets(lambda, map/filter/reduce et/ou disponibles via des extensions (vavr en java, language-ext en c#, fp-ts en typescript, ...). Dès lors il devient intéressant pour les développeurs de connaître ces outils pour exploiter au mieux leurs avantages (expressivité, plus facilement testable, facile à paralléliser,...).
Le but ici n'est pas de rentrer dans les débats sémantiques poussés de la communauté fonctionnelle (différence entre lamba/fonction anonyme, différence purity et referential transparency,...) mais de faire une introduction simple et pratique des concepts fonctionnels utiles en OOP. 

#### Plan
**Introduction**
Quel intérêt aurait un dév à apprendre la programmation fonctionnelle ? Si on regarde un classement des langages (comme l'index TIOBE), les langages fonctionnels pures (Haskel, Lisp, ...) ou à dominante fonctionnelle (Scala, Kotlin, ...) sont très peu utilisés dans l'industrie par rapport aux autres.
Exemple (ici en java):

```java
// get the average number of friends a user has  
double friendsCount = Arrays.stream(users)  
        .map(user -> user.getFriendList().length)  
        .reduce(0, Integer::sum)  
        / (double)users.length;
```

Ici 5 principes fonctionnels: 
* map/filter/reduce
* functor (le stream supporte map)
* les lambdas / fonctions anonymes
* first class function
* higher order function

--> les langages populaires supportent des fonctionnalités issues du FP. Autant les exploiter au mieux en comprenant les tenants et aboutissants de ces concepts.
L'idée est d'ouvrir ses horizons et d'ajouter un outil de plus à notre ceinture de développeur, pour repousser encore plus le marteau de Maslow.

**Présentation**
Le but est de voir certaines monades, pratiques pour améliorer la lisibilité et la maintenabilité du code.
L'introduction de certains concepts est nécessaire avant d'attaquer les monades, d'autant plus qu'ils ont eu aussi un intérêt.
Le tout illustré d'exemples avec une thématiques filées (gestion d'utilisateurs et de leurs relations), dans 1 ou 2 langages pour parler au plus grand nombre (Java, JS/TS, C# ou C++ à voir)

1. Concepts basiques
	* fonction pures (+ Simple: ne dépend pas de facteurs externes, + Maintenable,+ Réutilisable: composition plus facile,+ Testable, Parallélisable, Possibilité d’utiliser la mémoïsation/memoization)
	* Immutabilité (lien ddd avec always valid domain model, •Maintenable: limite les effets de bord, Parallélisable, facilite l'utilisation du method chaining)
2. Map/Filter/Reduce
	* Introduction de la notion de boîte, qui marche généralement très bien pour expliquer le fonctionnement de ces méthodes et des monades.
3. Monades
	* Maybe / option / optional / some / Just (plein de noms selon les langages) --> expliciter le null.
	* Introduction du concept functional core / imperative shell (et lien avec le DDD) en montrant une implémentation de repository utilisant cette monade:

```java
// style classique, quel est le comportement si pas de User pour cet ID ?
public User findById(int userId)
// avec Optional, fonctionnement explicite --> réduction de la charge mentale
public Optional<User> findById(int userId)
```

	* Try --> expliciter un traitement pouvant rater
	* Either / OneOf --> Généralisation du Maybe et du Try, pour renvoyer une valeur ou un type complexe en cas de problème

Conclusion en présentant plusieurs libs et sources utiles pour aller plus loin et commencer à mieux utiliser le fonctionnel en OOP.