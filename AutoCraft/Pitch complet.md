# Objectifs
Objectif 1 - Aide aux coachs
* Trouver automatiquement des smells de haut niveaux (quitte à faire des faux négatifs), pour accélérer la lecture des bases de code
* Aider à trouves les problématiques inter bases de code. Exemple: bounded context non respecté (des régles métier d'un même objet sont éclatées dans plusieurs applications).
* Trouver les potentiels optimisations de haut niveau pour la maintenabilité du code: application trop gross, ou au contraire trop fractionnées (micro services tellement petit que la logique métier est explosée à plein d'endroit et personne n'en est vraiment maitre)

Objectif 2 - Aide aux dévs / ventes accompagnement coachs
* Proposer la détection des smells des coachs et autres fonctionnalités, avec des articles explicatifs sur pourquoi ils sont problématiques, comment les résoudre, et proposer de l'aide pour leur contexte spécifique (intervention coach)

Objectifs 3 - Aide aux métiers / technique haut niveau
* Chat basé sur une ou plusieurs base de code --> limite le besoin d'accès au dévs pour obtenir des infos rapidement sur règles métier/spécificités techniques
* Fixer un unique ubiquitous language, en se basant / améliorant les 
* Relecture d'US pour vérifier l'utilisation de l'ubiquitous language

# Fonctionnalités
Toutes ces fonctionnalités peuvent potentiellement s'appliquer à une base de code, ou à plusieurs en même temps, qui composent ensemble une application globale (ex: frontend et multiples API).
## Détection de code smells
* Switch Statement  (pair avec Anemic Domain Model)
* Data clumps (pair avec Anemic Domain Model)
* Primitive obsession  (pair avec Anemic Domain Model)
* Shotgun surgery  (pair avec Anemic Domain Model)
* Feature envy / Message Chains (pair avec Anemic Domain Model)
* Fat controller / fat class en général

## Domain Driven Design
* Détection de l'ubiquitous language du code, ou en cas de mauvais code, inférence des concepts qui semblent se dégager
* Génération de graphique (mermaid) permettant de voir les core / supporting / generic domains, par rapport aux régles fonctionnels et concepts manipuler par l'application (au sens large, potentiellement plusieurs bases de code). Indique également, pour chaque domaine quelle ou quelles bases de code y correspondent

## Insigths
* Proposer des outils peu connus (libyear, codecharta) avec explications de ces outils
* Combiner tous les éléments appris dans les étapes précédents pour proposer des listes d'améliorations, priorisées d'abord par l'IA puis modifiable par les utilisateurs
* Générer des guides étapes par étapes pour chacun de ces axes d'améliorations, sur comment les mettre en place, en s'appuyant notamment sur des patterns de refactoring


# Étapes de traitement d'un projet
## Prérequis globaux
1. Upload du projet (direct upload ou lien repo git/github/gitlab/... selon les intégration dispos)
2. Téléchargement de la base de code côté serveur
3. Parsing des fichiers du projet (AST/Treesitter) pour indexations de toutes les classes / interfaces (base de travail)

## Analyse craft

## DDD
## Pour l'agent de conversation
1.  Génération d'un résumé IA des fonctionnalités de chaque 



# Outils potentiellement intéressant pour la réalisation
[![CocoIndex Logo](https://cocoindex.io/images/github.svg)](https://github.com/cocoindex-io/cocoindex)
: Permet de facilement créer des flow de transformation de données et persister les résultats finaux. Fonction de refresh incrémental: en cas de changement partiel des sources, il relance le flow de transformation uniquement sur les données changées. Exemple d'[embedding d'une base de code avec TreeSitter et CocoIndex](https://cocoindex.io/blogs/index-code-base-for-rag).

![[darkPocketFlow.png]][**PocketFlow**](https://github.com/The-Pocket/PocketFlow)
: Framework ultra light permettant de décrire des workflow de traitement. Exemple de [génération de doc/tuto sur une base de code](https://github.com/The-Pocket/PocketFlow-Tutorial-Codebase-Knowledge).
![PocketFlowAbstraction](https://raw.githubusercontent.com/The-Pocket/.github/main/assets/abstraction.png)

[**ColPali** 👀](https://github.com/illuin-tech/colpali)
: Visual Language Model, génère des embeddings à partir d'image (schéma, pdf, ...). Dispose d'une fonction text-to-visual, qui permet à partir d'un prompt textuel d'obtenir un embedding comparable à celui des images pour recherche.


# Potentiels futurs axes d'amélioration
* Collecter les exemples de codes pour chaque smell ou autres fonctionnalités, pour par la suite entrainer, via fine tuning de LLM classique ou TRM si assez évolués d'ici là, des modèles optimisés pour chacun de ces problèmes
* Par rapport au DDD, si les domaines ne correspondent pas aux bases de code, proposer une architecture et des étapes de migrations (sans tout casser, typiquement avec le strangler fig pattern) --> a tester, probablement raisonnement agentique car semble très complexe même pour un humain



1. Quel est le cœur du produit ? (audit craft ? ddd ? compréhension métier ?)
    Je pense que tout sera dedans à terme. Dans un premier temps, on peut se concentrer sur l'aspect compréhension métier / ubiquitous language, et extraire le rôle du code, les concepts métier manipulés, et les règles de gestion associées. 
2. Quelle profondeur d’analyse métier/tech attends-tu ?
    Comme c'est le cœur de la partie archéologie, je voudrais que la partie métier aille vraiment jusqu'à expliquer les flux métiers en langage naturel. Ça passera probablement par des analyse plus simples (identification des concepts métiers puis des règles), mais c'est bien l'objectif d'aller au plus profond possible.
3. MVP mono-codebase ou multi-codebases ?
    Le cœur de la valeur ajoutée sera sur le multi codebase. Le temps des monolithes est passé en entreprise, on a le phénomène inverse, de micro service ou micro front trop découpés. Analyser une codebase est bien, mais la vraie valeur sera sur le fait de considérer une application dans son ensemble, avec toutes les codebases la composant. Mais idem, dans un premier temps, on va faire les analyses sur du mono codebase, et cela nous servira pour construire la suite. 
4. Outil analytique ou raisonneur agentique ?
    Je pensais à un agent, s'appuyant notamment sur une analyse d'AST et d'autres outils, pour aller chercher des éléments avec la même façon de penser que moi. Mon travail sera donc à terme de créer cet agent avec les étapes de mon raisonnement. Dans un premier temps, on peut construire les bases d'informations nécessaire à ce raisonnement, qui est juste l'objectif final.
5. À qui vend-on en premier ? (coach ? ESN ? entreprise cliente ?)
    Je pense qu'il sera plus simple de vendre ce produit comme un "à côté" de ma prestation de coach. L'objectif va être:
    1. L'outil sera une aide pour moi et les autres coachs. On étudiera donc une base de code plus vite, ce qui apporte de la valeur à notre client, et donc satisfait mon ESN. Il suffit pour ce niveau d'avoir des fonctionnalités d'analyses basiques, l'approfondi sera fait par les coachs
    2. Lorsque le SaaS sera plus mature, l'ESN pourra s'en servir comme "pied dans la porte". Il permettra à des dévs de faire des analyses de leurs bases, puis l'ESN pourra leur proposer les services d'un coach. Il n'y aura pas encore toutes les fonctionnalités les plus approfondies à ce niveau, mais au moins des explications sur les différents problèmes détectés.
    3. SaaS en nom propre. Une fois que l'outil peut tout analyser de façon assez fiable, il pourra exister de lui même. Certains clients se débrouilleront seuls avec son aide, d'autres demanderont toujours du coaching en plus
6. Promesse principale du produit (1 phrase)
    Dans un premier temps je dirais "Reprenez le contrôle de votre legacy". Apporter une vision tant fonctionnelle que technique de l'application et de ses problèmes, et de comment l'améliorer.
    À terme, l'appli offrira un "Coach craftmanship de poche, pour créer les meilleurs applications possibles".
7. Degré d’automatisation souhaité
     La réponse à la question 5 donne la réponse. Pour l'étape 1, des analyses / graphes suffisent. Le 2 ajoutera la base à discussions et les explications. Et au final tout faire.
8. Types exacts de smells avancés attendus
    J'en ai listé quelques un dans le Pitch complet.md, j'en ajouterai d'autres au fur et à mesure que l'outils avancera, mais ceux là et certain basiques DDD (anemic domain model notamment) sont suffisant pour l'instant
9. Niveau de précision nécessaire
    Comme l'objectif est qu'un coach soit présent derrière, je préfère trop de suggestions, qu'il y ai des faux positifs. Comme ça je pourrai me reposer sur le travail du SaaS, et éliminer moi même les faux, plutôt que d'avoir une analyse trop conservatrice, qui me forcera à analyser le code de base moi même.