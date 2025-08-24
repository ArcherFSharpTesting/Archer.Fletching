<!-- (dl
(section-meta
  (title Should MeetStandard Validation Functions)
)
) -->

This document describes the MeetStandard functions provided by the `Should` validation helper in the Archer framework, specifically within the Fletching library. These functions are used for approval-style testing, where the output of a test is compared against a previously approved standard (golden master).


<!-- (dl (# Overview)) -->

The `Should.MeetStandard` function enables approval testing by comparing a test result (usually a string) to an approved file. If the result does not match the approved standard, a reporter is used to display the difference.

---


<!-- (dl (# MeetStandard Validation Method)) -->

- **MeetStandard ( reporter )**
  - Returns a function that takes an `ITestInfo` and a `string` result, and checks if the result matches the approved standard using the provided reporter.
  - If the result does not match, the reporter is invoked to show the difference.

---


<!-- (dl (# Usage Example)) -->

```fsharp
open Archer.Fletching.Lib
open ApprovalTests.Reporters

let reporter = DiffReporter() :> ApprovalTests.Core.IApprovalFailureReporter
let testInfo = // ... obtain or create an ITestInfo instance ...
let result = "output to verify"

let testResult = Should.MeetStandard ( reporter ) testInfo result
```

This function returns a `TestResult` indicating pass or failure, which can be composed or further processed in your test suite.

---

For more details, see the source in `Lib/ApprovalsSupport.fs`.
