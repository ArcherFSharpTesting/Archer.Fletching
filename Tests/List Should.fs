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

let ``Contain should return success when searching seq{ "a"; "b"; "c" } for "c"`` =
    feature.Test (fun _ ->
        ["a"; "b"; "c"]
        |> ListShould.Contain "c"
    )

let ``Contain should return failure when searching seq{ "a"; "b"; "c" } for "1"`` =
    feature.Test (fun _ ->
        let expected  = failureBuilder.ValidationFailure (Contains (["a"; "b"; "c"], "1"), Not (Contains (["a"; "b"; "c"], "1")), "C:\\at.meow", 33)
        let result = 
            ["a"; "b"; "c"]
            |> ListShould.Contain ("1", "C:\\at.meow", 33)
            
        result
        |> Should.BeEqualTo expected
    )

let ``NotContain should return success when searching seq{ "a"; "b"; "c" } for "1"`` =
    feature.Test (fun _ ->
        ["a"; "b"; "c"]
        |> ListShould.NotContain "1"
    )

let ``NotContain should return failure when searching seq{ "3"; "4"; "5" } for "4"`` =
    feature.Test (fun _ ->
        let expected  = failureBuilder.ValidationFailure (Not (Contains (["3"; "4"; "5"], "4")), Contains (["3"; "4"; "5"], "4"), "C:\\at.meow", 33)
        let result = 
            ["3"; "4"; "5"]
            |> ListShould.NotContain ("4", "C:\\at.meow", 33)
            
        result
        |> Should.BeEqualTo expected
    )

let ``Test Cases`` = feature.GetTests ()