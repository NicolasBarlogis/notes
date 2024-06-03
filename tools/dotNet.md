[[Dapper]] https://github.com/DapperLib/Dapper
Dapper is a [NuGet library](https://www.nuget.org/packages/Dapper) that you can add in to your project that will extend your `IDbConnection` interface. It provides 3 helpers.

[[Orlean]] https://dotnet.github.io/orleans/
Orleans takes familiar concepts like objects, interfaces, async/await, and try/catch and extends them to multi-server environments

[[language-ext]] https://github.com/louthy/language-ext
Extension fonctionnelle du C#

[[nsubstitute]] https://nsubstitute.github.io/
Facillite le mocking

[[Flurl]] https://flurl.dev/
Facilite grandement les call http et leurs tests

[[FluentAssertions]] https://fluentassertions.com/
Créer des assertions plus facilement lisible. Exemple:
```C#
// sans fluentAssertions
Assert.equal("toto",employee.Name)
// avec fluentAssertions
employee.Name.Should.Be("toto")
```

[[FluentValidation]] https://fluentvalidation.net/
Permet de facillement créer des validateurs plus ou moins complexes pour des classes. Vient avec des outils de testing des validateurs.

[[AutoMapper]] https://automapper.org/ [[Mapster]] https://github.com/MapsterMapper/Mapster
Mapper permettant d'automatiser la transformation d'un objet vers un autre (ex: DTO vers Domain)
```C#
//automapper ex:
public class MappingProfile : Profile {
	public MappingProfile() {
		// créer un mapper 1:1
		CreateMap<Employee, EmployeeEntity>()
			// nom différend, Employee.EmployeeID vers EmployeeEntity.id
			// on peut faire d'autres transfo ici
			.ForCtorParam("id", ept => ept.MapFrom(src => src.EmployeeId))
			// ici un changement de type
			.ForCtorParam("DateOfBirth", opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)));
	}
}
```

[[Records]] https://docs.microsoft.com/fr-fr/dotnet/csharp/whats-new/tutorials/records
Nouvelle structure .Net 6, parfait pour les value objects. On peut créer une nouvelle instance à partir d'une autre, en modifiant un ou plusieurs champs, via 'with':
```C#
public readonly record struct DailyTemperature(double HighTemp, double LowTemp);

var temp1 = new DailyTemperature(60, 35);
var temp2 = temp1 with { HighTemp = 65 };
```

[[BenchmarkDotNet]] https://github.com/dotnet/BenchmarkDotNet
Outil simple permettant de benchmarker directement des méthodes/fonctions

[[Archunit]]

[[Sharplab]] https://sharplab.io/
Analyse un snippet de code C# et montre le code généré en réponse

[[tools/tools details/Specflow]] https://specflow.org/
Automatisation test 'BDD'  / validation via du Gherkin.
--> https://docs.specflow.org/projects/specflow-livingdoc/en/latest/ livingdoc générée pour faciliter partage autour du Gherkin

[[FsCheck]]https://fscheck.github.io/FsCheck/
Lib de [[Fuzz tesing]] / [[Property Based Testing]]

OneOf https://github.com/mcintyre321/OneOf
Explicite le type de retour d'une méthode + les exceptions qu'elle peut lancer