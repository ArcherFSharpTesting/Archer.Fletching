[<AutoOpen>]
module Archer.ShouldTypes

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Fletching.Types.Internal

let private failureBuilder = TestResultFailureBuilder id

type Should =
    //Object Checks
    static member BeEqualTo<'a when 'a : equality> (expected: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        let check (actual: 'a) =
            if actual = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, actual, fullPath, lineNumber)
        check
        
    static member NotBeEqualTo<'a when 'a : equality> (expected: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        let check (actual: 'a) =
            if actual = expected |> not then
                failureBuilder.ValidationFailure (Not expected, actual, fullPath, lineNumber)
            else
                TestSuccess
        
        check