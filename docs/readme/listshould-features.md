
<!-- (dl
(section-meta
  (title ListShould List Validation Functions)
)
) -->


List validations check containment, length, and predicates using the `ListShould` helper.


<!-- (dl (# Overview)) -->

The `ListShould` type provides static members for validating F# lists (`'a list`). These validations include containment, length, and predicate-based checks.

---


<!-- (dl (# List Validation Methods)) -->

- **Contain ( value )**
  - Passes if the list contains the specified value.
- **NotContain ( value )**
  - Passes if the list does not contain the specified value.
- **HaveAllValuesPassTestOf ( predicateExpression )**
  - Passes if all values in the list satisfy the given predicate expression.
- **HaveNoValuesPassTestOf ( predicateExpression )**
  - Passes if no values in the list satisfy the given predicate expression.
- **HaveLengthOf ( length )**
  - Passes if the list has the specified length.
- **NotHaveLengthOf ( length )**
  - Passes if the list does not have the specified length.
- **HaveAllValuesPassAllOf ( tests )**
  - Passes if all values in the list pass all provided test functions.
- **HaveAllValuesBe ( value )**
  - Passes if all values in the list are equal to the specified value.

---


<!-- (dl (# Usage Example)) -->

```fsharp
open Archer.Fletching.Lib

let numbers = [ 1; 2; 3 ]

// Direct invocation
let result1 = ListShould.Contain ( 2 ) numbers
let result2 = ListShould.HaveLengthOf ( 3 ) numbers

// Using pipe notation
let result3 = numbers |> ListShould.NotContain ( 4 )
let result4 = numbers |> ListShould.HaveAllValuesPassTestOf ( <@ fun x -> x > 0 @> )
```


See [How to Use Fletcher Test Validations](#how-to-use-fletcher-test-validations) for usage patterns and return value details.

For more details, see the source in `Lib/ListShould.fs`.
