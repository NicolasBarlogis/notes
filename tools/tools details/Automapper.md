TDD automapper:
https://www.twilio.com/blog/test-driven-automapper-net-core

Mettre en place le test après avec créer les profiles par défaut.
Lancer le test nous indiquera ce que l'on doit configurer
```c#
public class MappingsTests
   {

       public MappingsTests()
       {
           Mapper.Initialize(cfg =>
           {
               cfg.AddProfile<Mappings>();
           });
       }


      [Task]
      public void Map_Should_HaveValidConfig()
      {
          Mapper.AssertConfigurationIsValid();
      }
```
```

---
Config automapper:

```c#
private readonly IMapper _mapper;

public EmployeeMapping()
{
    var config = new MapperConfiguration(cfg => { cfg.AddProfile<MapperProfile>(); });
    _mapper = config.CreateMapper();
}
```

-----
Utiliser dans le mapper profile : Profile, pour mapper un constructeur existant
sur 
```c#
CreateMap<T,V>()
	.ForCtorParam()....
```

On remplace par 
```c#
CreateMap<T,V>()
	.MapRecordMember(v => v.val, t => t.otherVal)
```

Permet d'être plus résilient qu'en utilisant les string du ctorParam par défaut.

```c#
public static class AutoMapperExtensions
{
    public static IMappingExpression<TSource, TDestination> MapRecordMember<TSource, TDestination, TMember>(
        this IMappingExpression<TSource, TDestination> mappingExpression,
        Expression<Func<TDestination, TMember>> destinationMember, Expression<Func<TSource, TMember>> sourceMember)
    {
        var memberName = ReflectionHelper.FindProperty(destinationMember).Name;

        return mappingExpression
            .ForMember(destinationMember, opt => opt.MapFrom(sourceMember))
            .ForCtorParam(memberName, opt => opt.MapFrom(sourceMember));
    }
}
```