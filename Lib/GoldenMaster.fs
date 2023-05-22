module Archer.Fletching.GoldenMaster

open System.IO
open ApprovalTests.Core
open ApprovalTests.Reporters
open Archer
open Archer.Fletching.Types.Internal

let thanksUrl = "https://github.com/approvals/ApprovalTests.Net/"

let private writeTo fullPath writer result =
    Directory.CreateDirectory (Path.GetDirectoryName fullPath) |> ignore
    do writer fullPath result
    fullPath

let private writeBinaryTo fullPath result =
    let writer path toWrite = File.WriteAllBytes (path, toWrite)
    result |> writeTo fullPath writer

let private writeTextTo fullPath result =
    let writer path toWrite = File.WriteAllText (path, toWrite, System.Text.Encoding.UTF8)
    result |> writeTo fullPath writer
    
let private getStringFileWriter result = 
    { new IApprovalWriter with 
        member _.GetApprovalFilename(baseName) = $"%s{baseName}.approved.txt"
        member _.GetReceivedFilename(baseName) = $"%s{baseName}.received.txt"
        member _.WriteReceivedFile(fullPathForReceivedFile) = 
            result |> writeTextTo fullPathForReceivedFile
    }

let private getBinaryFileWriter extensionWithoutDot result =
    { new IApprovalWriter with
        member _.GetApprovalFilename(baseName) = $"%s{baseName}.approved.%s{extensionWithoutDot}"
        member _.GetReceivedFilename(baseName) = $"%s{baseName}.received.%s{extensionWithoutDot}"
        member _.WriteReceivedFile fullPathForReceivedFile = 
            result |> writeBinaryTo fullPathForReceivedFile
    }

let private getBinaryStreamWriter extensionWithoutDot (result:Stream) =
    let length = int result.Length
    let data : byte array = Array.zeroCreate length

    result.Read(data, 0, data.Length) |> ignore
    getBinaryFileWriter extensionWithoutDot data
    
let canonicalizeString (value: string) =
    let toString : char seq -> string = Seq.map string >> String.concat ""
    let canonicalized =
        value 
        |> Seq.filter (fun c -> 
            (System.Char.IsLetterOrDigit c)
            || c = ' '
            || c = '_'
            || c = '.'
            || c = '-'
        )
        |> Seq.map (fun c -> 
            if c = ' '
            then '_'
            else c
        )
        
    (canonicalized |> toString).Trim()    

let private getNamer (testInfo: ITestInfo) = 
    let path = testInfo.Location.FilePath
    let canonicalizedContainerRoot = testInfo.ContainerPath |> canonicalizeString
    let canonicalizedContainerName = testInfo.ContainerName
    let canonicalizedName = testInfo.TestName
    
    {  new IApprovalNamer with
        member _.SourcePath with get () = path
        member _.Name with get () = $"%s{canonicalizedContainerRoot}.%s{canonicalizedContainerName}.%s{canonicalizedName}"
    }
    
let approve (testInfo:ITestInfo) (reporter:IApprovalFailureReporter) (approver:IApprovalApprover) fullPath lineNumber =
    if approver.Approve ()
    then
        do approver.CleanUpAfterSuccess reporter 
        TestSuccess
    else 
        do approver.ReportFailure reporter
                                                            
        match reporter with
        | :? IReporterWithApprovalPower as approvalReporter -> 
            if approvalReporter.ApprovedWhenReported ()
            then do approver.CleanUpAfterSuccess(reporter)
            ()
        | _ -> ()
        
        let namer = getNamer testInfo
        let fb = TestResultFailureBuilder id
        fb.ValidationFailure ({ ExpectedValue = $"%s{namer.Name}.approved"; ActualValue = $"%s{namer.Name}.received" }, fullPath, lineNumber)