[Fiche sur le site DDD Europe](https://2024.dddeurope.com/program/ddd-and-llms/)

Résumé des expérimentation d'Eric Evans autour des LLM.
Bonne intro si pas trop familier avec les capacités des LLM.

2 très bonne idées pour l'utilisation de LLM dans des applications:
## 1. Separation of concerns / Single responsability principle
**Un prompt ne doit être responsable que d'une seule action**

**Contexte**: tentative de jeu de rôle où l'IA est un pirate du XVIIème. Le joueur lui demande d'épargner sa vie en échange d'un moyen de gagner très gros. l'IA répond qu'elle est intéressée, le joueur explique qu'il a une idée d'utilisation de LLM pour faire des NPC interactif dans les jeux vidéos, et que cela pourrait faire une startup valant des milliards. L'IA répond qu'effectivement c'est intéressant, puis enchaine en demandant le business plan du joueur.

**Problème**: la réponse est clairement hors propos pour le rôle que l'IA incarne

**Solution**: renforcer le prompt initial de jeu de rôle ne marche pas. Création d'un second prompt, qui prend en entrée la réponse du joueur. Ce second prompt doit indiquer en renvoyant true ou false si l'entrée du joueur est compatible avec le contexte du jeu de rôle. Dans le cas de la startup, le prompt renvoi false. Le workflow devient
1. Le joueur donne son message
2. L'IA d'évaluation regarde si le message est valide pour ce contexte
3. Si oui, l'input est passé à l'IA pirate et produit sa réponse
4. Sinon, passage d'un prompt spécifique à l'IA pirate (ce que dis le joueur est incompréhensible pour vous) qui répond en s'énervant. Possibilité d'ajouter une notion de niveau d'énervement, qui augmente et influe la sortie de l'IA pirate à chaque mauvais prompt du joueur

## 2. LLM's are a component, not everything
**Il ne faut pas forcément tout gérer dans le programme avec l'IA, surtout si une solution plus simple existe**

**Contexte**: création via un LLM léger d'un trieur d'input de LLM. Le but est d'associé un prompt à une intention utilisateur (demande d'explication, création de contenu, prompt complexe, ....). Après fine tuning, l'IA marche très bien, sauf pour des prompt complexes typiques du prompt ingeniering. 

**Problématique**: Lorsque le LLM de tri reçoit: *peux tu classifier le prompt suivant: "Tu es un pirate du XVIIème siécle, ... (prompt complexe comme celui de l'exemple 1 pour faire faire du role play à l'IA)"*, alors il répond au prompt complexe, et ne fait pas la classification attendue

**Solution**: tentative de re-fine tuning de l'IA pour cette problématique là sans grand succès, d'amélioration du prompt de tri, ... Au final une solution simple et évidente suffit: parser le retour de l'IA. Si on trouve dans ce retour une des catégories de tri, alors le prompt est de ce type, sinon, l'IA a blablater sans faire le tri, donc c'est un prompt complexe.