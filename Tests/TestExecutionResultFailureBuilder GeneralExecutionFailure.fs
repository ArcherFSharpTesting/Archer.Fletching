module Archer.Validations.Tests.``TestExecutionResultFailureBuilder GeneralExecutionFailure``

open Archer
open Archer.Core
open Archer.Validations.Types.Internal

let private feature = FeatureFactory.NewFeature (
    TestTags [
        Category "TestExecutionResultFailureBuilder"
        Category "GeneralExecutionFailure"
    ],
    Setup (fun _ -> TestExecutionResultFailureBuilder().GeneralExecutionFailure |> Ok)
)

let ``ExceptionFailure should convert an Exception into a failure`` =
    feature.Test (
        fun builder ->
            let ex = System.ArgumentNullException "Your argument means nothing"
            let result = builder.ExceptionFailure ex
            let expected = GeneralExecutionFailure (GeneralExceptionFailure ex)
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``CancelFailure should return a failure`` =
    feature.Test (
        fun builder ->
            let result = builder.CancelFailure ()
            let expected = GeneralExecutionFailure GeneralCancelFailure
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``GeneralFailure should convert a message into a failure`` =
    feature.Test (
        fun builder ->
            let result = builder.GeneralFailure "Some Failure"
            let expected = GeneralExecutionFailure (GeneralFailure "Some Failure")
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )

let ``Test Cases`` = feature.GetTests ()