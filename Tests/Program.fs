module Archer.Fletching.Tests.Program

open Archer.Bow
open Archer.Fletching.Tests
open Archer.Fletching.Tests.RunHelpers
open Archer.Fletching.Types.Internal

printfn "Hello from F#"

let framework = bow.Framework ()

framework
|> addManyTests [
    TestResultFailureBuilder.``Test Cases``
    SetupTeardownResultFailureBuilder.``Test Cases``
]
|> runAndReport