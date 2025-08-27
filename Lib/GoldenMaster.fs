module Archer.Fletching.GoldenMaster

open System.IO
open ApprovalTests.Core
open ApprovalTests.Reporters
open Archer
open Archer.Fletching.Types.Internal

let thanksUrl = "https://github.com/approvals/ApprovalTests.Net/"

let private writeTo fullPath writer result =
// Use explicit type annotation for older frameworks, and let type inference work for newer ones
    Directory.CreateDirectory (Path.GetDirectoryName (fullPath : string)) |> ignore
    do writer fullPath result
    fullPath

let private writeBinaryTo fullPath result =
    let writer path toWrite = File.WriteAllBytes (path, toWrite)
    result |> writeTo fullPath writer

let private writeTextTo fullPath result =
    let writer path toWrite = File.WriteAllText (path, toWrite, System.Text.Encoding.UTF8)
    result |> writeTo fullPath writer
    
let getStringFileWriter result = 
    { new IApprovalWriter with 
        member _.GetApprovalFilename(baseName) = $"%s{baseName}.approved.txt"
        member _.GetReceivedFilename(baseName) = $"%s{baseName}.received.txt"
        member _.WriteReceivedFile(fullPathForReceivedFile) = 
            result |> writeTextTo fullPathForReceivedFile
    }

let getBinaryFileWriter extensionWithoutDot result =
    { new IApprovalWriter with
        member _.GetApprovalFilename(baseName) = $"%s{baseName}.approved.%s{extensionWithoutDot}"
        member _.GetReceivedFilename(baseName) = $"%s{baseName}.received.%s{extensionWithoutDot}"
        member _.WriteReceivedFile fullPathForReceivedFile = 
            result |> writeBinaryTo fullPathForReceivedFile
    }

let getBinaryStreamWriter extensionWithoutDot (result:Stream) =
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
    let canonicalizedContainerName = testInfo.ContainerName |> canonicalizeString
    let canonicalizedName = testInfo.TestName |> canonicalizeString
    
    { new IGoldMasterNamer with
        member _.CanonicalizedContainerRoot with get () = canonicalizedContainerRoot
        member _.CanonicalizedContainerName with get () = canonicalizedContainerName
        member _.CanonicalizedTestName with get () = canonicalizedName
        member _.SourcePath with get () = path
        member _.Name with get () = $"%s{canonicalizedContainerName}.%s{canonicalizedName}"
    }
    
let private getGoldMasterApprover (goldMasterNamer: IGoldMasterNamer) (approver: IApprovalApprover) =  
    { new IGoldMasterApprover with
        member _.CanonicalizedContainerRoot with get () = goldMasterNamer.CanonicalizedContainerRoot
        member _.CanonicalizedContainerName with get () = goldMasterNamer.CanonicalizedContainerName
        member _.CanonicalizedTestName with get () = goldMasterNamer.CanonicalizedTestName
        member _.SourcePath with get () = goldMasterNamer.SourcePath
        member _.Name with get () = goldMasterNamer.Name
        member _.Approve () = approver.Approve ()
        member _.CleanUpAfterSuccess reporter = approver.CleanUpAfterSuccess reporter
        member _.Fail () = approver.Fail ()
        member _.ReportFailure reporter = approver.ReportFailure reporter
    }
    
let getStringFileApprover testInfo result =
    let goldMasterNamer = getNamer testInfo
    let approver = ApprovalTests.Approvers.FileApprover (getStringFileWriter result, goldMasterNamer, true)
    
    getGoldMasterApprover goldMasterNamer approver

let getBinaryFileApprover testInfo extensionWithoutDot result =
    let goldMasterNamer = getNamer testInfo
    let approver = ApprovalTests.Approvers.FileApprover (getBinaryFileWriter extensionWithoutDot result, goldMasterNamer)
    
    getGoldMasterApprover goldMasterNamer approver
    
        
let getStreamFileApprover testInfo extensionWithoutDot (result:Stream) =
    let goldMasterNamer = getNamer testInfo
    let approver = ApprovalTests.Approvers.FileApprover (getBinaryStreamWriter extensionWithoutDot result, goldMasterNamer)
    
    getGoldMasterApprover goldMasterNamer approver
        
let approve fullPath lineNumber (reporter: IApprovalFailureReporter) (approver: IGoldMasterApprover) =
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
        
        let fb = TestResultFailureBuilder id
        fb.ValidationFailure ({ ExpectedValue = $"%s{approver.Name}.approved"; ActualValue = $"%s{approver.Name}.received" }, fullPath, lineNumber)