[<AutoOpen>]
module Archer.ShouldTypes

open System
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Fletching.Types.Internal

let private failureBuilder = TestResultFailureBuilder id

let private checkReference<'a> (expected: 'a) (actual: 'a) =
    Object.ReferenceEquals (actual, expected)
    
let private getType value = value.GetType ()

let private isInstanceOf<'expectedType> actual =
    let expected = typeof<'expectedType>
    
    expected.IsInstanceOfType actual

let private check fCheck fullPath lineNumber expectedModifier actualModifier expected actual =
    if actual |> fCheck then
        TestSuccess
    else
        failureBuilder.ValidationFailure (expectedModifier expected, actualModifier actual, fullPath, lineNumber)

type Should =
    // --- Object Checks ---------------------------------------------------------------------------------------------
    static member BeEqualTo (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check ((=) expected) fullPath lineNumber id id expected
        
    static member NotBeEqualTo (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check ((<>) expected) fullPath lineNumber Not id expected
        
    static member BeSameAs (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check (checkReference expected) fullPath lineNumber ReferenceOf id expected
        
    static member NotBeSameAs (expected, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check (checkReference expected >> not) fullPath lineNumber (ReferenceOf >> Not) id expected
        
    static member BeOfType<'expectedType> (actual, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check isInstanceOf<'expectedType> fullPath lineNumber id getType typeof<'expectedType> actual
        
    static member NotBeTypeOf<'expectedType> (actual, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check (isInstanceOf<'expectedType> >> not) fullPath lineNumber Not getType typeof<'expectedType> actual