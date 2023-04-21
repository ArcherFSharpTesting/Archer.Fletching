namespace Archer.Fletching.Lib


(*
+ Should
|   + Modifiers
|   + Dictionary
|   |   + {dictionary} |> should.HaveKey {key}
|   |   + {dictionary} |> should.NotHaveKey {key} 
|   + Collection
|   |   + {collection} |> should.Contain {value}
|   |   + {collection} |> should.NotContain {value}
|   |   + {collection} |> should.ContainAny {values}
|   |   + {collection} |> should.NotContainAny {values}
|   |   + {collection} |> should.ContainAll {values}
|   |   + {collection} |> should.NotContainAll {values}
|   |   + {collection} |> should.FindValueWith {predicate}
|   |   + {collection} |> should.NotFindValueWith {predicate}
|   |   + {collection} |> should.FindAllValuesWith {predicate}
|   |   + {collection} |> should.FindNoValuesWith {predicate}
|   |   + {collection} |> should.BeSorted
|   |   + {collection} |> should.NotBeSorted
|   |   + {collection} |> should.BeSortedBy {comparator}
|   |   + {collection} |> should.NotBeSortedBy {comparator}
|   |   + {collection} |> should.BeEmpty
|   |   + {collection} |> should.NotBeEmpty
|   |   + {collection} |> should.HaveLengthOf {integer}
|   + Object
|   |   + {value} |> should.BeEqualTo {value}
|   |   + {value} |> should.NotBeEqualTo {value}
|   |   + {value} |> should.BeSameAs {value}
|   |   + {value} |> should.NotBeSameAs {value}
|   |   + {value} |> should.BeOfType<Type>
|   |   + {value} |> should.NotBeOfType<Type>
|   |   + {value} |> should.BeNull
|   |   + {value} |> should.NotBeNull
|   |   + {value} |> should.BeDefaultOf<Type>
|   |   + {value} |> should.NotBeDefaultOf<Type>
|   |   + {value} |> should.PassTestOf {predicate}
|   |   + {value} |> should.NotPassTestOf {predicate}
|   + Functions
|   |   + {action} |> should.Return {value}
|   |   + {action} |> should.NotReturnValue {value}
|   |   + let result: Result<ex, TestExecutionResult> = {action} |> should.ThrowException
|   |   + {action} |> should.NotThrowException
|   |   + {action} |> should.Call {action} |> withParameter {predicate} {initialParameter}
|   + Events
|   |   + {IEvent} |> should.Trigger |> by {action}
|   |   + {IEvent} |> should.NotTrigger |> by {action}
|   + String
|   |   + {string} |> should.Contain {string}
|   |   + {string} |> should.NotContain {string}
|   |   + {string} |> should.BeMatchedBy {regex}
|   |   + {string} |> should.NotBeMatchedBy {regex}
|   + Numbers
|   |   + {number} |> should.BeWithin ({number}, {number})
|   |   + {number} |> should.BeBetween ({number}, {number})
|   |   + {number} |> should.BeCloseTo {number} |> byDelta {number}
|   + Boolean
|   |   + {bool} |> should.BeTrue
|   |   + {bool} |> should.BeFalse
|   + Other
|   |   + should.Fail ()
|   |   + should.Fail {string}
|   |   + {value} |> should.BeIgnored {string}
|   |   + {value} |> should.BeIgnored ()

*)

module Say =
    let hello name =
        printfn "Hello %s" name
