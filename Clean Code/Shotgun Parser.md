Shotgun parsing is a programming antipattern whereby parsing and input-validating code is mixed with and spread across processing code—throwing a cloud of checks at the input, and hoping, without any systematic justification, that one or another would catch all the “bad” cases.

Shotgun parsing necessarily deprives the program of the ability to reject invalid input instead of processing it. Late-discovered errors in an input stream will result in some portion of invalid input having been processed, with the consequence that program state is difficult to accurately predict.

https://lexi-lambda.github.io/blog/2019/11/05/parse-don-t-validate/
https://deepspec.org/event/dsss17/studenttalks/17-metzger.pdf
#anti-pattern 