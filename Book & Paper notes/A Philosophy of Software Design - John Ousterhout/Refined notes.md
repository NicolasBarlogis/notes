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
* Finish a task as quickly as possible3.
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
Ex: Facebook's motto, from “Move fast and break things.” (tactical) to “Move fast with solid infrastructure”

## 3 - What is complexe ?

## 4 - How to fight complexity
Fightingh Complexity:
* eliminate it
* encapsulate it away, isolating complexity in a place where it will never be seen (nearly equivalent to elimination)