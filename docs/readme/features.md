<!-- (dl
(section-meta
  (title Archer.Fletching)
)
) -->
A test verification Framework for Archer

<!-- (dl (# Should)) -->

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
- Approvals
  - [x] {testInfo} |> Should.MeetStandard {reporter} {string}
- Other
  - [x] {string} |> Should.Fail
  - [x] {value} |> Should.BeIgnored {string}
  - [x] {value} |> Should.BeIgnored

<!-- (dl (# ListShould)) -->

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

<!-- (dl (# SeqShould)) -->

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


<!-- (dl (# ArrayShould)) -->

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

<!-- (dl (# Not)) -->

- [x] Not.Implemented ()

