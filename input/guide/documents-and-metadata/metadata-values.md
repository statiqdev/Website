Order: 2
---
There are lots of different ways of defining metadata as atomic values, complex objects, or powerful lazily evaluated scripts and delegates.

Generally, metadata can be any instance of any type. Statiq's powerful [type conversion](xref:accessing-metadata#type-conversion) will help convert the metadata object to whatever type you need when you retrieve the value. Sometimes though, you need to define metadata that's a little more powerful. The sections below describe types of metadata values that are treated separately.

# Computed Values

There are _many_ cases where being able to define metadata as a computed value can be valuable. You can define computed metadata with "arrow" syntax. For example, placing this in the front matter of a document will result in an `int` of "3" being returned for the "MyNumber" metadata key:

```txt
MyNumber: => 1 + 2
---
My number is <?#= MyNumber /?>.
```

The actual value should be a string (the YAML syntax above assumes a string value) that starts with `=>` and is a full C# script. It can even include multiple statements:

```txt
MyNumber: "=> { int x = 1 + 2; int y = x; return y; }"
```

The script also has access to a number of predefined global properties available to it (see the `ScriptBase` class in `Statiq.Core` for all script properties):

- `ExecutionState` contains the current `IExecutionState` object.
- `Context` (and the `ctx` shorthand) contain the current [execution context](xref:execution#execution-context).
- `Document` (and the `doc` shorthand) contain the current [document](xref:documents-and-metadata).
- `PipelineName`: Gets the name of the currently executing pipeline.
- `Pipeline`: Gets the currently executing pipeline.
- `Phase`: Gets the currently executing [phase](xref:pipelines-and-modules#phases) of the current pipeline.
- `Parent`: Gets the parent execution context if currently in a nested [module](xref:about-modules).
- `Module`: Gets the current executing [module](xref:about-modules).
- `Inputs`: The collection of input [documents](xref:documents-and-metadata) to the current [module](xref:about-modules).

In addition, all metadata conversion methods are exposed as global methods. For example, if a document has another metadata value called `Foo` and you wanted to get the `int` value of that in another metadata value you could write something like this in your front matter:

```
Bar: => GetInt("Foo") + 2
```

## Cached Vs. Uncached

When using "arrow" syntax to define a computed metadata value, the behavior is different depending on what syntax you use:

- `=>` will cache the result of the script and return the same value every time it's accessed from a given document.
- `->` will re-evaluate the script every time it's accessed from a given document.

The cached version can often yield _much_ better performance, but the uncached version is provided for situations when the value needs to be re-evaluated every time it's accessed. As an example, consider the use of `DateTime.Now` in the computed metadata value. If the `=>` prefix is used, the value will be cached after the first access, and every time that metadata is requested from that point forward the same `DateTime` will be returned. If the `->` prefix is used, the value will be re-evaluated every time it's accessed and a new, different `DateTime` will be returned. It's recommended that you use the cached `=>` syntax unless you know you really need to re-evaluate the script every time.

# Configuration Delegates

Another way of accessing the current [document](xref:documents-and-metadata) or [execution context](xref:execution#execution-context) while computing a lazy metadata value is to use a [configuration delegate](xref:configuration-delegates). When an instance of `IConfig` is added as a metadata value, it will be evaluated on every request for that value.

# Lazy Values

In more advanced scenarios you sometimes want to defer figuring out the value of a metadata item until it’s accessed. This can help when the data to determine the value isn’t available yet, when computing the value would be expensive and you don’t know if it’ll actually need to be accessed, or you want to compute a fresh value each time it’s accessed. This can be accomplished by implementing `IMetadataValue`. If an object that implements that interface is added as a metadata value, it’s `object Get(IMetadata metadata)` method will be called when the value is requested (the `metadata` argument is the current metadata object, for example the [document](xref:documents-and-metadata)).