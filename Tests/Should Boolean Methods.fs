module Archer.Validations.Tests.``Should Boolean Methods``

open Archer
open Archer.Arrows
open Archer.Validations.Types.Internal

let private feature = Arrow.NewFeature (
    TestTags [
        Category "Should"
        Category "Boolean"
    ]
)

let ``BeTrue should succeed whe true is passed`` =
    feature.Test (
        fun _ ->
            true
            |> Should.BeTrue
    )
    
let ``BeTrue should fail when false is passed`` =
    feature.Test (
        fun _ ->
            let expected = failureBuilder.ValidationFailure (true, false, "A:\\fake\\path.t", 36)
            let result =
                Should.BeTrue (false, "A:\\fake\\path.t", 36)
                
            result
            |> Should.BeEqualTo expected
    )

let ``BeFalse should succeed whe false is passed`` =
    feature.Test (
        fun _ ->
            false
            |> Should.BeFalse
    )
    
let ``BeFalse should fail when true is passed`` =
    feature.Test (
        fun _ ->
            let expected = failureBuilder.ValidationFailure (false, true, "B:\\fake\\path.t", 38)
            let result =
                Should.BeFalse (true, "B:\\fake\\path.t", 38)
                
            result
            |> Should.BeEqualTo expected
    )

let ``Test Cases`` = feature.GetTests ()