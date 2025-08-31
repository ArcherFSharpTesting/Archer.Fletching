/// <summary>
/// Provides support utilities for ApprovalTests integration in Archer, including reporter management and helpers.
/// </summary>
module Archer.ApprovalsSupport

open System
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open ApprovalTests.Core
open ApprovalTests.Reporters
open Archer.Validations.GoldenMaster

let private createReporter<'a when 'a:> IApprovalFailureReporter> () =
    System.Activator.CreateInstance<'a> () :> IApprovalFailureReporter
    
type FindReporterResult =
    /// <summary>
    /// Represents the result of searching for an approval reporter.
    /// </summary>
    | FoundReporter of IApprovalFailureReporter
    /// <summary>
    /// Indicates that the search for a reporter is ongoing.
    /// </summary>
    | Searching

/// <summary>
/// Builds a composite reporter from a list of reporters, or returns a quiet reporter if the list is empty.
/// </summary>
/// <param name="reporters">A list of <c>IApprovalFailureReporter</c> instances.</param>
/// <returns>A composite or quiet reporter.</returns>
let buildReporter (reporters: IApprovalFailureReporter List) =
    if  reporters.IsEmpty
    then QuietReporter() :> IApprovalFailureReporter
    else
        MultiReporter(reporters |> List.toSeq) :> IApprovalFailureReporter

/// <summary>
/// Attempts to find the first reporter using a getter function, or returns the existing result.
/// </summary>
/// <param name="getter">A function that returns an <c>IApprovalFailureReporter</c>.</param>
/// <param name="findReporterResult">The current reporter search result.</param>
/// <returns>A <c>FindReporterResult</c> indicating the outcome.</returns>
let findFirstWith (getter: unit -> 'a when 'a :> IApprovalFailureReporter) findReporterResult =
    match findReporterResult with
    | FoundReporter _ -> findReporterResult
    | _ ->
        try
            let reporter = getter ()
            FoundReporter(reporter)
        with
        | _ ->
            Searching

/// <summary>
/// Attempts to find the first reporter of a given type, or returns the existing result.
/// </summary>
/// <typeparam name="'a">The reporter type to search for.</typeparam>
/// <param name="findReporterResult">The current reporter search result.</param>
/// <returns>A <c>FindReporterResult</c> indicating the outcome.</returns>
let findFirstReporter<'a when 'a :> IApprovalFailureReporter> findReporterResult =
    match findReporterResult with
    | FoundReporter _ -> findReporterResult
    | _ ->
        try
            let reporter = createReporter<'a> ()
            FoundReporter(reporter)
        with
        | _ ->
            Searching

/// <summary>
/// Unwraps a <c>FindReporterResult</c> to get the reporter, or returns a quiet reporter if not found.
/// </summary>
/// <param name="findReporterResult">The reporter search result.</param>
/// <returns>An <c>IApprovalFailureReporter</c> instance.</returns>
let unWrapReporter findReporterResult =
    match findReporterResult with
    | FoundReporter reporter -> 
        reporter
    | _ -> createReporter<QuietReporter> ()

type Should with
    /// <summary>
    /// Asserts that a string result meets the golden master standard using the provided reporter.
    /// </summary>
    /// <param name="reporter">The approval failure reporter to use.</param>
    /// <param name="fullPath">The file path where the check is performed (automatically provided).</param>
    /// <param name="lineNumber">The line number where the check is performed (automatically provided).</param>
    /// <returns>A function that takes test info and a string result, and returns a test result.</returns>
    static member MeetStandard (reporter: IApprovalFailureReporter, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (testInfo: ITestInfo) (result: string) =
            let result = result.Replace("\r\n", "\n").Replace("\n", "\r\n")
            let approver = getStringFileApprover testInfo result
            
            approve fullPath lineNumber reporter approver
            
        checkIt