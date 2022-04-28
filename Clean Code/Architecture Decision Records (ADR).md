https://blog.engineering.publicissapient.fr/2019/03/05/architecture-et-documentation-les-adrs/

Une **décision d’architecture** peut être définie comme la réponse à une exigence fonctionnelle ou non-fonctionnelle qui affecte la qualité du système. 
L’ensemble des ADRs d’un projet constitue le **decision log**, un journal des décisions d’architecture.

On peut ajouter ce suivi de décision dans chaque projet de code, pour avoir un trace de ce qui a été fait, et savoir ce qui était pensé pour être péréne ou non

### Titre

### Date

### Statut

### Catégorie

### Décision

### Contexte

### Conséquences

Toute décision est un compromis : si quelque chose est évident, indiscutable, il n’y a pas de décision à prendre et donc pas d’ADR.

Les conséquences positives comme négatives doivent être clairement exprimées. Ces conséquences constituent des opportunités et des risques. Au moment de la rédaction de l’ADR, exprimer clairement ces opportunités et ces risques encourage à décider d’actions appropriées, qui doivent également être documentées. Ceci permettra à un lecteur futur d’estimer si certaines conséquences n’ont pas été anticipées, auquel cas la décision sera vraisemblablement remise en cause.

### Justification

La justification de la décision est un élément qui n’est pas formalisé de manière explicite par tous les modèles d’ADRs, mais souvent diffus dans l’énoncé de la décision elle-même, dans son contexte ou dans ses conséquences.

Définir une partie dédiée est particulièrement pertinent pour une décision qui est la conclusion de la comparaison fine de plusieurs alternatives, par exemple dans le cadre du choix d’un outil. Mais si la définition des critères d’évaluation et l’évaluation de la conformité de chaque solution sont utiles dans certains cas spécifiques, imposer un formalisme aussi lourd pour toutes les décisions nuirait à la légèreté du document.

Quoi qu’il en soit, il est important que la justification de la décision soit précise et factuelle.

### Références externes

### Titre

Un ADR doit être identifiable facilement par un titre.

La numérotation des ADRs de manière unique et séquentielle facilite la définition de références.

### Date

La date de création de l’ADR constitue un élément du contexte. Elle permet également d’ordonner le _decision log_.

### Statut

Le statut permet de déterminer où se trouve une décision par rapport au cycle de vie défini de manière globale. Il s’agit du seul élément de l’ADR qui est susceptible d’évoluer dans le temps.

Le diagramme ci-dessous présente [le cycle de vie proposé par Michael Nygard](http://thinkrelevance.com/blog/2011/11/15/documenting-architecture-decisions).

![Cycle de vie d'un ADR](http://blog.engineering.publicissapient.fr/wp-content/uploads/2019/03/adr_lifecyle.png)

### Catégorie

Certains modèles proposent la définition d’un champ pour la catégorie, qui permet de regrouper les décisions pour en faciliter l’accès.

### Décision

En toute évidence, la décision elle-même doit être énoncée sans ambiguïté. C’est un élément nécessaire car la concision nécessaire à la définition du titre ne permet pas nécessairement d’y inclure l’ensemble des spécificités de la décision.

### Contexte

Le contexte d’une décision comprend plusieurs éléments :

-   Le problème rencontré : pourquoi cette décision s’est-elle avérée nécessaire ?
-   Les contraintes pertinentes dans le cadre de cette décision.
-   Les hypothèses qui ont amené à cette décision.

Les éléments de contexte doivent être pragmatiques : ils peuvent inclure des éléments stratégiques tels que les priorités métier, mais aussi des éléments organisationnels tels que la composition de l’équipe, les problèmes relationnels, etc.

### Conséquences

Toute décision est un compromis : si quelque chose est évident, indiscutable, il n’y a pas de décision à prendre et donc pas d’ADR.

Les conséquences positives comme négatives doivent être clairement exprimées. Ces conséquences constituent des opportunités et des risques. Au moment de la rédaction de l’ADR, exprimer clairement ces opportunités et ces risques encourage à décider d’actions appropriées, qui doivent également être documentées. Ceci permettra à un lecteur futur d’estimer si certaines conséquences n’ont pas été anticipées, auquel cas la décision sera vraisemblablement remise en cause.

### Justification

La justification de la décision est un élément qui n’est pas formalisé de manière explicite par tous les modèles d’ADRs, mais souvent diffus dans l’énoncé de la décision elle-même, dans son contexte ou dans ses conséquences.

Définir une partie dédiée est particulièrement pertinent pour une décision qui est la conclusion de la comparaison fine de plusieurs alternatives, par exemple dans le cadre du choix d’un outil. Mais si la définition des critères d’évaluation et l’évaluation de la conformité de chaque solution sont utiles dans certains cas spécifiques, imposer un formalisme aussi lourd pour toutes les décisions nuirait à la légèreté du document.

Quoi qu’il en soit, il est important que la justification de la décision soit précise et factuelle.

### Références externes