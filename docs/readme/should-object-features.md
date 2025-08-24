<!-- (dl
(section-meta
  (title Should Object Validation Functions)
)
) -->

This document describes the object-related validation functions provided by the `Should` helper in the `Archer` framework, specifically within the `Fletching` library. These functions are similar to `Assert` methods in other frameworks, but instead of throwing exceptions, they return a `TestResult` value, enabling functional and composable test flows.


<!-- (dl (# Overview)) -->

The `Should` type provides static members for validating objects in various ways. These validations include equality, reference checks, type checks, null/default checks, and predicate-based custom checks.

---


<!-- (dl (# Object Validation Methods)) -->


<!-- (dl (## Equality and Reference)) -->

- **BeEqualTo ( expected )**
  - Passes if the actual value is equal to `expected` (`=`).
- **NotBeEqualTo ( expected )**
  - Passes if the actual value is not equal to `expected` (`<>`).
- **BeSameAs ( expected )**
  - Passes if the actual value is the same reference as `expected`.
- **NotBeSameAs ( expected )**
  - Passes if the actual value is not the same reference as `expected`.


<!-- (dl (## Type Checks)) -->

- **BeOfType<'expectedType> ( actual )**
  - Passes if `actual` is an instance of `'expectedType`.
- **NotBeTypeOf<'expectedType> ( actual )**
  - Passes if `actual` is not an instance of `'expectedType`.


<!-- (dl (## Null and Default Checks)) -->

- **BeNull<'T when 'T : null> ( actual )**
  - Passes if `actual` is `null`.
- **NotBeNull<'T when 'T : null> ( actual )**
  - Passes if `actual` is not `null`.
- **BeDefaultOf<'T when 'T : equality> ( actual )**
  - Passes if `actual` is the default value for type `'T`.
- **NotBeDefaultOf<'T when 'T : equality> ( actual )**
  - Passes if `actual` is not the default value for type `'T`.


<!-- (dl (## Predicate and Custom Checks)) -->

- **PassTestOf ( predicateExpression )**
  - Passes if the provided predicate expression returns `true` for the actual value.
  - Example: `Should.PassTestOf ( <@ fun x -> x > 0 @> )`
- **NotPassTestOf ( predicateExpression )**
  - Passes if the provided predicate expression returns `false` for the actual value.
- **PassAllOf ( tests )**
  - Passes if all provided test functions return a passing `TestResult` for the actual value.
  - Example: `Should.PassAllOf [ test1; test2; test3 ]`

---


<!-- (dl (# Usage Example)) -->

```fsharp
open Archer.Fletching.Lib

// Direct invocation
let result1 = Should.BeEqualTo ( 42 ) 42
let result2 = Should.NotBeNull ( "hello" )
let result3 = Should.BeOfType<string> ( "test" )
let result4 = Should.PassTestOf ( <@ fun x -> x > 10 @> ) 15

// Using pipe notation
let result5 = 42 |> Should.BeEqualTo ( 42 )
let result6 = "hello" |> Should.NotBeNull
let result7 = "test" |> Should.BeOfType<string>
let result8 = 15 |> Should.PassTestOf ( <@ fun x -> x > 10 @> )
```

Each function returns a `TestResult` indicating pass or failure, which can be composed or further processed in your test suite.

---

For more details, see the source in `Lib/ShouldType.Objects.fs`.
