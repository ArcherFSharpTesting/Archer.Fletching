module Archer.Validations.Tests.Program

open Archer
open Archer.Runner
open Archer.Types.InternalTypes
open Archer.Types.InternalTypes.RunnerTypes
open Archer.Validations.Tests
open Archer.Reporting.Summaries
open MicroLang.Lang

let framework = runnerFactory.Runner ()

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
    ``Should Result Methods``.``Test Cases``
    ``Should Other Methods``.``Test Cases``
    ``Seq Should``.``Test Cases``
    ``List Should``.``Test Cases``
    ``Array Should``.``Test Cases``
    ``Should Meet Standard``.``Test Cases``
]
|> runAndReport