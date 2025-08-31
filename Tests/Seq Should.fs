module Archer.Validations.Tests.``Seq Should``

open Archer
open Archer.Arrows
open Archer.Validations.Types.Internal

let private feature = Arrow.NewFeature (
    TestTags [
        Category "Should"
        Category "Other"
    ]
)

let ``Contain should return success when searching seq{ "a"; "b"; "c" } for "c"`` =
    feature.Test (fun _ ->
        seq{ "a"; "b"; "c" }
        |> SeqShould.Contain "c"
    )

let ``Contain should return failure when searching seq{ "3"; "4"; "5" } for "1"`` =
    feature.Test (fun _ ->
        let expected  = failureBuilder.ValidationFailure (Contains (["3"; "4"; "5"], "1"), Not (Contains (["3"; "4"; "5"], "1")), "C:\\at.meow", 33)
        let result = 
            seq{ "3"; "4"; "5" }
            |> SeqShould.Contain ("1", "C:\\at.meow", 33)
            
        result
        |> Should.BeEqualTo expected
    )

let ``NotContain should return success when searching seq{ "a"; "b"; "c" } for "1"`` =
    feature.Test (fun _ ->
        seq{ "a"; "b"; "c" }
        |> SeqShould.NotContain "1"
    )

let ``NotContain should return failure when searching seq{ "3"; "4"; "5" } for "4"`` =
    feature.Test (fun _ ->
        let expected  = failureBuilder.ValidationFailure (Not (Contains (["3"; "4"; "5"], "4")), Contains (["3"; "4"; "5"], "4"), "C:\\at.meow", 33)
        let result = 
            seq{ "3"; "4"; "5" }
            |> SeqShould.NotContain ("4", "C:\\at.meow", 33)
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``HaveAllValuesPassTestOf Should return success when searching seq{ 2; 4; 6; 8 } for even numbers`` =
    feature.Test (fun _ ->
        seq{ 2; 4; 6; 8 }
        |> SeqShould.HaveAllValuesPassTestOf <@ fun x -> x % 2 = 0 @>
    )
    
let ``HaveNoValuesPassTestOf should return success when searching seq{ 1; 3; 5; 7 } for even numbers`` =
    feature.Test (fun _ ->
        seq{ 1; 3; 5; 7 }
        |> SeqShould.HaveNoValuesPassTestOf <@ fun x -> x % 2 = 0 @> 
    )
    
let ``HaveNoValuesPassTestOf should return failure when searching seq { 1; 2; 3; 5; 7 } for even numbers`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure (FailsTest "fun x -> x % 2 = 0", PassesTest [1; 2; 3; 5; 7], "C:\\aw.crow", 201)
        let result = 
            seq { 1; 2; 3; 5; 7 }
            |> SeqShould.HaveNoValuesPassTestOf (<@ fun x -> x % 2 = 0 @>, "C:\\aw.crow", 201)
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``HaveLengthOf should return success when comparing the length of seq{ 'a'; 'b'; 'c' } with 3`` =
    feature.Test (fun _ ->
        seq{ 'a'; 'b'; 'c' }
        |> SeqShould.HaveLengthOf 3
    )
    
let ``HaveLengthOf should return failure when comparing the length of seq{ 'a'; 'b'; 'c'; 'd' } with 5`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure (Length 5, ['a'; 'b'; 'c'; 'd'], "H:\\oney.bee", -41)
        let result = 
            seq{ 'a'; 'b'; 'c'; 'd' }
            |> SeqShould.HaveLengthOf (5, "H:\\oney.bee", -41)
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``NotHaveLengthOf should return success when comparing the length of seq{ 'a'; 'b'; 'c' } with 2`` =
    feature.Test (fun _ ->
        seq{ 'a'; 'b'; 'c' }
        |> SeqShould.NotHaveLengthOf 2
    )
    
let ``NotHaveLengthOf should return failure when comparing the length of seq{ 'a'; 'b'; 'c'; 'd' } with 4`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure (Not (Length 4), ['a'; 'b'; 'c'; 'd'], "H:\\oney.bee", -41)
        let result = 
            seq{ 'a'; 'b'; 'c'; 'd' }
            |> SeqShould.NotHaveLengthOf (4, "H:\\oney.bee", -41)
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``HaveAllValuesPassAllOf should return a success when all items pass all tests`` =
    feature.Test (fun _ ->
        seq{
            "Hello"
            "Howdy"
            "Hardy"
        }
        |> SeqShould.HaveAllValuesPassAllOf [
            Seq.length >> Should.BeEqualTo 5
            Seq.head >> Should.BeEqualTo 'H'
            Seq.last >> Should.PassTestOf <@System.Char.IsLower@>
        ]
    )
    
let ``HaveAllValuesPassAllOf should return a failure when one item fails any test`` =
    feature.Test (fun _ ->
        let expected = 3 |> Should.BeEqualTo (5, "A:\\pe.g", 96)
        let result = 
            seq{
                "Hello"
                "How"
                "Hardy"
            }
            |> SeqShould.HaveAllValuesPassAllOf ([
                Seq.length >> Should.BeEqualTo (5, "A:\\pe.g", 96)
                Seq.head >> Should.BeEqualTo ('H', "B:\\ird.c", 97)
                Seq.last >> Should.PassTestOf (<@System.Char.IsLower@>, "C:\\at.p", 98)
            ], "D:\\og.b", 99)
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``HaveAllValuesPassAllOf should return all failure when one item fails multiple tests`` =
    feature.Test (fun _ ->
        let expected =
            TestFailure (
                CombinationFailure (
                    (
                        TestExpectationFailure (
                            ExpectationVerificationFailure {
                                ExpectedValue = 5
                                ActualValue = 3
                            },
                            {
                                FilePath = "A:\\"
                                FileName = "pe.g"
                                LineNumber = 96
                            }
                        ),
                        None
                    ),
                    (
                        TestExpectationFailure (
                            ExpectationVerificationFailure {
                                ExpectedValue = 'H'
                                ActualValue = 'A'
                            },
                            {
                                FilePath = "B:\\"
                                FileName = "ird.c"
                                LineNumber = 97
                            }
                        ),
                        None
                    )
                )
        )
               
        let result = 
            seq{
                "Hello"
                "Are"
                "Hardy"
            }
            |> SeqShould.HaveAllValuesPassAllOf ([
                Seq.length >> Should.BeEqualTo (5, "A:\\pe.g", 96)
                Seq.head >> Should.BeEqualTo ('H', "B:\\ird.c", 97)
                Seq.last >> Should.PassTestOf (<@System.Char.IsLower@>, "C:\\at.p", 98)
            ], "D:\\og.b", 99)
            
        result
        |> Should.BeEqualTo expected
    )

    
let ``HaveAllValuesPassAllOf should return all failures when multiple items fails any test`` =
    feature.Test (fun _ ->
        let expected = TestFailure (
            CombinationFailure (
                (
                    TestExpectationFailure (
                        ExpectationVerificationFailure {
                            ExpectedValue = 5
                            ActualValue = 3
                        },
                        {
                            FilePath = "A:\\"
                            FileName = "pe.g"
                            LineNumber = 96
                        }
                    ),
                    None
                ),
                (
                    TestExpectationFailure (
                        ExpectationVerificationFailure {
                            ExpectedValue = 5
                            ActualValue = 8
                        },
                        {
                            FilePath = "A:\\"
                            FileName = "pe.g"
                            LineNumber = 96
                        }), None
                    )
                )
            )
        
        let result = 
            seq{
                "Hello"
                "How"
                "Howdy-do"
            }
            |> SeqShould.HaveAllValuesPassAllOf ([
                Seq.length >> Should.BeEqualTo (5, "A:\\pe.g", 96)
                Seq.head >> Should.BeEqualTo ('H', "B:\\ird.c", 97)
                Seq.last >> Should.PassTestOf (<@System.Char.IsLower@>, "C:\\at.p", 98)
            ], "D:\\og.b", 99)
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``HaveAllValuesBe should return success when Sequence has seq {'a'; 'a'; 'a'} and looking for 'a'`` =
    feature.Test (fun _ ->
        seq {'a'; 'a'; 'a'}
        |> SeqShould.HaveAllValuesBe 'a'
    )
    
let ``HaveAllValuesBe should return success Sequence has seq {'b'; 'b'; 'b'} and looking for 'b'`` =
    feature.Test (fun _ ->
        let values = seq {'b'; 'b'; 'b'}
        
        values
        |> SeqShould.HaveAllValuesBe 'b'
    )
    
let ``HaveAllValuesBe should return failure when Sequence has seq {'a'; 'a'; 'b'} and looking for 'a'`` =
    feature.Test (fun _ ->
        let values = seq {'a'; 'a'; 'b'}
        
        let expected = failureBuilder.ValidationFailure (HasOnlyValue 'a', values |> List.ofSeq, "D:\\ummy\\fil.e", 29)
        let actual = 
            values
            |> SeqShould.HaveAllValuesBe ('a', "D:\\ummy\\fil.e", 29)
             
        actual
        |> Should.BeEqualTo expected
    )
    
let ``Test Cases`` = feature.GetTests ()