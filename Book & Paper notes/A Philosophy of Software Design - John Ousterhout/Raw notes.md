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
![[Fig 1.png]]
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
![[Fig 2.png]]

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
![[Fig 3.png]]

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

The second way to break up a method is to split it into two separate methods, each visible to callers of the original method
if the original method had an overly complex interface because it tried to do multiple things that were not closely related
Ideally, most callers should only need to invoke one of the two new methods

> [!danger] Red Flag: Conjoined Method
> It should be possible to understand each method independently. If you can’t understand the implementation of one method without also understanding the implementation of another, that’s a red flag. This red flag can occur in other contexts as well: if two pieces of code are physically separated, but each can only be understood by looking at the other, that is a red flag.

---

## Chapter 10
#### Define Errors Out Of Existence
Exception handling is one of the worst sources of complexity in software systems. Code that deals with special conditions is inherently harder to write than code that deals with normal case

#### 10.1 Why exceptions add complexity
I use the term exception to refer to any uncommon condition that alters the normal flow of control in a program, exceptions can occur even without using a formal exception reporting mechanism, such as when a method returns a special value indicating that it didn’t complete its normal behavior.

When an exception occurs
	* move forward and complete the work in progress
	* abort the operation in progress and report the exception upwards 

exception handling code creates opportunities for more exceptions. Secondary exceptions occurring during recovery are often more subtle and complex than the primary exceptions

big try/catch: not obvious where each exception is generated
break up the code into many distinct try blocks
make it clear where exceptions occur, but break up the flow of the code and make it harder to read
A [recent study](https://www.usenix.org/system/files/conference/osdi14/osdi14-paper-yuan.pdf) found that more than 90% of catastrophic failures in distributed data-intensive systems were caused by incorrect error handling

It’s difficult to ensure that exception handling code really works. Some exceptions, such as I/O errors, can’t easily be generated in a test environment, so it’s hard to test the code that handles them, “code that hasn’t been executed doesn’t work”

#### 10.2 Too many exceptions
“the more errors detected, the better.” This leads to an over-defensive style.
It’s tempting to use exceptions to avoid dealing with difficult situations: rather than figuring out a clean way to handle it, just throw an exception and punt the problem to the caller
Generating an exception in a situation like this just passes the problem to someone else and adds to the system’s complexity.

**classes with lots of exceptions have complex interfaces, and they are shallower than classes with fewer exceptions**

best way to reduce the complexity damage caused by exception handling is to reduce the number of places where exceptions have to be handled

#### 10.3 Define errors out of existence
define your APIs so that there are no exceptions to handle: define errors out of existence
Ex: unset on non existant variable --> nothing
rather than deleting a variable, unset should ensure that a variable no longer exists

#### 10.4 Example: file deletion in Windows
Windows operating system does not permit a file to be deleted if it is open in a process
In Unix, if a file is open when it is deleted, Unix does not delete the file immediately. Instead, it marks the file for deletion, then the delete operation returns successfully. The file name has been removed from its directory, so no other processes can open the old file and a new file with the same name can be created, but the existing file data persists. Processes that already have the file open can continue to read it and write it normally. Once the file has been closed by all of the accessing processes, its data is freed

#### 10.5 Example: Java substring method
The error-ful approach may catch some bugs, but it also increases complexity, which results in other bugs
**Overall, the best way to reduce bugs is to make software simpler**

#### 10.6 Mask exceptions
e second technique for reducing the number of places where exceptions must be handled is exception masking
an exceptional condition is detected and handled at a low level in the system, so that higher levels of software need not be aware of the condition.

Exception masking doesn’t work in all situations, but it is a powerful tool in the situations where it works. It results in deeper classes, since it reduces the class’s interface (fewer exceptions for users to be aware of) and adds functionality in the form of the code that masks the exception. Exception masking is an example of pulling complexity downward.

####10.7 Exception aggregation
third technique for reducing complexity related to exceptions is exception aggregation
handle many exceptions with a single piece of code; rather than writing distinct handlers for many individual exceptions, handle them all in one place with a single handler.

Exception aggregation works best if an exception propagates several levels up the stack before it is handled; this allows more exceptions from more methods to be handled in the same place. This is the opposite of exception masking

One way of thinking about exception aggregation is that it replaces several special-purpose mechanisms, each tailored for a particular situation, with a single general-purpose mechanism that can handle multiple situations. This provides another illustration of the benefits of general-purpose mechanisms

#### 10.8 Just crash?
The fourth technique for reducing complexity related to exception handling is to crash the application. 
there will be certain errors that it’s not worth trying to handle
one example is “out of memory” errors that occur during storage allocation. an I/O error occurs while reading or writing an open file (such as a disk hard error), or if a network socket cannot be opened,

Whether or not it is acceptable to crash on a particular error depends on the application

#### 10.9 Design special cases out of existence
Special cases can result in code that is riddled with if statements. special cases should be eliminated
The best way to do this is by designing the normal case in a way that automatically handles the special cases without any extra code

ex: The notion of “no selection” makes sense in terms of how the user thinks about the application’s interface, but that doesn’t mean it has to be represented explicitly inside the application. Having a selection that always exists, but is sometimes empty and thus invisible, results in a simpler implementation.

#### 10.10 Taking it too far
Defining away exceptions, or masking them inside a module, only makes sense if the exception information isn’t needed outside the module
you must determine what is important and what is not important. Things that are not important should be hidden

---

## Chapter 11
#### Design it Twice
You’ll end up with a much better result if you consider multiple options for each major design decision: design it twice

Try to pick approaches that are radically different from each other; you’ll learn more that way
you are certain that there is only one reasonable approach, consider a second design anyway
list the pros and cons of each one.
	* Does one alternative have a simpler interface ?
	* Is one interface more general-purpose ?
	* Does one interface enable a more efficient implementation ?

Sometimes none of the alternatives is particularly attractive; when this happens, see if you can come up with additional schemes

The design-it-twice principle can be applied at many levels in a system

Designing it twice does not need to take a lot of extra time.
of time compared to the days or weeks you will spend implementing the class. The initial design experiments will probably result in a significantly better design, which will more than pay for the time spent designing it twice

 have noticed that the design-it-twice principle is sometimes hard for really smart people to embrace. When they are growing up, smart people discover that their first quick idea about any problem is sufficient for a good grade; there is no need to consider a second or third possibility. This makes it easy to develop bad work habits.
 Eventually, everyone reaches a point where your first ideas are no longer good enough
also improves your design skills

---

## Chapter 12
#### Why Write Comments? The Four Excuses
the process of writing comments, if done correctly, will actually improve a system’s design

#### 12.1 Good code is self-documenting
This is a delicious myth, like a rumor that ice cream is good for your health: we’d really like to believe it!
If users must read the code of a method in order to use it, then there is no abstraction/
Without comments, the only abstraction of a method is its declaration. Not enough for complexe function

#### 12.2 I don’t have time to write comments
if you allow documentation to be deprioritized, you’ll end up with no documentation.
investment mindset --> strategical development
Good comments make a huge difference in the maintainability
writing comments needn’t take a lot of time

#### 12.3 Comments get out of date and become misleading
Large changes to the documentation are only required if there have been large changes to the code, and the code changes will take more time than the documentation changes
keep the documentation close to the corresponding code
Code reviews provide a great mechanism for detecting and fixing stale comments

#### 2.4 All the comments I have seen are worthless
most existing documentation is so-so at best
writing solid documentation is not hard, once you know how

#### 12.5 Benefits of well-written comments
**The overall idea behind comments is to capture information that was in the mind of the designer but couldn’t be represented in the code**
When other developers come along later to make modifications, the comments will allow them to work more quickly and accurately.

Documentation can reduce cognitive load by providing developers with the information they need to make changes and by making it easy for developers to ignore information that is irrelevant.
Good documentation can clarify dependencies, and it fills in gaps to eliminate obscurity.

---

## Chapter 13
#### Comments Should Describe Things that Aren’t Obvious from the Code
programming language can’t capture all of the important information that was in the mind of the developer when the code was written.
**Developers should be able to understand the abstraction provided by a module without reading any code other than its externally visible declarations.**

#### 13.1 Pick conventions
ensure consistency, makes comments easier to read and understand.
ensure that you actually write comments

Most comments fall into one of the following categories:
	* **Interface**: immediately precedes the declaration of a module such as a class, data structure, function, or method. describe’s the module’s interface
	* **Data structure member**: comment next to the declaration of a field
	* **Implementation comment**: comment inside the code of a method
	* **Cross-module comment**: a comment describing dependencies that cross module boundaries.
most important comments are those in the first two categories

Every class should have an interface comment, every class variable should have a comment, and every method should have an interface comment

##### 13.2 Don’t repeat the code
> [!danger] Red Flag: Comment Repeats Code
> If the information in a comment is already obvious from the code next to the comment, then the comment isn’t helpful. One example of this is when the comment uses the same words that make up the name of the thing it is describing.

**use different words in the comment from those in the name of the entity being described**

#### 13.3 Lower-level comments add precision
**Comments augment the code by providing information at a different level of detail**
Comments at the same level as the code are likely to repeat the code

Comments can fill in missing details: 
* What are the units for this variable?
* Are the boundary conditions inclusive or exclusive? 
* If a null value is permitted, what does it imply? 
* If a variable refers to a resource that must eventually be freed or closed, who is  responsible for freeing or closing it? 
* Are there certain properties that are always true for the variable (invariants), such as “this list always contains at least one entry”?
Some of this information could potentially be figured out by examining all of the code where the variable is used. However, this is time-consuming and errorprone

#### 13.4 Higher-level comments enhance intuition
describes the code’s overall function at a higher level
Higher-level comments are more difficult to write than lower-level comments because you must think about the code in a different way : 
	* What is this code trying to do? 
	* What is the simplest thing you can say that explains everything in the code? 
	* What is the most important thing about this code?

#### 13.5 Interface documentation
**If you want code that presents good abstractions, you must document those abstractions with comments**
The first step in documenting abstractions is to separate interface comments from implementation comments

**If interface comments must also describe the implementation, then the class or method is shallow** :
* The comment usually starts with a sentence or two describing the behavior of the method as perceived by callers
* The comment must describe each argument and the return value (if any). These comments must be very precise, and must describe any constraints on argument values as well as dependencies between arguments. 
* If the method has any side effects, these must be documented in the interface comment.
* A method’s interface comment must describe any exceptions that can emanate from the method. 
* If there are any preconditions that must be satisfied before a method is invoked, these must be described

> [!danger] Red Flag: Implementation Documentation Contaminates Interface
> This red flag occurs when interface documentation, such as that for a method, describes implementation details that aren’t needed in order to use the thing being documented.

#### 13.6 Implementation comments: what and why, not how
**The main goal of implementation comments is to help readers understand what the code is doing**

For short methods, the code only does one thing, which is already described in its interface comment, so no implementation comments are needed

Longer methods have several blocks of code. Add a comment before each of the major blocks to provide a high-level (more abstract) description of what that block does

Explain, what the code is doing, also useful to explain why

#### 13.7 Cross-module design decisions
In a perfect world, every important design decision would be encapsulated within a single class
But not always possible -->  provider / consummer, sender / receiver
The biggest challenge with cross-module documentation is finding a place to put it where it will naturally be discovered by developers 
Unfortunately, in many cases there is not an obvious central place to put cross-module documentation.

 have recently been experimenting with an approach where cross-module issues are documented in a central file called designNotes. The file is divided up into clearly labeled sections, one for each major topic
 Then, in any piece of code that relates to one of these issues there is a short comment referring to the designNotes file
 However, this has the disadvantage that the documentation is not near any of the pieces of code that depend on it, so it may be difficult to keep up-to-date as the system evolves.

---

## Chapter 14
#### Choosing Names
Good names are a form of documentation: they make code easier to understand

#### 14.1 Example: bad names cause bugs
The file system code used the variable name "block" for two different purposes
Unfortunately, most developers don’t spend much time thinking about names. They tend to use the first name that comes to mind

#### 14.2 Create an image
When choosing a name, the goal is to create an image in the mind of the reader about the nature of the thing being named
Names are a form of abstraction

#### 14.3 Names should be precise
Good names have two properties: precision and consistency
> [!danger] Red Flag: Vague Name
> If a variable or method name is broad enough to refer to many different things, then it doesn’t convey much information to the developer and the underlying entity is more likely to be misused.

> [!danger] Red Flag: Hard to Pick Name
> If it’s hard to find a simple name for a variable or method that creates a clear image of the underlying object, that’s a hint that the underlying object may not have a clean design

#### 14.4 Use names consistently
In any program there are certain variables that are used over and over again. use the same name everywhere.

Consistency has three requirements
	* always use the common name for the given purpose
	* never use the common name for anything other than the given purpose
	* make sure that the purpose is narrow enough that all variables with the name have the same behavio
ex: i & j for loops. i always extern, j intern

#### 14.5 A different opinion: Go style guide
name choice for Go, [Andrew Gerrand](https://go.dev/talks/2014/names.slide#1) states that “long names obscure what the code does.”

The Go culture encourages the use of the same short name for multiple different things: ch for character or channel, d for data, difference, or distance, and so on. To me, ambiguous names like these are likely to result in confusion and error

The greater the distance between a name’s declaration and its uses, the longer the name should be

---

## Chapter 15
#### Write The Comments First (Use Comments As Part Of The Design Process)
Writing the comments first makes documentation part of the design process

#### 15.1 Delayed comments are bad comments
If they write documentation early, they say, they’ll have to rewrite it when the code changes
delaying documentation often means that it never gets written at all
By the time the code has inarguably stabilized, there is a lot of it, which means the task of writing documentation has become huge and even less attractive

Writting comments after: By this time in the process, you have checked out mentally. it’s been a while since you designed the code.

#### 15.2 Write the comments first
* For a new class, I start by writing the class interface comment. 
* Next, I write interface comments and signatures for the most important public methods, but I leave the method bodies empty.
* I iterate a bit over these comments until the basic structure feels about right. 
* At this point I write declarations and comments for the most important class instance variables in the class. 
* Finally, I fill in the bodies of the methods, adding implementation comments as needed. 
* While writing method bodies, I usually discover the need for additional methods and instance variables. For each new method I write the interface comment before the body of the method; for instance variables I fill in the comment at the same time that I write the variable declaration

When the code is done, the comments are also done. There is never a backlog of unwritten comments.

#### 15.3 Comments are a design tool
writing the comments at the beginning improves the system design
Comments serve as a canary in the coal mine of complexity. If a method or variable requires a long comment, it is a red flag that you don’t have a good abstraction (complex interface)
if the interface comment must describe all the major features of the implementation, then the method is shallow
> [!danger] Red Flag: Hard to Describe
> The comment that describes a method or variable should be simple and yet complete. If you find it difficult to write such a comment, that’s an indicator that there may be a problem with the design of the thing you are describing.

#### 15.4 Early comments are fun comments
The simpler the comments, the better I feel about my design, so finding simple comments is a source of pride

#### 15.5 Are early comments expensive?
Writing the comments first will mean that the abstractions will be more stable before you start writing code. This will probably save time during coding.

----

## Chapter 16
#### Modifying Existing Code
how to keep complexity from creeping in as the system evolves

#### Stay Strategic
when developers go into existing code to make changes such as bug fixes or new features, they don’t usually think strategically
typical mindset is “what is the smallest possible change I can make that does what I need?”
they worry that larger changes carry a greater risk of introducing new bugs
this results in tactical programming

**Ideally, when you have finished with each change, the system will have the structure it would have had if you had designed it from the start with that change in mind.**
resist the temptation to make a quick fix
if you invest a little extra time to refactor and improve the system design, you’ll end up with a cleaner system.
**If you’re not making the design better, you are probably making it worse.**

investment mindset sometimes conflicts with the realities of commercial software development. You should resist these compromises as much as possible
Every development organization should plan to spend a small fraction of its total effort on cleanup and refactoring; this work will pay for itself over the long run.

#### 16.2 Maintaining comments: keep the comments near the code
**The best way to ensure that comments get updated is to position them close to the code they describe**
the farther a comment is from the code it describes, the more abstract it should be (this reduces the likelihood that the comment will be invalidated by code changes)

#### 16.3 Comments belong in the code, not the commit log
a developer who needs the information is unlikely to think of scanning the repository log. Even if they do scan the log, it will be tedious to find the right log message

#### 6.4 Maintaining comments: avoid duplication
**If information is already documented someplace outside your program, don’t repeat the documentation inside the program; just reference the external documentation**

#### 16.5 Maintaining comments: check the diffs
before committing, make sure that each change is properly reflected in the documentation

#### 16.6 Higher-level comments are easier to maintain
higher-level and more abstract comments do not reflect the details of the code, so they will not be affected by minor code changes

---

## Chapter 17
#### Consistency
Consistency creates **cognitive leverage**: once you have learned how something is done in one place, you can use that knowledge to immediately understand other places that use the same approach.
Consistency reduces mistakes

#### 17.1 Examples of consistency
* names (chapter 14)
* coding style
* interface with multiple implementation
* using design pattern
* inviariants (property of a variable or structure that is always true)

#### 17.2 Ensuring consistency
Consistency is hard to maintain, especially when many people work on a project over a long time

**Document**. Create a document that lists the most important overall conventions

**Enforce**, tool that checks for violations. Automated checkers work particularly well for low-level syntactic conventions.

**Don’t change existing conventions**. Resist the urge to “improve” on existing conventions. Having a “better idea” is not a sufficient excuse to introduce inconsistencies.
* do you have significant new information justifying your approach that wasn’t available when the old convention was established?
* is the new approach so much better that it is worth taking the time to update all of the old uses?

#### 17.3 Taking it too far
overzealous about consistency and try to force dissimilar things into the same approach
Consistency only provides benefits when developers have confidence that “if it looks like an x, it really is an x.”

---

## Chapter 18
#### Code Should be Obvious
“Obvious” is in the mind of the reader: it’s easier to notice that someone else’s code is nonobvious than to see problems with your own code
best way to determine the obviousness of code is through code reviews

#### 18.1 Things that make code more obvious
good names & consistency

**Judicious use of white space**, ease reading / separate logical blocks

**Comments**, compensate nonobvious code by providing the missing information

#### 18.2 Things that make code less obvious
**Event-driven programming**. hard to follow the flow of control
To compensate for this obscurity, use the interface comment for each handler function to indicate when it is invoked,
> [!danger ] Red Flag: Nonobvious Code
> If the meaning and behavior of code cannot be understood with a quick reading, it is a red flag. Often this means that there is important information that is not immediately clear to someone reading the code.

**Generic containers** generic containers result in nonobvious code because the grouped elements have generic names that obscure their meaning
**software should be designed for ease of reading, not ease of writing**

**Different types for declaration and allocation**  variable is declared as a List, but the actual value is an ArrayList, The actual type may impact how the variable is used, so it is better to match the declaration with the allocation

**Code that violates reader expectations** 

#### 18.3 Conclusion
make code obvious, you must ensure that readers always have the information they need to understand :
	* reduce the amount of information that is needed
	* take advantage of information that readers have already acquired in other contexts (conventions, conforming to expectations)
	* present the important information to them in the code (good names, comments)

---

## Chapter 19
#### Software trends
#### 19.1 Object-oriented programming and inheritance
private methods and variables can be used to ensure information hiding

the more different implementations there are of an interface, the deeper the interface becomes

implementation inheritance (class -> subclass) reduces the amount of code that needs to be modified as the system evolves; in other words, it reduces the change amplification problem
However, implementation inheritance creates dependencies between the parent class and each of its subclasse. information leakage between the classes in the inheritance hierarchy
consider whether an approach based on composition can provide the same benefits
If there is no viable alternative to implementation inheritance, try to separate the state managed by the parent class from that managed by subclasses.

#### 19.2 Agile development
The best way to end up with a good design is to develop a system in increments. This is similar to the agile development approach

One of the risks of agile development is that it can lead to tactical programming. 
It can focus developers on features, not abstractions, and it encourages them to put off design decisions in order to produce working software as soon as possible

**the increments of development should be abstractions, not features**

#### 19.3 Unit tests
Tests, particularly unit tests, play an important role in software design because they facilitate refactoring.

#### 19.4 Test-driven development
**The problem with test-driven development is that it focuses attention on getting specific features working, rather than finding the best design**
This is tactical programming pure and simple

One place where it makes sense to write the tests first is when fixing bugs. Before fixing a bug, write a unit test that fails because of the bug

#### 19.5 Design patterns
Design patterns represent an alternative to design: rather than designing a new mechanism from scratch, just apply a well-known design pattern.

The greatest risk with design patterns is over-application. Using design patterns doesn’t automatically improve a software system

#### 19.6 Getters and setters
Although it may make sense to use getters and setters if you must expose instance variables, it’s better not to expose instance variables in the first place.

One of the risks of establishing a design pattern is that developers assume the pattern is good and try to use it as much as possible

---

#### Chapter 20
#### Designing for Performance
#### 20.1 How to think about performance
If you try to optimize every statement for maximum speed, it will slow down development and create a lot of unnecessary complexity. many of the “optimizations” won’t actually help performance
but if you completely ignore performance issues, it’s easy to end up with a large number of significant inefficiencies

The best approach is something between these extremes, where you use basic knowledge of performance to choose design alternatives that are “naturally efficient” yet also clean and simple.

The best way to learn which things are expensive is to run micro-benchmarks. Once you have a general sense for what is expensive and what is cheap, you can use that information to choose cheap operations whenever possible

**In general, **simpler code tends to run faster than complex code**. If you have defined away special cases and exceptions, then no code is needed to check for those cases and the system runs faster. Deep classes are more efficient than shallow ones, because they get more work done for each method call. Shallow classes result in more layer crossings, and each layer crossing adds overhead.

#### 20.2 Measure before modifying
Programmers’ intuitions about performance are unreliable. This is true even for experienced developers

 * the measurements will identify the places where performance tuning will have the biggest impact.
 * the measurements provides a baseline, so that you can compare after change

#### 20.3 Design around the critical path
best way to improve its performance is with a “fundamental” change, such as introducing a cache, or using a different algorithmic approach.
situations will sometimes arise where there isn’t a fundamental fix.

redesigning should be your last resort, and it shouldn’t happen often, but there are cases where it can make a big difference

Imagine that you are writing a new method that implements just the critical path, which is the minimum amount of code that must be executed in the the most common case.
consider only the data needed for the critical path, and assume whatever data structure is most convenient for the critical path.
Assume that you could completely redesign the system in order to minimize the code that must be executed for the critical path. Let’s call this code “the ideal.”

Redesign while keeping the ideal intact. You may have to add a bit of extra code to the ideal in order to allow clean abstractions;
the most important things that happens in this process is to remove special cases from the critical path
Ideally, there will be a single if statement at the beginning, which detects all special cases with one test.
Performance isn’t as important for special cases, so you can structure the specialcase code for simplicity rather than performance.


