[Repo](https://github.com/ldez/kebab-kata/tree/master)
[Guide facilitateur](https://github.com/malk/the-kebab-kata)
Kata pour montrer comment on arrive à du code legacy
--> Comment resister à la pression du PO

Déroulé en solo:
https://cyber-dojo.org/kata/edit/VD0BrT

## intro
Demander ce qu'est du code legacy
--> certains éléments devraient sortir, normalement ni définition précise ni consensus
--> Normalement on obtient la notion de code moche, non testé, de mauvaise qualité
Qui est responsable du fait que l'on obtienne un code dans cet état ?
--> voir si autre chose que les dévs / les techniques sort

On va faire un kata très simple, de gestion de kebab (plus après). En tant que PO propriétaire d'un restaurant de kebab, je vais essayer de vous pousser à créer du code legacy, via tout un tas de mauvaise pratique / antipattern qui sont malheureusement fréquente dans la vie réelle.

## aides
Listes d'ingrédients indicative (à nommer ou pas, mais peut servir pour faire une liste à la fin que les dévs n'ont pas suivi):
* salade
* tomate
* oignon
* viande
* poisson
* crevette
* sésame
* épinard
* ail
* sauce blanche
* ketchup
* sauce samouraï
* mayonnaise

Liste des techniques utilisées par le po:
* être vague 
* Ne pas donner la liste des ingrédients directement
* Faire comme si tout était évident
* être inconsistant dans les données/infos fournies (ex: ingrédient)
* Rabaisser leur travail
* Traiter une tâche de trivial et dire qu'il faudrait la moitié du temps alloué pour la réaliser
* Interrompre pour demander des estimations et essayer de réduire les estimations
* ceux qui ont accepté de le faire en moins de temps sont rappelés à la fin du sprint parcequ'ils n'ont pas tenu leur engagement
* Insister sur le fait de ne pas passer trop de temps sur les tests et la qualité
* Demander si les équipes ne serait pas plus productives en étant séparées
* Faire passer deux demandes pour une seule
* Refuser de prioriser 