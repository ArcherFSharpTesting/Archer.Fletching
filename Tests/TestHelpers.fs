[<AutoOpen>]
module Archer.Validations.Tests.TestHelpers

open Archer
open System.IO
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Validations.Types.Internal

type Location =
    static member Get([<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let fileInfo = FileInfo fileFullName
        let filePath = fileInfo.Directory.FullName
        let fileName = fileInfo.Name
        
        {
            FilePath = filePath
            FileName = fileName
            LineNumber = lineNumber 
        }
        
type WasCalled<'a> = | Called of 'a

let failureBuilder = TestResultFailureBuilder id
