module Archer.Fletching.Tests.Program

open Archer
open Archer.Bow
open Archer.CoreTypes.InternalTypes
open Archer.CoreTypes.InternalTypes.RunnerTypes
open Archer.Fletching.Tests
open Archer.Logger.Summaries
open MicroLang.Lang

let framework = bow.Runner ()

framework.RunnerLifecycleEvent
|> Event.add (fun args ->
    match args with
    | RunnerStartExecution _ ->
        printfn ""
    | RunnerTestLifeCycle (test, testEventLifecycle, _) ->
        match testEventLifecycle with
        | TestEndExecution testExecutionResult ->
            match testExecutionResult with
            | TestExecutionResult TestSuccess -> ()
            | result ->
                let transformedResult = defaultTestExecutionResultSummaryTransformer result test
                printfn $"%s{transformedResult}"
            
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
    ``Array Should``.``Test Cases``
    ``Should Meet Standard``.``Test Cases``
]
|> runAndReport