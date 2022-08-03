https://milkov.tech/assets/psd.pdf

## Preface
there has been surprisingly little conversation about how to design those programs or what good programs should look like
The most fundamental problem in computer science is problem decomposition: how to take a complex problem and divide it up into pieces that can be solved independently
Many people assume that software design skill is an innate talent that cannot be taught (see, for example, **Talent is Overrated** by Geoff Colvin)
The overall goal is to reduce complexity; this is more important than any particular principle or idea you read here

---

## Chapter 1
#### Intro
All programming requires is a creative mind and the ability to organize your thoughts.
the greatest limitation in writing software is our ability to understand the systems we are creating
two general approaches to fighting complexity
	* eliminate complexity by making code simpler and more obvious
	* encapsulate it, so that programmers can work on a system without being exposed to all of its complexity at once --> modular design
In modular design, a software system is divided up into modules, such as classes in an object-oriented language. The modules are designed to be relatively independent of each other. Work on one module without having to understand the details of other modules

waterfall model rarely works well for software. it isn’t possible to visualize the design for a large software system well enough to understand all of its implications before building anything.
The incremental approach works for software because software is malleable enough to allow significant design changes partway through implementation. design is never done. also means continuous redesign


This book is about how to use complexity to guide the design of software throughout its lifetime
This book has two overall goals
	* describe the nature of software complexity: what does “complexity” mean, why does it matter, and how can you recognize when a program has unnecessary complexity
	* present techniques to minimize complexity
there isn’t a simple recipe that will guarantee great software designs

#### 1.1 How to use this book
best way to use this book is in conjunction with code reviews
easier to see design problems in someone else’s code than your own
learn to recognize red flags: signs that a piece of code is probably more complicated than it needs to be.

---

## Chapter 2
#### The Nature of Complexity
It is easier to tell whether a design is simple than it is to create a simple design

#### 2.1 Complexity defined
**Complexity is anything related to the structure of a software system that makes it hard to understand and modify the system**
Complexity is what a developer experiences at a particular point in time when trying to achieve a particular goal. It doesn’t necessarily relate to the overall size or functionality of the system
Complexity is determined by the activities that are most common. If a system has a few parts that are very complicated, but those parts almost never need to be touched, then they don’t have much impact on the overall complexity of the system
![[A Philosophy of Software Design - Fig 1.png]]
The overall complexity of a system (C) is determined by the complexity of each part p (cp ) weighted by the fraction of time developers spend working on that part (tp ). **Isolating complexity in a place where it will never be seen is almost as good as eliminating the complexity entirely**

#### 2.2 Symptoms of complexity
Complexity manifests itself in three general ways
	* Change amplification: seemingly simple change requires code modifications in many different places
	* Cognitive load: how much a developer needs to know in order to complete a task. more time learning the required information, greater risk of bugs because they have missed something important.
	  **Sometimes an approach that requires more lines of code is actually simpler, because it reduces cognitive load.**
	* Unknown unknowns: not obvious which pieces of code must be modified to complete a task, or what information a developer must have to carry out the task successfully.
One of the most important goals of good design is for a system to be obvious. This is the opposite of high cognitive load and unknown unknowns.

#### 2.3 Causes of complexity
Complexity is caused by two things
	* dependencies: a dependency exists when a given piece of code cannot be understood and modified in isolation
	* obscurity: occurs when important information is not obvious

 Dependencies are a fundamental part of software and can’t be completely eliminated. One of the goals of software design is to reduce the number of dependencies and to make the dependencies that remain as simple and obvious as possible.
 
 Obscurity is often associated with dependencies, where it is not obvious that a dependency exists.
 The best way to reduce obscurity is by simplifying the system design.
 Obscurity creates unknown unknowns, and also contributes to cognitive load
 
#### 2.4 Complexity is incremental
Complexity isn’t caused by a single catastrophic error; it accumulates in lots of small chunks
The incremental nature of complexity makes it hard to control.

---

## Chapter 3
#### Working Code Isn’t Enough (Strategic vs. Tactical Programming)
a tactical mindset, focused on getting features working as quickly as possible.
if you want a good design, you must take a more strategic approach where you invest time to produce clean designs and fix problems

#### 3.1 Tactical programming
In the tactical approach, your main focus is to get something working, such as a new feature or a bug
you’re trying to finish a task as quickly as possible.
Perhaps you have a hard deadline. As a result, planning for the future isn’t a priority. You don’t spend much time looking for the best design
This is how systems become complicated. 

tactical programming to the extreme: a tactical tornado
a prolific programmer who pumps out code far faster than others but works in a totally tactical fashion
implementing a quick feature, nobody gets it done faster than the tactical tornado
other engineers must clean up the messes left behind by the tactical tornado, which makes it appear that those engineers (who are the real heroes) are making slower progress than the tactical tornado

#### 3.2 Strategic programming
The first step towards becoming a good software designer is to realize that working code isn’t enough
you should not think of “working code” as your primary goal
Your primary goal must be to produce a great design, which also happens to work. This is strategic programming.
proactive (try a couple of alternative designs and pick the cleanest one) && reactive (When you discover a design problem, don’t just ignore it or patch around it; take a little extra time to fix it.)

#### 3.3 How much to invest?
A huge up-front investment, such as trying to design the entire system, won’t be effective (waterfall)
the best approach is to make lots of small investments on a continual basis

I suggest spending about 10– 20% of your total development time on investments
![[A Philosophy of Software Design - Fig 2.png]]

#### 3.4 Startups and investment
In some environments there are strong forces working against the strategic approach
many startups take a tactical approach. They rationalize this with the thought that, if they are successful, they’ll have enough money to hire extra engineers to clean things up.
once a code base turns to spaghetti, it is nearly impossible to fix
Furthermore, the payoff for good (or bad) design comes pretty quickly

The best way to lower development costs is to hire great engineers.
best engineers care deeply about good design. If your code base is a wreck, this will make it harder for you to recruit

Facebook is an example of a startup that encouraged tactical programming.
motto was “Move fast and break things.”
much of the code was unstable and hard to understand, with few comments or tests, and painful to work with
changed its motto to “Move fast with solid infrastructure”

#### 3.5 Conclusion
Good design doesn’t come for free. It has to be something you invest in continually, so that small problems don’t accumulate into big ones. Fortunately, good design eventually pays for itself, and sooner than you might think.
When you get in a crunch it will be tempting to put off cleanups until after the crunch is over
Once you start delaying design improvements, it’s easy for the delays to become permanent and for your culture to slip into the tactical approach. the solutions become more intimidating, which makes it easy to put them off even more.

---

## Chapter 4
#### Modules Should Be Deep
## 4.1 Modular design
In modular design, software system is decomposed into a collection of modules that are relatively independent.
Modules can take many forms, such as classes, subsystems, or services
In an ideal world, each module would be completely independent of the others
In this world, the complexity of a system would be the complexity of its worst module
For example, the arguments for a method create a dependency between the method and any code that invokes the method

we think of each module in two parts: an interface and an implementation
The interface consists of everything that a developer working in a different module must know in order to use the given module. --> the what
The implementation consists of the code that carries out the promises made by the interface --> the how
A developer should not need to understand the implementations of modules other than the one he or she is working in

The best modules are those whose interfaces are much simpler than their implementations
	* simple interface minimizes the complexity that a module imposes on the rest of the system.
	* if a module is modified in a way that does not change its interface, then no other module will be affected by the modification

#### 4.2 What's in an interface
The interface to a module contains two kinds of information:
	* The formal parts of an interface are specified explicitly in the code, and some of these can be checked for correctness by the programming language. Ex: method signature
	* informal elements. These are not specified in a way that can be understood or enforced by the programming language. Ex: high-level behavior, one method must be called before another. if a developer needs to know a particular piece of information in order to use a module, then that information is part of the module’s interface
One of the benefits of a clearly specified interface is that it indicates exactly what developers need to know in order to use the associated module. This helps to eliminate the “unknown unknowns”

#### 4.3 Abstractions
An abstraction is a simplified view of an entity, which omits unimportant details
In modular programming, each module provides an abstraction in form of its interface. The interface presents a simplified view of the module’s functionality;
The more unimportant details that are omitted from an abstraction, the better

Includes unimportant details --> abstraction more complicated --> cognitive load
Omits important details --> obscurity, false abstraction: it might appear simple, but in reality it isn’t

#### 4.4 Deep modules
The best modules are those that provide powerful functionality yet have simple interfaces. I use the term deep to describe such modules
![[A Philosophy of Software Design - Fig 3.png]]

cost versus benefit
The benefit provided by a module is its functionality. The cost of a module (in terms of system complexity) is its interface.

#### 4.5 Shallow modules
Shallow classes are sometimes unavoidable, but they don’t provide help much in managing complexity
>[!danger] Red Flag: Shallow Module
> A shallow module is one whose interface is complicated relative to the functionality it provides. Shallow modules don’t help much in the battle against complexity, because the benefit they provide (not having to learn about how they work internally) is negated by the cost of learning and using their interfaces. Small modules tend to be shallo

#### 4.6 Classitis
The conventional wisdom in programming is that classes should be small, not deep.
break up larger classes into smaller ones. This approach results in large numbers of shallow classes and methods, which add to overall system complexity.

The extreme of the “classes should be small” approach is a syndrome I call classitis, which stems from the mistaken view that “classes are good, so more classes are better.” minimize the amount of functionality in each new class

Classitis may result in classes that are individually simple, but it increases the complexity of the overall system.

#### 4.7 Examples: Java and Unix I/O
**interfaces should be designed to make the common case as simple as possible**
--> in Unix, sequential I/O is most common, so they made that the default behavior

---

## Chapter 5
#### Information Hiding (and Leakage)
#### 5.1 Information hiding
The most important technique for achieving deep modules is information hiding. first described by [David Parnas](https://www.win.tue.nl/~wstomv/edu/2ip30/references/criteria_for_modularization.pdf).
each module should encapsulate a few pieces of knowledge, which represent design decisions. The knowledge is embedded in the module’s implementation but does not appear in its interface, so it is not visible to other modules
The information hidden within a module usually consists of details about how to implement some mechanism
it simplifies the interface to a module
makes it easier to evolve the system --> there are no dependencies on that information outside the module
If you can hide more information, you should also be able to simplify the module’s interface, and this makes the module deeper.

#### 5.2 Information leakage
opposite of information hiding
Information leakage occurs when a design decision is reflected in multiple modules. This creates a dependency
If a piece of information is reflected in the interface for a module, then by definition it has been leaked
information can be leaked even if it doesn’t appear in a module’s interface
> [!danger] Red Flag: Information Leakage
> Information leakage occurs when the same knowledge is used in multiple places, such as two different classes that both understand the format of a particular type of file

#### 5.3 Temporal decomposition
In temporal decomposition, the structure of a system corresponds to the time order in which operations will occur
When designing modules, focus on the knowledge that’s needed to perform each task, not the order in which tasks occur.
> [!danger] Red Flag: Temporal Decomposition
> In temporal decomposition, execution order is reflected in the code structure: operations that happen at different times are in different methods or classes. If the same knowledge is used at different points in execution, it gets encoded in multiple places, resulting in information leakage.

#### 5.4 Example: HTTP server
#### 5.5 Example: too many classes
**information hiding can often be improved by making a class slightly large**
bring together all of the code related to a particular capability (ex: correct temporal decomposition)
raise the level of the interface; for example, rather than having separate methods for each of three steps of a computation, have a single method that performs the entire computation. This can result in a simpler interface

#### 5.6 Example: HTTP parameter handling
Rather than returning a single parameter, the method returns a reference to the Map used internally to store all of the parameters.
--> shallow interface + information leakage

#### 5.7 Example: defaults in HTTP responses
Defaults illustrate the principle that interfaces should be designed to make the common case as simple as possible. They are also an example of partial information hiding: in the normal case, the caller need not be aware of the existence of the defaulted item
Whenever possible, classes should “do the right thing” without being explicitly asked
> [!danger] Red Flag: Overexposure
> If the API for a commonly used feature forces users to learn about other features that are rarely used, this increases the cognitive load on users who don’t need the rarely used features.

##### 5.8 Information hiding within a class
design the private methods within a class so that each method encapsulates some information or capability and hides it from the rest of the class
minimize the number of places where each instance variable is used

#### 5.9 Taking it too far
If the information is needed outside the module, then you must not hide it. (ex: conf param)
if a module can automatically adjust its configuration, that is better than exposing configuration parameters

#### 5.10 Conclusion
Information hiding and deep modules are closely related

## Chapter 6
#### General-Purpose Modules are Deeper
The general-purpose approach seems consistent with the strategic programming.
The special-purpose approach seems consistent with an incremental approach to software development.

#### 6.1 Make classes somewhat general-purpose
“somewhat general-purpose” means that the module’s functionality should reflect your current needs, but its interface should not
the interface should be general enough to support multiple uses.
don’t get carried away and build something so general-purpose that it is difficult to use for your current needs.

#### 6.2 Example: storing text for an editor
file manipulation class:
```
void backspace(Cursor cursor);
void delete(Cursor cursor);
```
leakage between the user interface and the text class

#### 6.3 A more general-purpose API
make the text class more generic. Its API should be defined only in terms of basic text features, without reflecting the higher-level operations that will be implemented with it

#### 6.4 Generality leads to better information hiding
One of the most important elements of software design is determining who needs to know what, and when.
When the details are important, Hiding this information behind an interface just creates obscurit

#### 6.5 Questions to ask yourself
**What is the simplest interface that will cover all my current needs?**
reduce the number of methods in an API without reducing its overall capabilities, then you are probably creating more general-purpose methods
Reducing the number of methods makes sense only as long as the API for each individual method stays simple
**In how many situations will this method be used?**
If a method is designed for one particular use, such as the backspace method, that is a red flag that it may be too special-purpose
**Is this API easy to use for my current needs?**
This question can help you to determine when you have gone too far in making an API simple and generalpurpose.

---

## Chapter 7
#### Different Layer, Different Abstraction
Software systems are composed in layers, where higher layers use the facilities provided by lower layers

#### 7.1 Pass-through methods
When adjacent layers have similar abstractions, the problem often manifests itself in the form of pass-through methods
A pass-through method is one that does little except invoke another method, whose signature is similar or identical to that of the calling method.
> [!danger] Red Flag: Pass-Through Method
> A pass-through method is one that does nothing except pass its arguments to another method, usually with the same API as the pass-through method. This typically indicates that there is not a clean division of responsibility between the classes.

They increase the interface complexity of the class, which adds complexity, but they don’t increase the total functionality of the system.
indicate confusion over the division of responsibility between classes

#### 7.2 When is interface duplication OK?
One example where it’s useful for a method to call another method with the same signature is a dispatcher. A dispatcher is a method that uses its arguments to select one of several other methods to invoke
When several methods provide different implementations of the same interface, it reduces cognitive load. Once you have worked with one of these methods, it’s easier to work with the other

#### 7.3 Decorators
The decorator design pattern (also known as a “wrapper”) is one that encourages API duplication across layers
The motivation for decorators is to separate special-purpose extensions of a class from a more generic core. However, decorator classes tend to be shallow: they introduce a large amount of boilerplate for a small amount of new functionality
consider alternatives:
	* add the new functionality directly to the underlying class
	* If the new functionality is specialized for a particular use case, merge it with the use case
	* merge the new functionality with an existing decorator rather than creating a new
	* implement it as a stand-alone class that is independent of the base class

#### 7.4 Interface versus implementation
the representations used internally should be different from the abstractions that appear in the interface. If the two have similar abstractions, then the class probably isn’t very deep
If the two have similar abstractions, then the class probably isn’t very deep
exemple: exposing array as array

#### 7.5 Pass-through variables
a variable that is passed down through a long chain of methods.
Ex: A command-line argument describes certificates to use for secure communication. This information is only needed by a low-level method m3, which calls a library method to open a socket, but it is passed down through all the methods on the path between main and m3.
they add complexity because they force all of the intermediate methods to be aware of their existence,
Eliminating pass-through variables can be challenging:
	* see if there is already an object shared between the topmost and bottommost methods
	* store the information in a global variable (but it creates other problems)
	* The solution I use most often is to introduce a context object . stores all of the application’s global state.
the context will probably be needed in many places, so it can potentially become a pass-through variable. To reduce the number of methods that must be aware of it, ,a reference to the context can be saved in most of the system’s major objects
Contexts are far from an ideal solution. The variables stored in a context have most of the disadvantages of global variables
it may not be obvious why a particular variable is present, or where it is used. Without discipline, a context can turn into a huge grab-bag of data that creates nonobvious dependencies
Contexts may also create thread-safety issues; the best way to avoid problems is for variables in a context to be immutable. Unfortunately, I haven’t found a better solution than contexts.

#### Conclusion
The “different layer, different abstraction” rule is just an application of an element must eliminate some complexity that would be present in the absence of the design element.  if different layers have the same abstraction,=  there’s a good chance that they haven’t provided enough benefit to compensate for the additional infrastructure they represent

---

## Chapter 8
#### Pull complexity Downwards
it is more important for a module to have a simple interface than a simple implementation.
As a developer, it’s tempting to behave in the opposite fashion: solve the easy problems and punt the hard ones to someone else.

#### 8.1 Example: editor text class

#### 8.2 Example: configuration parameters
Configuration parameters are an example of moving complexity upwards instead of down.
easy excuse to avoid dealing with important issues and pass them on to someone else
it’s difficult or impossible for users or administrators to determine the right values for the parameters. In other cases, the right values could have been determined automatically with a little extra work. In contrast, configuration parameters can easily become out of date.
**“will users (or higher-level modules) be able to determine a better value than we can determine here?**

#### 8.3 Taking it too far
pulling complexity downward is an idea that can easily be overdone.
makes the most sense if
	* the complexity being pulled down is closely related to the class’s existing functionality
	* pulling the complexity down will result in many simplifications elsewhere
	* pulling the complexity down simplifies the class’s interface

---

## Chapter 9
#### Better Together Or Better Apart?
given two pieces of functionality, should they be implemented together in the same place, or should their implementations be separated?
he goal is to reduce the complexity of the system as a whole and improve its modularity.
modularity --> divide the system into a large number of small components
--> the act of subdividing creates additional complexity
	* more components, harder to keep track of them,  harder to find a desired component
	* additional code to manage components --> using multiple objects instead of one
	* Separation makes it harde to see the components, or even to be aware of their existence. If the components are truly independent, then separation is good
	* Subdivision can result in duplication:

Here are a few indications that two pieces of code are related:
	* They share information (ex: particular syntax of document type)
	* They are always / often used together, the relation is bidirectionnal bidirectional
	* They overlap conceptually, in that there is a simple higher-level category that includes both of the pieces of code. For example, searching for a substring and case conversion both fall under the category of string manipulation
	* It is hard to understand one of the pieces of code without looking at the other

#### 9.1 Bring together if information is shared
ex: parsing http header / body --> need knwoledge about http

#### 9.2 Bring together if it will simplify the interface
it may be possible to define an interface for the new module that is simpler or easier to use than the original interfaces. This often happens when the original modules each implement part of the solution to a problem

#### 9.3 Bring together to eliminate duplication
factor the repeated code out into a separate method --> most effective if the repeated code snippet is long and the replacement method has a simple signature
snippet only one or two lines long, there may not be much benefit in replacing it
snippet interacts in complex ways with its environment, the replacement method might require a complex signature (such as many pass-by-reference arguments), which would reduce its value.
refactor the code so that the snippet in question only needs to be executed in one place

#### 9.4 Separate general-purpose and special-purpose code
Special-purpose code associated with a general-purpose mechanism should normally go in a different module

> [!danger] Red Flag: Repetition
> If the same piece of code (or code that is almost the same) appears over and over again, that’s a red flag that you haven’t found the right abstractions.

In general, the lower layers of a system tend to be more general-purpose and the upper layers more special-purpose

#### 9.5 Example: insertion cursor and selection
> [!danger] Red Flag: Special-General Mixture
> This red flag occurs when a general-purpose mechanism also contains code specialized for a particular use of that mechanism. This makes the mechanism more complicated and creates information leakage between the mechanism and the particular use case: future modifications to the use case are likely to require changes to the underlying mechanism as well.

Ex: mix text selection & cursor

#### 9.6 Example: separate class for logging
highly specific logger added complexity with no benefit. Each method was only invoked in a single place

#### 9.7 Example: editor undo mechanism

#### 9.8 Splitting and joining methods
length by itself is rarely a good reason for splitting up a method
Splitting up a method introduces additional interfaces, which add to complexity.
When designing methods, the most important goal is to provide clean and simple abstractions. **Each method should do one thing and do it completely**
The method should be deep: its interface should be much simpler than its implementation
If a method has all of these properties, then it probably doesn’t matter whether it is long or not.

Splitting up a method only makes sense if it results in cleaner abstractions, overall
The best way of splitting by factoring out a subtask into a separate method
Typically this means that the child method is relatively general-purpose: it could conceivably be used by other methods besides the parent

