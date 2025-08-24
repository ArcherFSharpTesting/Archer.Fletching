<!-- (dl
(section-meta
  (title Not Validation Helper)
)
) -->

This document describes the `Not` validation helper provided in the Archer framework, specifically within the Fletching library. This helper is used to mark tests or features as "not yet implemented" in a way that integrates with the test result system.


<!-- (dl (# Overview)) -->

The `Not` type provides a static member for marking a test as not yet implemented. Instead of throwing an exception, it returns a `TestResult` indicating the test is ignored, which can be useful for test-driven development or feature planning.

---


<!-- (dl (# Not Validation Method)) -->

- **Implemented ( )**
  - Marks the test as ignored with the message "Not Yet Implemented".
  - Can be used to indicate that a test or feature is planned but not yet available.

---


<!-- (dl (# Usage Example)) -->

```fsharp
open Archer.Fletching.Lib

let result = Not.Implemented ( )
```


See [How to Use Fletcher Test Validations](#how-to-use-fletcher-test-validations) for usage patterns and return value details.

For more details, see the source in `Lib/NotImplemented.fs`.
