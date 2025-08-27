<!-- (dl
(section-meta
  (title Should Other Validation Functions)
)
) -->


Other validations include marking tests as failed or ignored using the `Should` helper.


<!-- (dl (# Overview)) -->

The `Should` type provides static members for miscellaneous test outcomes, such as marking a test as failed or ignored.

---


<!-- (dl (# Other Validation Methods)) -->

- **Fail ( message )**
  - Marks the test as failed with the provided message.
- **BeIgnored**
  - Marks the test as ignored. Can be called with or without a message.
  - **BeIgnored ( )**: Ignores the test without a message.
  - **BeIgnored ( message )**: Ignores the test with a custom message.

---


<!-- (dl (# Usage Example)) -->

```fsharp
open Archer.Fletching.Lib

// Mark a test as failed
let result1 = Should.Fail ( "This test should fail." )

// Ignore a test without a message
let result2 = Should.BeIgnored ( ) "any value"

// Ignore a test with a message
let result3 = Should.BeIgnored ( "This test is ignored for now." ) "any value"
```


See [How to Use Fletcher Test Validations](#how-to-use-fletcher-test-validations) for usage patterns and return value details.

For more details, see the source in `Lib/ShouldType.Other.fs`.
