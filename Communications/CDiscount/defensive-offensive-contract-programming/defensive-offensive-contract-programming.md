# Programmation défensive, offensive et par contrat
*Temps de lecture* **12 minutes**

Aujourd'hui on va parler de philosophie du code et de styles de programmation.

![Visible confusion](https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExanhpdWhvNmU1MGUxYXB0N3k0ZGpnMjMyaGpjMGthaXZwa2RkOXd6ZyZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/1oJLpejP9jEvWQlZj4/giphy.gif)

Too Long; Didn't Read;
> Il existe plusieurs styles de programmation, qui amènent plusieurs philosophies de gestion des erreurs au sein d'une méthode et d'une application.
> * La [programmation défensive](https://wiki.c2.com/?DefensiveProgramming) cherche à gérer tous les cas problématiques, prévisibles ou non, et si possible à récupérer pour continuer l'exécution
> * La [programmation offensive](https://wiki.c2.com/?OffensiveProgramming), variante de la défensive, va aussi détecter tous les problèmes, mais préférera arrêter l'éxécution et hurler qu'il y a eu un problème
> * La [programmation par contrat](https://wiki.c2.com/?DesignByContract) est basée sur la confiance et ne cherchera pas à détecter les erreurs. L'appelant devra respecter les préconditions pour que la méthode retourne le résultat attendu

Avant de démarrer, commençons par différencier deux types d'erreurs:
 1. Les erreurs imprévisibles, qui viennent de l'environnement extérieur. Une entrée utilisateur invalide, la perte du réseau, un disque plein, ... Même improbables, ces problèmes finissent par arriver ([certains les provoquent](https://netflixtechblog.com/the-netflix-simian-army-16e57fbab116)).
 2. Les erreurs évitables, ou erreurs internes. Passer une référence nulle, une valeur hors scope, valeur de retour non prévue, ... Ces erreurs viennent directement de bugs ou d'oublis dans le code développé.
 
## Programmation défensive
L'idée de ce style est d'être un peu paranoïaque. On ne fait confiance à personne, ni à ceux qui appellent nos composants (classe, méthode, api, ...), ni au système qui l'exécute, et encore moins aux inputs venant de l'extérieur. On cherche donc à se couvrir des deux types d'erreurs définis ci-dessus.
Les paramètres entrants sont vérifiés, les exceptions catchées et traitées. Tous les systèmes externes (bdd, api, messaging, systéme d'authent, le hardware même, ...) sont [considérés faillibles](https://www.goodreads.com/quotes/8805878-the-major-difference-between-a-thing-that-might-go-wrong) et tous les appels à ces systèmes devront prendre en compte ces potentiels problèmes. 

Cette solution va notamment entraîner des doublons de validation. Exemple, pour une API web multi-layer, on validera un paramètre à l'entrée dans le contrôleur, dans un éventuel service, dans la couche infra, ...et même jusque dans l'entité métier, encore plus si l'on souhaite respecter le principe d'[always valid domain model](https://enterprisecraftsmanship.com/posts/always-valid-domain-model/) du DDD.
Cela assure néanmoins que chacun des composants puissent être réutilisés indépendamment des autres, sans risque de laisser passer une erreur.

Enfin, afin d'assurer la bonne continuité de l'application, la programmation défensive veut que l'on essaye le plus possible de continuer malgré les erreurs. Cela peut par exemple se traduire par:
*  Des valeurs par défaut pour certains champs (quand pertinent)
*  Des retry en cas d'erreur réseau sur des call de bdd ou d'api externes
*  La mise en place de buffer d'attente, etc...

Cette pseudo route simplifiée est un exemple de programmation défensive, on attrape toutes les exceptions et on met en place un retry sur l'enregistrement
```csharp
// --- ProductControler ---
[HttpPost("/product")]
public asyncTask<IActionResult> createNewProduct([FromBody] ProductDto productDto)
{
    // validation du contrat (simplifiée, on préférera donner un message détaillé selon qu'il manque)
    if(string.IsNullOrEmpty(productDto.Name) ||  string.IsNullOrEmpty(productDto.Gtin)) {
        return BadRequest();
    }
    
    try {
        Product product = new (productDto.Name, productDto.Gtin, productDto.Price);
        await productRepository.createProduct(product);
        return Created();
    } catch(InvalidProductException productException){
        return BadRequest();
    } catch(Exception e) {
        return StatusCode(500);
    }
}

// --- ProductRepositoryMongoDB ---
public async Task createProduct(product) {
    // re-validation du produit entrant
    try {
        ProductMongoModel mongoProduct = productMongoModelMapper.from(product);
    } catch(MappingException e) {
        throw new InvalidProductException();
    }
    
    // mécanisme de retry, aussi possible d'utiliser les retryable-writes de mongo dans ce cas précis
    try() {
        await retryCreateProduct(product, 0, NB_MAX_MONGO_RETRY);
    }
}

private async Task retryCreateProduct(product, retryCount, maxRetry) {
    if(retryCount > maxRetry) {
        throw new IOException();
    }
    
    try {
        await _productCollection.InsertOneAsync(product);
    } catch(TimeoutException e) {
        retryCreateProduct(product, retryCount + 1, maxRetry);
    }
}

```

**Avantages**
* Application plus sûre et robuste
* Composants (classes, méthodes, ...) réutilisables sans avoir besoin de maîtriser leur fonctionnement

**Inconvénients**
* Application plus complexe
* Plus de code et de tests à maintenir
* Multiplication des cas extrêmes (combinaison des mécanismes de backup des nombreux composants)
* Beaucoup de contrôles et d'assertions, probablement dupliqués

### Programmation offensive
Contrairement à ce que son nom pourrait laisser penser, la programmation offensive n'est pas l'opposé de la programmation défensive, mais une variante de celle-ci.
Là où la programmation défensive va tenter de gérer proprement les cas d'erreurs improbables (via retry, default value, ...), en programmation offensive on préférera [planter le plus tôt possible](https://medium.com/@mattklein123/crash-early-and-crash-often-for-more-reliable-software-597738dd21c5), tout en le faisant savoir, et donc de [ne pas cacher les erreurs](https://digitaldrummerj.me/dont-swallow-the-exceptions/) via des mécanismes de reprise. 

L'idée est de simplifier le debug en loggant les erreurs, de ne pas faire exploser la complexité du soft (crasher est beaucoup plus simple que de gérer des mécanismes de reprises) et aussi de limiter les effets de bord inattendus, qui peuvent entraîner des problèmes tels que la corruption de données. 
Cela 
Car même en pensant à tous les problèmes possibles et comment les gérer, il y aura forcément un cas ou une combinaison d'événements fortuis que l'on ne gérera pas. La programmation offensive cherche donc à éviter ces situations en arrêtant le traitement au moindre problème.

On aurait alors ceci:
```csharp
// --- ProductRepositoryMongoDB ---
public async Task createProduct(product) {
    // re-validation du produit entrant
    try {
        ProductMongoModel mongoProduct = productMongoModelMapper.from(product);
    } catch(MappingException e) {
        // l'idée est d'ajouter des infos pour faciliter la reproductibilité et le debug.
        logger.error("Problème de mapping");
        throw new InvalidProductException();
    }
    
    try {
        // plus de retry, on throw en cas de problème. Il est aussi tout à fait possible de juste laisser l'erreur se propager pour la traiter plus haut dans la stack
        await _productCollection.InsertOneAsync(product);
    } catch(Exception e) {
        logger.error("Erreur enregistrement");
        throw e;
    }
}
```

**Avantages**
* Plus simple à débugger car verbeux
* Composants (classes, méthodes, ...) réutilisables sans avoir besoin de maîtriser leur fonctionnement
* Moins de risque d'effet de bord ([mieux vaut ne rien faire que faire quelque chose de mauvais](https://massimo-nazaria.github.io/no-code.html))

**Inconvénients**
* L'application est moins robuste
* Beaucoup de contrôles et d'assertions, probablement dupliqués


### Programmation Offensive Défensive
[Petite variante](https://wiki.c2.com/?OffensiveDefensiveProgramming) consistant à combiner le côté gestion gracieuse des cas problématiques de la programmation défensive, tout en reportant systématiquement les problèmes comme le fait la programmation offensive.
On aura le côté verbeux des erreurs pour les dévs tout en essayant d'absorber les erreurs pour les utilisateurs.
C'est le principe du [fail silently](https://wiki.c2.com/?ReportBugsSilently).

Pour notre Repository mongo, cela reviendrait à conserver le mécanisme de retry, mais en loggant chaque tentative pour que les développeurs sâchent que tout ne se passe pas bien, même si l'application fonctionne du point de vue des utilisateurs:

```csharp
// --- ProductRepositoryMongoDB ---
public async Task createProduct(product) {
    // re-validation du produit entrant
    try {
        ProductMongoModel mongoProduct = productMongoModelMapper.from(product);
    } catch(MappingException e) {
        logger.error("Problème de mapping")
        throw new InvalidProductException();
    }
    
    // mécanisme de retry, aussi possible d'utiliser les retryable-writes de mongo dans ce cas précis
    try() {
        await retryCreateProduct(product, 0, NB_MAX_MONGO_RETRY);
    }
}

private async Task retryCreateProduct(product, retryCount, maxRetry) {
    if(retryCount > maxRetry) {
        logger.error("Abandon retry")
        throw new IOException();
    }
    
    try {
        await _productCollection.InsertOneAsync(product);
    } catch(TimeoutException e) {
        logger.warning("Timeout mongo, retry lancé")
        retryCreateProduct(product, retryCount + 1, maxRetry);
    }
}
```

**Avantages**
* Application plus sûre et robuste
* Plus simple à débugger car verbeux
* Composants (classes, méthodes, ...) réutilisables sans avoir besoin de maîtriser leur fonctionnement

**Inconvénients**
* Plus de code et de tests à maintenir
* Multiplication des cas extrêmes (combinaison des mécanismes de backup des nombreux composants)
* Beaucoup de contrôles et d'assertions, probablement dupliqués

## Programmation par contrat
La programmation par contrat est elle l'opposé de la programmation défensive.
On choisit de faire confiance et on arrête de douter de l'appelant. Si nos composants sont appelés correctement (càd avec les bons entrants), alors ils s'engagent à fournir un résultat correct. Dans le cas contraire, il n'y a aucune assurance sur ce qui va se passer. Le code peut crasher ou renvoyer un résultat invalide, ce comportement n'est ni documenté ni testé, car c'est à l'appelant de vérifier qui ne tombe pas dans ces cas là.

On formalise généralement cela sous forme de préconditions et de postconditions. L'idée du contrat est de dire que si l'appelant respecte les préconditions, alors l'appelé s'engage à respecter les postconditions.

De [nombreux langages](https://en.wikipedia.org/wiki/Design_by_contract#Language_support) supportent la programmation par contrat (nativement ou via extension), soit pour la génération de documentation, soit sous forme d'assertions qui aideront à valider le contrat lors du développement.

Ces contrats sont adaptés pour la gestion des erreurs évitables. Malheureusement, les erreurs imprévisibles (perte connexion, input utilisateurs, ...) sont par nature imprévisibles. Il vaut mieux continuer à les gérer défensivement ou offensivement.

Si l'on reprend notre exemple, on pourrait pousser la notion à l'extrême (càd sans contrôle des erreurs imprévisibles) avec un code qui ressemblerait à ça :
```csharp
// --- ProductControler ---
/* Create a new product
 * preconditions
 * * product name should be between 3 and 30 characters long, using only latin alphabet
 * * the gtin must respect the standard gtin format
 * * the price must be superior to 0
 * postconditions
 * * the product is succesfuly created
 */
[HttpPost("/product")]
public asyncTask<IActionResult> createNewProduct([FromBody] ProductDto productDto)
{
    Product product = new (productDto.Name, productDto.Gtin, productDto.Price);
    await productRepository.createProduct(product);
    return Created();
}

// --- ProductRepositoryMongoDB ---
/* Save a new product in the mongo collection products
 * preconditions
 * * product name should be between 3 and 30 characters long, using only latin alphabet
 * * the gtin must respect the standard gtin format
 * * the price must be superior to 0
 * postconditions
 * * the product is succesfuly saved in the collection
 */
public async Task createProduct(product) {
    await _productCollection.InsertOneAsync(product);
}
```

**Avantages**
* Code plus simple (pour l'appelé)
* Code documenté

**Inconvénients**
* Potentiellement beaucoup de doc pour les contrats importants
* Plus difficile à débugger (sauf si on utilise une lib qui traduit les pré/post conditions sous forme d'assertions)
* Repousse la responsabilité de validation des entrées à l'appelant, donc possible duplication si plusieurs appelants
* Risque de bugs non prévus si l'appelant n'est pas sérieux / ne respecte pas le contrat

## Quel style est le meilleur
Comme toujours, **ça dépend**.
Vous l'avez sûrement compris avec les exemples caricaturaux des différents exemples, aucun programme réel n'est entièrement défensif, offensif ou contractuel.

L'idée est que vous choisissiez en pleine conscience quel style adopter pour chacun de vos composants.
Par exemple, il est souvent pertinent d'être offensif dans les contrôleurs pour ce qui est de la gestion des entrées utilisateurs (le fameux [never trust user input](https://dev.to/abbeyperini/web-security-101-part-2-user-input-j20)).
Mais on pourrait aussi imaginer ne pas faire de contrat et faire confiance à l'appelant pour ces validations, lorsqu'il s'agit d'une route privée qui sera consommée par notre équipe et que nous avons confiance dans le fait que les validations ont déjà été faites. 

Idem pour des mécanismes de gestion d'erreurs ou de valeur par défaut. Ils sont coûteux à mettre en place, potentiellement gourmands en ressources et ne doivent être utilisés quand quand c'est pertinent.
Il n'y a peut être pas besoin de faire du retry pour une action que l'on sait qui va être répétée très fréquement et où une petite latence est tolérable niveau métier.
Au contraire, certains contextes plus sensibles vont l'utiliser.

Enfin dernier exemple, lorsque l'on sait que les couches API et domaine ont fait les validations, est-ce nécessaire de les refaire aux niveaux applications et infra ? Lorsqu'une même équipe est responsable de l'appelant et de l'appeler, la programmation par contrat permet de réduire le volume de code produit, en échange d'une plus grande rigueur dans le développement (coder le contrat [sous forme d'assertions](https://learn.microsoft.com/fr-fr/visualstudio/debugger/assertions-in-managed-code?view=vs-2022&tabs=csharp) peut aussi aider au développement) 

---

C'est tout pour cet article un peu long. J'espère que ça vous a intéressé et que vous commencerez à choisir de façon consciente comment se comportera votre code :)

Et comme d'habitude, n'hésitez pas à faire des retours ou poser des questions, je suis très preneur de feedback :p 
