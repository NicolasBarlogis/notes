# ADR - Architecture Decision Record
*Temps de lecture* **5 minutes**

Too Long; Didn't Read;
> Les Architecture Decision Records sont des documents traçant les décisions structurantes d'un projet (changement de framework, changement d'organisation des couches, ajout d'une catégorie de test, ...), qui permettent de documenter, au plus près du code (généralement en markdown, directement dans le repo du projet) les règles d'un projet.
> Il existe de nombreux formats, permettant de donner plus ou moins de détails ou de gérer et tracer des propositions de pratiques et leur cycle de vie.
 
Lors de leur [super présentation](https://officecdiscount-my.sharepoint.com/:v:/r/personal/maxime_andre_cdbdx_biz/Documents/Enregistrements/%5BWebinar%20TECH%5D%20-%20C4%20_%20Dynamitez%20vos%20sch%C3%A9mas%20!%20(as-code%20!)-20230907_113302-Meeting%20Recording.mp4?csf=1&web=1&e=L6Hfzh&nav=eyJyZWZlcnJhbEluZm8iOnsicmVmZXJyYWxBcHAiOiJTdHJlYW1XZWJBcHAiLCJyZWZlcnJhbFZpZXciOiJTaGFyZURpYWxvZy1MaW5rIiwicmVmZXJyYWxBcHBQbGF0Zm9ybSI6IldlYiIsInJlZmVycmFsTW9kZSI6InZpZXcifX0%3D) du modèle C4, Maxime Andre et Nicolas Boisseau ont mentionné le fait que Structurizr supporte les Architecture Decision Records et qu'ils peuvent être directement publiés dans confluence à partir des fichiers markdown présents dans vos projets.
 
Cette nouveauté m'a semblé être une superbe occasion pour vous présenter les ADR et vous encourager à en faire sur vos projets 😁

## Décision d'architecture ?
Les décisions d'architectures sont toutes les décisions difficiles à prendre / constituantes pour un projet. Quelques exemples:
* Monorepo ou multi-repo
* Choix d'un framework JS
* Généraliser l'utilisation de MediatR dans le projet

Et cætera. Ces décisions ne se limitent pas au code, et concernent tous les aspects d'un projet. Plus d'exemples [ici](https://github.com/joelparkerhenderson/architecture-decision-record/tree/main/locales/en/examples).
 
L'objectif des ADR est d'enregistrer ces décisions, de s'en servir de base de connaissance et de les faire vivre. Une vielle décision peut être abrogée, une proposition jamais adoptée. Tracer cela dans des ADR permet de donner du contexte au projet et d'éviter de refaire des efforts de POC ou d'analyse pour des choses déjà proposées.

En plus de documenter, l'idée est de mettre ces ADR [au plus prêt du code](https://principles.dev/p/documentation-should-be-close-to-the-code/). 

C'est le meilleur moyen que de nouveaux dévs soit au courant de ces données (pas besoin de sortir du projet dans un outil externe, pas de risque de lien cassé, d'outillage propriétaire plus supporté, ...).

C'est également une bonne façon d'encourager la maintenance de cette documentation, en évitant de l'enterrer dans un confluence rarement consulté.
 
## Que contient un ADR ?
Il existe de [très nombreuses propositions de formats](https://github.com/joelparkerhenderson/architecture-decision-record/tree/main/locales/en/templates) d'ADR, incluant plus ou moins de détails. A minima, on retrouvera :
* **Un titre**, pour décrire rapidement la décision
* **Une date**, et pourquoi pas des dates de révisions, pour savoir si la décision est récente ou commence à être obsolète
* **Un statut**, pas obligatoire mais recommandé, la liste peut inclure approuvé, en place, abrogé, brouillon, proposition, rejeté, etc. Selon le process interne de l'équipe
* **Une décision**, le coeur de l'ADR, le choix fait pour cette problématique
* **Un contexte**, la problématique qui a mené à cette décision
* **Des propositions**, optionnel également mais intéressant d'avoir la trace des solutions considérées, les éventuels tests, ressources, pros & cons, et  autres documents pertinents.
 
## Comment on s'y met ?
### 1 - Décidez d'un format d'ADR pour l'équipe.
Commencer juste avec les informations qui vous simple nécessaires (titre, contexte, ...). Le plus simple est de prendre un template existant qui vous semble pertinent. Vous pouvez choisir dans [cette liste](https://github.com/joelparkerhenderson/architecture-decision-record/tree/main/locales/en/templates).

Le format initial n'est pas des plus importants, vous le ferez évoluer à l'usage, en ajoutant ou retirant des sections, les rendant obligatoires ou non, ...

### 2 - Tracez vos décisions
Faire une habitude de votre équipe de tracer ces choix importants via votre structure d'ADR.
Documentez aussi bien les propositions acceptées que celles refusées. Cela permet de comprendre l'histoire du projet et permet de garder les réflexions passées pour les évolutions à venir.

### 3 - Publiez vos ADR
Dans l'absolu, peu importe l'endroit (confluence, un wiki, sharepoint, ...). Le plus important étant de tracer ces décisions.
 
L'idéal reste de les mettre au plus près du code. Un dossier ADR, decisions, ... hébergera vos ADR au format markdown à la racine de votre projet, ou potentiellement dans [le repo architecture](https://confluence.cdiscount.com/display/INGELOG/Initialisation+d%27un+repo+structurizr) de votre équipe si vous voulez centraliser vos ADR avec votre design C4.
Grâce au template de pipeline de la direction IT Finance, vous pouvez aussi [pousser automatiquement ces ADR sur confluence](https://confluence.cdiscount.com/display/INGELOG/Sur+Confluence), pour les partager avec d'autres équipes ou y avoir accès en version mise en page sans IDE.
 
### Optionnel - Documenter l'existant
Si vous appréciez le format et l'intégration de ces informations, n'hésitez pas à tracer, même sommairement, vos anciens choix marquants via des ADR. Cela contribuera à la documentation et au partage de connaissance concernant vos projets.
C'est peut être même la bonne occasion de reconsidérer certaines décisions dépassées 😋
 
Au final, ça ressemble à quoi ?
Voici un ADR extrait d'un repo d'exemples :
```markdown
# Decided - Secrets storage
Contents:
* [Summary](#summary)
  * [Issue](#issue)
  * [Decision](#decision)
  * [Status](#status)
* [Details](#details)
  * [Assumptions](#assumptions)
  * [Constraints](#constraints)
  * [Positions](#positions)
  * [Argument](#argument)
  * [Implications](#implications)
## Summary
### Issue
We need to store secrets, such as passwords, private keys, authentication tokens, etc.
Some of the secrets are user-oriented. For example, our developer wants to be able to use their mobile phone to look up a password to a service.
Some of the secrets are system-oriented. For example, our continuous delivery pipeline needs to be able to look up the credentials for our cloud hosting.
### Decision
Bitwarden for user-oriented secrets
Vault by HashiCorp for system-oriented secrets.
### Status
Decided. We are open to new alternatives as they arise.
## Details
### Assumptions
For this purpose, and our current state, we value user-oriented convenience, such as usable mobile apps.
  * We want to ensure fast easy access on the go, such as for a developer doing on-call system reliability engineering.
  * We want to be able to share some secrets among selected people, such as a team.
We are not trying to solve for single-provider, such as storing all secrets exclusively on Amazon or Azure or Google.
We do not want ad-hoc approachs such as "remember it" or "write it on a note" or "figure out your own way to store it".
Our security model for this purpose is fine with using well-respected COTS vendors, such as SaaS password management tools.
### Constraints
Right now we want something that is easy i.e. no need to write code, no need to install servers, no need to make a major commitment, no need to standardize everyone.
### Positions
We considered:
1. User-oriented off-the-self password managers: LastPass, 1Password, Bitwarden, Dashlane, KeePass, pass, GPG, etc.
2. System-oriented COTS password managers: AWS KMS, Vault by HashiCorp, EnvKy, Secret Server by Thycotic, Devolutions Password Server, Confidant by Lyft.
3. Sharing-oriented approaches: using a shared Google document, or shared Slack channel, or shared network folder, etc.
4. Low-tech ad-hoc approaches, such as remembering, writing a note, or relying on each user to figure out their own approach.
### Argument
Bitwarden, LastPass, 1Password, and Dashlane all are commerical off-the-shelf products.
  * Similar kinds of features for users, teams, organizations, etc.
  * Desktop capability for Windows and Mac, and mobile capability for Android and iOS.
  * Browser extensions for Chrome and Firefox, for automatic form fill in, etc.
Bitwarden has two advantages over the others:
  * Bitwarden is open source, which means the security can be peer reviewed and also the company is widely-appreciated by security-oriented developers.
  * Anecdotes by software workers describe a significant preference for Bitwarden over the others.
A typical good example writeup: https://jcs.org/2017/11/17/bitwarden
A typical side-by-side voting site: https://stackshare.io/stackups/bitwarden-vs-dashlane
We defer KeyPass, pass, GPG, etc. because there's additional complexity. All of these look like fine solutions for technical users. GPG looks especially good for technical users who want cross-system command-oriented capabilities.
We defer KMS because it has single-provider lock-in.
We choose Vault for system-oriented needs, because the reviews are amazingly positive, and because HashiCorp has an excellent track record fup top-quality software and support.
We veto the approaches of sharing approaches such as via shared documents, shared channels, shared network folders, etc. These do not provide the security qualities that we want.
We veto the ad-hoc low-tech approaches, because we all agree it's not a long-term path forward.
### Implications
Developers may need to track secrets in two places: Bitwarden for user-oriented access, and Vault for system-oriented access.
```
Voir l'[exemple complet](https://github.com/joelparkerhenderson/architecture-decision-record/tree/main/locales/en/examples/secrets-storage)

Bonne documentation à tous ! 