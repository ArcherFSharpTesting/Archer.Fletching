/// <summary>
/// Provides result-based assertion helpers for Archer test verifications.
/// </summary>
[<AutoOpen>]
module Archer.ShouldResult

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type Should with
    /// <summary>
    /// Asserts that the actual value is <c>Ok</c> and equal to the expected value.
    /// </summary>
    /// <param name="expected">The expected value inside <c>Ok</c>.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the actual value and returns <c>TestSuccess</c> if it is <c>Ok expected</c>; otherwise, a validation failure result.</returns>
    static member BeOk (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let check actual =
            match actual with
            | Ok result -> check ((=) expected) fullPath lineNumber id id (Ok expected) result
            | Error _ -> check ((=) (Ok expected)) fullPath lineNumber id id (Ok expected) actual
        check

    /// <summary>
    /// Asserts that the actual value is <c>Error</c> and equal to the expected value.
    /// </summary>
    /// <param name="expected">The expected value inside <c>Error</c>.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes the actual value and returns <c>TestSuccess</c> if it is <c>Error expected</c>; otherwise, a validation failure result.</returns>
    static member BeError (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let check actual =
            match actual with
            | Error err -> check ((=) expected) fullPath lineNumber id id (Error expected) err
            | Ok _ -> check ((=) (Error expected)) fullPath lineNumber id id (Error expected) actual
        check