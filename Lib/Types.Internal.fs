
/// <summary>
/// Internal types and helpers for Archer.Fletching test verification and failure handling.
/// </summary>
module Archer.Fletching.Types.Internal

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer

/// <summary>
/// Represents modifiers that can be applied to test values for verification.
/// </summary>
type Modifiers<'a> =
    /// <summary>Negates the condition for the value. <c>Not of value</c>: The value to negate the condition for.</summary>
    | Not of 'a
    /// <summary>References the value for further checks. <c>ReferenceOf of value</c>: The value to reference.</summary>
    | ReferenceOf of 'a
    /// <summary>Indicates the value passes a test. <c>PassesTest of value</c>: The value that passes the test.</summary>
    | PassesTest of 'a
    /// <summary>Indicates the value fails a test. <c>FailsTest of value</c>: The value that fails the test.</summary>
    | FailsTest of 'a
    /// <summary>Checks if a list contains a value. <c>Contains of list * value</c>: The list to check and the value to look for in the list.</summary>
    | Contains of 'a list * 'a
    /// <summary>Checks if a value is the only value present. <c>HasOnlyValue of value</c>: The value that should be the only one present.</summary>
    | HasOnlyValue of 'a
    /// <summary>Checks the length of a value (e.g., collection). <c>Length of value</c>: The value whose length is checked.</summary>
    | Length of 'a

/// <summary>
/// Builds a location record from a file name and line number for failure reporting.
/// </summary>
/// <param name="fullFileName">The full path to the file.</param>
/// <param name="lineNumber">The line number in the file.</param>
let buildLocation fullFileName lineNumber =
    let info = System.IO.FileInfo fullFileName
    let path = info.Directory.FullName
    let fileName = info.Name
    
    {
        FilePath = path
        FileName = fileName
        LineNumber = lineNumber 
    }

/// <summary>
/// Holds information about an expectation, including expected and actual values.
/// </summary>
type ExpectationInfo<'expected, 'actual> =
    {
        /// <summary>The expected value for the test.</summary>
        ExpectedValue: 'expected
        /// <summary>The actual value produced by the test.</summary>
        ActualValue: 'actual
    }
    interface IVerificationInfo with
        member this.Expected with get () = $"%A{this.ExpectedValue}"
        member this.Actual with get () = $"%A{this.ActualValue}"
        

/// <summary>
/// Converts an <c>ExpectationInfo</c> to a generic <c>IVerificationInfo</c> interface.
/// </summary>
let toVerificationInfo (expectation: ExpectationInfo<'expected, 'actual>) =
    expectation :> IVerificationInfo
    
/// <summary>
/// Builds various types of test failures and converts them to a result type.
/// </summary>
/// <param name="toResult">A function to convert a <c>TestFailure</c> to the desired result type.</param>
type TestFailureBuilder<'result> (toResult: TestFailure -> 'result) =
    /// <summary>
    /// Creates a validation failure result from expectation info, file, and line number.
    /// </summary>
    member _.ValidationFailure<'expected, 'actual> (expectationInfo: ExpectationInfo<'expected, 'actual>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let failure =
            expectationInfo
            |> toVerificationInfo
            |> ExpectationVerificationFailure
            
        let location = buildLocation fullPath lineNumber
        
        (
            failure,
            location
        )
        |> TestExpectationFailure
        |> toResult
            
    /// <summary>
    /// Creates a validation failure result from expected and actual values, file, and line number.
    /// </summary>
    member this.ValidationFailure (expected, actual, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.ValidationFailure ({ ExpectedValue = expected; ActualValue = actual }, fullPath, lineNumber)
            
    /// <summary>
    /// Creates a general test expectation failure result with a message, file, and line number.
    /// </summary>
    member _.GeneralTestExpectationFailure (message, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let location = buildLocation fullPath lineNumber
        (
            message |> ExpectationOtherFailure,
            location
        )
        |> TestExpectationFailure
        |> toResult
        
    /// <summary>
    /// Creates a test ignored result with an optional message, file, and line number.
    /// </summary>
    member _.IgnoreFailure (message: string option, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let location = buildLocation fullPath lineNumber
        (
            message,
            location
        )
        |> TestIgnored
        |> toResult
        
    /// <summary>
    /// Creates a test ignored result with a message, file, and line number.
    /// </summary>
    member this.IgnoreFailure (message: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.IgnoreFailure (message |> Some, fullPath, lineNumber)
        
    /// <summary>
    /// Creates a test ignored result with no message, file, and line number.
    /// </summary>
    member this.IgnoreFailure ([<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.IgnoreFailure (None, fullPath, lineNumber)
        
    /// <summary>
    /// Creates a test failure result from an exception.
    /// </summary>
    member _.ExceptionFailure (ex: exn) =
        ex |> TestExceptionFailure |> toResult
    
/// <summary>
/// Builds test result failures and converts them to a result type.
/// </summary>
/// <param name="toResult">A function to convert a <c>TestResult</c> to the desired result type.</param>
type TestResultFailureBuilder<'result> (toResult: TestResult -> 'result) =
    let testFailureBuilder = TestFailureBuilder TestFailure
    /// <summary>
    /// Creates a validation failure result for a test result from expectation info, file, and line number.
    /// </summary>
    member _.ValidationFailure<'expected, 'actual> (expectationInfo: ExpectationInfo<'expected, 'actual>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.ValidationFailure (expectationInfo, fullPath, lineNumber) |> toResult
            
    /// <summary>
    /// Creates a validation failure result for a test result from expected and actual values, file, and line number.
    /// </summary>
    member this.ValidationFailure (expected, actual, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.ValidationFailure (expected, actual, fullPath, lineNumber) |> toResult
            
    /// <summary>
    /// Creates a general test expectation failure result for a test result with a message, file, and line number.
    /// </summary>
    member _.GeneralTestExpectationFailure (message, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.GeneralTestExpectationFailure (message, fullPath, lineNumber) |> toResult
        
    /// <summary>
    /// Creates a test ignored result for a test result with an optional message, file, and line number.
    /// </summary>
    member _.IgnoreFailure (message: string option, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.IgnoreFailure (message, fullPath, lineNumber) |> toResult
        
    /// <summary>
    /// Creates a test ignored result for a test result with a message, file, and line number.
    /// </summary>
    member this.IgnoreFailure (message: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.IgnoreFailure (message, fullPath, lineNumber) |> toResult
        
    /// <summary>
    /// Creates a test ignored result for a test result with no message, file, and line number.
    /// </summary>
    member this.IgnoreFailure ([<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.IgnoreFailure (fullPath, lineNumber) |> toResult
        
    /// <summary>
    /// Creates a test result failure from an exception.
    /// </summary>
    member _.ExceptionFailure (ex: exn) =
        testFailureBuilder.ExceptionFailure ex |> toResult
        
/// <summary>
/// Builds setup/teardown result failures and converts them to a result type.
/// </summary>
/// <param name="toResult">A function to convert a <c>SetupTeardownFailure</c> to the desired result type.</param>
type SetupTeardownResultFailureBuilder<'result> (toResult: SetupTeardownFailure -> 'result) =
    /// <summary>
    /// Creates a setup/teardown failure from an exception.
    /// </summary>
    /// <param name="ex">The exception that caused the failure.</param>
    /// <returns>The result representing a setup/teardown exception failure.</returns>
    member _.ExceptionFailure (ex: exn) =
        ex |> SetupTeardownExceptionFailure |> toResult

    /// <summary>
    /// Creates a setup/teardown canceled failure result.
    /// </summary>
    /// <returns>The result representing a setup/teardown cancel failure.</returns>
    member _.CancelFailure () =
        SetupTeardownCanceledFailure |> toResult

    /// <summary>
    /// Creates a general setup/teardown failure result with a message, file, and line number.
    /// </summary>
    /// <param name="message">The message describing the failure.</param>
    /// <param name="fullPath">The full path to the file where the failure occurred.</param>
    /// <param name="lineNumber">The line number where the failure occurred.</param>
    /// <returns>The result representing a general setup/teardown failure with a message and location.</returns>
    member _.GeneralFailure (message: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let location = buildLocation fullPath lineNumber

        (
            message,
            location
        )
        |> GeneralSetupTeardownFailure
        |> toResult
        
/// <summary>
/// Builds general test failures and converts them to a result type.
/// </summary>
/// <param name="toResult">A function to convert a <c>GeneralTestingFailure</c> to the desired result type.</param>
type GeneralFailureBuilder<'result> (toResult: GeneralTestingFailure -> 'result) =
    /// <summary>
    /// Creates a general failure from an exception.
    /// </summary>
    /// <param name="ex">The exception that caused the failure.</param>
    /// <returns>The result representing a general exception failure.</returns>
    member _.ExceptionFailure (ex: exn) =
        ex |> GeneralExceptionFailure |> toResult

    /// <summary>
    /// Creates a general canceled failure result.
    /// </summary>
    /// <returns>The result representing a general cancel failure.</returns>
    member _.CancelFailure () =
        GeneralCancelFailure |> toResult

    /// <summary>
    /// Creates a general failure result with a message.
    /// </summary>
    /// <param name="message">The message describing the failure.</param>
    /// <returns>The result representing a general failure with a message.</returns>
    member _.GeneralFailure message =
        message |> GeneralFailure |> toResult
        
/// <summary>
/// Provides builders for different types of test execution result failures.
/// </summary>
type TestExecutionResultFailureBuilder () =
    let setupResultFailures = SetupTeardownResultFailureBuilder SetupExecutionFailure
    let testExecutionFailures = TestResultFailureBuilder TestExecutionResult  
    let teardownResultFailures = SetupTeardownResultFailureBuilder TeardownExecutionFailure
    let generalFailures = GeneralFailureBuilder GeneralExecutionFailure
    
    /// <summary>Gets the builder for setup execution failures.</summary>
    member _.SetupExecutionFailure with get () = setupResultFailures
    /// <summary>Gets the builder for test execution result failures.</summary>
    member _.TestExecutionResult with get () = testExecutionFailures
    /// <summary>Gets the builder for teardown execution failures.</summary>
    member _.TeardownExecutionFailure with get () = teardownResultFailures
    /// <summary>Gets the builder for general execution failures.</summary>
    member _.GeneralExecutionFailure with get () = generalFailures
