Order: 2
---
There are lots of different ways of defining metadata as atomic values, complex objects, or powerful lazily evaluated scripts and delegates.

Generally, metadata can be any instance of any type. Statiq's powerful [type conversion](xref:accessing-metadata#type-conversion) will help convert the metadata object to whatever type you need when you retrieve the value. Sometimes though, you need to define metadata that's a little more powerful. The sections below describe types of metadata values that are treated separately.

# Computed Values

There are _many_ cases where being able to define metadata as a computed value can be valuable. You can define computed metadata with "fat arrow" syntax. For example, placing this in the front matter of a document will result in an `int` of "3" being returned for the "MyNumber" metadata key:

```txt
MyNumber: => 1 + 2
---
My number is <?#= MyNumber /?>.
```

The actual value should be a string (the YAML syntax above assumes a string value) that starts with `=>` and is a full C# script. It can even include multiple statements:

```txt
MyNumber: "=> { int x = 1 + 2; int y = x; return y; }"
```

The script also a number of predefined global properties available to it (see the `ScriptBase` class in `Statiq.Core` for all script properties):

- `ExecutionState` contains the current `IExecutionState` object.
- `Context` (and the `ctx` shorthand) contain the current [execution context](xref:execution#execution-context).
- `Document` (and the `doc` shorthand) contain the current [document](xref:documents-and-metadata).
- `PipelineName`: Gets the name of the currently executing pipeline.
- `Pipeline`: Gets the currently executing pipeline.
- `Phase`: Gets the currently executing [phase](xref:pipelines-and-modules#phases) of the current pipeline.
- `Parent`: Gets the parent execution context if currently in a nested [module](xref:about-modules).
- `Module`: Gets the current executing [module](xref:about-modules).
- `Inputs`: The collection of input [documents](xref:documents-and-metadata) to the current [module](xref:about-modules).

In addition, all metadata values of the current document are exposed as properties in the script. For example, if a document has a metadata item with a key of "MyItem", a global property `MyItem` will be available to the script.

# Config Metadata

Another way of accessing the current [document](xref:documents-and-metadata) or [execution context](xref:execution#execution-context) while computing a lazy metadata value is to use a [configuration delegate](xref:configuration-delegates). When an instance of `IConfig` is added as a metadata value, it will be evaluated on every request for that value.

# Lazy Values

In more advanced scenarios you sometimes want to defer figuring out the value of a metadata item until it’s accessed. This can help when the data to determine the value isn’t available yet, when computing the value would be expensive and you don’t know if it’ll actually need to be accessed, or you want to compute a fresh value each time it’s accessed. This can be accomplished by implementing `IMetadataValue`. If an object that implements that interface is added as a metadata value, it’s `object Get(IMetadata metadata)` method will be called when the value is requested (the `metadata` argument is the current metadata object, for example the [document](xref:documents-and-metadata)).
