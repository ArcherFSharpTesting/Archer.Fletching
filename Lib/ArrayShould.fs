/// <summary>
/// Provides array-based assertion helpers for Archer test verifications.
/// </summary>
[<AutoOpen>]
module Archer.ShouldArray

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

/// <summary>
/// Provides array-based assertion helpers for Archer test verifications.
/// </summary>
type ArrayShould =
    /// <summary>
    /// Asserts that the array contains the specified value.
    /// </summary>
    /// <param name="value">The value to check for in the array.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the array and returns a test result.</returns>
    static member Contain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.Contain (value, fullPath, lineNumber)
        
    /// <summary>
    /// Asserts that the array does not contain the specified value.
    /// </summary>
    /// <param name="value">The value that should not be in the array.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the array and returns a test result.</returns>
    static member NotContain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.NotContain (value, fullPath, lineNumber)
        
    /// <summary>
    /// Asserts that all values in the array pass the provided predicate test.
    /// </summary>
    /// <param name="predicateExpression">The predicate expression to test each value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the array and returns a test result.</returns>
    static member HaveAllValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.HaveAllValuesPassTestOf (predicateExpression, fullPath, lineNumber)
        
    /// <summary>
    /// Asserts that no values in the array pass the provided predicate test.
    /// </summary>
    /// <param name="predicateExpression">The predicate expression to test each value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the array and returns a test result.</returns>
    static member HaveNoValuesPassTestOf (predicateExpression: Quotations.Expr<'a -> bool>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.HaveNoValuesPassTestOf (predicateExpression, fullPath, lineNumber)
        
    /// <summary>
    /// Asserts that the array has the specified length.
    /// </summary>
    /// <param name="length">The expected length of the array.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the array and returns a test result.</returns>
    static member HaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.HaveLengthOf (length, fullPath, lineNumber)
        
    /// <summary>
    /// Asserts that the array does not have the specified length.
    /// </summary>
    /// <param name="length">The length that the array should not have.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the array and returns a test result.</returns>
    static member NotHaveLengthOf (length: int, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.NotHaveLengthOf (length, fullPath, lineNumber)
        
    /// <summary>
    /// Asserts that all values in the array pass all provided test functions.
    /// </summary>
    /// <param name="tests">A list of test functions to apply to each value.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the array and returns a test result.</returns>
    static member HaveAllValuesPassAllOf (tests: ('a -> TestResult) list, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.HaveAllValuesPassAllOf (tests, fullPath, lineNumber)
        
    /// <summary>
    /// Asserts that all values in the array are equal to the specified value.
    /// </summary>
    /// <param name="value">The value that all elements in the array should be equal to.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the array and returns a test result.</returns>
    static member HaveAllValuesBe (value: 'a when 'a : equality, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        List.ofArray >> ListShould.HaveAllValuesBe (value, fullPath, lineNumber)