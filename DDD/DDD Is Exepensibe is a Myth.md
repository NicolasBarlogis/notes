# [DDD Is Expensive Is A Myth](http://blog.sapiensworks.com/post/2016/05/12/ddd-is-expensive-myth)

published on   **12 May 2016** in [Domain driven design](http://blog.sapiensworks.com/tags.html#Domain%20driven%20design)

If only I had a dollar every time I read something similar to this: "DDD is expensive and it should be used ONLY if the domain is BIG, behaviour rich and only for the core domain, not for the whole application" .

Well... today, I would say DDD is mainstream. Sure, from the crowd of people "doing DDD" only a minority actually does DDD, but nevertheless, there are plenty of experienced developers or architects who can properly apply DDD. Now, DDD isn't expensive by its intrinsic nature, but the people who can apply it aren't that cheap and if they deal with a complex domain, it could take some time. So, in the end you can say it's expensive.

However, thing is, a company is already paying those people and it doesn't pay them more because they're using DDD, that's just another development tool. So, it's not about the money. But what about time? Can we afford the time spend to understand the domain BEFORE we start coding? Well, can we afford NOT to? In what universe is it a better idea to just start coding functionality you don't fully understand?

"Hey, DDD is too complex for a CRUD app" you might say. How do you know it's a CRUD app, _without_ having a domain model? For any app, simple or complex you need to gather all the information you need in order to implement it. As I've said in the past [DDD is not programming](https://t.co/aLAf0Dbx4r), it's just a methodology to get that information, to understand the functionality that needs to be implemented. Only after you know the model, you can say: it's a CRUD app or it's a rich-behaviour app, but not before.

What about the tactical patterns? Guess what, they're still not code, but **ways to group information**. When I'm identifying an aggregate I'm interested in concepts and business rules. If I end up with some state and some validation, this is what the domain needs. And based on that I will choose an implementation which will be CRUDy in nature.

I think the problem lies in how the devs look at DDD: like it's a way to design objects (OOP), instead of a way to understand the relevant parts of the domain. And they think they **have** to use a certain architecture or 'behaviour-rich' objects or the [Repository pattern](http://blog.sapiensworks.com/post/2014/06/02/The-Repository-Pattern-For-Dummies.aspx). They view it as a complex solution which requires a complex problem (but how can they know upfront how complex the problem is?!).

Once someone understands the nature and purpose of DDD, they can apply it as the first step in building (any)software. And the result will be a specific (relevant) domain model (abstraction) which will contain all the information developers need in order to write the code. And btw, CRUD apps are the easiest and fastest to model with DDD.