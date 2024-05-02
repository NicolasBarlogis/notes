# Flag Parameter / Argument Anti-Pattern
*Temps de lecture* **4 minutes**

Too Long; Didn't Read;
> Le Flag Argument est un anti-pattern où l'on modifie le comportement comportement d'une fonction selon la valeur d'un booléen passé en entrée (le flag). Il vaut mieux faire deux méthodes explicites. Exemple :
> `void processOfferRequest(OfferRequest offerRequest, bool accepted)`
> donnera
> `void acceptOfferRequest(OfferRequest offerRequest)`
> et
> `void rejectOfferRequest(OfferRequest offerRequest)`
 
Petit post sur un anti-pattern assez méconnu et que l'on fait au final fréquemment. J'ai justement eu un échange dessus hier, ce qui me pousse à vous le partager aujourd'hui 🙂
 
Robert Martin a popularisé il y a certain temps, le terme de [flag argument](https://www.informit.com/articles/article.aspx?p=1392524) dans son livre Clean Code.
Le flag est ici un booléen permettant de changer tout ou partie du comportement de la fonction selon sa valeur.
Typiquement quelque chose comme ça :
```csharp
void processOfferRequest(OfferRequest offerRequest, bool accepted) {
  Offer offer = offerMapper.from(offerRequest);
  if(accepted) {
    // traitement d'acceptation
  } else {
    // traitement de rejet
  }
  offerRespository.save(offer);
}
```

Le problème devient plus apparent à l'usage, où on ne comprend pas l'appel fait à processOfferRequest sans aller regarder sa signature :

```csharp
// sur un point d'entrée où c'est validé
processOfferRequest(offerDto, true);
  
 // sur un point d'entrée où c'est rejeté
procecessOfferRequest(offerDto, false);

// avec un super format d'entrée sur une api générique
processOfferRequest(offerDto, bool.Parse(request.Fields[1]));
```
 
Le dernier appel est particulièrement cryptique
 
La solution pour apporter plus d'expressivité et expliciter le traitement, il suffit de splitter la méthode contenant le flag en deux méthodes :

```csharp
void acceptOfferRequest(OfferRequest offerRequest) {
  Offer offer = offerMapper.from(offerRequest);
  // traitement d'acceptation
  offerRespository.save(offer);
}
void rejectOfferRequest(OfferRequest offerRequest, bool accepted) {
  Offer offer = offerMapper.from(offerRequest);
  // traitement de rejet
  offerRespository.save(offer);
}
```
 
Vous pouvez remarquer ici que quelques lignes peuvent être dupliquées. Si cela vous semble pertinent n'hésitez pas à faire des fonctions pour factoriser ces traitements partagés, même si il faut se méfier d'un [code trop factorisé](https://medium.com/@dr.daler.boboev/dry-code-vs-simple-solutions-unraveling-the-issues-of-too-dry-code-378c3dd0ea96) 🙂
 
Nos signatures sont déjà meilleures, mais c'est surtout au niveau appel qu'on gagne en lisibilité :
```csharp
// sur un point d'entrée où c'est validé
acceptOfferRequest(offerDto);
  
 // sur un point d'entrée où c'est rejeté
rejectOfferRequest(offerDto);

// avec un super format d'entrée sur une api générique
bool isOfferAccepted = bool.Parse(request.Fields[1]);
isOfferAccepted ? 
  acceptOfferRequest(offerDto) :
  rejectOfferRequest(offerDto);
```
Plus clair non ?
 
### Les limites
Attention bien sûr, l'idée n'est pas de dire que [passer un booléen en paramètre est un smell](https://dev.to/rweisleder/the-flag-parameter-anti-pattern-1j82).
Typiquement, tout booléen passé en tant que data (i.e. qui ne modifie pas le comportement de la méthode) est évidemment ok.
Par exemple :
```csharp
// ok
setIsValidated(bool isValidated) {
  _isValidated = isValidated;
}
//--> appel
myObject.setIsValidated(paramValidated);

// overkill et pas forcément plus clair
Validate() {
  _isValidated = true;
}
Reject() {
  _isValidated = false;
}
//--> appel
if(paramValidated)
    myObject.Validate();
else
  myObject.Reject()
```

Sont également acceptables les fonctions qui switch un booléen, comme on en trouve pas mal sur les fronts:
```javascript
let switchBoolean = function(myBool) {
  return !myBool
}
```
 
Comme toujours, il n'y a pas de règle absolue, c'est à vous d'évaluer si le booléen est utilisé en flag qui change le comportement de la fonction ou non 🙂
 
### Vers la violation de l'[open/close principle](https://www.jessesquires.com/blog/2016/07/31/enums-as-configs/), ou l'introduction d'un [switch statement smell](https://refactoring.guru/fr/smells/switch-statements)
Une des autres raisons pour laquelle le flag est un antipattern, est que ce flag peut être un début de violation de l'open close principle des [principes SOLID](https://blog.cleancoder.com/uncle-bob/2020/10/18/Solid-Relevance.html).

Imaginons que notre order puisse maintenant être marqué comme en attente, en plus d'être accepté ou refusé.
On va surement se débarrasser du booléen et introduire une enum avec un statut. Notre méthode de traitement sera alors :
 
```csharp
 void processOfferRequest(OfferRequest offerRequest, OfferStatus status) {
  Offer offer = offerMapper.from(offerRequest);
  switch(status) {
    case OfferStatus.Accepted:
    // traitement d'acceptation
    case OfferStatus.Pending:
    // traitement de mise en attente
    case OfferStatus.Rejected:
    // traitement de rejet
  }
  offerRespository.save(offer);
}
```
 
Et là on est en pleine violation de l'OCP, qu'on va pouvoir [refacto](https://refactoring.guru/fr/smells/switch-statements#:~:text=think%20of%20polymorphism.-,Treatment,-To%20isolate).
 
Du coup vous vous demandez peut être pourquoi on ne refacto pas aussi dans le cas du booléen ?
La réponse est simple, on a que deux cas, donc on évite de généraliser trop tôt, ce qui est source de [complexité accidentelle](https://www.lilobase.me/certaines-complexites-sont-plus-utiles-que-dautres/). 

Le mieux étant de respecter la [rule of three](https://dev.to/jpswade/rule-of-three-1b9d) (ou principe de triangulation du TDD), on n'est pas obligé de généraliser pour le cas du booléen.
 
Bonne refacto à tous 😁