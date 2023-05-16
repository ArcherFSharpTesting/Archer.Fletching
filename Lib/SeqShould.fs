[<Microsoft.FSharp.Core.AutoOpen>]
module Archer.ShouldSeq

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type SeqShould =
    static member Contain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.Contain (value, fullPath, lineNumber)
        
    static member NotContain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.NotContain (value, fullPath, lineNumber)
        
    static member AllValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        List.ofSeq >> ListShould.AllValuesPassTestOf (predicateExpression, fullPath, lineNumber)
        
    static member NoValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        List.ofSeq >> ListShould.NoValuesPassTestOf (predicateExpression, fullPath, lineNumber)
        
    static member HaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        List.ofSeq >> ListShould.HaveLengthOf (length, fullPath, lineNumber)
        
    static member NotHaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        List.ofSeq >> ListShould.NotHaveLengthOf (length, fullPath, lineNumber)