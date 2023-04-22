[<AutoOpen>]
module Archer.ShouldTypes

open System
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Fletching.Types.Internal

let private failureBuilder = TestResultFailureBuilder id

let private checkReference<'a> (expected: 'a) (actual: 'a) =
    Object.ReferenceEquals (actual, expected)

let private check fCheck fullPath lineNumber modifier expected actual =
    if actual |> fCheck then
        TestSuccess
    else
        failureBuilder.ValidationFailure (modifier expected, actual, fullPath, lineNumber)

type Should =
    // --- Object Checks ---------------------------------------------------------------------------------------------
    static member BeEqualTo (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check ((=) expected) fullPath lineNumber id expected
        
    static member NotBeEqualTo (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check ((<>) expected) fullPath lineNumber Not expected
        
    static member BeSameAs (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check (checkReference expected) fullPath lineNumber ReferenceOf expected
        
    static member NotBeSameAs (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check (checkReference expected >> not) fullPath lineNumber (ReferenceOf >> Not) expected
        
    static member BeOfType<'expected> (actual, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        let tType = typeof<'expected>
        
        if tType.IsInstanceOfType actual then
            TestSuccess
        else
            failureBuilder.ValidationFailure (tType, actual.GetType (), fullPath, lineNumber)
            