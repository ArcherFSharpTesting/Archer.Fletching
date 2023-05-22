[<AutoOpen>]
module Archer.Fletching.Public

type IGoldMasterNamer =
    inherit ApprovalTests.Core.IApprovalNamer
    abstract member CanonicalizedContainerRoot : string with get
    abstract member CanonicalizedContainerName : string with get
    abstract member CanonicalizedTestName: string with get
    
type IGoldMasterApprover =
    inherit IGoldMasterNamer
    inherit ApprovalTests.Core.IApprovalApprover