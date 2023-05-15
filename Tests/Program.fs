module Archer.Fletching.Tests.Program

open Archer
open Archer.Bow
open Archer.CoreTypes.InternalTypes
open Archer.CoreTypes.InternalTypes.RunnerTypes
open Archer.Fletching.Tests
open Archer.Fletching.Tests.RunHelpers

let framework = bow.Runner ()

framework.RunnerLifecycleEvent
|> Event.add (fun args ->
    match args with
    | RunnerStartExecution _ -> ()
    | RunnerTestLifeCycle (test, testEventLifecycle, _) ->
        match testEventLifecycle with
        | TestEndExecution testExecutionResult ->
            let successMsg =
                match testExecutionResult with
                | TestExecutionResult TestSuccess -> "success"
                | _ -> "fail"
                
            printfn $"%A{test}: (%s{successMsg})"
        | _ -> ()
    | RunnerEndExecution ->
        printfn "\n"
)

framework
|> addMany [
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
    ``Seq Should``.``Test Cases``
    ``List Should``.``Test Cases``
]
|> runAndReport