module Archer.Fletching.Tests.Program

open Archer
open Archer.Bow
open Archer.CoreTypes.InternalTypes
open Archer.CoreTypes.InternalTypes.FrameworkTypes
open Archer.Fletching.Tests
open Archer.Fletching.Tests.RunHelpers

let framework = bow.Framework ()

framework.FrameworkLifecycleEvent
|> Event.add (fun args ->
    match args with
    | FrameworkStartExecution _ -> ()
    | FrameworkTestLifeCycle (test, testEventLifecycle, _) ->
        match testEventLifecycle with
        | TestEndExecution testExecutionResult ->
            let successMsg =
                match testExecutionResult with
                | TestExecutionResult TestSuccess -> "success"
                | _ -> "fail"
                
            printfn $"%A{test}: (%s{successMsg})"
        | _ -> ()
    | FrameworkEndExecution ->
        printfn "\n"
)

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
    ``Should Boolean Methods``.``Test Cases``
    ``Should Other Methods``.``Test Cases``
]
|> runAndReport