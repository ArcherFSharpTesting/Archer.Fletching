[<AutoOpen>]
module Archer.ShouldTypes

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Fletching.Types.Internal

let private failureBuilder = TestResultFailureBuilder id

type Should =
    //Object Checks
    static member BeEqualTo<'a> (expected: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        let check (actual: 'a) =
            if System.Object.ReferenceEquals (expected, actual) then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, actual, fullPath, lineNumber)
        check