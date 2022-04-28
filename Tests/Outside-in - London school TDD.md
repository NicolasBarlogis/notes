London Advantages

Behavioral Focused

This approach helps to flush out how users will navigate the application by starting at the entry points of your system and working your way down to lower layers.  Obviously, this will require heavy use of test doubles as you drill your way down.  While this helps to keep your code base small and ensures you're not writing dead code, it does tend to create brittle tests — making refactoring more difficult, which discourages continuous refactoring.

Command-Query Separation[_launch_](https://devlead.io/DevTips/CommandQuerySeparation)

Focusing on behavior helps to manage side effects by promoting pure functions — providing a gateway to functional programming.

London Disadvantages

Fragile Tests[_launch_](https://devlead.io/DevTips/Fragility)

While test-driving out behavior helps to keep your code base small and ensures you're not writing dead code, it does tend to create tests that break easily.

Difficult Refactoring

Having tests that are tightly coupled with your production code will make continuous refactoring very difficult and time-consuming.  And this is one of the biggest drawbacks of top-down TDD — dealing with broken tests with most code changes.

https://devlead.io/DevTips/LondonVsChicago