Order: 2
---
There are lots of different ways of defining metadata as atomic values, complex objects, or powerful lazily evaluated scripts and delegates.

# Front Matter

One of the more common ways to set metadata is to use *front matter*. Front matter is a common concept in static generators and gets around the problem of how to define metadata for a file without creating a whole separate file. Typically, front matter is placed at the top of an input file and uses some sort of format (like YAML or JSON) to define key/value pairs. In Statiq, front matter can be extracted using the `ExtractFrontMatter` module. That module accepts child modules that process whatever content is contained in the front matter block, such as YAML or JSON. Often front matter is delimited from the actual file content by a series of dashes such as `—`, though an alternate delimiter can be specified in the module.

A file that contains front matter might look like this:

``` txt
Title: Some Title
Description: This is a description.
Date: 5/25/2016
—
This is the content of the file.
```

An example of front matter usage would be using metadata to define tags for your blog posts. You could create a “Tags” metadata field in the front matter of your post file and then read that metadata later to create tag clouds, lists of similar posts, etc.

Practically, the `ParseYaml` module is usually used as the child of the `ExtractFrontMatter` module. However, like most things in Statiq Framework this is designed to be flexible. You could process any type of front matter (JSON, etc.) with this setup by specifying different child modules.

# Computed Values

There are _many_ cases where being able to define metadata as a computed value can be valuable. You can define computed metadata with “fat arrow” syntax. For example, placing this in the front matter of a document will result in an `int` of “3” being returned for the “MyNumber” metadata key:

```txt
MyNumber: => 1 + 2
—
My number is <?#= MyNumber /?>.
```

The actual value should be a string (the YAML syntax above assumes a string value) that starts with `=>` and is a full C# script. It can even include multiple statements:

```txt
MyNumber: “=> { int x = 1 + 2; int y = x; return y; }”
```

The script also a number of predefined global properties available to it (see the `ScriptBase` class in `Statiq.Core` for all script properties):

- `ExecutionState` contains the current `IExecutionState` object.

- `Context` (and the `ctx` shorthand) contain the current [execution context](xref:execution#execution-context).

- `Document` (and the `doc` shorthand) contain the current [document](xref:documents).

- `PipelineName`: Gets the name of the currently executing pipeline.

- `Pipeline`: Gets the currently executing pipeline.

- `Phase`: Gets the currently executing [phase](xref:pipelines#phases) of the current pipeline.

- `Parent`: Gets the parent execution context if currently in a nested [module](xref:modules).

- `Module`: Gets the current executing [module](xref:modules).

- `Inputs`: The collection of input [documents](xref:documents) to the current [module](xref:modules).

In addition, all metadata values of the current document are exposed as properties in the script. For example, if a document has a metadata item with a key of “MyItem”, a global property `MyItem` will be available to the script.

# Config Metadata

Another way of accessing the current [document](xref:documents) or [execution context](xref:execution#execution-context) while computing a lazy metadata value is to use a [configuration delegate](xref:configuration_delegates). When an instance of `IConfig` is added as a metadata value, it will be evaluated on every request for that value.

# Lazy Values

In more advanced scenarios you sometimes want to defer figuring out the value of a metadata item until it’s accessed. This can help when the data to determine the value isn’t available yet, when computing the value would be expensive and you don’t know if it’ll actually need to be accessed, or you want to compute a fresh value each time it’s accessed. This can be accomplished by implementing `IMetadataValue`. If an object that implements that interface is added as a metadata value, it’s `object Get(IMetadata metadata)` method will be called when the value is requested (the `metadata` argument is the current metadata object, for example the [document](xref:documents)).
