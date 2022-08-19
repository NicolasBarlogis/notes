### prérequis
Installer docker 
https://stackoverflow.com/a/59882818

Si problèmes lors du npm install:
```shell
[1] npm ERR! gyp ERR! find VS msvs_version not set from command line or npm config
[1] npm ERR! gyp ERR! find VS VCINSTALLDIR not set, not running in VS Command Prompt
```
Voir https://stackoverflow.com/a/59882818

--- 
Suite pas nécessaire à refaire
```shell
Module not found: Error: Can't resolve 'prop-types'
```
`npm install --save prop-types`

```shell
export 'withRouter' (imported as 'withRouter') was not found in 'react-router'
```
--> react router version trop récente (6.x), doit downgrade
`npm install react-router-dom@5.2.0 react-router@5.2.0`

### lancement
`npm start --prefix consumer`
lance le consumer

### step2
`npm i nock --save`
--> pas nécessaire quand lancé depuis racine, mais besoin de faire une modif dans un fichier


Salut Julien,

J'ai épeluché le repo que tu as initié et le workshop pact en js.
Je t'avoue que j'ai un peu de mal à bien cerner ce que tu as imaginé sur chacune des étapes juste à partir des facilitation.md 😅

Je te mets ici quelques intérogrations que j'ai noté. 
Après si tu as les exemples en java ou que tu mets les bases des exos je pense que j'en aurai plus beaucoup donc ne fais pas doublons 😄

##### 1. Consumer driven contract testing with Pact
L'activité est une revue de code. On revoit juste l'API ?
Ou on met un test d'API non PACT (qui ne gère pas les erreurs) ?
Ou directement un test PACT ?
avec / sans l'authent ? (j'ai pas regardé le workshop java, mais en js ils ajoutent une couche d'authent pour montrer comment on la passe avec les contrats)

Et l'api c'est juste le get all et get/id sur les produits on est d'accord ? ou c'est plus que ça

##### 2. HTTP consumer tests with the Pact DSL
happy path, on écrit les tests pour les 2 endpoints (all et /id) ou un seul ?

#### 3. Use case driven consumer tests
dans la practice, on écrit un test consumer pour une liste de produit filtré. Le back ne le supporte pas en js mais peut importe, on ne montre pas la validation du producer ici on est d'accord ?

#### 4. Choose the right type of matching
Pas de code dans celui là, on va voir le code de la learning hour précédente, on est d'accord ? Ou on met volontairement un test améliorable en backup au cas où ils aient déjà une bonne base ?

Désolé de t'embéter avec ça, et bonne vacances quand même :)