## Objectifs
Refactoriser un code peu compréhensible. La particularité étant que le gros algo comporte de nombreux if/else imbriqués, complexes au premier abord .
L'objectif principal est de refactoriser le code.
Il est également possible par la suite de consulter le fichier "Gilded Rose Requirements Specification" pour voir si tout est respecté, ajouter des tests ayant du sens et reprendre le code pour valider tous les aspects abordés dans le fichier.

## Concepts
* L'approval testing pour ajouter une couverture de TU
* le lift-up conditionnal pour refactoriser les if/else complexe
* l'open-close principle via refacto du switch statement obtenu après le lift-up

## Sources
[Gilded Rose d'Emily Bach](https://github.com/emilybache/GildedRose-Refactoring-Kata)
Explication en vidéo: [Part 1](https://www.youtube.com/watch?v=zyM2Ep28ED8), [Part 2](https://www.youtube.com/watch?v=OJmg9aMxPDI)& [Part 3](https://www.youtube.com/watch?v=NADVhSjeyJA)

## Notes
Le template est dispo ici: https://github.com/NicolasBarlogis/craft-katas/tree/main/GildedRose

## Déroulement du Kata
### 0 - Préface
* Regarder le code
* Lancer l'appli
* Lancer le TU

### 1 - fixer le TU
Fixer le TU:
* corriger le "fixme" en "foo"
* Nommer correctement le TU
Le test reste pas fou, on a besoin de plus.

### 2- Approval test global (C#)
Mise en place d'[[Approval Testing]]
https://approvaltests.com/
**Attention**, approvaltest.net n'est plus maintenu. Bien utiliser [Verify](https://github.com/VerifyTests/Verify/).

* installer le nugget: https://www.nuget.org/packages/Verify.Xunit/
* Créer une classe pour l'approval (ou mettre dans la classe test existante, pas nécessaire de séparer)
* Créer le test:
```C#
namespace GildedRoseTests
{
    [UsesVerify]
    public class Approval 
    {
        [Fact]
        public Task mainProgram()
        {
            StringBuilder fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));

            Program.Main(new string[] { });

            return Verify(fakeoutput.ToString());
        }
    }
}
```
Attention au `[UsesVerify]` et au `Task` en type de return. 
Besoin d'ajouter au csproj la ligne implicitUsings (dans la section PropertyGroup) au projet de test
```xml
<PropertyGroup>
	<ImplicitUsings>enable</ImplicitUsings>
</PropertyGroup>
```
Ou mettre ce using:
```c#
using static VerifyXunit.Verifier;
```
* Copier/coller le contenu du received vers le approved

Empêcher le scrubbing (retirer données par défaut):
https://github.com/VerifyTests/Verify/blob/main/docs/serializer-settings.md
```c#
[UsesVerify]
public class InventoryTest {
	public InventoryTest()     {
	    VerifierSettings.ModifySerialization(settings =>
	        settings.AddExtraSettings(serializerSettings =>
	            serializerSettings.DefaultValueHandling = DefaultValueHandling.Include));
	}              
	                                            
    [Fact]
    public Task item_should_have_valid_values_at_init() {
         var item = new Item { Name = "foo", SellIn = 0, Quality = 0 };
         return Verify(item);
	 }
 }
```

### 2- Approval test global (JS)
Mise en place d'[[Approval Testing]], via les [snapshots](https://jestjs.io/docs/snapshot-testing) Jest.

* Créer une classe pour l'approval (ou mettre dans la classe test existante, pas nécessaire de séparer)
* Créer le test:
```javascript
const App = require("../src/app")

describe("Gilded Rose snap", function() {
  it("should be the same output", function() {
    // capture console.log
    console.stdlog = console.log.bind(console);
    console.logs = [];
    console.log = function(){
      console.logs.push(Array.from(arguments));
	  // console.stdlog.apply(console, arguments);
    }

    let app = new App();
    app.run();

    expect(console.logs.join('\r\n')).toMatchSnapshot();
  });
});
```
Au premier run du test, un dossier `__snapshots__` est généré avec un premier fichier. Les tests suivants seront fait en comparant à ce fichier.

On peut manipuler/mettre à jour facilement le snapshot lors d'un changement, lorsque les tests sont lancés en mode `watch` :
```bash
› 1 snapshot failed.                                                                                         
Snapshot Summary                                                                                              
 › 1 snapshot failed from 1 test suite. Inspect your code changes or press `u` to update them.  

 Watch Usage                                                                                                   
 › Press f to run only failed tests.                                                                          
 › Press o to only run tests related to changed files.                                                        
 › Press p to filter by a filename regex pattern.                                                             
 › Press t to filter by a test name regex pattern.                                                            
 › Press u to update failing snapshots.                                                                       
 › Press i to update failing snapshots interactively.                                                         
 › Press q to quit watch mode.                                                                                
 › Press Enter to trigger a test run.           
```
via `-u` pour accepter automatiquement la nouvelle version, `-i` pour voir les options.

### ~~3 - Transformer le TU en approval test~~
* Transformer le TU en approval équivalent (via le nom)
* Améliorer le TU pour vérifier l'objet en entier (via toString)

### 4 - Vérifier le coverage des tests
Via Rider, ou VS Enterprise, ou [FineCodeCoverage](https://github.com/FortuneN/FineCodeCoverage) ou tout autre outil (C#).
Via Jest coverage `npm run test:coverage` puis visualisation dans un outil --> non testé (JS)

![[vs-features.png]]

Normalement on voit que l'approval test couvre 100% du `UpdateQuality`, qui est la méthode que l'on souhaite refactorer.
Si on n'a pas le 100%, on utilise la méthode de l'étape "Bonus 2".

### 5 - lift up conditional
On veut refactorer le `UpdateQuality`, pour séparer la logique en un traitement par nom. Après ça on pourra  refactorer les ifs en switch puis via le Replace Conditional with Polymorphism ou équivalent.

Pour le lift up, on fait un if/else où la condition est `item.Name == "X"`  où le X est une des valeurs possible du Name.
Dans le if et dans le else, on met l'entiéreté du code initial qu'il y a dans le for. Forcément on a du code redondant/mort, c'est l'objectif.

On fait tourner les TU pour obtenir le coverage. Les zones rouges peuvent être retirées intelligement.

Quand on a plus de rouge, on a isolé un des noms. On répéte l'opération dans le else, avec un autre nom. Répéter jusqu'à avoir isolé tous les noms.

### 6 - Refactorer le if
* Remonter les if/else imbriqués
* Convertir en switch

### 7 - Extraire la logique
* Extraire & factoriser les concepts métier (quality > 50...)
* Créer des tu pour les règles métier (en plus du test de non regression)

### 8 - Respecter l'open/close
* Extract method pour isoler le switch
* Move method pour repousser le switch dans item
* Replace Conditional with Polymorphism, un cas par un cas, en fixant les tests à chaque étape (UpdateQuality, avec `virtual`/`override` pour C#) --> fix le smell Switch Statement, qui est également une violation de l'open/close principle

9 - Mettre en place des tests fonctionnels

10 - Ajouter la nouvelle fonctionnalité

### Bonus 1 - Middle Man
En finissant le 6, quel reste l'intêret de la class GlidedRose ?

### ~~Bonus 2 - Code coverage avec le CombinationApprovals~~
Pas d'équivalent au `CombinationApprovals.VerifyAllCombinations` de ApprovalTest. Donc ici pas d'autre choix que de multiplier les TU (éventuellement via une Theory)

### Bonus 3 - Vérifier la bonne application des requirements 

### Bonus 4 - Ajouter la gestion des objets invoqués

## Tags
#moyen
