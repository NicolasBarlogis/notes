# Decorator Pattern

Hello @Craftmanship.

Aujourd'hui, focus sur un design pattern structurel : le pattern décorateur.

> Décorateur est un patron de conception structurel qui permet d’affecter dynamiquement de nouveaux comportements à des objets
> en les plaçant dans des wrappers qui implémentent ces comportements.

Comme d'habitude, un use case pour illustrer tout ça : nous allons fabriquer des gâteaux 🧁🧁🧁 :

- Notre petite entreprise fabrique donc 2 types de gateaux : des cupcakes et des cookies. 
- Chaque gateau est fabriqué à partir d'une base sur laquelle peuvent être ajoutées des garnitures (chocolat, noisettes, sucre ...).
- Un gateau garni est un gateau : 
  - son nom est composé du nom du gateau et de ses garnitures
  - son prix est la somme des prix du gateau et de ses garnitures

Nous souhaitons pouvoir gérer facilement le nom des chaque composition gâteau + garnitures selon les grilles de tarifs suivantes :

| Cakes     | Prix  |
|-----------|-------|
| Cupcake🧁 | 3.00$ |
| Cookie 🍪 | 2.00$ |


| Toppings     | Prix  |
|--------------|-------|
| Chocolate 🍫 | 1.00$ |
| Nut 🥜       | 2.00$ |
| Sugar 🍚     | 0.50$ |

Nos spécifications de départ sont les suivantes :

```csharp
public class CakeTests
{
    [Fact]
    public void cupcakes_should_be_named_with_symbol()
    {
        var cupCake = new CupCake();
        cupCake.Name.Should().Be("🧁");
    }

    [Fact]
    public void cupcakes_should_cost_3_dollars()
    {
        var cupCake = new CupCake();
        cupCake.Price.ToString().Should().Be("3.00$");
    }

    [Fact]
    public void cookies_should_be_named_with_symbol()
    {
        var cookie = new Cookie();
        cookie.Name.Should().Be("🍪");
    }

    [Fact]
    public void cookies_should_cost_2_dollars()
    {
        var cookie = new Cookie();
        cookie.Price.ToString().Should().Be("2.00$");
    }
}
```

et le code correspondant :

```csharp
public interface ICake
{
    string Name { get; }
    Price Price { get; }
}

public abstract record Cake(Price Price, string Name): ICake;

public record Cookie() : Cake(new Price(2), "🍪");

public record CupCake() : Cake(new Price(3), "🧁");

public record Price(float Value)
{
    public override string ToString()
        => $"{this.Value:N2}$".Replace(",", ".");

    public static Price operator +(Price price) => price;

    public static Price operator +(Price price, Price otherPrice)
        => new Price(price.Value + otherPrice.Value);

    public static Price operator *(Price price, float multiplier)
        => new Price(price.Value * multiplier);
}
```

## Création d'un décorateur

> Un décorateur est wrapper, un objet qui est lié à un objet cible. 
> Il possède le même ensemble de méthodes que la cible et lui délègue toutes les demandes qu’il reçoit. 
> Il peut exécuter un traitement et modifier le résultat avant ou après avoir envoyé sa demande à la cible.
 
Créons donc nos classes Chocolate et Nut à partir d'une classe Toppings qui va encapsuler nos Cakes, en nous basant sur les spécifications suivantes :

```csharp
[Fact]
public void Should_have_compound_name_when_contains_one_topping()
{
    var cupCake = new Chocolate(new CupCake());
    cupCake.Name.Should().Be("🧁 with 🍫");
}

[Fact]
public void Should_cost_sum_of_topic_and_cake_prices_when_it_have_one_topping()
{
    var cookie = new Chocolate(new Cookie());
    cookie.Price.ToString().Should().Be("2.10$");
}
```

ce qui sera obtenu avec le code suivant :
```csharp
public abstract class Topping
{
    private readonly string _name;

    private readonly Price _price;

    private readonly Cake _cake;

    protected Topping(string name, Cake cake, float price)
    {
        _name = name;
        _cake = cake;
        _price = new Price(price);
    }

    public string Name => $"{_cake.Name} with {_name}";

    public Price Price => _price + _cake.Price;
}

public class Nut : Topping
{
    public Nut(Cake cake) : base("🥜", cake, 0.2f){ }
}
public class Chocolate : Topping
{
    public Chocolate(Cake cake) : base("🍫", cake, 0.1f) { }
}
```

## Ajoutons donc un peu de sucre

À partir de quel moment un wrapper devient un décorateur ? Et bien lorsque celui-ci va implémenter la même interface que l'objet qu'il accueille.
```csharp
public abstract class Topping : ICake
{
    /* ... */
    private readonly ICake _cake;

    protected Topping(string name, ICake cake, float price)
    { /* ... */ }
}
public class Nut : Topping
{
    public Nut(ICake cake) : base("🥜", cake, 0.2f) { }
}
public class Chocolate : Topping
{
    public Chocolate(ICake cake) : base("🍫", cake, 0.1f) { }
}
```

Avec ce simple changement, nos Toppings vont donc pouvoir accueillir n'importe quel objet qui implémente l'interface ICake et permettre de combiner les comportements de différentes classes implémentant cette interface.

```csharp
[Fact]
public void Should_have_compound_name_when_contains_many_toppings()
{
    var chocolateCookie = new Chocolate(new Cookie());
    var nutChocolateCookie = new Nut(chocolateCookie);
    var sugarNutChocolateCookie = new Sugar(nutChocolateCookie);

    chocolateCookie.Name.Should().Be("🍪 with 🍫");
    nutChocolateCookie.Name.Should().Be("🍪 with 🍫 and 🥜");
    sugarNutChocolateCookie.Name.Should().Be("🍪 with 🍫 and 🥜 and 🍚");
}

[Fact]
public void Should_cost_sum_of_topics_and_cake_prices_when_it_have_many_toppings()
{
    var chocolateCookie = new Chocolate(new Cookie());
    var nutChocolateCookie = new Nut(chocolateCookie);
    var sugarNutChocolateCookie = new Sugar(nutChocolateCookie);

    chocolateCookie.Price.ToString().Should().Be("2.10$");
    nutChocolateCookie.Price.ToString().Should().Be("2.30$");
    sugarNutChocolateCookie.Price.ToString().Should().Be("2.80$");
}
```

## Composition over inheritance

S'il est tout à fait possible de faire passer nos tests avec une implémentation basée sur l'héritage, celle-ci apporterait plusieurs inconvénients :

- La relation d'héritage signifie "est un". En héritant de Cake, Topping deviendrait un Cake, et nous pourrions donc créer des gateaux avec juste du chocolat et des noisettes.
- Les sous-classes ne peuvent avoir qu'un seul parent dans la majorité des langages de programmation, donc l'organisation du code devient plus complexe.
- Topping devrait réécrire l'entièreté des méthodes de son parent, puisque les noms et prix ont un comportement différent.
- Le couplage entre les 2 classes serait plus fort puisque les modifications sur la classe parente pourraient avoir des effets de bord sur la sous-classe. 

C'est le principe de *Composition over inheritance* : favoriser la composition, c'est à dire la gestion de dépendances pour modifier les comportements du code, plutôt que d'utiliser l'héritage.

## Quand utiliser ce pattern

### Combiner plusieurs comportements sur des services

Combiner plusieurs comportements en emballant un objet dans plusieurs décorateurs, 
par exemple pour respecter les règles de [l'architecture logicielle](https://blog.octo.com/application-domain-infrastructure-des-mots-de-la-layered-hexagonal-clean-architecture/) choisie ou gérer l'activation d'une partie du code.

```csharp
interface IDomainService {
    void MakeSomeStuff();
}
class SomeDomainService: IDomainService {
    public void MakeSomeStuff()
    { /* ... */ }
}
class FeaturedDomainService: IDomainService {
    private readonly IDomainService _decoratedService;
    private readonly bool _featureEnabled = false;
    public FeaturedDomainService(IDomainService decoratedService, bool featureEnabled)
    {
        _decoratedService = decoratedService;
        _featureEnabled = featureEnabled;
    }
    public void MakeSomeStuff()
    {
        if (_featureEnabled)
            _decoratedService.MakeSomeStuff();
    }
}
class LoggedDomainService: IDomainService {
    private readonly IDomainService _decoratedService;
    public LoggedDomainService(IDomainService decoratedService)
    {
        _decoratedService = decoratedService;
    }
    public void MakeSomeStuff()
    {
        Log("before");
        _decoratedService.MakeSomeStuff();
        Log("after");
    }
    private void Log(string log)
    { /* ... */ }
}
```

Avantages :

- le service métier est complètement isolé du reste de l'application
- tous les services sont testables de manière indépendante et isolée
- la composition se fait au moment de l'injection de dépendance en se basant sur le dernier service créé pour être injecté en tant qu'interface
- retirer la gestion du feature flipping se résumera à changer supprimer remplacer une dépendance et supprimer le décorateur dédié ainsi que sa classe de test.

### Refactoring through Single Responsibility Principle.

Vous pouvez découper une classe monolithique qui implémente plusieurs responsabilités différentes en plusieurs petits morceaux.

```java
public interface CandidateProductService {
  Optional<CandidateProduct> findById(String id);
  CandidateProductsResponse findCandidateProductsWithCursor(ProductSearchCriteria productSearchCriteria);
  CandidateProduct findCandidateProductReadyById(String id);
  void update(String productId, CandidateProductUpdateRequest updateRequest, String updatedBy);
  List<CandidateProduct> findCandidateProducts(ProductSearchCriteria productSearchCriteria);
  void addOffer(CandidateProduct candidateProduct, Offer offer);
  void updateOffer(CandidateProduct candidateProduct, Offer offer);
  void removeOffer(CandidateProduct candidateProduct, Offer offer);
  void create(CandidateProduct candidateProduct);
}
```

Dans ce cas, utiliser le refactoring [Extract interface](https://refactoring.guru/fr/extract-interface) pour respecter le principe de ségrégation d'interface et découpler les responsabilités :

```java
// les noms donnés aux interfaces ci-dessous peuvent être remis en question, 
// ils ont été choisis pour permettre d'écrire un exemple simple et ne reflettent pas ceux utilisé dans la vraie base de code :)
public interface CandidateProductSearcher {
  Optional<CandidateProduct> findById(String id);
  CandidateProductsResponse findCandidateProductsWithCursor(ProductSearchCriteria productSearchCriteria);
  CandidateProduct findCandidateProductReadyById(String id);
  List<CandidateProduct> findCandidateProducts(ProductSearchCriteria productSearchCriteria);
}
public interface CandidateProductWriter {
  void update(String productId, CandidateProductUpdateRequest updateRequest, String updatedBy);
  void create(CandidateProduct candidateProduct);
}
public interface CandidateProductOfferManager {
  void addOffer(CandidateProduct candidateProduct, Offer offer);
  void updateOffer(CandidateProduct candidateProduct, Offer offer);
  void removeOffer(CandidateProduct candidateProduct, Offer offer);
}
public interface CandidateProductService
 extends CandidateProductSearcher, CandidateProductWriter, CandidateProductOfferManager { }   
```

Ensuite, utiliser le refactoring [Extract class](https://refactoring.guru/fr/extract-class) pour créer les classes implémentant ces interfaces en transformant l'implémentation initiale en décorateur pour chacune d'elles.

*Avantages :*

- les tests ne sont pas modifiés pendant toutes ces opérations, ce qui en font un vrai refactoring
- les refactorings employés peuvent être réalisés en quelques cliques (ou en utilisant un raccourci) par votre IDE ce qui rend l'opération encore plus sûre
- une fois le code modifié, il ne reste plus qu'à isoler les tests de chaque classe et de remplacer l'injection du service initial dans l'application par celles des nouvelles classes.

Happy refactoring ;)