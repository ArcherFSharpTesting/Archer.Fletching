[<Microsoft.FSharp.Core.AutoOpen>]
module Archer.ShouldList

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Fletching.Types.Internal

type ListShould =
    static member Contain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        let checkIt (items: 'a list) =
            check (List.contains value) fullPath lineNumber (fun a -> Contains (a, value)) (fun a -> (a, value) |> Contains |> Not) items items
            
        checkIt
        
    static member NotContain (value: 'a, [<CallerFilePath; Optional; DefaultParameterValue("")>] fullPath: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>]lineNumber: int) =
        let checkIt (items: 'a list) =
            check (List.contains value >> not) fullPath lineNumber (fun a -> (a, value) |> Contains |> Not) (fun a -> Contains (a, value)) items items
            
        checkIt