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

let ``Test Cases`` = feature.GetTests ()