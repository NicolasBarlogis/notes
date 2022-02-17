Méthode utilisée pour tester du code legacy, où une dépendance externe (typiquement un repo) n'est pas injectée. Donc impossible à tester initialement car non mockable.

Le seam permet de tester facillement, sans avoir à retoucher trop le code d'origine (contrairement au passage à l'injection de dépendance par exemple).

## Exemple
On souhaite tester cette classe :

```C#
public class TripService
{
	public List<Trip> GetTripsByUser(User.User user)
	{
		List<Trip> tripList = new List<Trip>();
		User.User loggedUser = UserSession.GetInstance().GetLoggedUser();
		bool isFriend = false;
		if (loggedUser != null)
		{
			foreach(User.User friend in user.GetFriends())
			{
				if (friend.Equals(loggedUser))
				{
					isFriend = true;
					break;
				}
			}
			if (isFriend)
			{
				tripList = TripDAO.FindTripsByUser(user);
			}
			return tripList;
		}
		else
		{
			throw new UserNotLoggedInException();
		}
	}
}
```

Impossible de'ajouter des [[Tests Unitaires]] sur la méthode `GetTripsByUser`, car ces deux lignes sont des dépendances à des bdd ou autre :
```C#
User.User loggedUser = UserSession.GetInstance().GetLoggedUser();
tripList = TripDAO.FindTripsByUser(user);
```

#### Seams Étape 1 - Isoler
On isole l'appel problématique dans une méthode dédiée (ici on faire juste avec le UserSession):

```C#
public class TripService
{
	public List<Trip> GetTripsByUser(User.User user)
	{
		List<Trip> tripList = new List<Trip>();
		User.User loggedUser = getLoggedUser();
		bool isFriend = false;
		if (loggedUser != null)
		{
			foreach(User.User friend in user.GetFriends())
			{
				if (friend.Equals(loggedUser))
				{
					isFriend = true;
					break;
				}
			}
			if (isFriend)
			{
				tripList = TripDAO.FindTripsByUser(user);
			}
			return tripList;
		}
		else
		{
			throw new UserNotLoggedInException();
		}
	}

	protected virtual User.User getLoggedUser() {
		return UserSession.GetInstance().GetLoggedUser();
	}
}
```

#### Seams Étape 2 - Hériter
Dans la classe de test, on peut créer une classe privée testable, en héritant de notre classe a tester et en surchargeant la méthode créée précédement, dans laquelle on peut mettre les données nécessaire à nos tests. On utilise alors cette classe pour nos tests :

```C#
public class TripServiceTests
{
	[Fact]
	public void firstTest()
	{
		var tripService = new TripServiceTestable();
		// la suite du test
	}

	private class TripServiceTestable 
	{
		protected override User.User getLoggedUser() {
			return null;
		}
	}
}
```

#Refactoring 