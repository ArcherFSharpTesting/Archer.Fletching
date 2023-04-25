module Archer.Fletching.Tests.TestResultFailureBuilder

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

let private feature = Arrow.NewFeature (
    Setup (fun _ -> TestResultFailureBuilder Called |> Ok)
)

let ``ValidationFailure should convertExpectationInfo into a ValidationFailure`` =
    feature.Test (
        fun builder ->
            let (Called result) = builder.ValidationFailure ({ ExpectedValue = 4; ActualValue = "Not four" }, "X:/Test/this.fs", -110)
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = "4"; Actual = "\"Not four\"" }), { FilePath = "X:\\Test"; FileName = "this.fs"; LineNumber = -110 }))
            if result = expected then
                TestSuccess
            else
                (
                    {
                        Expected = $"%A{expected}"
                        Actual = $"%A{result}" 
                    } |> ExpectationVerificationFailure,
                    Location.Get ()
                )
                |> TestExpectationFailure
                |> TestFailure
    )
    
let ``ValidationFailure should take an expected and an actual and return a ValidationFailure`` =
    feature.Test (
        fun builder ->
            let (Called result) = builder.ValidationFailure ("45", false, "Z:/Test/blah.fs", 99)
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = "\"45\""; Actual = "false" }), { FilePath = "Z:\\Test"; FileName = "blah.fs"; LineNumber = 99 }))
            
            if result = expected then
                TestSuccess
            else
                (
                    {
                        Expected = $"%A{expected}"
                        Actual = $"%A{result}" 
                    } |> ExpectationVerificationFailure,
                    Location.Get ()
                )
                |> TestExpectationFailure
                |> TestFailure
    )

let ``GeneralTestExpectationFailure should convert a message into a failure`` =
    feature.Test (
        fun builder ->
            let (Called result) = builder.GeneralTestExpectationFailure ("A message as to why this fails", "G:\\Test\\Place\\file.fs", 87)
            let expected = TestFailure (TestExpectationFailure ((ExpectationOtherFailure "A message as to why this fails"), { FilePath = "G:\\Test\\Place"; FileName = "file.fs"; LineNumber = 87 }))
            
            if result = expected then
                TestSuccess
            else
                (
                    {
                        Expected = $"%A{expected}"
                        Actual = $"%A{result}" 
                    } |> ExpectationVerificationFailure,
                    Location.Get ()
                )
                |> TestExpectationFailure
                |> TestFailure
    )
    
let ``IgnoreFailure should take a message and return a failure`` =
    feature.Test (
        fun builder ->
            let (Called result) = builder.IgnoreFailure ("I ignored this", "F:\\A\\Dir\\aFile.fs", 29)
            let expected = TestFailure (TestIgnored (Some "I ignored this", { FilePath = "F:\\A\\Dir"; FileName = "aFile.fs"; LineNumber = 29 }))
            
            if result = expected then
                TestSuccess
            else
                (
                    {
                        Expected = $"%A{expected}"
                        Actual = $"%A{result}" 
                    } |> ExpectationVerificationFailure,
                    Location.Get ()
                )
                |> TestExpectationFailure
                |> TestFailure
    )
    
let ``IgnoreFailure should take Some (message) and return a failure`` =
    feature.Test (
        fun builder ->
            let (Called result) = builder.IgnoreFailure (Some "I ignored this also", "Q:\\myFile.fsx", 42)
            let expected =TestFailure (TestIgnored (Some "I ignored this also", { FilePath = "Q:\\"; FileName = "myFile.fsx"; LineNumber = 42 }))
            
            if result = expected then
                TestSuccess
            else
                (
                    {
                        Expected = $"%A{expected}"
                        Actual = $"%A{result}" 
                    } |> ExpectationVerificationFailure,
                    Location.Get ()
                )
                |> TestExpectationFailure
                |> TestFailure
    )
    
let ``IgnoreFailure should take None and return a failure`` =
    feature.Test (
        fun builder ->
            let (Called result) = builder.IgnoreFailure (None, "Y:\\Z\\a.xls", 1)
            let expected = TestFailure (TestIgnored (None, { FilePath = "Y:\\Z"; FileName = "a.xls"; LineNumber = 1 }))
            
            if expected = result then
                TestSuccess
            else
                (
                    {
                        Expected = $"%A{expected}"
                        Actual = $"%A{result}" 
                    } |> ExpectationVerificationFailure,
                    Location.Get ()
                )
                |> TestExpectationFailure
                |> TestFailure
    )
    
let ``IgnoreFailure should return a failure`` =
    feature.Test (
        fun builder ->
            let (Called result) = builder.IgnoreFailure ("E:\\t.ts", 90)
            let expected = TestFailure (TestIgnored (None, { FilePath = "E:\\"; FileName = "t.ts"; LineNumber = 90 }))
            
            if result = expected then
                TestSuccess
            else
                (
                    {
                        Expected = $"%A{expected}"
                        Actual = $"%A{result}" 
                    } |> ExpectationVerificationFailure,
                    Location.Get ()
                )
                |> TestExpectationFailure
                |> TestFailure
    )

let ``ExceptionFailure should convert an exception into a failure`` =
    feature.Test (
        fun builder ->
            let ex = System.NotImplementedException ("Not Ready")
            let (Called result) = builder.ExceptionFailure ex
            let expected = TestFailure (TestExceptionFailure ex)
            
            if result = expected then
                TestSuccess
            else
                (
                    {
                        Expected = $"%A{expected}"
                        Actual = $"%A{result}" 
                    } |> ExpectationVerificationFailure,
                    Location.Get ()
                )
                |> TestExpectationFailure
                |> TestFailure
    )

let ``Test Cases`` = feature.GetTests ()