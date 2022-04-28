Lancer des tests existants (TU, TI, ...) avec des générateurs de données aléatoire en entrée. Permet de valider les cas aux limites / très étranges.
https://www.ionos.fr/digitalguide/sites-internet/developpement-web/quest-ce-que-le-fuzzing/

```c#
// BrandBuilder.cs

public static BrandBuilder AnActiveBrand(IFuzz fuzzer)
{
    string reference = fuzzer.GeneratePositiveInteger(99999).ToString("00000");

    DateTime createdAt = fuzzer.GenerateDateTimeBetween(DateTime.Today.Subtract(new TimeSpan(365 * 24, 0, 0)), DateTime.Now);

    return new BrandBuilder
    {
       _reference = reference,
       _id = reference,
       _name = fuzzer.GenerateSentence(3),
       _state = BrandState.Active,
       _spellings = fuzzer.GenerateWords(4) as string[],
       _createdAt = createdAt,
       _updatedAt = 
       fuzzer.GenerateDateTimeBetween(createdAt, DateTime.Now),
       _comment = fuzzer.GenerateParagraph(),
       _guid = fuzzer.GenerateGuid().ToString(),
 };
}

// FuzzerExtension.cs
public static class FuzzerExtension
{
   public static BrandBuilder AnActiveBrand(this IFuzz fuzzer)
   {
      return BrandBuilder.AnActiveBrand(fuzzer);
   }
}

// BrandTests.cs
public class BrandTests
{
   private Fuzzer _fuzzer;
   public FuzzingActionResultTests(ITestOutputHelper testOutputHelper)
   {
     Fuzzer.Log = s => testOutputHelper.WriteLine(s);
     _fuzzer = new Fuzzer();
   }

 [Fact]
 public void Should_return_action_result_with_default_random_values()
   {
     var brand = _fuzzer.AnActiveBrand().LabeledBy("My brand name").Build();
 // ...
   }
}
```
