# Archer.Fletching
A test verification Framework for Archer

## Should

- Dictionary
  - [ ] {dictionary} |> should.HaveKey {key}
  - [ ] {dictionary} |> should.NotHaveKey {key}
- Object
  - [x] {value} |> should.BeEqualTo {value}
  - [x] {value} |> should.NotBeEqualTo {value}
  - [x] {value} |> should.BeSameAs {value}
  - [x] {value} |> should.NotBeSameAs {value}
  - [x] {value} |> should.BeOfType\<Type\>
  - [x] {value} |> should.NotBeOfType\<Type\>
  - [x] {value} |> should.BeNull
  - [x] {value} |> should.NotBeNull
  - [x] {value} |> should.BeDefaultOf\<Type\>
  - [x] {value} |> should.NotBeDefaultOf\<Type\>
  - [x] {value} |> should.PassTestOf {predicate}
  - [x] {value} |> should.NotPassTestOf {predicate}
  - [x] {value} |> should.PassAllOf [ {value -> TestResult} ]
- Functions
  - [ ] {action} |> should.Return {value}
  - [ ] {action} |> should.NotReturnValue {value}
  - [ ] let result: Result<ex, TestExecutionResult> = {action} |> should.ThrowException
  - [ ] {action} |> should.NotThrowException
  - [ ] {action} |> should.Call {action} |> withParameter {predicate} {initialParameter}
- Events
  - [ ] {IEvent} |> should.Trigger |> by {action}
  - [ ] {IEvent} |> should.NotTrigger |> by {action}
- String
  - [ ] {string} |> should.Contain {string}
  - [ ] {string} |> should.NotContain {string}
  - [ ] {string} |> should.BeMatchedBy {regex}
  - [ ] {string} |> should.NotBeMatchedBy {regex}
- Numbers
  - [ ] {number} |> should.BeWithin ({number}, {number})
  - [ ] {number} |> should.BeBetween ({number}, {number})
  - [ ] {number} |> should.BeCloseTo {number} |> byDelta {number}
- Boolean
  - [x] {bool} |> should.BeTrue
  - [x] {bool} |> should.BeFalse
- Other
  - [x] {string} |> should.Fail
  - [x] {value} |> should.BeIgnored {string}
  - [x] {value} |> should.BeIgnored

## ListShould

- [x] {list} |> should.Contain {value}
- [x] {list} |> should.NotContain {value}
- [ ] {list} |> should.ContainAny {values}
- [ ] {list} |> should.NotContainAny {values}
- [ ] {list} |> should.ContainAll {values}
- [ ] {list} |> should.NotContainAll {values}
- [ ] {list} |> should.FindValueWith {predicateExpression}
- [ ] {list} |> should.NotFindValueWith {predicateExpression}
- [x] {list} |> should.FindAllValuesWith {predicateExpression}
- [x] {list} |> should.FindNoValuesWith {predicateExpression}
- [ ] {list} |> should.BeSorted
- [ ] {list} |> should.NotBeSorted
- [ ] {list} |> should.BeSortedBy {comparator}
- [ ] {list} |> should.NotBeSortedBy {comparator}
- [ ] {list} |> should.BeEmpty
- [ ] {list} |> should.NotBeEmpty
- [ ] {list} |> should.HaveLengthOf {integer}

## SeqShould

- [x] {collection} |> should.Contain {value}
- [x] {collection} |> should.NotContain {value}
- [ ] {collection} |> should.ContainAny {values}
- [ ] {collection} |> should.NotContainAny {values}
- [ ] {collection} |> should.ContainAll {values}
- [ ] {collection} |> should.NotContainAll {values}
- [ ] {collection} |> should.FindValueWith {predicateExpression}
- [ ] {collection} |> should.NotFindValueWith {predicateExpression}
- [x] {collection} |> should.FindAllValuesWith {predicateExpression}
- [x] {collection} |> should.FindNoValuesWith {predicateExpression}
- [ ] {collection} |> should.BeSorted
- [ ] {collection} |> should.NotBeSorted
- [ ] {collection} |> should.BeSortedBy {comparator}
- [ ] {collection} |> should.NotBeSortedBy {comparator}
- [ ] {collection} |> should.BeEmpty
- [ ] {collection} |> should.NotBeEmpty
- [ ] {collection} |> should.HaveLengthOf {integer}