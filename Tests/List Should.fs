module Archer.Fletching.Tests.``List Should``

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

let private feature = Arrow.NewFeature (
    TestTags [
        Category "Should"
        Category "Other"
    ]
)

let ``Contain should return success when searching ["a"; "b"; "c"] for "c"`` =
    feature.Test (fun _ ->
        ["a"; "b"; "c"]
        |> ListShould.Contain "c"
    )

let ``Contain should return failure when searching ["a"; "b"; "c"] for "1"`` =
    feature.Test (fun _ ->
        let expected  = failureBuilder.ValidationFailure (Contains (["a"; "b"; "c"], "1"), Not (Contains (["a"; "b"; "c"], "1")), "C:\\at.meow", 33)
        let result = 
            ["a"; "b"; "c"]
            |> ListShould.Contain ("1", "C:\\at.meow", 33)
            
        result
        |> Should.BeEqualTo expected
    )

let ``NotContain should return success when searching ["a"; "b"; "c"] for "1"`` =
    feature.Test (fun _ ->
        ["a"; "b"; "c"]
        |> ListShould.NotContain "1"
    )

let ``NotContain should return failure when searching ["3"; "4"; "5"] for "4"`` =
    feature.Test (fun _ ->
        let expected  = failureBuilder.ValidationFailure (Not (Contains (["3"; "4"; "5"], "4")), Contains (["3"; "4"; "5"], "4"), "C:\\at.meow", 33)
        let result = 
            ["3"; "4"; "5"]
            |> ListShould.NotContain ("4", "C:\\at.meow", 33)
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``FindAllValuesWith Should return success when searching [2; 4; 6; 8] for even numbers`` =
    feature.Test (fun _ ->
        [2; 4; 6; 8]
        |> ListShould.FindAllValuesWith <@ fun x -> x % 2 = 0 @>
    )
    
let ``FindAllValuesWith Should return failure when searching [1; 2; 3; 4] for even numbers`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure (PassesTest "fun x -> x % 2 = 0", FailsTest [1; 2; 3; 4], "M:\\oo.cow", -78)
        let result =
            [1; 2; 3; 4]
            |> ListShould.FindAllValuesWith (<@ fun x -> x % 2 = 0 @>, "M:\\oo.cow", -78)
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``FindNoValuesWith should return success when searching [1; 3; 5; 7] for even numbers`` =
    feature.Test (fun _ ->
        [1; 3; 5; 7]
        |> ListShould.FindNoValuesWith <@ fun x -> x % 2 = 0 @> 
    )
    
let ``FindNoValuesWith should return failure when searching [1; 2; 3; 5; 7] for even numbers`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure (FailsTest "fun x -> x % 2 = 0", PassesTest [1; 2; 3; 5; 7], "C:\\aw.crow", 201)
        let result = 
            [1; 2; 3; 5; 7]
            |> ListShould.FindNoValuesWith (<@ fun x -> x % 2 = 0 @>, "C:\\aw.crow", 201)
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``HaveLengthOf should return success when comparing the length of ['a'; 'b'; 'c'] with 3`` =
    feature.Test (fun _ ->
        ['a'; 'b'; 'c']
        |> ListShould.HaveLengthOf 3
    )
    
let ``HaveLengthOf should return failure when comparing the length of ['a'; 'b'; 'c'; 'd'] with 5`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure (Length 5, ['a'; 'b'; 'c'; 'd'], "H:\\oney.bee", -41)
        let result = 
            ['a'; 'b'; 'c'; 'd']
            |> ListShould.HaveLengthOf (5, "H:\\oney.bee", -41)
            
        result
        |> Should.BeEqualTo expected
    )
    
let ``NotHaveLengthOf should return success when comparing the length of ['a'; 'b'; 'c'] with 2`` =
    feature.Test (fun _ ->
        ['a'; 'b'; 'c']
        |> ListShould.NotHaveLengthOf 2
    )
    
let ``NotHaveLengthOf should return failure when comparing the length of ['a'; 'b'; 'c'; 'd'] with 4`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure (Not (Length 4), ['a'; 'b'; 'c'; 'd'], "H:\\oney.bee", -41)
        let result = 
            ['a'; 'b'; 'c'; 'd']
            |> ListShould.NotHaveLengthOf (4, "H:\\oney.bee", -41)
            
        result
        |> Should.BeEqualTo expected
    )

let ``Test Cases`` = feature.GetTests ()