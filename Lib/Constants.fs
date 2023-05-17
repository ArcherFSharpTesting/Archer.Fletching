[<AutoOpen>]
module Archer.Constants

open Archer.Fletching.Types.Internal

let inline NotImplemented () =
    let failureBuilder = TestResultFailureBuilder id
    failureBuilder.IgnoreFailure (message = "Not Yet Implemented")
let inline ``Not Implemented`` () =
    let failureBuilder = TestResultFailureBuilder id
    failureBuilder.IgnoreFailure (message = "Not Yet Implemented")