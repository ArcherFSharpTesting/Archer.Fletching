module Archer.ApprovalsSupport

open ApprovalTests.Core
open ApprovalTests.Reporters

let private createReporter<'a when 'a:> IApprovalFailureReporter> () =
    System.Activator.CreateInstance<'a> () :> IApprovalFailureReporter
    
type FindReporterResult =
    | FoundReporter of IApprovalFailureReporter
    | Searching
    
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
    static member MeetStandard () = ()