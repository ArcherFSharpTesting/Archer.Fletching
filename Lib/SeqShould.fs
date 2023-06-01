[<Microsoft.FSharp.Core.AutoOpen>]
module Archer.ShouldSeq

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type SeqShould =
    static member Contain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.Contain (value, fullPath, lineNumber)
        
    static member NotContain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.NotContain (value, fullPath, lineNumber)
        
    static member HaveAllValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.HaveAllValuesPassTestOf (predicateExpression, fullPath, lineNumber)
        
    static member HaveNoValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.HaveNoValuesPassTestOf (predicateExpression, fullPath, lineNumber)
        
    static member HaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.HaveLengthOf (length, fullPath, lineNumber)
        
    static member NotHaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.NotHaveLengthOf (length, fullPath, lineNumber)
        
    static member HaveAllValuesPassAllOf (tests: ('a -> TestResult) list, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.HaveAllValuesPassAllOf (tests, fullPath, lineNumber)
        
    static member HaveAllValuesBe (value: 'a when 'a : equality, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.HaveAllValuesBe (value, fullPath, lineNumber)