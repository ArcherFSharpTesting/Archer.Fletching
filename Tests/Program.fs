module Archer.Fletching.Tests.Program

open Archer.Bow
open Archer.Fletching.Tests
open Archer.Fletching.Tests.RunHelpers

printfn "Hello from F#"

let framework = bow.Framework ()

framework
|> addManyTests [
    TestResultFailureBuilder.``Test Cases``
    SetupTeardownResultFailureBuilder.``Test Cases``
    GeneralFailureBuilder.``Test Cases``
    ``TestExecutionResultFailureBuilder SetupExecutionFailure``.``Test Cases``
    ``TestExecutionResultFailureBuilder TestExecutionResult``.``Test Cases``
    ``TestExecutionResultFailureBuilder TeardownExecutionFailure``.``Test Cases``
]
|> runAndReport