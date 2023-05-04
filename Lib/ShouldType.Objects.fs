namespace Archer

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Fletching.Types.Internal

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
        
    static member BeNull<'expectedType when 'expectedType: null> (actual: 'expectedType, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check (fun v -> match v with | null -> true | _ -> false) fullPath lineNumber id id null actual
        
    static member NotBeNull<'expectedType when 'expectedType: null> (actual: 'expectedType, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check (fun v -> match v with | null -> false | _ -> true) fullPath lineNumber id id (Not null) actual
        
    static member BeDefaultOf<'expectedType when 'expectedType: equality> (actual: 'expectedType, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check ((=) Unchecked.defaultof<'expectedType>) fullPath lineNumber id id Unchecked.defaultof<'expectedType> actual
        
    static member NotBeDefaultOf<'expectedType when 'expectedType: equality> (actual: 'expectedType, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        check ((<>) Unchecked.defaultof<'expectedType>) fullPath lineNumber id id (Not Unchecked.defaultof<'expectedType>) actual
        
    static member PassTestOf (predicate: 'a -> bool, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        let checkIt actual =
            check predicate fullPath lineNumber id FailsTest (PassesTest actual) actual
            
        checkIt
        
    static member NotPassTestOf (predicate: 'a -> bool, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        let checkIt actual =
            check (predicate >> not) fullPath lineNumber id PassesTest (FailsTest actual) actual
            
        checkIt
        
    static member PassAllOf (predicates: ('a -> TestResult) list, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        let checkIt actual =
            if 0 < predicates.Length then
                predicates
                |> List.map (fun f -> f actual)
                |> List.reduce (+)
            else
                failureBuilder.GeneralTestExpectationFailure ("Should.PassAllOf needs to be passed at least one verification function", fullPath, lineNumber)
            
        checkIt