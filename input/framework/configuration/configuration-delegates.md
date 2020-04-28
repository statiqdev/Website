Order: 3
---
Most modules that require configuration use a special `Config<TValue>` delegate. This type allows you to specify lazy logic that's evaluated at [execution time](xref:execution) and optionally uses the document and/or [execution context](xref:execution#execution-context), is async or not, or just converts from a simple value.

Creating a `Config<TValue>` is often done via factory methods from the `Config` static class. For example, if a module needs a string for a given setting it will accept a `Config<string>` which can be created by passing a `string` directly:

``` csharp
new SomeModule("my-string")
```

or by using one of the many factory methods:

``` csharp
new SomeModule(
    Config.FromDocument(doc => doc.GetString("key")))
```