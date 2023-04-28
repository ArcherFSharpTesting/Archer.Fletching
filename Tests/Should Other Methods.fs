module Archer.Fletching.Tests.``Should Other Methods``

open Archer
open Archer.Arrows

let private feature = Arrow.NewFeature ()

let ``Fail should return a failure with an empty message`` =
    feature.Test (
        fun _ ->
            let expected = failureBuilder.GeneralTestExpectationFailure ("", "C:\\Test.fs", 11)
            let result = Should.Fail ("C:\\Test.fs", 11)
            result
            |> Should.BeEqualTo expected
    )

let ``Fail should return a failure with specified message`` =
    feature.Test (
        fun _ ->
            let expected = failureBuilder.GeneralTestExpectationFailure ("My Message", "C:\\Test.fs", 11)
            let result = Should.Fail (Failure "My Message", "C:\\Test.fs", 11)
            result
            |> Should.BeEqualTo expected
    )
    
let ``BeIgnored should take a value and return an ignored failure`` =
    feature.Test (
        fun _ ->
            let expected = failureBuilder.IgnoreFailure ("P:\\p.pt", 79)
            let result = Should.BeIgnored  ("P:\\p.pt", 79) "Value"
            
            result
            |> Should.BeEqualTo expected
    )
    
let ``BeIgnored should take a value and a message and return an ignored failure with message`` =
    feature.Test (
        fun _ ->
            let expected = failureBuilder.IgnoreFailure ("Why am I ignored", "P:\\p.pt", 79)
            let result = Should.BeIgnored ("Why am I ignored", "P:\\p.pt", 79) "Value"
            
            result
            |> Should.BeEqualTo expected
    )

let ``Test Cases`` = feature.GetTests ()