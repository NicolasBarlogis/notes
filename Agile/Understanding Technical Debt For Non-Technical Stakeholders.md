# [Understanding Technical Debt For Non-Technical Stakeholders](https://blog.sapiensworks.com/post/2018/02/21/Explaining-technical-debt)

published on   **21 February 2018** in [Business](https://blog.sapiensworks.com/tags.html#Business)

I've seen this [tweet](https://twitter.com/ThePracticalDev/status/963603756574216192):

>  
> 
> How do you best communicate the value of refactoring to non-technical stakeholders?

Great question! Now, I'm assuming it's the same old 'we have this big ball of mud and it's increasingly painful to change it and we _really_ need to do something about it'. Keep in mind that while you're thinking that, the non-technical stakeholder (manager or client) is focused on something entirely different: they want feature X and they want it yesterday!

As far as refactoring is concerned, I believe that it's a concept that should remain in the techie land. We need to expose the problem and the business consequences in a business related manner. To do that we have a better concept, which is easier to understand and more meaningful: **technical debt**.

## Every stakeholder should be aware of technical debt

Do you own and use a car? Chances are you do. And you know that a car needs to be serviced at certain intervals if you want it to keep running well for a long time. It is inconvenient, it costs time and money but you do it anyway, because it's **cheaper** in the long run and you can **identify and solve problems before they happen**. And cars aren't the only asset or equipment that needs regular maintenance. For many products it's normal to take maintenance into account, it's a reality.

Software is such a product although it acts quite weird: it doesn't need preventive servicing if it's only used or ignored. But once it gets _changed_ it starts decaying. With every change of the codebase, the wear and tear increases. Going unchecked, it will begin to rot until you end up with a product that nobody knows why it works and for how long. In software development, that wear and tear is abstracted as `Technical Debt`. Technical debt can increase with every code change and it signals the need of servicing. And like with a car, time should be allocated for codebase maintenance.

From what I've seen plenty of stakeholders aren't aware or don't care about it. They seem to ignore the reality that software needs maintenance, too. And that's an issue, because usually, other business assets are regularly maintained, time and money being allocated for that purpose.

It's in the best interest of everyone if a codebase is regularly serviced i.e allocating time for improving the 'decayed' parts of it. The alternative is dealing with an increased technical debt, which like any debt, it bears interest and you don't want it to increase out of control.

In the case of technical debt, the 'interest' to be paid (someday) is an _unkown quantity of time and money_. And as things go, chances are the interest is due when you have the least time. If a lender would ask you to sign a loan where the interest is 'to be determined at a later date', would you do it?

## Understanding the technical debt

Now, if an application is well designed a.k.a maintainable, we shouldn't have to deal with technical debt, right? Well, in the real world, even with good design there are moments when, because of deadlines or edge cases, we need to just 'do it' and deliver the fastest solution that works. This will increase the technical debt (maintenance required) but it can also deliver real value, if it's used wisely to achieve strategic objectives.

But most of the time, the high technical debt is because of legacy apps, which in most cases are old and badly designed, therefore hard to maintain. Imagine a very old car or building. It's common sense to renovate it, at least from time to time. In the software world, this means refactoring (fancy word for 'improving the codebase') which in case of the said legacy apps, often requires rewriting of some parts. You know... renovations, but for software.

If, as a non-technical stakeholder, you don't allow time for maintenance you'll get similar results to using a car that no one knows when it was last inspected. And that's your only car to use for many years and many trips. It will start to break down often and usually when you are in the middle of nowhere. You'll have to service it whether you like it or not and it will cost you time and money.

There are some stakeholders who consider that allocating time regularly for reducing technical debt is too high of a cost. They prefer to pay the price when the shit hits the fan, but not before and even then, only to solve that pressing problem. It's a form of living dangerously, I suppose, but being only reactive to problems is not a good long term strategy.

Think a bit, do you like having things under control? Don't you think it's best that you decide when maintenance takes place, as opposed to 'when it happens'? How about the fact that it can be a budgeted item, allowing you to organize the delivery of business value in a more effective manner.

Don't think of technical debt as a necessary evil. As I've said above, it can be used as a leverage, allowing you to deffer doing things properly, in exchange for delivering a must-have functionality in a very short timespan. But in order to do that, the product owner or the manager needs to acknowledge that technical debt exists by default, even if you don't have any complains from developers (usually, developers complain when things are really bad). Next, find out how simple or dire technical debt is i.e allow developers to conduct code reviews.

Always, try to minimize the debt, the less you have, the more you can leverage it when you really need to buy some time. If you're an active investor, think of it as having a margin (or a line of credit). As long as your debt is below the threshold, you can leverage it. That's another reason why it pays off to know about the technical debt you're dealing with.

# Conclusion

It's unfortunate how often we hear about developers struggling with an increasingly hard to work codebase and how non-technical stakeholders seem not to care about it. Understanding the reality of software development is how we can become more effective. Keeping technical debt under control and leveraging it is one of the ways we can deliver business value faster on the long term.

#article