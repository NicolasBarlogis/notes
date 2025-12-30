**Jour 6 (05/10/2025)**
* Génération de résumé globale appli et stack technique
* Début embeddings résumés par classe
**Jour 5 (04/10/2025)**
* Pivot libyear (chiant à généraliser) vers indexeur de dépendance par langage (Java -> commande maven directe)
* Analyse niveau classe par Mistral, beaucoup plus rapide et efficace pour ça que CodeLlama, avec parsing laxe sur le json retourné par l'IA pour limiter les échecs
**Jour 4 (03/10/2025)**
* Recherches spécifiques sur les RAG et AI agents*
* Démarrage v2, avec parsing basé sur tree-sitter (adaptable à d'autres langages)
* Obtention export satisfaisant
* Début automatisation analyse libyear (pour data dépendances projets + retard update)
**Jour 3 (30/09/2025)**
* Génération embedding avec nomic-embed-text sur ollama
* Upload embedding vers Qdrant
* Ajout de la notion de projet pour pouvoir analyser plusieurs projets
* Début tentative de mise en place de contexte pour répondre à des questions globales. Besoin de choisir des fichiers pertinents pour générer un résumé global de l'application
	* Ajout de NbLines et CyclomaticComplexity aux classes pour trouver les plus conséquentes
	* Classement des keywords en Domain/Technical/Generic (via liste de mots statiques puis IA), avec le nombre d'occurrences
**Jour 2 (29/05/2025)**
* Analyse codellama 2, génération summary / keywords
* Passage json vers mongodb
* Pagination de l'analyse IA (trop longue sinon)
* Création cluster Qdrant cloud
**Jour 1 (28/09/2025)** 
* Parseur C# avec rosalyn
* Stockage json
* Premier test sur [eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb)
* Install ollama / codellama2 

