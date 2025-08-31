
<!-- (dl
(section-meta
  (title SeqShould Sequence Validation Functions)
)
) -->


Sequence validations check containment, length, and predicates using the `SeqShould` helper.


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
open Archer.Validations.Lib

let numbers = seq { 1; 2; 3 }

// Direct invocation
let result1 = SeqShould.Contain ( 2 ) numbers
let result2 = SeqShould.HaveLengthOf ( 3 ) numbers

// Using pipe notation
let result3 = numbers |> SeqShould.NotContain ( 4 )
let result4 = numbers |> SeqShould.HaveAllValuesPassTestOf ( <@ fun x -> x > 0 @> )
```


See [How to Use Fletcher Test Validations](#how-to-use-fletcher-test-validations) for usage patterns and return value details.

For more details, see the source in `Lib/SeqShould.fs`.
