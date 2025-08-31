module Archer.Validations.Tests.SetupTeardownResultFailureBuilder

open Archer
open Archer.Core
open Archer.Validations.Types.Internal

let private feature = FeatureFactory.NewFeature (
    TestTags [
        Category "SetupTeardownResultFailureBuilder"
    ],
    Setup (fun _ -> Called |> SetupTeardownResultFailureBuilder |> Ok)
)

let ``ExceptionFailure should convert an exception into a failure`` =
    feature.Test (
        fun builder ->
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
        fun builder ->
            let (Called result) = builder.CancelFailure ()
            let expected = SetupTeardownCanceledFailure
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``GeneralFailure should convert a message into a failure`` =
    feature.Test (
        fun builder ->
            let (Called result) = builder.GeneralFailure ("Why did I fail", "T:\\some\\file.abc", 33)
            let expected = GeneralSetupTeardownFailure ("Why did I fail", { FilePath = "T:\\some"; FileName = "file.abc"; LineNumber = 33 })
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )

let ``Test Cases`` = feature.GetTests ()