# Sucre syntaxique C# 8
*Temps de lecture* **5 minutes**

Sujet plus léger et technique aujourd'hui, pour vous montrer quelques notations plus ou moins récentes et plus ou moins connues en C#

Too Long; Didn't Read;
> De nombreux opérateurs (^, .., ??=) et outils (pattern matching) sont apparus depuis C# 8
 
## Index operator ^
*Depuis:* C# 8
Permet d'indiquer l'index d'une liste ou d'un tableau, en partant du dernier élément.

 ```csharp
 IList<int> numbers = new List<int> { 0, 1, 2, 3, 4, 5 }; // fonctionne sur les listes et les tableaux
 // avant
 int lastValue = numbers[numbers.Count - 1] // .Last() si on ajout Linq
 // après
 int lastValue = numbers[^1];
 ```
 
^2 permet d'accéder à l'avant-dernière valeur, et on peut continuer.


## Range operator ..
*Depuis:* C# 8
Permet de sélectionner un ensemble d'éléments dans un tableau, mais malheureusement pas d'une liste...

  ```csharp
 IList<int> numbers = new List<int> { 0, 1, 2, 3, 4, 5 };
 int[] numbers = numbersList.ToArray(); // les ranges ne sont disponibles que sur les tableau
 
 // .. sélectionne l'ensemble des éléments du tableau
 numbers.SequenceEqual(numbers[..]); // true
 
 // on peut indiquer un index de début pour retirer les premières valeurs
 numbers[2..] // tous les éléments à partir de numbers[2] sont inclus
 // --> { 2, 3, 4, 5 }
 
 // on peut indiquer un index de fin, qui sera exclu avec les valeurs suivantes
 numbers[..2] // tous les éléments avant numbers[2]
 // --> { 0, 1 }
 
 // on peut combiner les deux
 numbers[1..4] // --> { 1, 2, 3 }
 ```
 
On peut combiner les opérateurs index et range pour faire des choses comme ça :
 
   ```csharp
 IList<int> numbers = new List<int> { 0, 1, 2, 3, 4, 5 };
 int[] numbers = numbersList.ToArray(); // les ranges ne sont disponibles que sur les tableau
 
 // ^0 correspond à numbers.Count, donc pas d'exclusion ici
 numbers.SequenceEqual(numbers[0..^0]); // true
 
 // commence à numbers[2] inclu et exclu les 2 derniers éléments
 numbers[2..] // --> { 2, 3 }
 
 // on peut aussi utiliser l'index en début de range. Ici on commence au 4ème index en partant de la fin,
 // jusqu'à l'avant-dernier index
 numbers[^4..^2] // --> { 2, 3 }
 ```
 
## Null-coalescing assignment operator ??=
*Depuis:* C# 8
Vous connaissez sûrement l'opérateur ?? qui permet de simplifier les ternaires nulls et a introduit les [throw expressions](https://learn.microsoft.com/fr-fr/cpp/cpp/try-throw-and-catch-statements-cpp) en C# 7.

L'ajout de l'opérateur d'assignation permet de simplifier encore un peu plus la notation.
Voilà des exemples équivalents avec les différents styles pour illustrer: 

```csharp
// avec un if standard
if (myValue is null) {
    myValue = defaultValue;
}

// avec le null-coalescing operator ??
myValue = myValue ?? defaultValue;

// avec le null-coalescing assignment operator ??=
myValue ??= defaultValue
```
 
## Pattern matching is / switch / and / or / not / when
*Depuis:* C# 8 à C# 11 selon les patterns
Partie beaucoup plus large. Le is permet, notamment, de faire des vérifications de type:

```csharp
User user = methodThatReturnAUser();

// version classique avec un typeof
if (user.getType() == typeof(StandardUser)) {
    // traitement ...
}

// version avec le is
if (user is StandardUser) {
    // traitement ...
}
```
 
Plus lisible non ? Je vous laisse regarder [l'opérateur as](https://learn.microsoft.com/fr-fr/dotnet/csharp/language-reference/operators/type-testing-and-cast#as-operator) si vous voulez aussi faire du cast simplement.
 
Autre exemple pour l'utilisation de when et du pattern matching, que vous pouvez retrouver dans ce [repo d'exemple](https://github.com/pitchart/csharp-refactoring-patterns/tree/master/IfElseIfElse) de Julien Vitte.

```csharp
// avec when
public int Calculate(char operation, int number, int value)
{
    return operation.ToOperator() switch
    {
        Operator.Addition => number + value,
        Operator.Substraction => number - value,
        Operator.Multiplication => number * value,
        Operator.Division when value == 0 => throw new InvalidOperationException(),
        Operator.Division => number / value,
        _ => throw new InvalidOperationException(),
    };
}

// avec when et matching
public int Calculate(char operation, int number, int value)
{
    this._operations.TryGetValue(operation.ToOperator(), out IOperation op);
    return op switch
    {
        Division division when value == 0 => throw new InvalidOperationException(),
        null => throw new InvalidOperationException(),
        _ => op.GetResult(number, value),
    };
}
```

Dernier petit point, il est possible de combiner les exemples précédents avec and, or et not pour créer des expressions plus complexes. [Exemple de la doc](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#logical-patterns) de l'utilisation du and et des opérateurs relationnels (<, <=, >, >=) dans une switch expression: 

```csharp
static string Classify(double measurement) => measurement switch
{
    < -40.0 => "Too low",
    >= -40.0 and < 0 => "Low",
    >= 0 and < 10.0 => "Acceptable",
    >= 10.0 and < 20.0 => "High",
    >= 20.0 => "Too high",
    double.NaN => "Unknown",
};
```
 
Il existe d'autres outils pour le pattern matching, je vous laisse regarder [la doc](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#logical-patterns) 
 
Pour terminer, n'hésitez pas à jeter un coup d'œil aux [what's new](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12) des différentes versions de C#, toutes ces améliorations syntaxiques y sont répertoriées.
 
Bon code à tous !