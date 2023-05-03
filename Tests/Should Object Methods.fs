module Archer.Fletching.Tests.``Should Object Methods``

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

let private feature = Arrow.NewFeature ()

// -------------------------------- BeEqualTo --------------------------------
let ``BeEqualTo should return success if both items are the same string`` =
    feature.Test (fun _ ->
        let thing = "a thing"
        thing
        |> Should.BeEqualTo thing
    )
    
let ``BeEqualTo should return success if both items are the same object`` =
    feature.Test (fun _ ->
        let thing = obj ()
        thing
        |> Should.BeEqualTo thing
    )
    
let ``BeEqualTo should return a validation failure if they are different`` =
    feature.Test (fun _ ->
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
    feature.Test (fun _ ->
        let a = 5
        let b = 5
        
        a
        |> Should.BeEqualTo b
    )
    
let ``BeEqualTo should return success if both are equivalent lists of strings`` =
    feature.Test (fun _ ->
        let a = ["Hello"; " world"]
        let b = ["Hello"; " world"]
        
        a
        |> Should.BeEqualTo b
    )
    
// ------------------------------- NotBeEqualTo -------------------------------
let ``NotBeEqualTo should return failure of both objects are the same string`` =
    feature.Test (fun _ ->
        let thing = "a thing"
        let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{Not thing}"; Actual = $"%A{thing}" }), { FilePath = "W:\\"; FileName = "thingTest.tst"; LineNumber = -24 }))
        
        let result = 
            thing
            |> Should.NotBeEqualTo (thing, "W:\\thingTest.tst", -24)
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``NotBeEqualTo should return failure if both items are the same object`` =
    feature.Test (fun _ ->
        let thing = obj ()
        let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{Not thing}"; Actual = $"%A{thing}" }), { FilePath = "X:\\"; FileName = "wingTest.tst"; LineNumber = -86 }))

        let result =
            thing
            |> Should.NotBeEqualTo (thing, "X:\\wingTest.tst", -86)
        
        result
        |> Should.BeEqualTo expected
    )
    
let ``NotBeEqualTo should return success if both are different objects`` =
    feature.Test (fun _ ->
        let thing1 = obj ()
        let thing2 = obj ()
        
        thing1
        |> Should.NotBeEqualTo thing2
    )
    
let ``NotBeEqualTo should return success if both lists contain different strings`` =
    feature.Test (fun _ ->
        let thing1 = ["Hello"; " world"]
        let thing2 = ["Bye"; " world"]
        
        thing1
        |> Should.NotBeEqualTo thing2
    )
    
let ``NotBeEqualTo should return failure if both are equivalent Booleans`` =
    feature.Test (fun _ ->
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
    feature.Test (fun _ ->
        let thing1 = obj ()
        let thing2 = thing1
        
        thing1
        |> Should.BeSameAs thing2
    )
    
let ``BeSameAs should return success of both strings are the same`` =
    feature.Test (fun _ ->
        let thing1 = "A thing like no other"
        
        thing1
        |> Should.BeSameAs thing1
    )
    
let ``BeSameAs should return failure if both are different objects`` =
    feature.Test (fun _ ->
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
    feature.Test (fun _ ->
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
    feature.Test (fun _ ->
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
    feature.Test (fun _ ->
        let thing1 = obj ()
        let thing2 = obj ()
        
        thing1
        |> Should.NotBeSameAs thing2
    )
    
let ``NotBeSameAs should return true if both are different strings`` =
    feature.Test (fun _ ->
        let thing1 = "Hello Thing"
        let thing2 = "Goodbye Hulk"
        
        thing1
        |> Should.NotBeSameAs thing2
    )
    
let ``NotBeSameAs should return failure if both are same object`` =
    feature.Test (fun _ ->
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
    feature.Test (fun _ ->
        let thing1 = (true, false)
        let thing2 = (true, false)
        
        thing1
        |> Should.NotBeSameAs thing2
    )

// --------------------------------- BeOfType ---------------------------------
let ``BeOfType<string> should return success if it is a string`` =
    feature.Test (fun _ ->
        let thing = "A thing"
        
        thing
        |> Should.BeOfType<string>
    )
    
let ``BeOfType<obj> should return success if it is an object`` =
    feature.Test (fun _ ->
        let thing = obj ()
        
        thing
        |> Should.BeOfType<obj>
    )
    
let ``BeOfType<int> should return failure if it is a float`` =
    feature.Test (fun _ ->
        let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{typeof<int>}"; Actual = $"%A{(1.0).GetType ()}" }), { FilePath = "W:\\"; FileName = "thingTest.tst"; LineNumber = -24 }))
        
        let result =
            Should.BeOfType<int> (1.0, "W:\\thingTest.tst", -24)
        
        result
        |> Should.BeEqualTo expected
    )
    
let ``BeOfType<IEnumerable> should return success of it is a string`` =
    feature.Test(fun _ ->
        "Hello"
        |> Should.BeOfType<System.Collections.IEnumerable>
    )

// -------------------------------- NotBeOfType --------------------------------
let ``NotBeOfType<string> should return success if item is an int`` =
    feature.Test (fun _ ->
        1
        |> Should.NotBeTypeOf<string>
    )
    
let ``NotBeOfType<string list> should return success if item is a char list`` =
    feature.Test (fun _ ->
        "hello"
        |> List.ofSeq
        |> Should.NotBeTypeOf<string list>
    )
    
let ``NotBeOfType<int> should return failure if item is 5`` =
    feature.Test (fun _ ->
        let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{Not typeof<int>}"; Actual = $"%A{(5).GetType ()}" }), { FilePath = "W:\\"; FileName = "thingTest.tst"; LineNumber = -24 }))
        
        let result =
            Should.NotBeTypeOf<int> (5, "W:\\thingTest.tst", -24)
            
        result
        |> Should.BeEqualTo expected
    )

let ``NotBeOfType<IEnumerable<char>> should return failure if item is a string`` =
    feature.Test (fun _ ->
        let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{Not typeof<System.Collections.Generic.IEnumerable<char>>}"; Actual = $"%A{typeof<string>}" }), { FilePath = "W:\\"; FileName = "thingTest.tst"; LineNumber = -24 }))
        
        let result =
            Should.NotBeTypeOf<System.Collections.Generic.IEnumerable<char>> ("hello", "W:\\thingTest.tst", -24)
            
        result
        |> Should.BeEqualTo expected
    )

// ---------------------------------- BeNull ----------------------------------
let ``BeNull should return success if item is null`` =
    feature.Test (fun _ ->
        let thing: obj = null
        thing
        |> Should.BeNull
    )
    
let ``BeNull should return success if string is null`` =
    feature.Test (fun _ ->
        let message: string = null
        message
        |> Should.BeNull
    )
    
let ``BeNull should return a failure if item is an instantiated object`` =
    feature.Test (fun _ ->
        let thing = obj ()
        
        let expected = failureBuilder.ValidationFailure (null, thing, "B:\\oy.txt", 35)
        let result = Should.BeNull (thing, "B:\\oy.txt", 35)
        
        result
        |> Should.BeEqualTo expected
    )

// --------------------------------- NotBeNull ---------------------------------
let ``NotBeNull Should return Success if object is not null`` =
    feature.Test (fun _ ->
        let thing = obj ()
        
        thing
        |> Should.NotBeNull
    )
    
let ``NotBeNull Should return failure if object is null`` =
    feature.Test (fun _ ->
        let thing: obj = null
        
        let expected = failureBuilder.ValidationFailure ({ ExpectedValue = Not null; ActualValue = null; }, "F:\\ull\\path.com", 58)
        
        let result = Should.NotBeNull (thing, "F:\\ull\\path.com", 58)
        
        result
        |> Should.BeEqualTo expected
    )

// -------------------------------- BeDefaultOf --------------------------------
let ``BeDefaultOf<string> should return success for null`` =
    feature.Test (fun _ ->
        null
        |> Should.BeDefaultOf<string>
    )
    
let ``BeDefaultOf<int> should return success for Unchecked.defaultOf<int>`` =
    feature.Test (fun _ ->
        Unchecked.defaultof<int>
        |> Should.BeDefaultOf<int>
    )
    
let ``BeDefaultOf<int> should return failure for 100`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure (Unchecked.defaultof<int>, 100, "F:\\ull\\Path.p", 16)
        let result = Should.BeDefaultOf<int> (100, "F:\\ull\\Path.p", 16)
        
        result
        |> Should.BeEqualTo expected
    )


// ------------------------------ NotBeDefaultOf ------------------------------
let ``BeDefaultOf<string> should return failure for null`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure(Not null, null, "F:\\ull\\Path.io", 98)
        let result = Should.NotBeDefaultOf<string> (null, "F:\\ull\Path.io", 98)
        
        result
        |> Should.BeEqualTo expected
    )
    
let ``BeDefaultOf<int> should return Failure for Unchecked.defaultOf<int>`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure (Not Unchecked.defaultof<int>, Unchecked.defaultof<int>, "F:\\ull\\Path.c", 41)
        let result = Should.NotBeDefaultOf<int> (Unchecked.defaultof<int>, "F:\\ull\\Path.c", 41)
        
        result
        |> Should.BeEqualTo expected
    )
    
let ``BeDefaultOf<int> should return success for 100`` =
    feature.Test (fun _ ->
        Should.NotBeDefaultOf<int> 100
    )
    
// -------------------------------- PassTestOf --------------------------------
let ``PassTestOf should return successful if predicate returns true`` =
    feature.Test (fun _ ->
        "Hello"
        |> Should.PassTestOf (fun _ -> true)
    )
    
let ``PassTestOf should pass the given item to the predicate`` =
    feature.Test (fun _ ->
        let thing = obj ()
        let mutable testResult = failureBuilder.ValidationFailure ("To be changed", "Was not changed")
        
        thing
        |> Should.PassTestOf (fun value ->
            testResult <-
                value
                |> Should.BeSameAs thing
            true
        )
        |> ignore
        
        testResult
    )
    
let ``PassTestOf should fail if the item does not pass the test`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure (PassesTest 5, FailsTest 5, "G:\\ood\\test.c", 28)
        let result =
            5
            |> Should.PassTestOf ((fun _ -> false), "G:\\ood\\test.c", 28)
            
        result
        |> Should.BeEqualTo expected
    )
    
// ------------------------------- NotPassTestOf -------------------------------
let ``NotPassTestOf should pass if item does not satisfy the predicate`` =
    feature.Test (fun _ ->
        42
        |> Should.NotPassTestOf (fun _ -> false)
    )
    
let ``NotPassTestOf should pass the item to the predicate`` =
    feature.Test (fun _ ->
        let mutable testResult = failureBuilder.ValidationFailure ("To have changed", "Did not change")
        let thing = "A good thing"
        
        thing
        |> Should.NotPassTestOf (fun value ->
            testResult <-
                value
                |> Should.BeSameAs thing
            true
        )
        |> ignore
            
        testResult
    )
    
let ``NotPassTestOf should fail if item satisfies predicate`` =
    feature.Test (fun _ ->
        let thing = obj ()
        let expected = failureBuilder.ValidationFailure ((FailsTest thing), (PassesTest thing), "G:\\ood\\thin.gs", 77)
        let result =
            thing
            |> Should.NotPassTestOf ((fun _ -> true), "G:\\ood\\thin.gs", 77)
            
        result
        |> Should.BeEqualTo expected
    )
    
// -------------------------------- PassAllOf --------------------------------
let ``PassAllOf should succeed when all tests return TestSuccess`` =
    feature.Test (fun _ ->
        let original =  "hello"
        
        original
        |> Should.PassAllOf [
            fun item -> item |> Seq.head |> Should.BeEqualTo 'h'
            fun item -> item.ToLower () |> Should.BeEqualTo original
            fun item -> item |> Should.BeEqualTo original
        ]
    )
    
let ``PassAllOf should return all the failures`` =
    feature.Test (fun _ ->
        let original = "Bye"
        
        let expected =
            let (TestFailure a) = failureBuilder.ValidationFailure (original, original.ToUpper (), "C:\\cccc.c", 698)
            let (TestFailure b) = failureBuilder.ValidationFailure ('H', 'B', "T:\\tt.t", 45)
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        
        let result =
            original
            |> Should.PassAllOf [
                fun item -> item.ToUpper () |> Should.BeEqualTo (original, "C:\\cccc.c", 698)
                fun item -> item |> Seq.head |> Should.BeEqualTo ('H', "T:\\tt.t", 45)
            ]
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``PassAllOf should return all the failures even if fist succeed`` =
    feature.Test (fun _ ->
        let original = "Bye"
        
        let expected =
            let (TestFailure a) = failureBuilder.ValidationFailure (original, original.ToUpper (), "C:\\cccc.c", 698)
            let (TestFailure b) = failureBuilder.ValidationFailure ('H', 'B', "T:\\tt.t", 45)
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        
        let result =
            original
            |> Should.PassAllOf [
                fun _ -> TestSuccess
                fun item -> item.ToUpper () |> Should.BeEqualTo (original, "C:\\cccc.c", 698)
                fun item -> item |> Seq.head |> Should.BeEqualTo ('H', "T:\\tt.t", 45)
            ]
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``PassAllOf should return all the failures even if middle succeed`` =
    feature.Test (fun _ ->
        let original = "Bye"
        
        let expected =
            let (TestFailure a) = failureBuilder.ValidationFailure (original, original.ToUpper (), "C:\\cccc.c", 698)
            let (TestFailure b) = failureBuilder.ValidationFailure ('H', 'B', "T:\\tt.t", 45)
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        
        let result =
            original
            |> Should.PassAllOf [
                fun item -> item.ToUpper () |> Should.BeEqualTo (original, "C:\\cccc.c", 698)
                fun _ -> TestSuccess
                fun item -> item |> Seq.head |> Should.BeEqualTo ('H', "T:\\tt.t", 45)
            ]
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``PassAllOf should return all the failures even if middle last`` =
    feature.Test (fun _ ->
        let original = "Bye"
        
        let expected =
            let (TestFailure a) = failureBuilder.ValidationFailure (original, original.ToUpper (), "C:\\cccc.c", 698)
            let (TestFailure b) = failureBuilder.ValidationFailure ('H', 'B', "T:\\tt.t", 45)
            CombinationFailure ((a, None), (b, None)) |> TestFailure
        
        let result =
            original
            |> Should.PassAllOf [
                fun item -> item.ToUpper () |> Should.BeEqualTo (original, "C:\\cccc.c", 698)
                fun item -> item |> Seq.head |> Should.BeEqualTo ('H', "T:\\tt.t", 45)
                fun _ -> TestSuccess
            ]
            
        result
        |> Should.BeEqualTo expected
    )

let ``Test Cases`` = feature.GetTests ()