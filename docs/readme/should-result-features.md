
<!-- (dl
(section-meta
  (title Should Result Validation Functions)
)
) -->


Result validations check if a value is `Ok` or `Error` using the `Should` helper.


<!-- (dl (# Overview)) -->

The `Should` type provides static members for validating F# `Result` values. These validations check whether a value is an `Ok` or an `Error` with the expected content.

---


<!-- (dl (# Result Validation Methods)) -->

- **BeOk ( expected )**
  - Passes if the actual value is `Ok expected`.
- **BeError ( expected )**
  - Passes if the actual value is `Error expected`.

---


<!-- (dl (# Usage Example)) -->

```fsharp
open Archer.Validations.Lib

// Direct invocation
let result1 = Should.BeOk ( 42 ) ( Ok 42 )
let result2 = Should.BeError ( "fail" ) ( Error "fail" )

// Using pipe notation
let result3 = Ok 42 |> Should.BeOk ( 42 )
let result4 = Error "fail" |> Should.BeError ( "fail" )
```


See [How to Use Fletcher Test Validations](#how-to-use-fletcher-test-validations) for usage patterns and return value details.

For more details, see the source in `Lib/ShouldType.Result.fs`.
