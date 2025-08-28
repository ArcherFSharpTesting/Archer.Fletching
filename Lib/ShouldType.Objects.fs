namespace Archer

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Fletching.Types.Internal
open FSharp.Quotations.Evaluator
open Swensen.Unquote

/// <summary>
/// Provides object and type-based assertion helpers for Archer test verifications.
/// </summary>
type Should =
    /// <summary>
    /// Asserts that the actual value is equal to the expected value.
    /// </summary>
    /// <param name="expected">The expected value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if the values are equal; otherwise, a validation failure result.</returns>
    static member BeEqualTo (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check ((=) expected) fullPath lineNumber id id expected

    /// <summary>
    /// Asserts that the actual value is not equal to the expected value.
    /// </summary>
    /// <param name="expected">The value that the actual value should not be equal to.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if the values are not equal; otherwise, a validation failure result.</returns>
    static member NotBeEqualTo (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check ((<>) expected) fullPath lineNumber Not id expected

    /// <summary>
    /// Asserts that the actual value is the same reference as the expected value.
    /// </summary>
    /// <param name="expected">The expected reference.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if the references are the same; otherwise, a validation failure result.</returns>
    static member BeSameAs (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check (checkReference expected) fullPath lineNumber ReferenceOf id expected

    /// <summary>
    /// Asserts that the actual value is not the same reference as the expected value.
    /// </summary>
    /// <param name="expected">The reference that the actual value should not be the same as.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if the references are not the same; otherwise, a validation failure result.</returns>
    static member NotBeSameAs (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check (checkReference expected >> not) fullPath lineNumber (ReferenceOf >> Not) id expected

    /// <summary>
    /// Asserts that the actual value is an instance of the expected type.
    /// </summary>
    /// <typeparam name="'expectedType">The expected type.</typeparam>
    /// <param name="actual">The value to check.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if the value is of the expected type; otherwise, a validation failure result.</returns>
    static member BeOfType<'expectedType> (actual, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check isInstanceOf<'expectedType> fullPath lineNumber id getType typeof<'expectedType> actual

    /// <summary>
    /// Asserts that the actual value is not an instance of the expected type.
    /// </summary>
    /// <typeparam name="'expectedType">The type that the actual value should not be.</typeparam>
    /// <param name="actual">The value to check.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if the value is not of the expected type; otherwise, a validation failure result.</returns>
    static member NotBeTypeOf<'expectedType> (actual, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check (isInstanceOf<'expectedType> >> not) fullPath lineNumber Not getType typeof<'expectedType> actual

    /// <summary>
    /// Asserts that the actual value is null.
    /// </summary>
    /// <param name="actual">The value to check for null.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if the value is null; otherwise, a validation failure result.</returns>
    static member BeNull<'expectedType when 'expectedType: null> (actual: 'expectedType, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check (fun v -> match v with | null -> true | _ -> false) fullPath lineNumber id id null actual

    /// <summary>
    /// Asserts that the actual value is not null.
    /// </summary>
    /// <param name="actual">The value to check for not being null.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if the value is not null; otherwise, a validation failure result.</returns>
    static member NotBeNull<'expectedType when 'expectedType: null> (actual: 'expectedType, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check (fun v -> match v with | null -> false | _ -> true) fullPath lineNumber id id (Not null) actual

    /// <summary>
    /// Asserts that the actual value is the default value for the expected type.
    /// </summary>
    /// <param name="actual">The value to check for being the default.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if the value is the default; otherwise, a validation failure result.</returns>
    static member BeDefaultOf<'expectedType when 'expectedType: equality> (actual: 'expectedType, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check ((=) Unchecked.defaultof<'expectedType>) fullPath lineNumber id id Unchecked.defaultof<'expectedType> actual

    /// <summary>
    /// Asserts that the actual value is not the default value for the expected type.
    /// </summary>
    /// <param name="actual">The value to check for not being the default.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if the value is not the default; otherwise, a validation failure result.</returns>
    static member NotBeDefaultOf<'expectedType when 'expectedType: equality> (actual: 'expectedType, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check ((<>) Unchecked.defaultof<'expectedType>) fullPath lineNumber id id (Not Unchecked.defaultof<'expectedType>) actual

    /// <summary>
    /// Asserts that the actual value passes the provided predicate test.
    /// </summary>
    /// <param name="predicateExpression">The predicate expression to test the value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the actual value and returns <c>TestSuccess</c> if the predicate passes; otherwise, a validation failure result.</returns>
    static member PassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt actual =
            let predicateString = decompile predicateExpression
            let predicate: 'a -> bool = predicateExpression |> QuotationEvaluator.Evaluate
            check predicate fullPath lineNumber id FailsTest (PassesTest predicateString) actual

        checkIt

    /// <summary>
    /// Asserts that the actual value does not pass the provided predicate test.
    /// </summary>
    /// <param name="predicateExpression">The predicate expression to test the value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the actual value and returns <c>TestSuccess</c> if the predicate fails; otherwise, a validation failure result.</returns>
    static member NotPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt actual =
            let predicateString = decompile predicateExpression
            let predicate: 'a -> bool = predicateExpression |> QuotationEvaluator.Evaluate
            check (predicate >> not) fullPath lineNumber id PassesTest (FailsTest predicateString) actual

        checkIt

    /// <summary>
    /// Asserts that the actual value passes all provided test functions.
    /// </summary>
    /// <param name="tests">A list of test functions to apply to the value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the actual value and returns <c>TestSuccess</c> if all tests pass; otherwise, a validation failure result.</returns>
    static member PassAllOf (tests: ('a -> TestResult) list, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt actual =
            if 0 < tests.Length then
                tests
                |> List.map (fun f -> safeTry f actual)
                |> List.reduce (+)
            else
                failureBuilder.GeneralTestExpectationFailure ("Should.PassAllOf needs to be passed at least one verification function", fullPath, lineNumber)

        checkIt