Chicago Advantages

Strong Safety Net[_launch_](https://devlead.io/DevTips/SafetyNet)

Since we are starting at the lower levels of the architecture, we are continuously building on prior tests.  This tends to produce tests that are decoupled from the implementation — enabling aggressive refactoring of the implementation without breaking the existing tests.  This in turn provides a highly redundant regression test suite that provides a strong safety net for continuous refactoring.

High Cohesion

As we progressively write very specific tests to more generalized tests, the resulting production code becomes highly cohesive.  As the tests become more general, the production code becomes more specific.  This promotes high cohesion.  And with high cohesion comes loose coupling.  Which in turn promotes high code quality including maintainability, testability, extensibility, etc.

Minimizes Test Doubles

Building from the inside out requires far fewer test doubles since we are building on top of previously written tests.  There is rarely a need to stub out or mock dependencies since, in most cases, we build them as a result of prior (lower) tests.  This helps to develop a more reliable, less fragile test suite.

Chicago Disadvantages

YAGNI[_launch_](https://devlead.io/DevTips/YAGNI)

Building from the inside often fosters over-engineering — writing code that is often not needed at the end of the day.

https://devlead.io/DevTips/LondonVsChicago