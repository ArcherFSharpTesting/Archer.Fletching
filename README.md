<!-- GENERATED DOCUMENT DO NOT EDIT! -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->

<!-- Compiled with doculisp https://www.npmjs.com/package/doculisp -->

# Fletcher Test Validations for the Archer Test Framework #

1. Feature: [Should Object Validation Functions](#should-object-validation-functions)
2. Review: [Archer.Fletching](#archerfletching)

## Should Object Validation Functions ##

This document describes the object-related validation functions provided by the `Should` helper in the `Archer` framework, specifically within the `Fletching` library. These functions are similar to `Assert` methods in other frameworks, but instead of throwing exceptions, they return a `TestResult` value, enabling functional and composable test flows.

### Overview ###

The `Should` type provides static members for validating objects in various ways. These validations include equality, reference checks, type checks, null/default checks, and predicate-based custom checks.

---

### Object Validation Methods ###

#### Equality and Reference ####

- **BeEqualTo ( expected )**
  - Passes if the actual value is equal to `expected` (`=`).
- **NotBeEqualTo ( expected )**
  - Passes if the actual value is not equal to `expected` (`<>`).
- **BeSameAs ( expected )**
  - Passes if the actual value is the same reference as `expected`.
- **NotBeSameAs ( expected )**
  - Passes if the actual value is not the same reference as `expected`.

#### Type Checks ####

- **BeOfType<'expectedType> ( actual )**
  - Passes if `actual` is an instance of `'expectedType`.
- **NotBeTypeOf<'expectedType> ( actual )**
  - Passes if `actual` is not an instance of `'expectedType`.

#### Null and Default Checks ####

- **BeNull<'T when 'T : null> ( actual )**
  - Passes if `actual` is `null`.
- **NotBeNull<'T when 'T : null> ( actual )**
  - Passes if `actual` is not `null`.
- **BeDefaultOf<'T when 'T : equality> ( actual )**
  - Passes if `actual` is the default value for type `'T`.
- **NotBeDefaultOf<'T when 'T : equality> ( actual )**
  - Passes if `actual` is not the default value for type `'T`.

#### Predicate and Custom Checks ####

- **PassTestOf ( predicateExpression )**
  - Passes if the provided predicate expression returns `true` for the actual value.
  - Example: `Should.PassTestOf ( <@ fun x -> x > 0 @> )`
- **NotPassTestOf ( predicateExpression )**
  - Passes if the provided predicate expression returns `false` for the actual value.
- **PassAllOf ( tests )**
  - Passes if all provided test functions return a passing `TestResult` for the actual value.
  - Example: `Should.PassAllOf [ test1; test2; test3 ]`

---

### Usage Example ###

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

## Archer.Fletching ##

A test verification Framework for Archer

### Should ###

- Dictionary
  - [ ] {dictionary} |> Should.HaveKey {key}
  - [ ] {dictionary} |> Should.NotHaveKey {key}
- Object
  - [x] {value} |> Should.BeEqualTo {value}
  - [x] {value} |> Should.NotBeEqualTo {value}
  - [x] {value} |> Should.BeSameAs {value}
  - [x] {value} |> Should.NotBeSameAs {value}
  - [x] {value} |> Should.BeOfType\<Type\>
  - [x] {value} |> Should.NotBeOfType\<Type\>
  - [x] {value} |> Should.BeNull
  - [x] {value} |> Should.NotBeNull
  - [x] {value} |> Should.BeDefaultOf\<Type\>
  - [x] {value} |> Should.NotBeDefaultOf\<Type\>
  - [x] {value} |> Should.PassTestOf {predicate}
  - [x] {value} |> Should.NotPassTestOf {predicate}
  - [x] {value} |> Should.PassAllOf [ {value -> TestResult} ]
- Result
  - [x] {result} |> Should.BeOk {value}
  - [x] {result} |> Should.BeError {value}
- Functions
  - [ ] {action} |> Should.Return {value}
  - [ ] {action} |> Should.NotReturnValue {value}
  - [ ] let result: Result<ex, TestExecutionResult> = {action} |> Should.ThrowException
  - [ ] {action} |> Should.NotThrowException
  - [ ] {action} |> Should.Call {action} |> withParameter {predicate} {initialParameter}
- Events
  - [ ] {IEvent} |> Should.Trigger |> by {action}
  - [ ] {IEvent} |> Should.NotTrigger |> by {action}
- String
  - [ ] {string} |> Should.Contain {string}
  - [ ] {string} |> Should.NotContain {string}
  - [ ] {string} |> Should.BeMatchedBy {regex}
  - [ ] {string} |> Should.NotBeMatchedBy {regex}
  - [ ] {string} |> Should.MatchStandard {ITestInfo} {reporter}
- Numbers
  - [ ] {number} |> Should.BeWithin ({number}, {number})
  - [ ] {number} |> Should.BeBetween ({number}, {number})
  - [ ] {number} |> Should.BeCloseTo {number} |> byDelta {number}
- Boolean
  - [x] {bool} |> Should.BeTrue
  - [x] {bool} |> Should.BeFalse
- Other
  - [x] {string} |> Should.Fail
  - [x] {value} |> Should.BeIgnored {string}
  - [x] {value} |> Should.BeIgnored

### ListShould ###

- [x] {list} |> ListShould.Contain {value}
- [x] {list} |> ListShould.NotContain {value}
- [ ] {list} |> ListShould.ContainAny {values}
- [ ] {list} |> ListShould.NotContainAny {values}
- [ ] {list} |> ListShould.ContainAll {values}
- [ ] {list} |> ListShould.NotContainAll {values}
- [ ] {list} |> ListShould.FindValueWith {predicateExpression}
- [ ] {list} |> ListShould.NotFindValueWith {predicateExpression}
- [x] {list} |> ListShould.HaveAllValuesPassTestOf {predicateExpression}
- [x] {list} |> ListShould.HaveNoValuesPassTestOf {predicateExpression}
- [ ] {list} |> ListShould.BeSorted
- [ ] {list} |> ListShould.NotBeSorted
- [ ] {list} |> ListShould.BeSortedBy {comparator}
- [ ] {list} |> ListShould.NotBeSortedBy {comparator}
- [ ] {list} |> ListShould.BeEmpty
- [ ] {list} |> ListShould.NotBeEmpty
- [x] {list} |> ListShould.HaveLengthOf {integer}
- [x] {list} |> ListShould.NotHaveLengthOf {integer}
- [x] {list} |> ListShould.HaveAllValuesPassAllOf [ {value -> TestResult} ]

### SeqShould ###

- [x] {collection} |> SeqShould.Contain {value}
- [x] {collection} |> SeqShould.NotContain {value}
- [ ] {collection} |> SeqShould.ContainAny {values}
- [ ] {collection} |> SeqShould.NotContainAny {values}
- [ ] {collection} |> SeqShould.ContainAll {values}
- [ ] {collection} |> SeqShould.NotContainAll {values}
- [ ] {collection} |> SeqShould.FindValueWith {predicateExpression}
- [ ] {collection} |> SeqShould.NotFindValueWith {predicateExpression}
- [x] {collection} |> SeqShould.HaveAllValuesPassTestOf {predicateExpression}
- [x] {collection} |> SeqShould.HaveNoValuesPassTestOf {predicateExpression}
- [ ] {collection} |> SeqShould.BeSorted
- [ ] {collection} |> SeqShould.NotBeSorted
- [ ] {collection} |> SeqShould.BeSortedBy {comparator}
- [ ] {collection} |> SeqShould.NotBeSortedBy {comparator}
- [ ] {collection} |> SeqShould.BeEmpty
- [ ] {collection} |> SeqShould.NotBeEmpty
- [x] {collection} |> SeqShould.HaveLengthOf {integer}
- [x] {collection} |> SeqShould.NotHaveLengthOf {integer}
- [x] {collection} |> SeqShould.HaveAllValuesPassAllOf [ {value -> TestResult} ]

### ArrayShould ###

- [x] {array} |> ArrayShould.Contain {value}
- [x] {array} |> ArrayShould.NotContain {value}
- [ ] {array} |> ArrayShould.ContainAny {values}
- [ ] {array} |> ArrayShould.NotContainAny {values}
- [ ] {array} |> ArrayShould.ContainAll {values}
- [ ] {array} |> ArrayShould.NotContainAll {values}
- [ ] {array} |> ArrayShould.FindValueWith {predicateExpression}
- [ ] {array} |> ArrayShould.NotFindValueWith {predicateExpression}
- [x] {array} |> ArrayShould.HaveAllValuesPassTestOf {predicateExpression}
- [x] {array} |> ArrayShould.HaveNoValuesPassTestOf {predicateExpression}
- [ ] {array} |> ArrayShould.BeSorted
- [ ] {array} |> ArrayShould.NotBeSorted
- [ ] {array} |> ArrayShould.BeSortedBy {comparator}
- [ ] {array} |> ArrayShould.NotBeSortedBy {comparator}
- [ ] {array} |> ArrayShould.BeEmpty
- [ ] {array} |> ArrayShould.NotBeEmpty
- [x] {array} |> ArrayShould.HaveLengthOf {integer}
- [x] {array} |> ArrayShould.NotHaveLengthOf {integer}
- [x] {array} |> ArrayShould.HaveAllValuesPassAllOf [ {value -> TestResult} ]

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->
<!-- GENERATED DOCUMENT DO NOT EDIT! -->