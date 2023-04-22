module Archer.Fletching.Tests.``Should Object Methods``

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

let private feature = Arrow.NewFeature ()

// -------------------------------- BeEqualTo --------------------------------
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
    
// ------------------------------- NotBeEqualTo -------------------------------
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
    
let ``NotBeEqualTo should return failure if both are equivalent Booleans`` =
    feature.Test (
        fun _ ->
            let thing1 = true
            let thing2 = true
            
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{Not thing1}"; Actual = $"%A{thing2}" }), { FilePath = "R:\\"; FileName = "ringTest.tst"; LineNumber = 100 }))
            let result =
                thing1
                |> Should.NotBeEqualTo (thing2, "R:\\ringTest.tst", 100)
                
            result
            |> Should.BeEqualTo expected
    )

// --------------------------------- BeSameAs =--------------------------------
let ``BeSameAs should return success if both objects are the same`` =
    feature.Test (
        fun _ ->
            let thing1 = obj ()
            let thing2 = thing1
            
            thing1
            |> Should.BeSameAs thing2
    )
    
let ``BeSameAs should return success of both strings are the same`` =
    feature.Test (
        fun _ ->
            let thing1 = "A thing like no other"
            
            thing1
            |> Should.BeSameAs thing1
    )
    
let ``BeSameAs should return failure if both are different objects`` =
    feature.Test (
        fun _ ->
            let thing1 = obj()
            let thing2 = obj()
            
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{ReferenceOf thing2}"; Actual = $"%A{thing1}" }), { FilePath = "W:\\"; FileName = "thingTest.tst"; LineNumber = -24 }))
            
            let result =
                thing1
                |> Should.BeSameAs (thing2, "W:\\thingTest.tst", -24)
                
            result
            |> Should.BeEqualTo expected
    )
    
let ``BeSameAs should return failure if both are different strings`` =
    feature.Test (
        fun _ ->
            let thing1 = "Hello"
            let thing2 = "Good Bye"
            
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{ReferenceOf thing2}"; Actual = $"%A{thing1}" }), { FilePath = "W:\\"; FileName = "thingTest.tst"; LineNumber = -24 }))
            
            let result =
                thing1
                |> Should.BeSameAs (thing2, "W:\\thingTest.tst", -24)
                
            result
            |> Should.BeEqualTo expected
    )
    
let ``BeSameAs should return failure if both are equivalent Booleans`` =
    feature.Test (
        fun _ ->
            let thing1 = 5
            let thing2 = 5
            
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{ReferenceOf thing2}"; Actual = $"%A{thing1}" }), { FilePath = "W:\\"; FileName = "thingTest.tst"; LineNumber = -24 }))
            
            let result =
                thing1
                |> Should.BeSameAs (thing2, "W:\\thingTest.tst", -24)
                
            result
            |> Should.BeEqualTo expected
    )

// -------------------------------- NotBeSameAs --------------------------------
let ``NotBeSameAs should return success if both are different`` =
    feature.Test (
        fun _ ->
            let thing1 = obj ()
            let thing2 = obj ()
            
            thing1
            |> Should.NotBeSameAs thing2
    )
    
let ``NotBeSameAs should return true if both are different strings`` =
    feature.Test (
        fun _ ->
            let thing1 = "Hello Thing"
            let thing2 = "Goodbye Hulk"
            
            thing1
            |> Should.NotBeSameAs thing2
    )
    
let ``NotBeSameAs should return failure if both are same object`` =
    feature.Test (
        fun _ ->
            let thing1 = obj ()
            let thing2 = thing1
            
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{Not (ReferenceOf thing2)}"; Actual = $"%A{thing1}" }), { FilePath = "W:\\"; FileName = "thingTest.tst"; LineNumber = -24 }))
            
            let result =
                thing1
                |> Should.NotBeSameAs (thing2, "W:\\thingTest.tst", -24)
                
            result
            |> Should.BeEqualTo expected
    )
    
let ``NotBeSameAs should return success both are equivalent Boolean Tuples`` =
    feature.Test (
        fun _ ->
            let thing1 = (true, false)
            let thing2 = (true, false)
            
            thing1
            |> Should.NotBeSameAs thing2
    )

// --------------------------------- BeOfType ---------------------------------
let ``BeOfType<string> return success if it is a string`` =
    feature.Test (
        fun _ ->
            let thing = "A thing"
            
            thing
            |> Should.BeOfType<string>
    )
    
let ``BeOfType<obj> return success if it is an object`` =
    feature.Test (
        fun _ ->
            let thing = obj ()
            
            thing
            |> Should.BeOfType<obj>
    )
    
let ``BeOfType<int> return failure if it is a float`` =
    feature.Test (
        fun _ ->
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{typeof<int>}"; Actual = $"%A{(1.0).GetType ()}" }), { FilePath = "W:\\"; FileName = "thingTest.tst"; LineNumber = -24 }))
            
            let result =
                Should.BeOfType<int> (1.0, "W:\\thingTest.tst", -24)
            
            result
            |> Should.BeEqualTo expected
    )

let ``Test Cases`` = feature.GetTests ()