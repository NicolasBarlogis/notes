https://blog.cleancoder.com/uncle-bob/2014/12/17/TheCyclesOfTDD.html

#### **Second-by-Second** _nano-cycle_: The Three Laws of TDD.
1.  You must write a failing test before you write any production code.
2.  You must not write more of a test than is sufficient to fail, or fail to compile.
3.  You must not write more production code than is sufficient to make the currently failing test pass.

#### **Minute-by-Minute**: _micro-cycle_: Red-Green-Refactor
1.  Create a unit tests that fails
2.  Write production code that makes that test pass.
3.  Clean up the mess you just made.

#### **Decaminute-by-Decaminute**: _milli-cycle_: Specific/Generic
This is the cycle in which we apply the [_Transformation Priority Premise_](http://en.wikipedia.org/wiki/Transformation_Priority_Premise). We look for the symptoms of over-specificity by checking the kinds of production code we have written.


#### **Hour-by-Hour**: _Primary Cycle_: Boundaries
So every hour or so we stop and look at the overall system. We hunt for boundaries that we want to control. We make decisions about where to draw those boundaries, and which side of those boundaries our current activities should be constrained to.