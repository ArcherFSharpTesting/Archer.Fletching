module Archer.Fletching.Tests.``TestExecutionResultFailureBuilder SetupExecutionFailure``

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

let private feature = Arrow.NewFeature (
    Setup (fun _ -> TestExecutionResultFailureBuilder().SetupExecutionFailure |> Ok)
)

let ``ExceptionFailure should convert an exception into a Failure`` =
    feature.Test (
        fun builder _ ->
            let ex = System.ApplicationException "Sure why not"
            let result = builder.ExceptionFailure ex
            let expected = SetupExecutionFailure (SetupTeardownExceptionFailure ex)
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``CancelFailure should return a failure`` =
    feature.Test (
        fun builder _ ->
            let result = builder.CancelFailure ()
            let expected = SetupExecutionFailure SetupTeardownCanceledFailure
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``GeneralFailure should convert a message into a failure`` =
    feature.Test (
        fun builder _ ->
            let result = builder.GeneralFailure ("Yep I am a failure", "B:\\or\\not\\toBe.question", 20)
            let expected = SetupExecutionFailure (GeneralSetupTeardownFailure ("Yep I am a failure", { FilePath = "B:\\or\\not"; FileName = "toBe.question"; LineNumber = 20 }))
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )

let ``Test Cases`` = feature.GetTests ()