[<AutoOpen>]
module Archer.ShouldTypeHelpers

open System
open Archer.Fletching.Types.Internal

let failureBuilder = TestResultFailureBuilder id

let checkReference<'a> (expected: 'a) (actual: 'a) =
    Object.ReferenceEquals (actual, expected)
    
let getType value = value.GetType ()

let isInstanceOf<'expectedType> actual =
    let expected = typeof<'expectedType>
    
    expected.IsInstanceOfType actual

let check fCheck fullPath lineNumber expectedModifier actualModifier expected actual =
    if actual |> fCheck then
        TestSuccess
    else
        failureBuilder.ValidationFailure (expectedModifier expected, actualModifier actual, fullPath, lineNumber)