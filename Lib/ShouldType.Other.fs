
/// <summary>
/// Provides miscellaneous assertion helpers for Archer test verifications.
/// </summary>
[<AutoOpen>]
module Archer.ShouldOther

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type Should with
    /// <summary>
    /// Fails a test with the provided message.
    /// </summary>
    /// <param name="message">The failure message.</param>
    /// <param name="fullPath">The file path where the failure is triggered (automatically provided).</param>
    /// <param name="lineNumber">The line number where the failure is triggered (automatically provided).</param>
    /// <returns>A test result representing a general test expectation failure.</returns>
    static member Fail (message: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        failureBuilder.GeneralTestExpectationFailure (message, fullPath, lineNumber)

    /// <summary>
    /// Marks a test as ignored without a message.
    /// </summary>
    /// <param name="fullPath">The file path where the ignore is triggered (automatically provided).</param>
    /// <param name="lineNumber">The line number where the ignore is triggered (automatically provided).</param>
    /// <returns>A function that takes any value and returns a test result representing an ignored test.</returns>
    static member BeIgnored ([<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let ignorer _ =
            failureBuilder.IgnoreFailure (fullPath, lineNumber)
        ignorer

    /// <summary>
    /// Marks a test as ignored with a message.
    /// </summary>
    /// <param name="message">The ignore message.</param>
    /// <param name="fullPath">The file path where the ignore is triggered (automatically provided).</param>
    /// <param name="lineNumber">The line number where the ignore is triggered (automatically provided).</param>
    /// <returns>A function that takes any value and returns a test result representing an ignored test with a message.</returns>
    static member BeIgnored (message: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let ignorer _ =
            failureBuilder.IgnoreFailure (message, fullPath, lineNumber)
        ignorer
