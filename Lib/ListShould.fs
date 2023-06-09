﻿[<Microsoft.FSharp.Core.AutoOpen>]
module Archer.ShouldList

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Fletching.Types.Internal
open FSharp.Quotations.Evaluator
open Swensen.Unquote

type ListShould =
    static member Contain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (items: 'a list) =
            check (List.contains value) fullPath lineNumber (fun a -> Contains (a, value)) (fun a -> (a, value) |> Contains |> Not) items items
            
        checkIt
        
    static member NotContain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (items: 'a list) =
            check (List.contains value >> not) fullPath lineNumber (fun a -> (a, value) |> Contains |> Not) (fun a -> Contains (a, value)) items items
            
        checkIt
        
    static member HaveAllValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (actual: 'a list) =
            let predicateString = decompile predicateExpression
            let predicate: 'a -> bool = predicateExpression |> QuotationEvaluator.Evaluate
            let expectedLength = actual.Length
            
            check (List.filter predicate >> List.length >> ((=) expectedLength)) fullPath lineNumber PassesTest FailsTest predicateString actual
            

        checkIt
        
    static member HaveNoValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (actual: 'a list) =
            let predicateString = decompile predicateExpression
            let predicate: 'a -> bool = predicateExpression |> QuotationEvaluator.Evaluate
            
            check (List.filter predicate >> List.length >> (=) 0) fullPath lineNumber FailsTest PassesTest predicateString actual
            
        checkIt
        
    static member HaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check (List.length >> ((=) length)) fullPath lineNumber Length id length
        
    static member NotHaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check (List.length >> ((<>) length)) fullPath lineNumber (Length >> Not) id length
        
    static member HaveAllValuesPassAllOf (tests: ('a -> TestResult) list, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (actual: 'a list) =
            actual
            |> List.map (Should.PassAllOf (tests, fullPath, lineNumber))
            |> List.reduce (+)
            
        checkIt
        
    static member HaveAllValuesBe (value: 'a when 'a : equality, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) = 
        let checkIt (actual: 'a list) =
            let isCorrect =
                actual
                |> List.map ((=) value)
                |> List.reduce (&&)
            
            if isCorrect then TestSuccess
            else
                failureBuilder.ValidationFailure (HasOnlyValue value, actual, fullPath, lineNumber)
        
        checkIt