[Voir l'infographie](https://www.canva.com/design/DAFIdl08MkE/HOKTdqwuaKbOdoUi3epFaA/edit?utm_content=DAFIdl08MkE&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton)
[Icônes de l'infographie, style stickers](https://icones8.fr/icon/set/direction/stickers)

## 1 - what is complexity
The overall goal of design is to reduce complexity

Complexity in a software, is anything that make it:
* hard to understand
* hard to modify

Caused by
* Dependencies, piece of code cannot be understand / modified in isolation
* obscurity, any info not obvious

Manifest through
 * Change amplification ("simple" change --> lots of modifications)
 * Cognitive load: need lots of information to understand it
 * Unknown unknowns, anything not obvious or hidden (dependancy or knowledge)

Fight complexity with a good design, it makes a system obvious

## 2 - How complexity happens
Complexity isn’t caused by a single catastrophic error; it accumulates in lots of small chunks

Invest in your software --> tactic vs strategic programming
Tactical Programming
* Finish a task as quickly as possible.
* Main focus is to get something working, such as a new feature or a bug

	Tactical tornado
	* Implements feature really quick
	* Pumps out code faster than anyone else
	* Rest of the team clean slowly after the tornado

Strategic programming
* produce a great design, which also happens to work
* Proactive: try multiple designs
* Reactive: don't patch around design problems, take the time to fix it when you discover it
![[Fig 2.png]]

How much to invest ?
A huge up-front investment, such as trying to design the entire system, won’t be effective .waterfall model rarely works well for software. it isn’t possible to visualize the design for a large software system well enough to understand all of its implications before building anything

10 ~ 20% total dev time on investments
Ex: Facebook's motto, from “Move fast and break things.” (tactical) to “Move fast with solid infrastructure” (starting 2014)

## 3 - Design simple systems
#### Deep modules
Modules can take many forms, such as classes, subsystems, or services
 each module in two parts: an interface and an implementation
 In an ideal world, each module would be completely independent of the others
 
The best modules are those whose interfaces are much simpler than their implementations
Deep modules vs shallow modules
![[Fig 3.png]]

Includes unimportant details --> abstraction more complicated --> cognitive load
Omits important details --> obscurity, false abstraction: it 

**Bad pattern: classitis**,  “classes should be small”, minimize the amount of functionality in each new class, Classitis may result in classes that are individually simple, but it increases the complexity of the overall system.

“somewhat general-purpose” means that the module’s functionality should reflect your current needs, but its interface should not
the interface should be general enough to support multiple uses.

#### Information Hiding
each module should encapsulate a few pieces of knowledge, which represent design decisions.
it simplifies the interface to a module
makes it easier to evolve the system --> there are no dependencies on that information outside the module
In
**Red flag: information leakage** the same knowledge is used in multiple places

Red flag: temporal decomposition**  execution order is reflected in the code structure. If the same knowledge is used at different points in execution, it gets encoded in multiple places, resulting in information leakage.

interfaces should be designed to make the common case as simple as possible. --> should “do the right thing” without being explicitly asked. Defaults params / autoconfiguration enable this.

**Red flag: overexposure** API for a commonly used feature forces users to learn about other features that are rarely used (increase cognitive load)

## 4 - Fight existing complexity
Fightingh Complexity:
* eliminate it
* encapsulate it away, isolating complexity in a place where it will never be seen (nearly equivalent to elimination)


“somewhat general-purpose” means that the module’s functionality should reflect your current needs, but its interface should not
the interface should be general enough to support multiple uses.
**What is the simplest interface that will cover all my current needs?**
**In how many situations will this method be used?**
**Is this API easy to use for my current needs?**


The “different layer, different abstraction” rule is just an application of an element must eliminate some complexity that would be present in the absence of the design element.  if different layers have the same abstraction,=  there’s a good chance that they haven’t provided enough benefit to compensate for the additional infrastructure they represent


#### Pull complexity Downwards
it is more important for a module to have a simple interface than a simple implementation.
As a developer, it’s tempting to behave in the opposite fashion: solve the easy problems and punt the hard ones to someone else.
Configuration parameters are an example of moving complexity upwards instead of down.
**“will users (or higher-level modules) be able to determine a better value than we can determine here?**
makes the most sense if
	* the complexity being pulled down is closely related to the class’s existing functionality
	* pulling the complexity down will result in many simplifications elsewhere
	* pulling the complexity down simplifies the class’s interface


#### Better Together Or Better Apart?
 the act of subdividing creates additional complexity
	* more components, harder to keep track of them,  harder to find a desired component
	* additional code to manage components --> using multiple objects instead of one
	* Separation makes it harde to see the components, or even to be aware of their existence. If the components are truly independent, then separation is good
	* Subdivision can result in duplication:

Bring together if information is shared
Bring together if it will simplify the interface (happens when the original modules each implement part of the solution to a problem)
Bring together to eliminate duplication
	not if 
	snippet only one or two lines long, there may not be much benefit in replacing it
	snippet interacts in complex ways with its environment, the replacement method might require a complex signature (such as many pass-by-reference arguments)

Red Flag: Conjoined Method
It should be possible to understand each method independently. If you can’t understand the implementation of one method without also understanding the implementation of another, that’s a red flag. This red flag can occur in other contexts as well: if two pieces of code are physically separated, but each can only be understood by looking at the other, that is a red flag.

Separate 
	In general, the lower layers of a system tend to be more general-purpose and the upper layers more special-purpose
Splitting up a method only makes sense if it results in cleaner abstractions, overall

#### exceptions
Code that deals with special conditions is inherently harder to write than code that deals with normal case

exception handling code creates opportunities for more exceptions

A [recent study](https://www.usenix.org/system/files/conference/osdi14/osdi14-paper-yuan.pdf) found that more than 90% of catastrophic failures in distributed data-intensive systems were caused by incorrect error handling

It’s difficult to ensure that exception handling code really works

rather than figuring out a clean way to handle it, just throw an exception and punt the problem to the caller --> pull complexity downwards 
	* Mask exceptions at a low level (if not needed outside)
	* define your APIs so that there are no exceptions to handle (null, default values,...)
	* Design special cases out of existence
**classes with lots of exceptions have complex interfaces, and they are shallower than classes with fewer exceptions**

One way of thinking about exception aggregation is that it replaces several special-purpose mechanisms, each tailored for a particular situation, with a single general-purpose mechanism that can handle multiple situations

The fourth technique for reducing complexity related to exception handling is to crash the application.  ertain errors that it’s not worth trying to handle

#### Comment
--- todo: faire section comment
**The best way to ensure that comments get updated is to position them close to the code they describe** (not in commit log, far away in header, ...) & avoid doc duplication
Higher-level comments are easier to maintain

#### Explicit naming

#### be consistent

Consistency creates **cognitive leverage**: once you have learned how something is done in one place, you can use that knowledge to immediately understand other places that use the same approach.

#### make the code obvious

## 5 - Tips
#### Design it twice
#### Write comments first

## 6 - Designing for performance