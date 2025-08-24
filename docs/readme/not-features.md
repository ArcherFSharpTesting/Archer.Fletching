# Not Validation Helper

This document describes the `Not` validation helper provided in the Archer framework, specifically within the Fletching library. This helper is used to mark tests or features as "not yet implemented" in a way that integrates with the test result system.

## Overview

The `Not` type provides a static member for marking a test as not yet implemented. Instead of throwing an exception, it returns a `TestResult` indicating the test is ignored, which can be useful for test-driven development or feature planning.

---

## Not Validation Method

- **Implemented ( )**
  - Marks the test as ignored with the message "Not Yet Implemented".
  - Can be used to indicate that a test or feature is planned but not yet available.

---

## Usage Example

```fsharp
open Archer.Fletching.Lib

let result = Not.Implemented ( )
```

This function returns a `TestResult` indicating the test is ignored, which can be composed or further processed in your test suite.

---

For more details, see the source in `Lib/NotImplemented.fs`.
