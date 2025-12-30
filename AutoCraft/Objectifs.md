## 1 - Interpréter et offrir un expert sur la base de code
Fournir un agent pour échanger sur la base de code
* Principalement pour des non techniques (PO, SM, PM, ...) pour pouvoir avoir des infos à jour par rapport à la base de code sans déranger les dévs / en l'absence de dév expert (à quoi sert l'app ? Quelle sont les régles pour la génération de tel ou tel composant, à quelle heure à lieu tel traitement, ...)
* Pour les dévs pour les aider à naviguer une base peu connue (Où dois-je implémenter telle fonctionnalité ? Quelle architecture est utilisée ?)
Générer une doc haut niveau
* Liste des technologies / frameworks / addons utilisés
* Description haut niveau du composant (appli web ou API, qui fait ceci ou cela)
* Lister les points d'entrées et sortie de l'appli (endpoint API, sortie en base de données, ...)
Fournir les services d'un expert DDD / craftmanship
* Compiler l'ubiquitous language du projet (avec ou sans définition selon la clarté du code)

## 2 - Critiquer et donner des pistes pour améliorer la base de code
Détecter les code smells non détectables par une analyse statique de code classique
* Primitive obsesion, data clumps, shotgun surgery, ...
* Si application avec domain riche, trouver les mauvaises pratiques DDD: anemic domain model, patterns non respectés, ...
Proposer des pistes d'amélioration, avec analyse du gain par rapport au temps de mise en place (pour priorisation) --> À personnaliser selon l'objectif de l'équipe (réduire les bugs uniquement, préparer des évolutions futures, ou extraire certaines responsabilités de l'appli)
* Créer un graph des patterns tactiques de devraient avoir le projet (entité A, B; service X, Y; repo C, D; ...)
* Donner des pistes sur comment résoudre les problèmes détectés avant, via doc / vidéos / articles écrit par des experts, et dans un second temps peut être même de la génération de code directement

## 3 - Étendre les fonctionnalités sur différentes bases de code
Une base de code est généralement un maillon d'une chaîne. Pouvoir décrire cette chaîne (ou l'importer depuis un modèle type C4 ou autre) et offrir les fonctionnalités principales sur cet ensemble (que fait ce pipeline ? Comment interagissent A et B ? Qui est en charge de quel traitement ? / attention, bounded context violé, appli C à des règles de gestion d'une entité de l'appli D; ms trop morcelés, les responsabilités sont trop diluées; pas de règles métier, architecture inutilement complexe; ....)