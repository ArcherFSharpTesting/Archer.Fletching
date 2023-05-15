[<Microsoft.FSharp.Core.AutoOpen>]
module Archer.ShouldSeq

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type SeqShould =
    static member Contain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.Contain (value, fullPath, lineNumber)
        
    static member NotContain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.NotContain (value, fullPath, lineNumber)