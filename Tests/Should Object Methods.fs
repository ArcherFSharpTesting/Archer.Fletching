﻿module Archer.Fletching.Tests.``Should Object Methods``

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

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
    
let ``BeEqualTo should return success if both are equivalent numbers`` =
    feature.Test (
        fun _ ->
            let a = 5
            let b = 5
            
            a
            |> Should.BeEqualTo b
    )
    
let ``BeEqualTo should return success if both are equivalent lists of strings`` =
    feature.Test (
        fun _ ->
            let a = ["Hello"; " world"]
            let b = ["Hello"; " world"]
            
            a
            |> Should.BeEqualTo b
    )
    
let ``NotBeEqualTo should return failure of both objects are the same string`` =
    feature.Test (
        fun _ ->
            let thing = "a thing"
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{Not thing}"; Actual = $"%A{thing}" }), { FilePath = "W:\\"; FileName = "thingTest.tst"; LineNumber = -24 }))
            
            let result = 
                thing
                |> Should.NotBeEqualTo (thing, "W:\\thingTest.tst", -24)
                
            result
            |> Should.BeEqualTo expected
    )
    
let ``NotBeEqualTo should return failure if both items are the same object`` =
    feature.Test (
        fun _ ->
            let thing = obj ()
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{Not thing}"; Actual = $"%A{thing}" }), { FilePath = "X:\\"; FileName = "wingTest.tst"; LineNumber = -86 }))

            let result =
                thing
                |> Should.NotBeEqualTo (thing, "X:\\wingTest.tst", -86)
            
            result
            |> Should.BeEqualTo expected
    )
    
let ``NotBeEqualTo should return success if both are different objects`` =
    feature.Test (
        fun _ ->
            let thing1 = obj ()
            let thing2 = obj ()
            
            thing1
            |> Should.NotBeEqualTo thing2
    )
    
let ``NotBeEqualTo should return success if both lists contain different strings`` =
    feature.Test (
        fun _ ->
            let thing1 = ["Hello"; " world"]
            let thing2 = ["Bye"; " world"]
            
            thing1
            |> Should.NotBeEqualTo thing2
    )

let ``Test Cases`` = feature.GetTests ()