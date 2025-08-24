<!-- (dl
(section-meta
  (title Archer.Fletching)
)
) -->
A test verification Framework for Archer

<!-- (dl (# Complete Features)) -->

- Should
  - Object
    - {value} |> Should.BeEqualTo {value}
    - {value} |> Should.NotBeEqualTo {value}
    - {value} |> Should.BeSameAs {value}
    - {value} |> Should.NotBeSameAs {value}
    - {value} |> Should.BeOfType<Type>
    - {value} |> Should.NotBeOfType<Type>
    - {value} |> Should.BeNull
    - {value} |> Should.NotBeNull
    - {value} |> Should.BeDefaultOf<Type>
    - {value} |> Should.NotBeDefaultOf<Type>
    - {value} |> Should.PassTestOf {predicate}
    - {value} |> Should.NotPassTestOf {predicate}
    - {value} |> Should.PassAllOf [ {value -> TestResult} ]
  - Result
    - {result} |> Should.BeOk {value}
    - {result} |> Should.BeError {value}
  - Boolean
    - {bool} |> Should.BeTrue
    - {bool} |> Should.BeFalse
  - Approvals
    - {testInfo} |> Should.MeetStandard {reporter} {string}
  - Other
    - {string} |> Should.Fail
    - {value} |> Should.BeIgnored {string}
    - {value} |> Should.BeIgnored
- ListShould
  - {list} |> ListShould.Contain {value}
  - {list} |> ListShould.NotContain {value}
  - {list} |> ListShould.HaveAllValuesPassTestOf {predicateExpression}
  - {list} |> ListShould.HaveNoValuesPassTestOf {predicateExpression}
  - {list} |> ListShould.HaveLengthOf {integer}
  - {list} |> ListShould.NotHaveLengthOf {integer}
  - {list} |> ListShould.HaveAllValuesPassAllOf [ {value -> TestResult} ]
  - {list} |> ListShould.HaveAllValuesPassTestOf {indexedPredicateExpression}
  - {list} |> ListShould.HaveNoValuesPassTestOf {indexedPredicateExpression}
- SeqShould
  - {collection} |> SeqShould.Contain {value}
  - {collection} |> SeqShould.NotContain {value}
  - {collection} |> SeqShould.HaveAllValuesPassTestOf {predicateExpression}
  - {collection} |> SeqShould.HaveNoValuesPassTestOf {predicateExpression}
  - {collection} |> SeqShould.HaveLengthOf {integer}
  - {collection} |> SeqShould.NotHaveLengthOf {integer}
  - {collection} |> SeqShould.HaveAllValuesPassAllOf [ {value -> TestResult} ]
  - {collection} |> SeqShould.HaveAllValuesPassTestOf {indexedPredicateExpression}
  - {collection} |> SeqShould.HaveNoValuesPassTestOf {indexedPredicateExpression}
- ArrayShould
  - {array} |> ArrayShould.Contain {value}
  - {array} |> ArrayShould.NotContain {value}
  - {array} |> ArrayShould.HaveAllValuesPassTestOf {predicateExpression}
  - {array} |> ArrayShould.HaveNoValuesPassTestOf {predicateExpression}
  - {array} |> ArrayShould.HaveLengthOf {integer}
  - {array} |> ArrayShould.NotHaveLengthOf {integer}
  - {array} |> ArrayShould.HaveAllValuesPassAllOf [ {value -> TestResult} ]
  - {array} |> ArrayShould.HaveAllValuesPassTestOf {indexedPredicateExpression}
  - {array} |> ArrayShould.HaveNoValuesPassTestOf {indexedPredicateExpression}
- Not
  - Not.Implemented ()


<!-- (dl (# Feature status)) -->

This is a list of feature ideas. All features on this list _may_ or _may not_ end up in the final product.

<!-- (dl (## Should)) -->

- Dictionary
  - [ ] {dictionary} |> Should.HaveKey {key}
  - [ ] {dictionary} |> Should.NotHaveKey {key}
  - [ ] {dictionary} |> Should.HaveValue {value}
  - [ ] {dictionary} |> Should.HavePair ({key}, {value})
  - [ ] {dictionary} |> Should.BeEmpty
  - [ ] {dictionary} |> Should.NotBeEmpty
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
  - [ ] {action} |> Should.ThrowException
  - [ ] {action} |> Should.NotThrowException
  - [ ] {action} |> Should.Call {action} |> withParameter {predicate} {initialParameter}
- Events
  - [ ] {IEvent} |> Should.Trigger |> by {action}
  - [ ] {IEvent} |> Should.NotTrigger |> by {action}
  - [ ] {IEvent} |> Should.TriggerWith {expectedArgs} |> by {action}
- String
  - [ ] {string} |> Should.Contain {string}
  - [ ] {string} |> Should.NotContain {string}
  - [ ] {string} |> Should.StartsWith {prefix}
  - [ ] {string} |> Should.EndWith {suffix}
  - [ ] {string} |> Should.HaveLengthOf {integer}
  - [ ] {string} |> Should.BeMatchedBy {regex}
  - [ ] {string} |> Should.NotBeMatchedBy {regex}
  - [ ] {string} |> Should.MatchStandard {ITestInfo} {reporter}
- Numbers
  - [ ] {number} |> Should.BeWithin ({number}, {number})
  - [ ] {number} |> Should.BeBetween ({number}, {number})
  - [ ] {number} |> Should.BeCloseTo {number} |> byDelta {number}
  - [ ] {number} |> Should.BeEqualTo {number}
  - [ ] {number} |> Should.BePositive
  - [ ] {number} |> Should.BeNegative
- Boolean
  - [x] {bool} |> Should.BeTrue
  - [x] {bool} |> Should.BeFalse
- Approvals
  - [x] {testInfo} |> Should.MeetStandard {reporter} {string}
- Other
  - [x] {string} |> Should.Fail
  - [x] {value} |> Should.BeIgnored {string}
  - [x] {value} |> Should.BeIgnored

<!-- (dl (## ListShould)) -->

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
- [ ] {list} |> ListShould.HaveAnyPassTestOf {predicateExpression}
- [ ] {list} |> ListShould.BeEmpty
- [ ] {list} |> ListShould.NotBeEmpty
- [ ] {list} |> ListShould.FindValueWith {indexedPredicateExpression}
- [ ] {list} |> ListShould.NotFindValueWith {indexedPredicateExpression}
- [x] {list} |> ListShould.HaveAllValuesPassTestOf {indexedPredicateExpression}
- [x] {list} |> ListShould.HaveNoValuesPassTestOf {indexedPredicateExpression}
- [ ] {list} |> ListShould.HaveAnyPassTestOf {indexedPredicateExpression}

<!-- (dl (## SeqShould)) -->

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
- [ ] {collection} |> SeqShould.HaveAnyPassTestOf {predicateExpression}
- [ ] {collection} |> SeqShould.BeEmpty
- [ ] {collection} |> SeqShould.NotBeEmpty
- [ ] {collection} |> SeqShould.FindValueWith {indexedPredicateExpression}
- [ ] {collection} |> SeqShould.NotFindValueWith {indexedPredicateExpression}
- [x] {collection} |> SeqShould.HaveAllValuesPassTestOf {indexedPredicateExpression}
- [x] {collection} |> SeqShould.HaveNoValuesPassTestOf {indexedPredicateExpression}
- [ ] {collection} |> SeqShould.HaveAnyPassTestOf {indexedPredicateExpression}

<!-- (dl (## ArrayShould)) -->

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
- [ ] {array} |> ArrayShould.HaveAnyPassTestOf {predicateExpression}
- [ ] {array} |> ArrayShould.BeEmpty
- [ ] {array} |> ArrayShould.NotBeEmpty
- [ ] {array} |> ArrayShould.FindValueWith {indexedPredicateExpression}
- [ ] {array} |> ArrayShould.NotFindValueWith {indexedPredicateExpression}
- [x] {array} |> ArrayShould.HaveAllValuesPassTestOf {indexedPredicateExpression}
- [x] {array} |> ArrayShould.HaveNoValuesPassTestOf {indexedPredicateExpression}
- [ ] {array} |> ArrayShould.HaveAnyPassTestOf {indexedPredicateExpression}

<!-- (dl (## Not)) -->

- [x] Not.Implemented ()

