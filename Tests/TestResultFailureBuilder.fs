module Archer.Fletching.Tests.TestResultFailureBuilder

open Archer
open Archer.Arrows
open Archer.Fletching.Types.Internal

let private feature = Arrow.NewFeature (
    Setup (fun _ -> TestResultFailureBuilder (id) |> Ok)
)

let ``ValidationFailure should convertExpectationInfo into a ValidationFailure`` =
    feature.Test (
        fun builder _ ->
            let result = builder.ValidationFailure ({ ExpectedValue = 4; ActualValue = "Not four" }, "X:/Test/this.fs", -110)
            let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { Expected = "4"; Actual = "\"Not four\"" }), { FilePath = "X:\\Test"; FileName = "this.fs"; LineNumber = -110 }))
            if result = expected then
                TestSuccess
            else
                (
                    {
                        Expected = $"%A{expected}"
                        Actual = $"%A{result}" 
                    } |> ExpectationVerificationFailure,
                    {
                        FilePath = ".\\Tests"
                        FileName = "TestResultFailureBuilder.fs"
                        LineNumber = 16 
                    }
                )
                |> TestExpectationFailure
                |> TestFailure
    )

let ``Test Cases`` = feature.GetTests ()