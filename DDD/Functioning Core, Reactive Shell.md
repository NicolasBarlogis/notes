https://mokacoding.com/blog/functional-core-reactive-shell/

Our software now boils down to a pipeline of purely functional values transformations that eventually produces side effects which are sent to objects that know how to perform them.

This is what Gary Bernhardt calls "Functional Core, Imperative Shell".

The functional core is made by the values and the purely functional logic, the imperative shell is the straightforward glue that takes value from one function, passes them to the next, and eventually provides a side effect value to its performer.