
<!-- (dl
(section-meta
  (title ArrayShould Array Validation Functions)
)
) -->

This document describes the array validation functions provided by the `ArrayShould` helper in the Archer framework, specifically within the Fletching library. These functions are similar to `Assert` methods in other frameworks, but instead of throwing exceptions, they return a `TestResult` value, enabling functional and composable test flows.


<!-- (dl (# Overview)) -->

The `ArrayShould` type provides static members for validating F# arrays (`'a[]`). These validations include containment, length, and predicate-based checks.

---


<!-- (dl (# Array Validation Methods)) -->

- **Contain ( value )**
  - Passes if the array contains the specified value.
- **NotContain ( value )**
  - Passes if the array does not contain the specified value.
- **HaveAllValuesPassTestOf ( predicateExpression )**
  - Passes if all values in the array satisfy the given predicate expression.
- **HaveNoValuesPassTestOf ( predicateExpression )**
  - Passes if no values in the array satisfy the given predicate expression.
- **HaveLengthOf ( length )**
  - Passes if the array has the specified length.
- **NotHaveLengthOf ( length )**
  - Passes if the array does not have the specified length.
- **HaveAllValuesPassAllOf ( tests )**
  - Passes if all values in the array pass all provided test functions.
- **HaveAllValuesBe ( value )**
  - Passes if all values in the array are equal to the specified value.

---


<!-- (dl (# Usage Example)) -->

```fsharp
open Archer.Fletching.Lib

let numbers = [| 1; 2; 3 |]

// Direct invocation
let result1 = ArrayShould.Contain ( 2 ) numbers
let result2 = ArrayShould.HaveLengthOf ( 3 ) numbers

// Using pipe notation
let result3 = numbers |> ArrayShould.NotContain ( 4 )
let result4 = numbers |> ArrayShould.HaveAllValuesPassTestOf ( <@ fun x -> x > 0 @> )
```


See [How to Use Fletcher Test Validations](#how-to-use-fletcher-test-validations) for usage patterns and return value details.

For more details, see the source in `Lib/ArrayShould.fs`.
