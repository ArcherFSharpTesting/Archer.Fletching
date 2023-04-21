module Archer.Fletching.Tests.SetupTeardownResultFailureBuilder

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

let private feature = Arrow.NewFeature (
    Setup (fun _ -> SetupTeardownResultFailureBuilder (Called) |> Ok)
)

let private failureBuilder = TestResultFailureBuilder id

let ``ExceptionFailure should convert an exception into a failure`` =
    feature.Test (
        fun builder _ ->
            let ex = System.ArgumentOutOfRangeException "I refuse to argue"
            let (Called result) = builder.ExceptionFailure ex
            let expected = SetupTeardownExceptionFailure ex
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``CancelFailure should return a failure`` =
    feature.Test (
        fun builder _ ->
            let (Called result) = builder.CancelFailure ()
            let expected = SetupTeardownCanceledFailure
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``GeneralFailure should convert a message into a failure`` =
    feature.Test (
        fun builder _ ->
            let (Called result) = builder.GeneralFailure ("Why did I fail", "T:\\some\\file.abc", 33)
            let expected = GeneralSetupTeardownFailure ("Why did I fail", { FilePath = "T:\\some"; FileName = "file.abc"; LineNumber = 33 })
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )

let ``Test Cases`` = feature.GetTests ()