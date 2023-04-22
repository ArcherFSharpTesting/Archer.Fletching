module Archer.Fletching.Tests.``Should Object Methods``

open Archer
open Archer.Arrows

let private feature = Arrow.NewFeature ()

let ``BeEqualTo should return success if both items are the same string`` =
    feature.Test (
        fun _ ->
            let thing = "a thing"
            thing
            |> Should.BeEqualTo thing
    )
    
let ``BeEqualTo should return success if both items are the same object`` =
    feature.Test (
        fun _ ->
            let thing = obj ()
            thing
            |> Should.BeEqualTo thing
    )
    
let ``BeEqualTo should return a validation failure if they are different`` =
    feature.Test (
        fun _ ->
            let thing1 = obj ()
            let thing2 = obj ()
            
            let result = thing1 |> Should.BeEqualTo (thing2, "W:\\thingTest.tst", -24)
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{thing1}"; Actual = $"%A{thing2}" }), { FilePath = "W:\\"; FileName = "thingTest.tst"; LineNumber = -24 }))
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )

let ``Test Cases`` = feature.GetTests ()