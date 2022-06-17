## Objectifs

## Concepts
[[Seams]]
[[Refactoring]]
Bonus:
[[Test Data Builder]]


## Sources
https://github.com/sandromancuso/trip-service-kata
https://www.youtube.com/watch?v=_NnElPO5BU0

## Notes
![[shortest test - deepest refacto.png]]

## Learning hour
#### Demo
Test from shortest branch, on part sur le TripService, le premier test se fait sur l'exception
```c#
[Fact]  
public void should_throw_exception_when_user_is_not_logged()  
{  
    // arrange  
    var tripService = new TripService();  
    // act  
    // assert
    Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(new User()));  
}
```

Le test casse, on va extraire la ligne problématique, via l'ide (ctrl+alt+m) --> mise en place du seam. La passer en protected virtual
```C#
protected virtual User.User? GetLoggedUser()  
{  
    return UserSession.GetInstance().GetLoggedUser();  
}
```

puis on génére la classe interne
```c#
[Fact]  
public void should_throw_exception_when_user_is_not_logged()  
{  
    // arrange  
    TripService tripService = new TestableTripService();  
    // act  
    // assert    Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(new User()));  
}  
  
public class TestableTripService : TripService  
{  
    protected override User? GetLoggedUser()  
    {        return null;  
    }}
```

## Déroulement du kata
##### 1 - ajout des seams
ajout des seames pour loggedIn et DAO, via l'ajout des tests en commençant par la branche la  moins profonde

Le code coverage est une bonne indication 

Possibilité de rendre les tests plus lisibles en ajoutant un UserBuilder
Dans la vidéo: https://youtu.be/_NnElPO5BU0?t=1493

##### 2 - Refactoring
En partant de la branche la plus profonde
* Feature envy isWithFriend User (design en tdd)
* Passer le UserNotLoggedInException en guarde
* Supprimer la variable tripList (moins de variables --> moins de compléxité) pour finir sur une ternaire
* Injecter du sens : remplacer liste de Trip vide par une méthode noTrip()
* DDD: tripService, domaine, a une dépendance sur le loggedUser, technique. Passer le loggedUser à la méthode. Note sur la difficulté de remettre en cause le design, a fortiori avec une bonne base de test. Mais garder ça en tête
* DDD: idem TripService / DAO statique --> inversion de dépendance via injection


## Tags