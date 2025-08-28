/// <summary>
/// Provides boolean assertion helpers for Archer test verifications.
/// </summary>
[<AutoOpen>]
module Archer.ShouldBoolean

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type Should with
    /// <summary>
    /// Asserts that the actual value is <c>true</c>.
    /// </summary>
    /// <param name="actual">The boolean value to check.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if <paramref name="actual"/> is <c>true</c>; otherwise, a validation failure result.</returns>
    static member BeTrue (actual: bool, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check id fullPath lineNumber id id true actual

    /// <summary>
    /// Asserts that the actual value is <c>false</c>.
    /// </summary>
    /// <param name="actual">The boolean value to check.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns><c>TestSuccess</c> if <paramref name="actual"/> is <c>false</c>; otherwise, a validation failure result.</returns>
    static member BeFalse (actual: bool, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check not fullPath lineNumber id id false actual