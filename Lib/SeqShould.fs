/// <summary>
/// Provides sequence-based assertion helpers for Archer test verifications.
/// </summary>
[<AutoOpen>]
module Archer.ShouldSeq

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

/// <summary>
/// Provides sequence-based assertion helpers for Archer test verifications.
/// </summary>
type SeqShould =
    /// <summary>
    /// Asserts that the sequence contains the specified value.
    /// </summary>
    /// <param name="value">The value to check for in the sequence.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the sequence and returns a test result.</returns>
    static member Contain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.Contain (value, fullPath, lineNumber)

    /// <summary>
    /// Asserts that the sequence does not contain the specified value.
    /// </summary>
    /// <param name="value">The value that should not be in the sequence.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the sequence and returns a test result.</returns>
    static member NotContain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.NotContain (value, fullPath, lineNumber)

    /// <summary>
    /// Asserts that all values in the sequence pass the provided predicate test.
    /// </summary>
    /// <param name="predicateExpression">The predicate expression to test each value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the sequence and returns a test result.</returns>
    static member HaveAllValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.HaveAllValuesPassTestOf (predicateExpression, fullPath, lineNumber)

    /// <summary>
    /// Asserts that no values in the sequence pass the provided predicate test.
    /// </summary>
    /// <param name="predicateExpression">The predicate expression to test each value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the sequence and returns a test result.</returns>
    static member HaveNoValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.HaveNoValuesPassTestOf (predicateExpression, fullPath, lineNumber)

    /// <summary>
    /// Asserts that the sequence has the specified length.
    /// </summary>
    /// <param name="length">The expected length of the sequence.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the sequence and returns a test result.</returns>
    static member HaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.HaveLengthOf (length, fullPath, lineNumber)

    /// <summary>
    /// Asserts that the sequence does not have the specified length.
    /// </summary>
    /// <param name="length">The length that the sequence should not have.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the sequence and returns a test result.</returns>
    static member NotHaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.NotHaveLengthOf (length, fullPath, lineNumber)

    /// <summary>
    /// Asserts that all values in the sequence pass all provided test functions.
    /// </summary>
    /// <param name="tests">A list of test functions to apply to each value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the sequence and returns a test result.</returns>
    static member HaveAllValuesPassAllOf (tests: ('a -> TestResult) list, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.HaveAllValuesPassAllOf (tests, fullPath, lineNumber)

    /// <summary>
    /// Asserts that all values in the sequence are equal to the specified value.
    /// </summary>
    /// <param name="value">The value that all elements in the sequence should be equal to.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the sequence and returns a test result.</returns>
    static member HaveAllValuesBe (value: 'a when 'a : equality, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofSeq >> ListShould.HaveAllValuesBe (value, fullPath, lineNumber)