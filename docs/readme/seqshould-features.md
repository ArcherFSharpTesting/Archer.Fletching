
<!-- (dl
(section-meta
  (title SeqShould Sequence Validation Functions)
)
) -->

This document describes the sequence validation functions provided by the `SeqShould` helper in the Archer framework, specifically within the Fletching library. These functions are similar to `Assert` methods in other frameworks, but instead of throwing exceptions, they return a `TestResult` value, enabling functional and composable test flows.


<!-- (dl (# Overview)) -->

The `SeqShould` type provides static members for validating F# sequences (`seq<'a>`). These validations include containment, length, and predicate-based checks.

---


<!-- (dl (# Sequence Validation Methods)) -->

- **Contain ( value )**
  - Passes if the sequence contains the specified value.
- **NotContain ( value )**
  - Passes if the sequence does not contain the specified value.
- **HaveAllValuesPassTestOf ( predicateExpression )**
  - Passes if all values in the sequence satisfy the given predicate expression.
- **HaveNoValuesPassTestOf ( predicateExpression )**
  - Passes if no values in the sequence satisfy the given predicate expression.
- **HaveLengthOf ( length )**
  - Passes if the sequence has the specified length.
- **NotHaveLengthOf ( length )**
  - Passes if the sequence does not have the specified length.
- **HaveAllValuesPassAllOf ( tests )**
  - Passes if all values in the sequence pass all provided test functions.
- **HaveAllValuesBe ( value )**
  - Passes if all values in the sequence are equal to the specified value.

---


<!-- (dl (# Usage Example)) -->

```fsharp
open Archer.Fletching.Lib

let numbers = seq { 1; 2; 3 }

// Direct invocation
let result1 = SeqShould.Contain ( 2 ) numbers
let result2 = SeqShould.HaveLengthOf ( 3 ) numbers

// Using pipe notation
let result3 = numbers |> SeqShould.NotContain ( 4 )
let result4 = numbers |> SeqShould.HaveAllValuesPassTestOf ( <@ fun x -> x > 0 @> )
```

Each function returns a `TestResult` indicating pass or failure, which can be composed or further processed in your test suite.

---

For more details, see the source in `Lib/SeqShould.fs`.
