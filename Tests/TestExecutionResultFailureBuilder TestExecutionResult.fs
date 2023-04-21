module Archer.Fletching.Tests.``TestExecutionResultFailureBuilder TestExecutionResult``

open System.Security.Authentication.ExtendedProtection
open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

let private feature = Arrow.NewFeature (
    Setup (fun _ -> TestExecutionResultFailureBuilder().TestExecutionResult |> Ok)
)

let ``ValidationFailure should convert ExpectationInfo into a failure`` =
    feature.Test (
        fun builder _ ->
            let a = Ok "Its Good"
            let result = builder.ValidationFailure ({ ExpectedValue = a; ActualValue = Called 33 }, "H:\\f.th", 25)
            let expected = TestExecutionResult (TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{a}"; Actual = $"%A{Called 33}" }), { FilePath = "H:\\"; FileName = "f.th"; LineNumber = 25 })))
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``ValidationFailure should convert expected and actual into a failure`` =
    feature.Test (
        fun builder _ ->
            let a = ("are you good?", true)
            let result = builder.ValidationFailure (a, [], "L:\\456\\q.com", 99)
            let expected = TestExecutionResult (TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = $"%A{a}"; Actual = $"%A{[]}" }), { FilePath = "L:\\456"; FileName = "q.com"; LineNumber = 99 })))
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``GeneralTestExpectationFailure should convert a message into a failure`` =
    feature.Test (
        fun builder _ ->
            let result = builder.GeneralTestExpectationFailure ("This is a failure", "V:\\yup\\bb.b", 63)
            let expected = TestExecutionResult (TestFailure (TestExpectationFailure (ExpectationOtherFailure "This is a failure", { FilePath = "V:\\yup"; FileName = "bb.b"; LineNumber = 63 })))
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``IgnoreFailure should convert a message into a failure`` =
    feature.Test (
        fun builder _ ->
            let result = builder.IgnoreFailure ("I ignore you", "U:\\Ing.pxl", 11)
            let expected = TestExecutionResult (TestFailure (TestIgnored (Some "I ignore you", { FilePath = "U:\\"; FileName = "Ing.pxl"; LineNumber = 11 })))
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``IgnoreFailure should convert Some (message) into a failure`` =
    feature.Test (
        fun builder _ ->
            let result = builder.IgnoreFailure (Some "Yep I am ignoring you", "I:\\ignore.u", 40)
            let expected = TestExecutionResult (TestFailure (TestIgnored (Some "Yep I am ignoring you", { FilePath = "I:\\"; FileName = "ignore.u"; LineNumber = 40 })))
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``IgnoreFailure should convert None into a failure`` =
    feature.Test (
        fun builder _ ->
            let result = builder.IgnoreFailure (None, "W:\\ho\\r.u", 77)
            let expected = TestExecutionResult (TestFailure (TestIgnored (None, { FilePath = "W:\\ho"; FileName = "r.u"; LineNumber = 77 })))
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``IgnoreFailure should return a failure`` =
    feature.Test (
        fun builder _ ->
            let result = builder.IgnoreFailure ("S:\\o\\what.duh", lineNumber = 83)
            let expected = TestExecutionResult (TestFailure (TestIgnored (None, { FilePath = "S:\\o"; FileName = "what.duh"; LineNumber = 83 })))
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )
    
let ``ExceptionFailure should convert an exception into a failure`` =
    feature.Test (
        fun builder _ ->
            let ex = System.RankException "This is rank"
            let result = builder.ExceptionFailure ex
            let expected = TestExecutionResult (TestFailure (TestExceptionFailure ex))
            
            if result = expected then
                TestSuccess
            else
                failureBuilder.ValidationFailure (expected, result)
    )

let ``Test Cases`` = feature.GetTests ()