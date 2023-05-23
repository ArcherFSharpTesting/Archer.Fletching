module Archer.ApprovalsSupport

open System
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open ApprovalTests.Core
open ApprovalTests.Reporters
open Archer.Fletching.GoldenMaster

let private createReporter<'a when 'a:> IApprovalFailureReporter> () =
    System.Activator.CreateInstance<'a> () :> IApprovalFailureReporter
    
type FindReporterResult =
    | FoundReporter of IApprovalFailureReporter
    | Searching
    
let buildReporter (reporters: IApprovalFailureReporter List) =
    if  reporters.IsEmpty
    then QuietReporter() :> IApprovalFailureReporter
    else
        MultiReporter(reporters |> List.toSeq) :> IApprovalFailureReporter
    
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

let unWrapReporter findReporterResult =
    match findReporterResult with
    | FoundReporter reporter -> 
        reporter
    | _ -> createReporter<QuietReporter> ()
    
type Should with
    static member MeetStandard (reporter: IApprovalFailureReporter, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let checkIt (testInfo: ITestInfo) (result: string) =
            let result = result.Replace("\r\n", "\n").Replace("\n", "\r\n")
            let approver = getStringFileApprover testInfo result
            
            approve fullPath lineNumber reporter approver
            
        checkIt