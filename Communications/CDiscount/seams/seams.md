# Méthode des seams
*Temps de lecture* **7 minutes**

Too Long; Didn't Read;
> Les seams sont les endroits du code qui font la jonction entre plusieurs composants (méthodes, classes, ...). Pour les tests il est possible d'injecter des données dans ces jointures grâce à des [doubles](https://martinfowler.com/bliki/TestDouble.html).
> Néanmoins il est généralement complexe de tester du code qui n'a pas d'injection de dépendance.
> La méthode des seams permet d'introduire à moindre coût une façon d'injecter un comportement spécifique pour nous permettre d'écrirer des tests, sans toutefois nécessiter une grosse refacto telle que celle nécessaire pour introduire un mécanisme d'injection de dépendance


Est-ce que vous avez déjà eu du mal à ajouter des tests unitaires à un projet legacy qui fait beaucoup trop appel à des méthodes statiques ou qui ne fait pas d'injection de dépendance et initialise lui même ses dépendances (comme un repository) ?
Si oui, je vais vous montrer une méthode transitoire pour dépasser ce problème 
 
Tout d'abord, je dois vous parler du concept de [Seam](https://biratkirat.medium.com/working-effectively-with-legacy-code-changing-software-part-1-chapter-4-b997b78fc0a2), introduit dans le livre [Working Effectively With Legacy Code](https://understandlegacycode.com/blog/key-points-of-working-effectively-with-legacy-code/).
Un seam est un endroit dans le code où il est possible de changer le comportement du programme sans modifier son code. C'est la couture (--> seam) entre deux pièces de codes assemblées.

Pour faire simple, un seam est tout appel où il est possible d'injecter une autre instance ou un mock de la classe manipulée, pour modifier le comportement de la méthode appelante. Une illustration vous aidera probablement à mieux comprendre:

```csharp
public class Order {
  private final IList<Product> _products = new List<>;
  private int _totalCents = 0;
  public Status Status;
  public Order(Status status) {
    // tout appel à Status sera un seam car je peux injecter un mock
    // ou même le redéfinir, l'attribut étant publique
    this.Status = status;
  }
  
  public void addProduct(String productName, ProductRepository repo) {
    // repo.findByName(productName) est un seam car je peux injecter un
    // mock à l'appel de addProduct
    Product product = repo.findByName(productName);
    if(product == null) {
      throw new UnknownProductException(productName);
    }
    // product.getPriceCents() est un seam car si le repo est mocké,
    // je peux lui faire retourner un produit mocké
    _totalCents += product.getPriceCents();
    // _products.add n'est pas un seam car je dois modifier Order pour
    // que cet attribut soit autre chose qu'une List
    _products.add(product);
  }
  
  public void markAsSent() {
    // Status.GetSentStatus() n'est pas un seam car je ne peux pas changer
    // le comportement sans modifier Order ou Status (appel static)
    Status sentStatus = Status.GetSentStatus();
    // (new StatusValidator()).OrderCanGoToStatus() n'est pas un seam car
    // je ne peux pas changer l'instance de StatusValidator utilisée
    if((new StatusValidator()).OrderCanGoToStatus(this, sentStatus)) {
      this.Status = sentStatus;
    }
  }
}
```

Toute injection de dépendance va typiquement engendrer des seams. Ce qui est extrêmement pratique lorsque l'on souhaite tester ce code, il suffit d'injecter un mock ou un [fake]((https://martinfowler.com/bliki/TestDouble.html)) pour adapter le comportement du code à notre scénario de test.
 
Les problèmes surviennent lorsque des appels à des méthodes externes ne sont pas des seams, comme les appels statiques ou à des entités créées directement dans la classe.

C'est d'autant plus gênant lorsque l'on intervient sur du code legacy non testé:
1. Il faut ajouter des tests au code pour le modifier, mais on ne peut pas le tester car il fait des appels non mockables.
2. Donc il faudrait passer ces dépendances par injection, mais ça revient à modifier le code, donc on risque de casser si on a pas de tests
3. Il faudrait alors ajouter les tests d'abord mais on ne peut toujours pas...

Joli petite boucle qui en théorie nous donnerait une bonne excuse pour ne pas mettre les mains dans du legacy. Malheureusement on a rarement le choix...
Donc plutôt que de vous lancer dans de la refacto en aveugle (sans tests) pour ensuite passer 2 semaines à corriger les bugs détectés en prod, je vous propose une petite technique pour rendre testable du code sans seam.

Pour faire simple, on va appeler ça la méthode de refacto des seams, Sandro Mancuso la décrit très bien dans [cette vidéo](https://www.youtube.com/watch?v=_NnElPO5BU0), je vais aussi vous l'expliquer rapidement.
Disons que l'on veuille ajouter des TU sur le markAsSent qu'on a vu dans les exemples:

```csharp
public void markAsSent() {
  Status sentStatus = Status.GetSentStatus();
  
  if((new StatusValidator()).OrderCanGoToStatus(this, sentStatus)) {
    this._status = sentStatus;
  }
}
```

On peux probablement faire deux scénarios fonctionnels, une premier passant où les règles de changement d'état sont ok et le statut passe à envoyé, et un autre où l'état reste inchangé. 
En l'état on dépend du retour de (new StatusValidator()).OrderCanGoToStatus(this, sentStatus), et donc d'une autre classe, ce qui n'est pas idéal pour nos TU.
 
Le mieux serait de pouvoir injecter le StatusValidator (voir même de réintégrer la logique de changement d'état dans la classe Order ou même dans State avec un state pattern, mais ce n'est pas le sujet ici), afin de pouvoir injecter un mock de l'objet paramétré pour répondre true ou false. 
C'est l'objectif à terme, mais on le fera lorsque des tests seront en place, car ça impliquerait de modifier plusieurs classes, avec donc plus de risque d'erreur.
 
Pour mettre en place ces deux premiers tests sans trop impacter le code nous allons procéder comme suit:
 
## 1. Isoler la méthode problématique
Via un extract method de l'IDE (ça limite le risque  d'erreur), on sort l'appel dans une méthode qui ne contient que ça
On marque la méthode virtual pour l'étape suivante

```csharp
public void markAsSent() {
  Status sentStatus = Status.GetSentStatus();
  
  if(CanBeSend()) {
      this._status = sentStatus;
  }
}
public virtual bool CanBeSend() {
  return (new StatusValidator()).OrderCanGoToStatus(this, sentStatus);
}
```

## 2. Créer un [stub](https://martinfowler.com/bliki/TestDouble.html)
Dans les tests, on peut alors créer une classe héritant de Order, uniquement pour surcharger l'appel problématique. Dans le cas du test passant, on voudrait que true soit renvoyé:

```csharp
public class SendableOrderStub : Order {
  public override bool CanBeSend() {
    return true;
    }
}
```

Notre test ressemblera alors à ça

```csharp
[Fact]
public void order_should_have_status_sent_when_it_is_sendable() {
  // arrange
  Order order = new SendableOrderStub(Status.GetCreatedStatus());
  // act
  order.markAsSent();
  // assert
  order.Status.Should().Be(Status.GetSentStatus())
}
```

Et voilà, à partir de là on peut créer un autre stub qui renverrai false, ou le rendre paramétrable pour renvoyer true ou false.
Étant donné qu'on ne surcharge que CanBeSend(), c'est bien la logique de la classe Order d'origine que l'on test dans notre appel àmarkAsSent. 
 
Une fois les tests nécessaires mis en place sur la base de code, on pourra à terme entamer les refacto pour passer en injection de dépendance, mais cette fois avec les gardes fou des tests qui nous assureront qu'on ne casse rien d'autre. 
Et si le nettoyage de cette classe n'a que peu d'intérêt, alors on dispose au moins de tests dessus.
 
Ce sera tout pour la notion de seam, n'hésitez pas à poser vos questions si vous en avez 🙂