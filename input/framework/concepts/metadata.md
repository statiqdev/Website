Order: 2
---
Metadata is the primary way information is passed though modules and pipelines via documents.

Along with its content, every [document](/framework/concepts/documents) contains *metadata*. As with documents, metadata is immutable and you must clone a document to add additional metadata. Several [modules](/framework/concepts/modules), such as `SetMetadata`, are designed to allow you to manipulate document metadata as part of your [pipeline](/framework/concepts/pipelines).

# Defining Metadata

## Front Matter

One of the more common ways to set metadata is to use *front matter*. Front matter is a common concept in static generators and gets around the problem of how to define metadata for a file without creating a whole separate file. Typically, front matter is placed at the top of an input file and uses some sort of format (like YAML or JSON) to define key/value pairs. In Statiq, front matter can be extracted using the `ExtractFrontMatter` module. That module accepts child modules that process whatever content is contained in the front matter block, such as YAML or JSON. Often front matter is delimited from the actual file content by a series of dashes such as `---`, though an alternate delimiter can be specified in the module.

A file that contains front matter might look like this:

``` txt
Title: Some Title
Description: This is a description.
Date: 5/25/2016
---
This is the content of the file.
```

An example of front matter usage would be using metadata to define tags for your blog posts. You could create a "Tags" metadata field in the front matter of your post file and then read that metadata later to create tag clouds, lists of similar posts, etc.

Practically, the `ParseYaml` module is usually used as the child of the `ExtractFrontMatter` module. However, like most things in Statiq Framework this is designed to be flexible. You could process any type of front matter (JSON, etc.) with this setup by specifying different child modules.

## Lazy Values

In more advanced scenarios you sometimes want to defer figuring out the value of a metadata item until it's accessed. This can help when the data to determine the value isn't available yet, when computing the value would be expensive and you don't know if it'll actually need to be accessed, or you want to compute a fresh value each time it's accessed. This can be accomplished by implementing `IMetadataValue`. If an object that implements that interface is added as a metadata value, it's `object Get(IMetadata metadata)` method will be called when the value is requested (the `metadata` argument is the current metadata object, for example the [document](/framework/concepts/documents)).

## Computed Values

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

- `Context` (and the `ctx` shorthand) contain the current [execution context](/framework/concepts/execution#execution-context).

- `Document` (and the `doc` shorthand) contain the current [document](/framework/concepts/documents).

- `PipelineName`: Gets the name of the currently executing pipeline.

- `Pipeline`: Gets the currently executing pipeline.

- `Phase`: Gets the currently executing [phase](/framework/concepts/pipelines#phases) of the current pipeline.

- `Parent`: Gets the parent execution context if currently in a nested [module](/framework/concepts/modules).

- `Module`: Gets the current executing [module](/framework/concepts/modules).

- `Inputs`: The collection of input [documents](/framework/concepts/documents) to the current [module](/framework/concepts/modules) (probably the most important property of the execution context).

In addition, all metadata values of the current document are exposed as properties in the script. For example, if a document has a metadata item with a key of "MyItem", a global property `MyItem` will be available to the script.

## Config Metadata

Adding a Another way of accessing the current [document](/framework/concepts/documents) or [execution context](/framework/concepts/execution#execution-context) while computing a lazy metadata value is 

# Accessing Metadata

Every [document](/framework/concepts/documents) acts like a dictionary and implements `IReadOnlyDictionary<string, object>` for easy access. Metadata key/value pairs can be accessed through this interface.

## Type Conversion 

All metadata is represented internally as raw objects. This allows you to store not just strings, but more complex data as well. However, when you access metadata you probably don't want to think about how it's stored or what the original type was. For example, YAML doesn't really distinguish between numbers and strings when it reads data, it's only when getting a value that you care. To make metadata as easy to work with as possible, Statiq Framework includes a very powerful [type conversion capability](/framework/usage/type-conversion) that lets you convert nearly any metadata value to any other compatible type. For example, when you call `IMetadata.Get<TValue>(string key)` it doesn’t matter what the underlying type of the metadata is because the type converter will convert it to the requested `TValue` type if at all possible.

## Raw Values

## Metadata Lookup

There are several extensions to make working with documents and metadata easier. One of the more powerful ones lets you generate an `ILookup<T, IDocument>` from a sequence of documents based on a metadata key. The signature of the extension method is `ILookup<T, IDocument> ToLookup<T>(this IEnumerable<IDocument> documents, string key)` where `key` is the metadata key that you want to generate a lookup for.

For example, say you have a sequence of documents, some of which contain metadata for the key "Tags". Also, assume that some of the documents with "Tags" metadata contain a single value some contain arrays. If you simply call `Documents.ToLookup<string>("Tags")` you will get back an `ILookup<T, IDocument>` keyed by each possible tag string with a sequence of the documents that contain that tag as the value.