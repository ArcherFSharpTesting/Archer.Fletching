[<AutoOpen>]
module Archer.ShouldResult

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type Should with
    static member BeOk (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let check actual =
            match actual with
            | Ok result -> check ((=) expected) fullPath lineNumber id id (Ok expected) result
            | Error _ -> check ((=) (Ok expected)) fullPath lineNumber id id (Ok expected) actual
        check
        
    static member BeError (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let check actual =
            match actual with
            | Error err -> check ((=) expected) fullPath lineNumber id id (Error expected) err
            | Ok _ -> check ((=) (Error expected)) fullPath lineNumber id id (Error expected) actual
        check