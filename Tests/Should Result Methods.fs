module Archer.Validations.Tests.``Should Result Methods``

open Archer
open Archer.Core
open Archer.Validations.Types.Internal

let private feature = FeatureFactory.Ignore (
    TestTags [
        Category "Should"
        Category "Other"
    ]
)

let ``BeOk should succeed when result is Ok`` =
    feature.Test (fun _ ->
        let actual = Ok "Hello world!" |> Should.BeOk "Hello world!"
        
        actual
        |> Should.BeEqualTo TestSuccess
    )

let ``BeOk should fail when an error happens`` =
    feature.Test (fun _ ->
        let actual = Error "ByeBy" |> Should.BeOk ("Hello world!", @"C:\Ok\path\_result.fs", -152)
        let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { ExpectedValue = (Ok "Hello world!"); ActualValue =  Error "ByeBy" }), { FilePath = @"C:\Ok\path"; FileName = "_result.fs"; LineNumber = -152 }))
        
        actual
        |> Should.BeEqualTo expected
    )

let ``BeOk should if value is wrong`` =
    feature.Test (fun _ ->
        let actual = Ok 36 |> Should.BeOk (22, @"C:\Ok\path\_result.fs", -182)
        let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { ExpectedValue = Ok 22; ActualValue =  Ok 36 }), { FilePath = @"C:\Ok\path"; FileName = "_result.fs"; LineNumber = -182 }))
        
        actual
        |> Should.BeEqualTo expected
    )

let ``BeError should succeed when result is an error`` =
    feature.Test (fun _ ->
        let actual = Error true |> Should.BeError true
        
        actual
        |> Should.BeEqualTo TestSuccess
    )

let ``BeError should fail when result is ok`` =
    feature.Test (fun _ ->
        let actual = Ok "Happy Day" |> Should.BeError (123, @"C:\Ok\path\_result.fs", -1)
        let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { ExpectedValue = Error 123; ActualValue =  Ok "Happy Day" }), { FilePath = @"C:\Ok\path"; FileName = "_result.fs"; LineNumber = -1 }))
        
        actual
        |> Should.BeEqualTo expected
    )

let ``BeError should fail when error value is wrong`` =
    feature.Test (fun _ ->
        let actual = Error "Your bad day" |> Should.BeError ("My Bad Day", @"C:\Ok\path\_result.fs", -1)
        let expected = TestFailure (TestExpectationFailure ((ExpectationVerificationFailure { ExpectedValue = Error "My Bad Day"; ActualValue =  Error "Your bad day" }), { FilePath = @"C:\Ok\path"; FileName = "_result.fs"; LineNumber = -1 }))
        
        actual
        |> Should.BeEqualTo expected
    )

let ``Test Cases`` = feature.GetTests ()