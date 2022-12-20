Order: 11
---
Sometimes it's desirable to have more control over the current date/time
and to use an alternate date/time for the execution
(for example, if you want to see what will happen during generation on a future date).

The [engine](xref:execution#engine) and [execution context](xref:execution#execution-context) both contain a `ExecutionDateTime`
property that is always set to the current local date/time (I.e. `DateTime.Now`). However, all code and themes should use the
`GetCurrentDateTime()` extension method that's available on the engine and execution context anywhere the current date/time is needed.
This extension method also checks the [setting](xref:settings)
`CurrentDateTime` and will use that value if it's set instead of the absolute local date/time.
This allows you to "override" the current date/time for the execution by setting `CurrentDateTime`
in your settings file or [via the bootstrapper](xref:specifying-settings).