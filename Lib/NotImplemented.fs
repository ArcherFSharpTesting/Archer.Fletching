namespace Archer

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Validations.Types.Internal

/// <summary>
/// Provides a method to indicate that a test or feature is not yet implemented.
/// </summary>
type Not =
    /// <summary>
    /// Marks a test or feature as not yet implemented. This does not throw an exception, but returns a failure result for reporting.
    /// </summary>
    /// <param name="fullPath">The file path of the calling code. Automatically provided by the compiler.</param>
    /// <param name="lineNumber">The line number in the source file. Automatically provided by the compiler.</param>
    static member Implemented ([<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let failureBuilder = TestResultFailureBuilder id
        failureBuilder.IgnoreFailure ("Not Yet Implemented", fullPath, lineNumber)