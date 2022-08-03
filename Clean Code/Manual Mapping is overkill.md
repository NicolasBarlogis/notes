# [The Myth of Manual POCO Mapping Performance](http://blog.sapiensworks.com/post/2016/03/02/ado-net-manual-mapping)

published on   **02 March 2016** in [.net](http://blog.sapiensworks.com/tags.html#.net)

We are in 2016 and I still see developers proud that they're using Ado.Net and writing all that boring mapping code because "it's faster" and they want maximum performance. Well, the phrase "stealing from your employer" comes to mind and in this post I'm going to tell you why.

As the author of an object mapper a.ka. micro-ORM called [SqlFu](https://github.com/sapiens/SqlFu), for version 3 I've decided to change how I do the benchmarks. In previous versions, I've taken the easy way and benchmarked _whole queries_ but this meant I also benchmarked the server configuration and ado.net performance. So, I wanted to test **only** the maping part from a `DbDataReader` to a POCO. This way, I could measure exactly the differences between manual mapping and code generated mapping.

The results were pretty much what I've expect them to be:

-   manualy mapping to a POCO is between 80-100%+ faster than code generated
-   manualy mapping to an anonymous object (projection) is similar to the previous case
-   manualy mapping to dynamic is ~50% **slower** than code generated.

Yes, it's _faster_ to use SqlFu to map to dynamic, but that's not the point. In fairness, when doing dynamic mappings, we're cheating a bit, by using specially crafted objects.However, the interested part wasn't this. The interesting part was transforming the relative numbers to absolute numbers.

Sure, manually mappings are 2x faster but you see, it's the difference between 4 microseconds and 8 microseconds. Let me put that into perspecitve: assuming you have a 1 ms query, which is a _very fast_ query, the performance gain you get by doing things manually is .... 0.4%. For a 1 ms query. Let that sink in for a bit.

You're spending at least 30 seconds (that's 30 000 000 microseconds) writing boring, repetitive and error prone code for a 0.4% performance gain (and that's when you're query is already fast). Sure, you might say that "every microsecond counts" but if you need _that_ kind of performance, I'm afraid you've chosen the wrong platform to start with. .Net is fast, but in a scenario where _every_ microsecond matters, carefully written C code is much better.

Thing is, if you have slow queries, then manually object mapping should be last on your optimizations list. Ensure the query is good, the server is well configured, the network has low latency etc. The 0.4% impact the code generated mapping has is simply **insignificant**. You're wasting time(money) and maintainability for an optimization that doesn't really matter in the big picture. And you should always _properly_ benchmark things to assess if it's worth spending time for 'optimizations'.

In 2016, the only excuse you have to use Ado.Net directly is that you're building your own (micro)ORM. For the other use cases, you're just wasting time and money. And every time you're tempted to map to POCOs manually, ask yourself why StackOverflow, which is _obssessed_ with performance, prefers to use an object mapper. After all, the main and unique feature of dapper.net is speed. Why didn't they go with manual mapping, after all, even in their (old and kinda flawed and obsolete) benchmarks manual is still faster.

#article