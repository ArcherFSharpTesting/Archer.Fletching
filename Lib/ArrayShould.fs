[<Microsoft.FSharp.Core.AutoOpen>]
module Archer.ShouldArray

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type ArrayShould =
    static member Contain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.Contain (value, fullPath, lineNumber)
        
    static member NotContain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.NotContain (value, fullPath, lineNumber)
        
    static member HaveAllValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.HaveAllValuesPassTestOf (predicateExpression, fullPath, lineNumber)
        
    static member HaveNoValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.HaveNoValuesPassTestOf (predicateExpression, fullPath, lineNumber)
        
    static member HaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.HaveLengthOf (length, fullPath, lineNumber)
        
    static member NotHaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.NotHaveLengthOf (length, fullPath, lineNumber)
        
    static member HaveAllValuesPassAllOf (tests: ('a -> TestResult) list, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.HaveAllValuesPassAllOf (tests, fullPath, lineNumber)