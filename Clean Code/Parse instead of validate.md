validation --> vérifier que c'est ok
parser --> assurer que c'est ok

Exemple:
```C#
public class MyList<T> {
	private IList<T> _list = new List<T>;
	public T? head {
		return _list[0];
	}
}
```
--> autorise le return d'un null. Idem en mettant un Maybe, on affaibli le type de retour et on repousse le cout de la vérification à l'appelant.

```C#
public class NonEmptylist<T> {
	private IList<T> _list = new List<T>;
	private T _firstElement;

	public NonEmptyList(T firstElement) {
		_firstElement = firstElement;
	}

	public T head {
		return list[0];
	}
}
```
On renforce le type d'entrée plutôt que d'affaiblir la sortie.

--> utiliser un type permettant de forcer une contrainte de validation plutôt que de la repousser partout dans le code (ce qui mène au [[Shotgun Parser]]).


1.  **Use a data structure that makes illegal states unrepresentable.** Model your data using the most precise data structure you reasonably can. If ruling out a particular possibility is too hard using the encoding you are currently using, consider alternate encodings that can express the property you care about more easily. Don’t be afraid to refactor.
    
2.  **Push the burden of proof upward as far as possible, but no further.** Get your data into the most precise representation you need as quickly as you can. Ideally, this should happen at the boundary of your system, before _any_ of the data is acted upon.

https://lexi-lambda.github.io/blog/2019/11/05/parse-don-t-validate/