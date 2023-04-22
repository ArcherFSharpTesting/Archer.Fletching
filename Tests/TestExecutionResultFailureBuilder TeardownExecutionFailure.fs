module Archer.Fletching.Tests.``TestExecutionResultFailureBuilder TeardownExecutionFailure``

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

let private feature = Arrow.NewFeature (
    Setup (fun _ -> TestExecutionResultFailureBuilder().TeardownExecutionFailure |> Ok)
)

let ``ExceptionFailure should convert exception to failure`` =
    feature.Test (
        fun builder _ ->
            let ex = System.OverflowException "So many exception tests"
            let result = builder.ExceptionFailure ex
            let expected = TeardownExecutionFailure (SetupTeardownExceptionFailure ex)
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``CancelFailure should return a failure`` =
    feature.Test (
        fun builder _ ->
            let result = builder.CancelFailure ()
            let expected = TeardownExecutionFailure SetupTeardownCanceledFailure
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``GeneralFailure should convert message to failure`` =
    feature.Test (
        fun builder _ ->
            let result = builder.GeneralFailure ("teardown failed", "Y:\\aho.gif", 75)
            let expected = TeardownExecutionFailure (GeneralSetupTeardownFailure ("teardown failed", { FilePath = "Y:\\"; FileName = "aho.gif"; LineNumber = 75 }))
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )

let ``Test Cases`` = feature.GetTests ()