
[<AutoOpen>]
module Archer.ShouldOther

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type Should with
    static member Fail (message: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        failureBuilder.GeneralTestExpectationFailure (message, fullPath, lineNumber)

    static member BeIgnored ([<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let ignorer _ =
            failureBuilder.IgnoreFailure (fullPath, lineNumber)
        ignorer

    static member BeIgnored (message: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let ignorer _ =
            failureBuilder.IgnoreFailure (message, fullPath, lineNumber)
        ignorer
