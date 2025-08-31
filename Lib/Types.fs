
/// <summary>
/// Contains public types for Archer.Validations verification and approval integration.
/// </summary>
[<AutoOpen>]
module Archer.Validations.Public

/// <summary>
/// Represents a namer for Gold Master (approval) tests, providing canonicalized names for containers and tests.
/// </summary>
type IGoldMasterNamer =
    inherit ApprovalTests.Core.IApprovalNamer
    /// <summary>
    /// Gets the canonicalized root directory for the test container.
    /// </summary>
    abstract member CanonicalizedContainerRoot : string with get
    /// <summary>
    /// Gets the canonicalized name of the test container (e.g., class or module).
    /// </summary>
    abstract member CanonicalizedContainerName : string with get
    /// <summary>
    /// Gets the canonicalized name of the test itself.
    /// </summary>
    abstract member CanonicalizedTestName: string with get
    
/// <summary>
/// Represents an approver for Gold Master (approval) tests, combining naming and approval responsibilities.
/// </summary>
type IGoldMasterApprover =
    inherit IGoldMasterNamer
    inherit ApprovalTests.Core.IApprovalApprover