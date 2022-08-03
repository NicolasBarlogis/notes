![](DecoratorPattern.jpg)

## Connect
![[DecoratorPattern-schema1.png]]
Différents scénarios:
-   Notifier par SMS et Slack
-   Notifier par Slack, SMS et Mail
-   Changer les combinaisons selon l'heure (uniquement mail la nuit)
-   Changer les combinaisons selon les préférences de l'utilisateur
Et si on ajoute un notifier Facebook ? Twitter ? Teams ? ...

## Concept
-   Étendre le comportement d'une classe à l'exécution
-   Combiner le comportement de classes similaires sans héritage (classe C combinant A et B ou A hérite de B)
![[DecoratorPattern-schema2.png]]

https://refactoring.guru/fr/design-patterns/decorator
```c#
new SmsNotifier(new SlackNotifier());
new SlackNotifier(new SmsNotifier(new MailNotifier()));
var notifier = (hour > 0:00 && hour < 8:00) ?
 new MailNotifier() :
 new SlackNotifier(new SmsNotifier(new MailNotifier()));
```

## Concrete practice - design
Conception basique:
![[DecoratorPattern-schema3.png]]

Problème: `new chocolate(new cupcake(new Cookie()))`

Une solution:
![[DecoratorPattern-schema4.png]]