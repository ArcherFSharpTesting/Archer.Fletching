namespace Archer

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Fletching.Types.Internal

type Not =
    static member Implemented ([<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let failureBuilder = TestResultFailureBuilder id
        failureBuilder.IgnoreFailure ("Not Yet Implemented", fullPath, lineNumber)