Order: 2
---
Most modules that require configuration use a special `Config<TValue>` delegate. This type allows you to specify lazy logic that's evaluated at [execution time](xref:execution) and optionally uses the document and/or [execution context](xref:execution#execution-context), is async or not, or just converts from a simple value.

# Factory Methods

Creating a `Config<TValue>` is often done via factory methods from the `Config` static class. For example, if a module needs a string for a given setting it will accept a `Config<string>` which can be created by passing a `string` directly:

``` csharp
new SomeModule("my-string")
```

or by using one of the many factory methods:

``` csharp
new SomeModule(
    Config.FromDocument(doc => doc.GetString("key")))
```

Some of the available configuration delegate factory methods include:

- `Config.FromContext()` overloads create a delegate using the [execution context](xref:execution-context).
- `Config.FromDocument()` overloads create a delegate using the current [document](xref:documents-and-metadata).
- `Config.FromSetting()` overloads create a delegate using the value of a [setting](xref:settings).
- `Config.FromSettings()` overloads create a delegate using an action on the full set of [settings](xref:settings).
- `Config.FromValue()` overloads create a delegate using a single value.

# Value Casting

In addition to creating a configuration delegate using the [factory methods](#factory-methods), a `TValue` can be implicitly cast to the appropriate `Config<TValue>` type. For example, if a module argument is configured using a `Config<string>`, a simple `"string value"` can be passed to the module method and it will be implicitly converted to the appropriate delegate. This makes configuration delegate usage invisible in many simple cases.

# Extensions

There are also several extensions that can help work with configuration delegates:

- `CombineWith()` overloads combine two configuration delegates together in various ways.
- `Transform()` overloads transform the configuration delegate at runtime using it's evaluated value.
- `Cast()` casts a `Config<TValue>` of one `TValue` type to a different type.
- `MakeEnumerable()` converts a `Config<TValue>` to a `Config<IEnumerable<TValue>>` with a single item in the enumerable.