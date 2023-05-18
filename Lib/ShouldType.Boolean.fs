[<AutoOpen>]
module Archer.ShouldBoolean

open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type Should with
    // --- Boolean Checks ---------------------------------------------------------------------------------------------
    static member BeTrue (actual: bool, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check id fullPath lineNumber id id true actual
        
    static member BeFalse (actual: bool, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        check not fullPath lineNumber id id false actual