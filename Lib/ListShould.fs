/// <summary>
/// Provides list-based assertion helpers for Archer test verifications.
/// </summary>
[<AutoOpen>]
module Archer.ShouldList

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Fletching.Types.Internal
open FSharp.Quotations.Evaluator
open Swensen.Unquote

type ListShould =
    /// <summary>
    /// Asserts that the list contains the specified value.
    /// </summary>
    /// <param name="value">The value to check for in the list.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the list and returns a test result.</returns>
    static member Contain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (items: 'a list) =
            check (List.contains value) fullPath lineNumber (fun a -> Contains (a, value)) (fun a -> (a, value) |> Contains |> Not) items items

        checkIt

    /// <summary>
    /// Asserts that the list does not contain the specified value.
    /// </summary>
    /// <param name="value">The value that should not be in the list.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the list and returns a test result.</returns>
    static member NotContain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (items: 'a list) =
            check (List.contains value >> not) fullPath lineNumber (fun a -> (a, value) |> Contains |> Not) (fun a -> Contains (a, value)) items items

        checkIt

    /// <summary>
    /// Asserts that all values in the list pass the provided predicate test.
    /// </summary>
    /// <param name="predicateExpression">The predicate expression to test each value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the list and returns a test result.</returns>
    static member HaveAllValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (actual: 'a list) =
            let predicateString = decompile predicateExpression
            let predicate: 'a -> bool = predicateExpression |> QuotationEvaluator.Evaluate
            let expectedLength = actual.Length
            
            check (List.filter predicate >> List.length >> ((=) expectedLength)) fullPath lineNumber PassesTest FailsTest predicateString actual

        checkIt

    /// <summary>
    /// Asserts that no values in the list pass the provided predicate test.
    /// </summary>
    /// <param name="predicateExpression">The predicate expression to test each value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the list and returns a test result.</returns>
    static member HaveNoValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (actual: 'a list) =
            let predicateString = decompile predicateExpression
            let predicate: 'a -> bool = predicateExpression |> QuotationEvaluator.Evaluate
            
            check (List.filter predicate >> List.length >> (=) 0) fullPath lineNumber FailsTest PassesTest predicateString actual

        checkIt

    /// <summary>
    /// Asserts that the list has the specified length.
    /// </summary>
    /// <param name="length">The expected length of the list.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A test result.</returns>
    static member HaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check (List.length >> ((=) length)) fullPath lineNumber Length id length

    /// <summary>
    /// Asserts that the list does not have the specified length.
    /// </summary>
    /// <param name="length">The length that the list should not have.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A test result.</returns>
    static member NotHaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check (List.length >> ((<>) length)) fullPath lineNumber (Length >> Not) id length

    /// <summary>
    /// Asserts that all values in the list pass all provided test functions.
    /// </summary>
    /// <param name="tests">A list of test functions to apply to each value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the list and returns a test result.</returns>
    static member HaveAllValuesPassAllOf (tests: ('a -> TestResult) list, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (actual: 'a list) =
            actual
            |> List.map (Should.PassAllOf (tests, fullPath, lineNumber))
            |> List.reduce (+)

        checkIt

    /// <summary>
    /// Asserts that all values in the list are equal to the specified value.
    /// </summary>
    /// <param name="value">The value that all elements in the list should be equal to.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the list and returns a test result.</returns>
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