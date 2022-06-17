![[cyber-dojo-2022-5-12-XFRUte.tgz]]
https://cyber-dojo.proagile.se/kata/edit/XFRUte

## Connect - 5'
![[connect test data builder.png]]
Objectifs:
 * Être capable de mettre en place un Test data builder
 * Comprendre comment injecter du sens métier via les Test Data Builder

## Concept - Démo - 7'
Montrer la longueur des créations dans EducationnalBookTest

Programmer par l'appellant
```C#
var usa = CountryBuilder.ACountry()
                .Named("U.S.A.")
                .Using(Currency.UsDollar)
                .Speaking(Language.English)
                .Build();
```
Créer le CountryBuilder et les  méthodes qui vont bien
```c#
using System;

public class CountryBuilder {
    private string _name { get; set; }
    private Currency _currency { get; set; }
    private Language _language { get; set; }
    
    public static CountryBuilder ACountry() {
        return new CountryBuilder();
    }
    
    public CountryBuilder Named(String name) {
        _name = name;
        return this;
    }
    
    public CountryBuilder Using(Currency currency) {
        _currency = currency;
        return this;
    }
    
    public CountryBuilder Speaking(Language language) {
        _language = language;
        return this;
    }
    
    public Country Build() {
        return new Country(_name, _currency, _language);
    }
}
```

On peut ajouter un bout de mother mélangé
```C#
public static CountryBuilder TheUSA() {
    return CountryBuilder.ACountry()
            .Named("U.S.A.")
            .Using(Currency.UsDollar)
            .Speaking(Language.English);
}
```

## Concret Practice - 30'
Durant la learning hour, pousser à commencer dans Country.
Pour le test France / Belgique, pousser l'introduction du But:
```C#
// Arrange
var builderFR = CountryBuilder.ACountry()
    .Using(Currency.Euro)
    .Speaking(Language.French);
var france = builderFR.But().Named("France").Build();
var belgium = builderFR.But().Named("Belgique").Build();
```
Le CountryBuilder:
```C#
public CountryBuilder() {
}
    
private CountryBuilder(CountryBuilder builder) {
    _name = builder._name;
    _currency = builder._currency;
    _language = builder._language;
}

public CountryBuilder But() {
   return new CountryBuilder(this);
}
```

Selon l'avancée, on peut combiner le but avec le mother France:
```C#
public static CountryBuilder TheFrance() {
    return CountryBuilder.ACountry()
            .Named("France")
            .Using(Currency.Euro)
            .Speaking(Language.French);
}
```

```C#
// Arrange
var franceBuilder = CountryBuilder.TheFrance()
var france = franceBuilder.Build();
var belgium = franceBuilder.But().Named("Belgique").Build();
```

## Conclusion - 5'
Expliquez les Test Data Builder en une phrase