# Le Mob programming / ensemble working
*Temps de lecture* **9 minutes**

Comme vous êtes tous impatient de vous lancer dans des [coding dojo](https://teams.microsoft.com/l/message/19:ed4f47f0eca941c7b4baacb83bc366eb@thread.tacv2/1707231243223?tenantId=34314e6e-4023-4e4b-a15e-143f63244e2b&groupId=55b4758c-8dac-48c5-9727-9d20b61fca84&parentMessageId=1707231243223&teamName=Peaksys%20Engineering&channelName=Ensemble%20Working&createdTime=1707231243223) entre midi et deux, je me dis qu'il pourrait être utile de refaire un point sur le mob programming 😁

Too Long; Didn't Read;
> Le mob programming est une technique permettant de partager de la connaissance et de l'expérience au sein de l'équipe.
> Il existe plusieurs façons d'organiser un mob programmimng. On peut inclure des personnes non techniques (comme un PO) ou assigner des rôles précis à chaque participant.
> Le mob programming n'est pas regarder la même personne coder pendant une heure ou plus

## Ça sert à quoi ?
On va recourir au mob programming pour 2 raisons principales:
### 1. Aligner l'équipe
Un des avantages à travailler au même moment sur le même code est que l'on peut partager sur les façons de faire et de penser de chacun. Cela permet de partager de la connaissance technique (raccourcis dans l'ide, features du langage) - et il n'y a clairement pas que les juniors qui apprennent – mais aussi fonctionnelle.
En parcourant un asset peu connu de l'équipe, un sachant va généralement diffuser sa connaissance du contexte de façon très efficace, avec beaucoup plus de chance que les autres membres de l'équipe le retiennent, car elle arrive pour répondre au besoin / à une question que se pose l'équipe (contrairement à la doc plus à jour que personne ne lit de toute façon).
Lorsqu'il s'agit de connaissance fonctionnelle, le sachant peut être un PO ou une autre personne issue du métier. Même s'il ne comprend pas le code, il pourra répondre aux questions des développeurs et donner du contexte sur les règles du domaine.

Dans tous les cas, on apprend beaucoup mieux en pratiquant (cf [training from the back of the room](http://tfs.cdbdx.biz:8080/tfs/DefaultCollection/craftmanship/_git/coms-craft?path=%2Flearning-hours%2Flearning-hours.md&_a=preview&anchor=concepts-(5-%C3%A0-10-minutes))). Donc, pour montrer le fonctionnement d'un asset à l'équipe, aller implémenter une US dessus en mob sera généralement plus efficace que de simplement présenter le code. 

Dans la partie alignement, le mob programming permet aussi une discussion sur les pratiques de code et les standards de l'équipe. Il ne faut pas hésiter à ressortir d'une session de mob avec des nouveaux standards et des procédures d'équipes selon ce qui émerge.

### 2. Résoudre un problème
Avec tous ces cerveaux concentrés sur un même bout de code, on aboutit souvent à une solution, construite sur les apports incrémentaux de chacun (à la manière d'un brainstorming).
Que ce soit pour résoudre un ticket complexe ou reprendre une base de code legacy, la participation de tous fait émerger des solutions adaptées beaucoup plus rapidement que si un dév se penche sur le problème puis va petit à petit demander de l'aide au reste de l'équipe.

Dernier petit avantage du mob: pas besoin de code review, toute l'équipe ayant participé à son élaboration 😁

## Comment pratiquer ?
Avec un nombre certain de participants, il peut être complexe de maintenir l'attention de tous pendant la durée du mob, ou d'éviter que les discussions n'évoluent en brouhaha.


Pour contrer cela, le mieux est d'utiliser le style **driver / navigateur**. Parmi les participants, on désigne une personne comme driver et une autre navigateur, puis ces rôles tourneront à intervalle régulier (5 à 15 minutes selon la taille de l'équipe, l'expérience en mob pro,... l'idéal étant que tout le monde soit au moins une fois driver sur une heure).

### Le Driver
C'est la seule personne autorisée à écrire du code. Il ne décide néanmoins pas tout seul de ce qu'il écrit. Il participe au débat et peut aussi proposer des idées. Sa mission principale reste de transposer ce que demande le navigateur dans le code.

> Tips : commencer par un développeur moins expérimenté sur le sujet traité permet de ne pas faire naitre de stress inutile ("Je vais faire plus d'erreurs", "Je connais moins bien le langage, cette librairie, ce pattern", "Il faut que j'aille aussi vite" ...)

### Le Navigateur
Donne des indications claires au conducteur en accord avec la foule. C'est grâce à ce rôle que le drive n'est pas perdu parmi toutes les propositions du mob. Le navigateur doit jouer un rôle de protecteur du conducteur, en clarifiant les demandes du mob et en synthétisant les idées. Il n'est pas obligé de tout savoir et peut demander de l'aide aux autres participants.

> Tips : commencer par quelqu'un ayant une bonne vision de ce que l'on souhaite faire, cela évitera les débuts laborieux

## Et si on n'y arrive pas ?
Il peut être compliqué de faire du mob, que l'on soit débutant ou expérimenté dans cette pratique, diverses problèmes se présentent. Il existe de nombreux conseils et astuces pour les dépasser, en voici quelques-uns parmi les plus intéressants :

### 1.Faire des pauses
Pour rester opérationnels et efficaces durant ces sessions, n'hésitez pas à faire des petites pauses toutes les 1h/1h30. Les sujets abordés étant souvent complexes et le partage plus énergivore que du développement en solo, on finit souvent par décrocher sans pause.

### 2.Utiliser un outil dédié
[Mobtime](https://mobti.me/) ou [Mobster](http://mobster.cc/) sont des outils vous permettant de gérer automatiquement les rotations de rôle, le timer et potentiellement des rôles supplémentaires que le navigateur/driver (voir astuce 5)

### 3.Donner le bon niveau de détail au driver - Intention / Location / Detail
En tant que navigateur, il est souvent compliqué de s'adapter au niveau du driver. Ne pas donner assez de détail peut dérouter le driver, mais dicter chaque ligne de code peut également être fatiguant.
Une technique simple est de passer par différents niveaux de détail dans nos demandes:
1. **Intention** - On commence par énoncer uniquement ce que l'on veut faire: "je veux supprimer le [data clumps](https://refactoring.guru/fr/smells/data-clumps) présent dans cette classe et en extraire la notion de marque produit"
2. **Location** - Si vous voyez que le driver ne sait pas vraiment où commencer avec cette information, on peut alors lui donner l'emplacement d'où il devra commencer ses modifications: tel classe, tel fichier, telle méthode,...: "tu peux commencer par extraire le nom et le type de la marque de la méthode createProduct ligne 87"
3. **Detail** - Enfin, si ce n'est toujours pas clair, alors le navigateur peut descendre dans les détails techniques: "ajoute un paramètre de type Brand à la signature de la méthode ligne 87, génère la classe brand dans le dossier entities et ajoute lui les attributs name et type"

Cette méthode assure de toujours expliciter les intentions du navigateur, et permet de s'adapter à la compréhension du driver. Le niveau utilisé peut varier à chaque demande. Typiquement, on descend au niveau detail, puis au bout d'une ou deux modifications, le driver va avoir compris l'intention, et le navigateur pourra alors rester au niveau intention.

### 4.Trop de discussions, pas assez de code
Des débats sur une meilleure solution ou un problème arrivent souvent. Il ne faut néanmoins pas passer plus d'un quart sans coder. Les solutions deviennent généralement plus claires lorsqu'on les test dans le code que lorsqu'on en débat théoriquement.

Lorsqu'un débat s'éternise, n'importe quel participant peut l'interrompre et trancher: "On n'avance pas vraiment, je propose que l'on essaie la solution de Untel, puis on refactora vers la proposition de Unautre si on voit que ça ne marche pas. Ça vous va ?"

### 5.Les participants ne savent pas vraiment comment se comporter
Ceux qui ne sont ni driver ni navigateur peuvent parfois avoir l'impression de ne pas contribuer, a fortiori sur un domaine qu'ils ne maîtrisent pas bien. Pourtant, il existe de nombreuses tâches qu'ils peuvent effectuer, au bénéfice de l'équipe:
* Noter toutes les idées / actions à faire et suivre leur réalisation, les rappeler à l'équipe quand on cherche la prochaine action, ...
* Chercher dans les docs / internet / copilot les réponses aux questions que se pose l'équipe
* Se porter garant du respect des standards d'équipe et faire remonter au fur et à mesure les violations
* ...

Il existe encore d'autres postures, que vous pouvez utiliser au besoin.
Pour les apprendre, vous pouvez utiliser le [mob programmin RPG](https://github.com/willemlarsen/mobprogrammingrpg), on a même des [templates miro/pdf en interne](https://miro.com/app/board/uXjVNV8LaQs=/?moveToWidget=3458764580700100779&cot=14), si vous voulez y jouer 😉

### 6.On ne sait pas vraiment à quelle occasion en faire
Contrairement au pair-programming, il n'est pas envisageable de pratiquer tout le temps le mob-programming.
Néanmoins, vous pouvez en planifier une session régulière (toutes les 1 ou 2 semaines) pour développer une US en commun lorsque vous avez des nouveaux arrivants ou des nouveaux assets, pour partager la connaissance.
Vous pouvez aussi pratiquer plus dynamiquement, pour des runs ou des problématiques ciblées.

Même si votre équipe est stable et partage bien la connaissance, il peut être utile d'avoir un créneau récurrent, même si peu fréquent (en début de sprint, une fois par mois, ...), car cela permet toujours d'échanger techniquement et fonctionnellement, tout en continuant à découvrir de nouvelles choses ensemble.

### Autres problèmes
Voici deux très bonnes sources d'autres astuces et résolutions de problèmes:
* Les [Ensemble Enabler](https://proagileab.github.io/EnsembleEnablers/), une liste de problèmes courants et des propositions de solution
* [A few tips for mob-programming](https://www.industriallogic.com/blog/a-few-tips-for-mob-programming/), un article intéressant d'astuces pour pratiquer le mob programming

Enfin, n'hésitez pas à chercher des vidéos de sessions de mob, en physique ou en ligne, ça aide à se rendre compte du déroulement si vous n'en avez jamais fait.

J'espère que tout ça vous aura donné envie de tester ou retester le mob programming 😁
