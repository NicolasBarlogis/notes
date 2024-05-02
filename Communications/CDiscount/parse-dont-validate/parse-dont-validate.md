# Parse, don't validate

Hello @Craftsmanship,

Aujourd'hui nous poursuivons notre voyage dans la programmation fonctionnelle avec un principe appelé "_Parse, don't validate_".

> TLDR;
> - La validation est un élément crucial pour vérifier les données entrant dans un système.
> - La validation en tant que processus externe expose le risque de créer une nouvelle instance sans appeler la méthode de validation avant.
> - "Parse, don't validate" est un principe permettant de retirer ce risque.
> - Un _parser_ est juste une fonction qui consomme une entrée peu structurée et produit une sortie plus structurée.

# Les problèmes liés à la validation

Prenons l'exemple suivant : l'identifiant d'une offre comporte de nombreuses de formatage.

Il est composé de 4 parties distinctes, séparées par des `_` :
- L'*identifiant* et la *condition* du *produit* auquel il se réfère
- L'*identifiant du vendeur* qui souhaite vendre le produit
- Le *canal de vente* sur lequel le produit sera mis en vente

C'est 4 valeurs sont également soumises à des règles de validation :
- l'identifiant produit est composé de 16 caractères hexadécimaux
- la condition d'un produit est une valeur numérique correspondant à des valeurs d'énumérateurs
- l'identifiant d'un vendeur est un entier
- l'identifiant d'un canal de vente est composé des 6 lettres majuscules

Afin de garantir l'unicité de l'identifiant d'une offre, nous devons garantir la validité de toutes ces règles 

Un code utilisant la validation pourrait ressembler à :

```csharp
public record OfferId(string Product, long Seller, ProductCondition ProductCondition, string SalesChannel)
{
    public static bool IsValid(string offerId)
    {
        var parts = offerId.Split('_');

        return parts.Length == 4
           && ProductIdIsValid(parts[0])
           && SellerIdIsValid(parts[1])
           && ProductConditionIsValid(parts[2])
           && SalesChannelIdIsValid(parts[3]);
    }
}
```
Et utilisé de la manière suivante :
```csharp
if (!OfferId.IsValid(offerIdAsString) {
    // Log, user feedback, exception ...
}
var parts = offerId.Split('_');
var offerId = new OfferId(parts[0], Convert.ToInt64(parts[1]), parts[2], parts[3]); 
// do something with the valid offerId
```

Limitations : 

- Nous faisons quelque chose si l'identifiant n'est pas valide sans en connaitre le contexte, car _IsValid(offerId)_ encapsule la logique. Ainsi, chaque effet secondaire (log, feedback, exception, etc.) ne sera pas explicite sur le problème.
- La validation des données n'est pas liée à la construction, c'est un processus supplémentaire. Il y a donc un risque de créer des identifiants non valides si la validation est oubliée.
- Une partie de la logique de construction est laissée à la charge de l'appelant : décomposer la chaîne de caractère, convertir la partie représentant le vendeur en entier. 
- Ce pattern apparaîtra chaque fois que nous créons un nouvel OfferId, favorisant donc l'apparition de duplication de code.

# Parsing to the rescue

> What is a Parser?
> Really, a parser is just a function that consumes less-structured input and produces more-structured output.
> _Alexis King_

Pour remplacer notre méthode de validation par un parser qui, à partir de la représentation de l'identifiant d'une offre sous forme de chaîne de caractères, va nous construire un modèle objet plus structuré,
nous allons avoir besoin de :
- Supprimer les types primitifs utilisés afin de bien séparer les responsabilités de chaque partie de l'identifiant
- Mettre en place des constructeurs moins permissifs afin d'obtenir uniquement des instances valides
- Faciliter l'instanciation de ces objets à partir du type d'origine, une chaîne de caractères : le scénario passant de notre parser
- Fournir du contexte en cas d'erreur

Nous allons donc créer un type par partie de l'identifiant d'une offre, garant des règles métier qui leur sont propres, 
et fournissant leur propre parsing.

```csharp
public record ProductId
{
    public string Value { get; }

    public ProductId(string value)
    {
        // Mise en place d'un Guard dans le constructeur
        if (!IsValid(value)) throw new InvalidOfferId("Invalid ProductId", nameof(value));
        Value = value;
    }

    public static bool IsValid(string productId) =>
        productId.Length == 16 
        && productId.All(c => IsHexadecimal(c));

    private static bool IsHexadecimal(char c) => /* ... */;

    public static ProductId Parse(string potentialProductId) =>
        !IsValid(potentialProductId)
            ? throw new InvalidOfferId("Invalid ProductId", nameof(potentialProductId))
            : new ProductId(potentialProductId);
}
```

Problème, notre parseur contient maintenant un effet de bord : une exception est lancée si le format est invalide.

# Des fois ça marche, des fois ça ne marche pas

Pour améliorer notre parser, nous pourrions renvoyer une valeur nulle en cas de format non valide, 
mais nous déporterions alors le problème dans les classes appelantes.
Nous avons vu précédemment que cela pouvait être évité [en utilisant des Options](../options/option.md),
mais nous perdrions également le contexte de notre erreur.

C'est là qu'intervient un autre type de monade : _Either_.

_Either_ peut être de 2 types différents :
- _Right_ en cas de succès 
- _Left_ en cas d'erreur

> **Pourquoi _Left_ et _Right_ ?**  
> Il faut voir _Either_ comme un pipeline :  
> _process1 -> process2 -> process3 -> ..._  
> en cas de succès, on continue d'avancer vers l'étape suivante, à droite.

Changeons donc notre typage de retour en utilisant le type _Either<InvalidOfferId, ProductId>_.
En c#, Either est fourni par le package [LanguageExt](https://github.com/louthy/language-ext) et,
grâce à l'utilisation de la [conversion implicite](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators),
les changements dans le code sont minimes :
```csharp
public record ProductId(string ProductId)
{
    /* ... */
    public static Either<InvalidOfferId, ProductId> Parse(string potentialProductId) =>
        !IsValid(potentialProductId)
            ? new InvalidOfferId("Invalid ProductId", nameof(potentialProductId))
            : new ProductId(potentialProductId);
}
```
Une fois le parsing de toutes les parties effectué, le parsing de l'identifiant de l'offre est alors simplifié :

```csharp
public record OfferId(ProductId Product, SellerId Seller, ProductCondition ProductCondition, SalesChannelId SalesChannel)
{
    public static Either<InvalidOfferId, OfferId> Parse(string potentialOfferId)
    {
        var parts = potentialOfferId.Split('_');

        return parts.Length != 4
            ? new InvalidOfferId("Invalid offerId format", nameof(potentialOfferId)) : ParseSafely(parts);
    }

    private static Either<InvalidOfferId, OfferId> ParseSafely(string[] parts)
    {
        return from productId in ProductId.Parse(parts[0])
            from sellerId in SellerId.Parse(parts[1])
            from productCondition in ProductConditionParser.Parse(parts[2])
            from salesChannel in SalesChannelId.Parse(parts[3])
            select new OfferId(productId, sellerId, productCondition, salesChannel);
    }
    /* ... */
}
```

_**Nb:** "le parsing est simplifié" à condition de connaître quelques subtilités du langage C#.   
Si ce n'est pas le cas, pas de panique : la méthode ParseSafely() utilise un sucre syntaxique, 
mais les fonctionnalités utilisées sont disponibles sur n'importe quelle implémentation de la monade _Either_.  
Nous y reviendrons certainement lors d'un prochain article ;)_

## Bonus : le principe de round-tripping

Un identifiant "maitrisé", c'est-à-dire qui n'est pas incrémenté automatiquement, est souvent persisté sous forme de chaîne de caractère.  
Donc s'il est utile de pouvoir instancier un modèle objet valide à partir d'une chaîne de caractères, l'opération inverse le sera également :
les 2 opérations nous permettrons de stocker et de restituer l'information.   

Pour nous assurer cela, nous pouvons donc mettre en place un test de "Round tripping" :
```csharp
public class OfferIdShould
{
    [Fact]
    public void Make_a_round_trip()
    {
        OfferId.Parse("ABCDEF0123456789_123564_1_CDISFR")
            .Match(
                Right: offerId => offerId.ToString(),
                Left: _ => ""
            ).Should()
            .Be("ABCDEF0123456789_123564_1_CDISFR");
    }
}
```

**Comment faire passer ce test ?**  
C'est à vous de jouer dans les commentaires ;)

## Quelques ressources pour finir
- [Parse, don't validate](https://lexi-lambda.github.io/blog/2019/11/05/parse-don-t-validate/)
- [Either & Error Handling](https://itnext.io/either-monad-a-functional-approach-to-error-handling-in-js-ffdc2917ab2)
- [Retrouver cette com et le code associé](http://tfs.cdbdx.biz:8080/tfs/DefaultCollection/craftmanship/_git/coms-craft?path=%2Fparse-dont-validate%2Fparse-dont-validate.md&_a=preview)