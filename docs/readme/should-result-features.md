
<!-- (dl
(section-meta
  (title Should Result Validation Functions)
)
) -->

This document describes the result validation functions provided by the `Should` helper in the Archer framework, specifically within the Fletching library. These functions are similar to `Assert` methods in other frameworks, but instead of throwing exceptions, they return a `TestResult` value, enabling functional and composable test flows.


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
open Archer.Fletching.Lib

// Direct invocation
let result1 = Should.BeOk ( 42 ) ( Ok 42 )
let result2 = Should.BeError ( "fail" ) ( Error "fail" )

// Using pipe notation
let result3 = Ok 42 |> Should.BeOk ( 42 )
let result4 = Error "fail" |> Should.BeError ( "fail" )
```

Each function returns a `TestResult` indicating pass or failure, which can be composed or further processed in your test suite.

---

For more details, see the source in `Lib/ShouldType.Result.fs`.
