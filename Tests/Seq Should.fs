module Archer.Fletching.Tests.``Seq Should``

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

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
    
let ``FindAllValuesWith Should return success when searching seq{ 2; 4; 6; 8 } for even numbers`` =
    feature.Test (fun _ ->
        seq{ 2; 4; 6; 8 }
        |> SeqShould.FindAllValuesWith <@ fun x -> x % 2 = 0 @>
    )
    
let ``FindNoValuesWith should return success when searching seq{ 1; 3; 5; 7 } for even numbers`` =
    feature.Test (fun _ ->
        seq{ 1; 3; 5; 7 }
        |> SeqShould.FindNoValuesWith <@ fun x -> x % 2 = 0 @> 
    )
    
let ``FindNoValuesWith should return failure when searching seq { 1; 2; 3; 5; 7 } for even numbers`` =
    feature.Test (fun _ ->
        let expected = failureBuilder.ValidationFailure (FailsTest "fun x -> x % 2 = 0", PassesTest [1; 2; 3; 5; 7], "C:\\aw.crow", 201)
        let result = 
            seq { 1; 2; 3; 5; 7 }
            |> SeqShould.FindNoValuesWith (<@ fun x -> x % 2 = 0 @>, "C:\\aw.crow", 201)
            
        result
        |> Should.BeEqualTo expected
    )

let ``Test Cases`` = feature.GetTests ()