module Archer.Fletching.Tests.``Should Meet Standard``

open Archer
open Archer.Arrows
open Archer.ApprovalsSupport
open Archer.Fletching.Types.Internal
open ApprovalTests

let private feature = Arrow.NewFeature (
    TestTags [
        Category "Should"
        Category "Approvals"
    ],
    Setup (fun _ ->
        [
            Searching
                |> findFirstReporter<Reporters.DiffReporter>
                |> findFirstReporter<Reporters.WinMergeReporter>
                |> findFirstReporter<Reporters.InlineTextReporter>
                |> findFirstReporter<Reporters.AllFailingTestsClipboardReporter>
                |> unWrapReporter

            Reporters.ClipboardReporter() :> Core.IApprovalFailureReporter;

            Reporters.QuietReporter() :> Core.IApprovalFailureReporter;
        ]
        |> buildReporter
        |> Ok
    )
)

let ``Approve a string of a type`` =
    feature.Test (fun reporter environment ->
        let value = { ExpectedValue = 54; ActualValue =  31 }
        let str = $"%A{value}"
        
        str
        |> Should.MeetStandard reporter environment.TestInfo 
    )

let ``Test Cases`` = feature.GetTests ()