/// <summary>
/// Helper functions and utilities for type-based assertions and test verifications in Archer.
/// </summary>
[<AutoOpen>]
module Archer.ShouldTypeHelpers

open System
open Archer.Fletching.Types.Internal

/// <summary>
/// A default test result failure builder using the identity function.
/// </summary>
let failureBuilder = TestResultFailureBuilder id

/// <summary>
/// Checks if two references of type <c>'a</c> refer to the same object instance.
/// </summary>
/// <param name="expected">The expected reference.</param>
/// <param name="actual">The actual reference.</param>
/// <returns><c>true</c> if both references refer to the same object; otherwise, <c>false</c>.</returns>
let checkReference<'a> (expected: 'a) (actual: 'a) =
    Object.ReferenceEquals (actual, expected)
    
/// <summary>
/// Gets the runtime type of the given value.
/// </summary>
/// <param name="value">The value to get the type of.</param>
/// <returns>The <c>Type</c> of the value.</returns>
let getType value = value.GetType ()

/// <summary>
/// Checks if the actual value is an instance of the expected type.
/// </summary>
/// <typeparam name="'expectedType">The expected type.</typeparam>
/// <param name="actual">The value to check.</param>
/// <returns><c>true</c> if <paramref name="actual"/> is an instance of <typeparamref name="'expectedType"/>; otherwise, <c>false</c>.</returns>
let isInstanceOf<'expectedType> actual =
    let expected = typeof<'expectedType>
    expected.IsInstanceOfType actual

/// <summary>
/// Checks a condition on the actual value and returns <c>TestSuccess</c> if true, otherwise a validation failure.
/// </summary>
/// <param name="fCheck">The function to check the actual value.</param>
/// <param name="fullPath">The full path to the file where the check is performed.</param>
/// <param name="lineNumber">The line number where the check is performed.</param>
/// <param name="expectedModifier">A function to modify the expected value for reporting.</param>
/// <param name="actualModifier">A function to modify the actual value for reporting.</param>
/// <param name="expected">The expected value.</param>
/// <param name="actual">The actual value.</param>
/// <returns><c>TestSuccess</c> if the check passes; otherwise, a validation failure result.</returns>
let check fCheck fullPath lineNumber expectedModifier actualModifier expected actual =
    if actual |> fCheck then
        TestSuccess
    else
        failureBuilder.ValidationFailure (expectedModifier expected, actualModifier actual, fullPath, lineNumber)
        
/// <summary>
/// Adds a failure message to a <c>TestResult</c> if it is a test expectation failure.
/// </summary>
/// <param name="message">The failure message to add.</param>
/// <param name="result">The test result to annotate.</param>
/// <returns>The annotated test result if applicable; otherwise, the original result.</returns>
let withFailureComment (message: string) (result: TestResult) =
    match result with
    | TestFailure (TestExpectationFailure (failure, location)) ->
        (FailureWithMessage (message, failure), location)
        |> TestExpectationFailure
        |> TestFailure
    | _ -> result

/// <summary>
/// Safely executes a function on a value, returning a failure result if an exception is thrown.
/// </summary>
/// <param name="f">The function to execute.</param>
/// <param name="v">The value to pass to the function.</param>
/// <returns>The result of the function, or a failure result if an exception occurs.</returns>
let safeTry f v =
    try
        f v
    with ex ->
        failureBuilder.ExceptionFailure ex