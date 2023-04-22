[<AutoOpen>]
module Archer.ShouldTypes

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Fletching.Types.Internal

let private failureBuilder = TestResultFailureBuilder id

let private check fCheck fullPath lineNumber modifier expected actual =
    if actual |> fCheck then
        TestSuccess
    else
        failureBuilder.ValidationFailure (modifier expected, actual, fullPath, lineNumber)

type Should =
    // --- Object Checks ---------------------------------------------------------------------------------------------
    static member BeEqualTo<'a when 'a : equality> (expected: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check ((=) expected) fullPath lineNumber id expected
        
    static member NotBeEqualTo<'a when 'a : equality> (expected: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check ((<>) expected) fullPath lineNumber Not expected
        
    static member BeSameAs<'a> (expected: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        let checkIt (actual: 'a) =
            TestSuccess
            
        checkIt