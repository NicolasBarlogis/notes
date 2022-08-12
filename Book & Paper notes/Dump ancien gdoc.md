
# Liste non revue

-   The mythical man-month: essays on software engineering (source: The Pragmatic Programmer: your journey to mastery, 20th Anniversary Edition)
    
-   Peopleware: Productive projects and teams (source: The Pragmatic Programmer: your journey to mastery, 20th Anniversary Edition)
    
-   Dinosaur Brains: dealing with all those impossible people at work (source: The Pragmatic Programmer: your journey to mastery, 20th Anniversary Edition)
    
-   Structured Design: Fundamentals of a discipline of computer program and Systems Design (source: The Pragmatic Programmer: your journey to mastery, 20th Anniversary Edition)
    
-   The practice of programming (source: The Pragmatic Programmer: your journey to mastery, 20th Anniversary Edition)
    
-   Clean Code: A Handbook of Agile Software Craftsmanship (source: Julien Vitte, coach craft)
    
-   The Software Craftsman: Professionalism, Pragmatism, Pride (source: Julien Vitte, coach craft)
    
-   Extreme Programming Explained: Embrace Change (source: Julien Vitte, coach craft)
    
-   Technical Agile Coaching with the Samman Method (source: Julien Vitte, coach craft)
    
-   Accelerate: The Science Behind Devops: Building and Scaling High Performing Technology Organizations (source: Julien Vitte, coach craft)
    
-   Domain Modeling Made Functional: Tackle Software Complexity With Domain-Driven Design and F# (source yoan courtel) + https://fsharpforfunandprofit.com
    
-   The Manager's Path: A Guide for Tech Leaders Navigating Growth and Change  
- 
https://www.amazon.com/Scrum-Doing-Twice-Work-Half/dp/038534645X

  

# Liste revue

Must read

  

Should read

  

Could read

  

Won’t read

  

# Liste lue

## The Pragmatic Programmer: your journey to mastery, 20th Anniversary Edition (source: Julien Vitte, coach craft)

### Acronymes & concepts:

-   ETC: Easy to Change. Quand on écrit du code, de la doc ou autre, toujours faire en sorte que ce soit ETC
    
-   DRY: Don’t Repeat Yourself: ne pas faire de répétition de code, de fonctionnalité, de but, etc…
    
-   Systèmes orthogonaux: deux systèmes sont orthogonaux (orthogonal systems) si les modifs sur l’un n’affectent pas l’autre (cf découplage, modulaire, layered, component-based, ...). Peut s’appliquer, au code, test, doc, ...
    
-   Loi de Déméter: découplage, un module/objet devrait exposer le moins de choses possibles de sont fonctionnement. Càd gérer les spécificité du module/classe dans celui-ci
    
-   Tracer bullets: balles lumineuses pour visualiser l’endroit d’impact et pouvoir ajuster. Principes du MVP, sortir un core concept le plus simple possible pour pouvoir mesurer puis itérer/ajuster. Valable avec tout, test early. Différent du prototype, on veut ici plutôt voir l’intégration / avoir des retours
    
-     
    

  
  

## Accelerate: The Science Behind Devops: Building and Scaling High Performing Technology Organizations (source: Julien Vitte, coach craft)

Plus important: https://itrevolution.com/24-key-capabilities-to-drive-improvement-in-software-delivery/

### Capabilities vs Maturity

Modèles par maturité posent plusieurs problèmes:

-   Donnent l’impression qu’il y a une fin au processus d’amélioration
    

-   Les capacités doivent être maintenues / améliorées pour les garder
    

-   Généralisent toutes les situations en niveaux de maturité (peu de prise en compte des spécificités)
    

-   le découpage en capacités permet de s’adapter (différentes parties orga prisent en charge par différentes composantes de l’entreprise) et se concentrer sur des capacités à acquérir/améliorer
    

-   La maturité est mesurée par outils/connaissances. Cela peut mener à des métriques uniquement cosmétiques
    

-   Capacités basées sur les résultats
    

-   Les modèles par maturité s’intéressent beaucoup aux solutions, ce qui peut verrouiller les technologies et freiner l’évolution / adaptation.
    

-   Maintien des capacités demandent évolution constante
    

### Mesure de la performance![](https://lh6.googleusercontent.com/2bLfIA6KiAvv1SvCnBj80GDCA-A7PZ6gh08B6ZlNoN1lw0IHF3hmzz6vhkuPyJxpgGZcWViHqPUpNXIajJvuiN7NV-wjPBjo01DguyhMu1IUUtBZs520xBl9SNWm3YgMXERXbqXCuXws7eV72CiUxg)

Outcomes plutôt que outputs, team/global plutôt qu’individu/local. Avant: 

-   lignes de code (logique production usine, pas d’indication qualité/erreur/utilité ?)  
    [exemple](https://www.folklore.org/StoryView.py?story=Negative_2000_Lines_Of_Code.txt)
    
-   vélocité (pas forcément comparable entre équipe réellement, facilement manipulable)
    
-   Utilization, taux d’occupation des ressources. Mais 100% occupation → lead time infini. Pas de place à l’adaptation.
    

  

### Développement stratégique

The fact that software delivery performance matters provides a strong argument against outsourcing the development of software that is strategic to your business, and instead bringing this capability into the core of your organization. [...]

In contrast, most software used by businesses (such as office productivity software and payroll systems) are not strategic and should in many cases be acquired using the software-as-a-service model.

Distinguishing which software is strategic and which isn’t, and managing them appropriately, is of enormous importance. This topic is dealt with at length by Simon Wardley, creator of the Wardley mapping method (Wardley 2015).

  

### Types de culture

Abordé dans devops handbook. Modèle de Westrun

![](https://lh6.googleusercontent.com/RdmFXetCZ6JVwq_VppSFKqFuZ8JY4dRLb_GHTVmvZpXU-4ZHK5pUVbB2KcoBoAF_AFtBl1R7ApUCbJvcbu8WyTau8Xacl1XqQ3BF5UfxRo2DbAEtzkfi9wUdKl3xxJxZdRBBEHQ_qlewbykkNss1lw)

### Continuous Delivery

Principes clés

-   Build quality in
    
-   Work in small batches
    
-   Computers perform repetitive tasks; people solve problems
    
-   Relentlessly pursue continuous improvement
    
-   Everyone is responsible
    

Besoin de fondations:

-   Comprehensive configuration management
    
-   Continuous integration (CI)
    
-   Continuous testing
    

Ce qui marche

-   Version control pour tout
    
-   Test automation
    
-   test data management
    
-   trunk-based development
    
-   information security
    
-   continuous delivery
    

  

### Types de systèmes / logiciels:

-   Greenfield: new systems that have not yet been released
    
-   Systems of engagement (used directly by end users)
    
-   Systems of record (used to store business-critical information where data consistency and integrity is critical)
    
-   Custom software developed by another company
    
-   Custom software developed in-house
    
-   Packaged, commercial off-the-shelf software
    
-   Embedded software that runs on a manufactured hardware device
    
-   Software with a user-installed component (including mobile apps)
    
-   Non-mainframe software that runs on servers operated by another company
    
-   Non-mainframe software that runs on our own servers Mainframe software
    

# Méthodes / outils

## Wardley mapping method

Source: Accelerate: The Science Behind Devops: Building and Scaling High Performing Technology Organizations

Une carte Wardley est une carte de stratégie commerciale. Permet de déterminer quels sont les composants stratégiques, et lesquels ne sont pas spécifiques.

  
  
  
  

# Termes

-   [Ubiquitous Language](https://martinfowler.com/bliki/UbiquitousLanguage.html) (DDD): the practice of building up a common, rigorous language between developers and users. This language should be based on the [Domain Model](https://martinfowler.com/eaaCatalog/domainModel.html) used in the software
    
-   [Domain Driven Design](https://www.amazon.com/gp/product/0321125215/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=0321125215&linkCode=as2&tag=martinfowlerc-20)
    
-   Supple Design (DDD): building that code in a way that is easy to understand, modify and extend, in sum: code that is maintainable
    

-   Intention revealing interfaces: le nom des interfaces (méthodes, classes, …) doit être transparent et suffisant pour comprendre leur rôle/action (Ubiquitous Language)
    
-   Side effect free functions: fonction pas de modif, procédure le peuvent
    
-   Assertions: programmation par contrat dans le langage ou tests automatisés (TDD)
    
-   Conceptual Contours: As we further understand the domain, we will better understand what are the key elements in it. These key elements are the ones we must decompose into highly cohese and decoupled code units (operations, interfaces, classes and AGGREGATES)  
    The end goal is to have a set of simple interfaces that combine and match with the domain UBIQUITOUS LANGUAGE, without having the burden of options that are irrelevant to the client developer and client code.
    
-   Standalone Classes: an extreme example of low coupling, it's a class that is completely self contained and can be studied, understood and tested alone
    
-   Closure of Operations: When possible we should define an interface of an operation such that its arguments and return type are the same.
    

-   Rugged movement (DevSecOps): Team up your release and security engineers
    
-   Golden Master: Le Golden Master Testing permet de sécuriser la refonte d’une application en appliquant une couche de test sur l’application initiale avant toute modification.
    
-   Outside-In TDD: adding end-to-end tests that are written as part of a second loop  
    ![](https://lh6.googleusercontent.com/1n-FSnFkA6ZZRJ1yQ0S2pWU_qBU3xVkfYCyKx5N7hAR3tDAkF7-AD5sSsYEIrd5K7ncL_bjGS6odHEjDsusDIaO9ZphXPrHy7w4p8B6A7MPvD6tXEhZ1QpfNEjlYO-Ths-fAMUIIBj4RWFevmFdpqA)
    
-   SOLID:
    

-   [Responsabilité unique](https://fr.wikipedia.org/wiki/Principe_de_responsabilit%C3%A9_unique) (Single responsibility principle): une [classe](https://fr.wikipedia.org/wiki/Classe_(informatique)), une fonction ou une méthode doit avoir une et une seule responsabilité
    
-   [Ouvert/fermé](https://fr.wikipedia.org/wiki/Principe_ouvert/ferm%C3%A9) (Open/closed principle): une entité applicative (classe, fonction, module ...) doit être fermée à la modification directe mais ouverte à l'extension
    
-   [Substitution de Liskov](https://fr.wikipedia.org/wiki/Principe_de_substitution_de_Liskov) (Liskov substitution principle): une instance de type T doit pouvoir être remplacée par une instance de type G, tel que G sous-type de T, sans que cela ne modifie la cohérence du programme
    
-   [Ségrégation des interfaces](https://fr.wikipedia.org/wiki/Principe_de_s%C3%A9gr%C3%A9gation_des_interfaces) (Interface segregation principle) : préférer plusieurs interfaces spécifiques pour chaque client plutôt qu'une seule interface générale
    
-   [Inversion des dépendances](https://fr.wikipedia.org/wiki/Inversion_des_d%C3%A9pendances) (Dependency inversion principle): il faut dépendre des abstractions, pas des [implémentations](https://fr.wikipedia.org/wiki/Impl%C3%A9mentation)
    



**