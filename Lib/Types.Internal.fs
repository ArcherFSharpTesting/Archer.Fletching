module Archer.Fletching.Types.Internal

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer

type Modifiers<'a> =
    | Not of 'a
    | ReferenceOf of 'a
    | PassesTest of 'a
    | FailsTest of 'a
    | Contains of 'a list * 'a
    | Length of 'a

let buildLocation fullFileName lineNumber =
    let info = System.IO.FileInfo fullFileName
    let path = info.Directory.FullName
    let fileName = info.Name
    
    {
        FilePath = path
        FileName = fileName
        LineNumber = lineNumber 
    }

type ExpectationInfo<'expected, 'actual> =
    {
        ExpectedValue: 'expected
        ActualValue: 'actual
    }
    interface IVerificationInfo with
        member this.Expected with get () = $"%A{this.ExpectedValue}"
        member this.Actual with get () = $"%A{this.ActualValue}"
        

let toVerificationInfo (expectation: ExpectationInfo<'expected, 'actual>) =
    expectation :> IVerificationInfo
    
type TestFailureBuilder<'result> (toResult: TestFailure -> 'result) =
    member _.ValidationFailure<'expected, 'actual> (expectationInfo: ExpectationInfo<'expected, 'actual>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let failure =
            expectationInfo
            |> toVerificationInfo
            |> ExpectationVerificationFailure
            
        let location = buildLocation fullPath lineNumber
        
        (
            failure,
            location
        )
        |> TestExpectationFailure
        |> toResult
            
    member this.ValidationFailure (expected, actual, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.ValidationFailure ({ ExpectedValue = expected; ActualValue = actual }, fullPath, lineNumber)
            
    member _.GeneralTestExpectationFailure (message, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let location = buildLocation fullPath lineNumber
        (
            message |> ExpectationOtherFailure,
            location
        )
        |> TestExpectationFailure
        |> toResult
        
    member _.IgnoreFailure (message: string option, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let location = buildLocation fullPath lineNumber
        (
            message,
            location
        )
        |> TestIgnored
        |> toResult
        
    member this.IgnoreFailure (message: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.IgnoreFailure (message |> Some, fullPath, lineNumber)
        
    member this.IgnoreFailure ([<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.IgnoreFailure (None, fullPath, lineNumber)
        
    member _.ExceptionFailure (ex: exn) =
        ex |> TestExceptionFailure |> toResult
    
type TestResultFailureBuilder<'result> (toResult: TestResult -> 'result) =
    let testFailureBuilder = TestFailureBuilder TestFailure
    member _.ValidationFailure<'expected, 'actual> (expectationInfo: ExpectationInfo<'expected, 'actual>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.ValidationFailure (expectationInfo, fullPath, lineNumber) |> toResult
            
    member this.ValidationFailure (expected, actual, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.ValidationFailure (expected, actual, fullPath, lineNumber) |> toResult
            
    member _.GeneralTestExpectationFailure (message, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.GeneralTestExpectationFailure (message, fullPath, lineNumber) |> toResult
        
    member _.IgnoreFailure (message: string option, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.IgnoreFailure (message, fullPath, lineNumber) |> toResult
        
    member this.IgnoreFailure (message: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.IgnoreFailure (message, fullPath, lineNumber) |> toResult
        
    member this.IgnoreFailure ([<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        testFailureBuilder.IgnoreFailure (fullPath, lineNumber) |> toResult
        
    member _.ExceptionFailure (ex: exn) =
        testFailureBuilder.ExceptionFailure ex |> toResult
        
type SetupTeardownResultFailureBuilder<'result> (toResult: SetupTeardownFailure -> 'result) =
    member _.ExceptionFailure (ex: exn) =
        ex |> SetupTeardownExceptionFailure |> toResult
        
    member _.CancelFailure () =
        SetupTeardownCanceledFailure |> toResult
        
    member _.GeneralFailure (message: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let location = buildLocation fullPath lineNumber
        
        (
            message,
            location
        )
        |> GeneralSetupTeardownFailure
        |> toResult
        
type GeneralFailureBuilder<'result> (toResult: GeneralTestingFailure -> 'result) =
    member _.ExceptionFailure (ex: exn) =
        ex |> GeneralExceptionFailure |> toResult
        
    member _.CancelFailure () =
        GeneralCancelFailure |> toResult
        
    member _.GeneralFailure message =
        message |> GeneralFailure |> toResult
        
type TestExecutionResultFailureBuilder () =
    let setupResultFailures = SetupTeardownResultFailureBuilder SetupExecutionFailure
    let testExecutionFailures = TestResultFailureBuilder TestExecutionResult  
    let teardownResultFailures = SetupTeardownResultFailureBuilder TeardownExecutionFailure
    let generalFailures = GeneralFailureBuilder GeneralExecutionFailure
    
    member _.SetupExecutionFailure with get () = setupResultFailures
    member _.TestExecutionResult with get () = testExecutionFailures
    member _.TeardownExecutionFailure with get () = teardownResultFailures
    member _.GeneralExecutionFailure with get () = generalFailures
