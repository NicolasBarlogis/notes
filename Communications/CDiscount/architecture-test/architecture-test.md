# Tests d'architecture - Archunit
*Temps de lecture* **6 minutes**

Aujourd'hui, on voit un type de test pas forcément très connu, les tests d'architecture.
 
Too Long; Didn't Read;
> Les tests d'architectures permettent de mettre des validations automatiques du respect de vos règles d'architecture (MVC, Clean Archi,...) et certaines autres pratiques (ex: ne pas utiliser certaines classes, conventions de nommage).
> Ils à réduire la dette technique mais ne sont pas intéressants à mettre en place sur tous les projets.
> Il existe des librairies permettant de faciliter leur écriture:
> * [Archunit](https://www.archunit.org/) (Java)
> * [ArchunitNet](https://archunitnet.readthedocs.io/en/latest/) (C#)
> * [ts-arch](https://github.com/ts-arch/ts-arch) (JS/TS)
 
## Pour tester quoi ?
**Le respect des couches**
Que votre projet soit basé sur une base MVC, d'architecture hexagonale, de clean architecture ou n'importe quel autre modèle plus ou moins standard, la séparation des couches permet notamment:
* D'avoir un fonctionnement standardisé des différents flux de l'application --> réduction de charge mentale (avoir compris la structure d'un flux == avoir compris la structure de tous les flux)
* De limiter les dépendances non nécessaires, qui augmentent le couplage et réduisent la réutilisabilité des composants de code

Automatiser le contrôle du respect de ces règles est la base des tests d'architecture. Cela se fait via des tests, qui vont typiquement ressembler à ça pour les API avec des couches API, infra, service et domaine:
 ```java
 public class LayeredArchitectureTests {
    private final JavaClasses classes = new ClassFileImporter().importPackages("archunit.layered");

    @Test
    public void domainLayerRules() {
        noClasses().that()
          .resideInAPackage("..domain..")
          .should()
          .dependOnClassesThat().resideInAnyPackage("..api..","..infrastructure..","..service..")
          .check(classes);
    }

    @Test
    public void ServiceLayerRules() {
        noClasses().that()
          .resideInAPackage("..service..")
          .should()
          .dependOnClassesThat().resideInAnyPackage("..api..","..infrastructure..")
          .check(classes);
    }

    @Test
    public void APILayerRules() {
        noClasses().that()
          .resideInAPackage("..api..")
          .should()
          .dependOnClassesThat().resideInAPackage("..infrastructure..")
          .check(classes);
    }
}
 ```
L'exemple complet est disponible en [Java](https://github.com/katalogs/learning-hours/tree/main/archunit/solution-java/src/test/java) et en [C#](https://github.com/katalogs/learning-hours/tree/main/archunit/solution/ArchUnit-kata-tests)
 
## Faire respecter les conventions
Idem pour ce qui est convention de nommage ou de code (ex: ne pas utiliser la classe x pour lui préférer y, toujours passer par une interface, ...). 

Il est possible de les contrôler lors des code review, mais automatiser cela permet de limiter le temps de relecture et d'éviter les oublis:
```csharp
public class ServiceTests
{
    [Fact]
    public void ClassThatExtendsBaseServiceShouldBeSuffixedByService()
    {
        Classes().That()
            .AreAssignableTo(typeof(BaseService))
            .Should().HaveNameEndingWith("Service")
            .Because("Services should have Service as suffix")
            .Check();
    }
  
    [Fact]
    public void ServicesShouldResidesInServicesFolder()
    {
        Services()
            .Should().ResideInNamespace("Cdiscount.Controllers.Base.Services.*", true)
            .Because("Services should be in Services folder")
            .Check();
    }
}
```
Exemple complet [ici](http://tfs:8080/tfs/DefaultCollection/Seller/_git/sellerzone-site?version=GBflashteam%2FNBA-architecture_tests&path=%2FSellerZone.Architecture.Tests.Unit%2FTests%2FServicesTests.cs)
 
Il existe bien d'autres cas d'usage pour ces tests (anti-patterns, dépendances cycliques, ...).
Je vous laisse regarder les exemples des différentes lib ([Java](https://github.com/TNG/ArchUnit-Examples), [C#](https://github.com/TNG/ArchUnitNET/tree/main/ExampleTest) et [JS](https://github.com/ts-arch/ts-arch/tree/main/test)) pour trouver de l'inspiration 🙂
 
## Pour quels projets ?
Les tests d'architecture représentent une couche supplémentaire à implémenter et entretenir, et donc un coût supplémentaire. Ils ne sont donc pas adaptés à tous les projets. Il est important de ne considérer ce genre de tests que dans les cas où la valeur ajoutée dépasse ces coûts.
 
### Projets impliquants de nombreux développeurs
Les bases de code maintenues par plusieurs équipes demande plus d'attention que les autres pour éviter que [l'entropie logicielle](https://lilobase.wordpress.com/2014/05/27/lentropie-logicielle-pourquoi-la-dette-technique-ne-fait-quaugmenter/) ne rende le code inexploitable.

Les reviews de code permettent de limiter l'ajout de dette technique, mais demandent un temps important sur ces projets complexes et volumineux.

Convenir de règles est une première étape (organisation des couches, quelle classe peut appeler quelle autre, gestion des exceptions, des logs, ...), pour éviter que chaque relecteur n'amène un point de vue trop subjectif.
Une fois cette effort fait, automatiser ces vérifications permet de gagner du temps de relecture tout en assurant la cohérence dans le temps de la base de code.
 
### Projets à fort turn-over
La connaissance se perdant, il est facile que la doc des bonnes pratiques finisse oubliée dans confluence. Dans ce cas là, quelques tests d'architecture peuvent limiter l'augmentation de la dette technique liée au turn-over mais aussi aider les nouveaux dévs en leur indiquant automatiquement les règles en vigueur sur le projet (même si en pratique ce genre de tooling n'est pas la priorité pour une équipe en difficulté, il n'empêche que c'est à ce moment que ça devient très utile).
 
### Projets stratégiques / importants
Un projet pérenne et crucial devra rester maintenable dans le temps, et ne pas subir de baisse de vélocité liée à l'accumulation progressive de dette. Dans ces cas là, même si peu de développeurs sont impliqués, la mise en place de tests d'archi peux se justifier.
 
### Projets legacy importants
Dans le cas de reprise d'assets legacy, les tests d'architecture peuvent avoir un intérêt double: 
1. **Lister la dette**. Les tests seront cassés, mais une liste exhaustive de toutes les violations ressortira, ce qui aide à la mise en place d'un backlog tech pour réduire la dette technique
2. **Arrêter l'hémorragie**. Même si la dette met un temps certain à être résorbée, il est quand même possible de mettre en place les tests d'architecture en mode bloquant. Archunit peut [freezer un test](https://www.archunit.org/userguide/html/000_Index.html#_freezing_arch_rules). Lors du premier jeu du test, les violations actuelles seront enregistrées dans un fichier. Par la suite le test ne cassera plus pour ces anciens problèmes. Seules de nouvelles violations entraineront l'échec du test
 
## Les règles sont vivantes
Enfin, dernier point important, l'architecture d'un projet n'est jamais figée. Les règles vont évoluer au fil du temps, c'est pour ça qu'il est important de faire de temps en temps le point dessus et potentiellement de faire évoluer les tests pour représenter l'évolution de ces règles.

C'est aussi l'occasion de documenter ces changements avec un [Architecture Decision Record](../architecture-decision-record/architecture-decision-record.md) 😁 😁