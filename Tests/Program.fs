module Archer.Fletching.Tests.Program

open Archer.Bow
open Archer.Fletching.Tests
open Archer.Fletching.Tests.RunHelpers

let framework = bow.Framework ()

framework
|> addManyTests [
    TestResultFailureBuilder.``Test Cases``
    SetupTeardownResultFailureBuilder.``Test Cases``
    GeneralFailureBuilder.``Test Cases``
    ``TestExecutionResultFailureBuilder SetupExecutionFailure``.``Test Cases``
    ``TestExecutionResultFailureBuilder TestExecutionResult``.``Test Cases``
    ``TestExecutionResultFailureBuilder TeardownExecutionFailure``.``Test Cases``
    ``TestExecutionResultFailureBuilder GeneralExecutionFailure``.``Test Cases``
    ``Should Object Methods``.``Test Cases``
]
|> runAndReport