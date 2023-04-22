[<AutoOpen>]
module Archer.ShouldTypes

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type Should =
    //Object Checks
    static member BeEqualTo<'a> (expected: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        let check (actual: 'a) =
            TestSuccess
        check