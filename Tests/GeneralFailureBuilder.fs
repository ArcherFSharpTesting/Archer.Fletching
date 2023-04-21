module Archer.Fletching.Tests.GeneralFailureBuilder

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

let private feature = Arrow.NewFeature (
    Setup (fun _ -> Called |> GeneralFailureBuilder |> Ok)
)

let ``ExceptionFailure should convert an exception into a failure`` =
    feature.Test (
        fun builder _ ->
            let ex = System.Exception "HA! HA! HA! Batman."
            let (Called result) = builder.ExceptionFailure ex
            let expected = GeneralExceptionFailure ex
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``CancelFailure should return a failure`` =
    feature.Test (
        fun builder _ ->
            let (Called result) = builder.CancelFailure ()
            let expected = GeneralCancelFailure
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``GeneralFailure should convert a message into a failure`` =
    feature.Test (
        fun builder _ ->
            let (Called result) = builder.GeneralFailure "This Fails"
            let expected = GeneralFailure "This Fails"
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )

let ``Test Cases`` = feature.GetTests ()