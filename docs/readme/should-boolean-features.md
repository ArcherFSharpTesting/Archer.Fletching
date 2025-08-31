
<!-- (dl
(section-meta
  (title Should Boolean Validation Functions)
)
) -->


Boolean validations check if a value is `true` or `false` using the `Should` helper.


<!-- (dl (# Overview)) -->

The `Should` type provides static members for validating boolean values. These validations check whether a value is `true` or `false`.

---


<!-- (dl (# Boolean Validation Methods)) -->

- **BeTrue ( actual )**
  - Passes if the actual value is `true`.
- **BeFalse ( actual )**
  - Passes if the actual value is `false`.

---


<!-- (dl (# Usage Example)) -->

```fsharp
open Archer.Validations.Lib

// Direct invocation
let result1 = Should.BeTrue ( true )
let result2 = Should.BeFalse ( false )

// Using pipe notation
let result3 = true |> Should.BeTrue
let result4 = false |> Should.BeFalse
```


See [How to Use Fletcher Test Validations](#how-to-use-fletcher-test-validations) for usage patterns and return value details.

For more details, see the source in `Lib/ShouldType.Boolean.fs`.
