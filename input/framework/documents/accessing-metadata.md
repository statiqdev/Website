Order: 3
---
Every [document](xref:documents) acts like a dictionary and implements `IReadOnlyDictionary<string, object>` for easy access. Metadata key/value pairs can be accessed through this interface.

# Type Conversion 

All metadata is represented internally as raw objects. This allows you to store not just strings, but more complex data as well. However, when you access metadata you probably don’t want to think about how it’s stored or what the original type was. For example, YAML doesn’t really distinguish between numbers and strings when it reads data, it’s only when getting a value that you care. To make metadata as easy to work with as possible, Statiq Framework includes a very powerful type conversion capability that lets you convert nearly any metadata value to any other compatible type. For example, when you call `IMetadata.Get<TValue>(string key)` it doesn’t matter what the underlying type of the metadata is because the type converter will convert it to the requested `TValue` type if at all possible.

Converting between sequences of different types or from a single item to a sequence is also supported. If you request an `IList<T>`, `IEnumerable<T>`, or array of `T` and the original value is also enumerable, all elements will be converted to the requested type `T` and those that cannot be converted will be omitted from the result. If the original value is not enumerable, it will be returned as a single element of the requested enumerable type.

While type conversion is most often used when dealing with metadata, it’s also very helpful in situations when you only have string values (such as dealing with data from service endpoints) and can be used directly from your own code. The type conversion capability is accessible through the static `TypeHelper` class. These utility methods check all .NET type conversion techniques including `TypeConverter`, `IConvertible`, casting, etc. in order to convert any type to any other compatible type. The conversion support is provided by a fork of the [UniversalTypeConverter](http://www.codeproject.com/Articles/248440/Universal-Type-Converter) library. New conversions can also be added at run-time through the `TypeHelper.RegisterTypeConverter()` methods.

# Raw Values

There may be times when you don’t want [lazy values](#lazy-values), [computed values](#computed-values), or [config metadata](#config-metadata) to evaluate and instead need to access the raw metadata object (for example, to use or set it directly in another document). In those cases you can access the raw metadata values without running delegates or scripts using the `GetRaw()`, `TryGetRaw()`, and `GetRawEnumerator()` methods of the document or metadata object.

# Metadata Lookup

There are several extensions to make working with documents and metadata easier. One of the more powerful ones lets you generate an `ILookup<T, IDocument>` from a sequence of documents based on a metadata key. The signature of the extension method is `ILookup<T, IDocument> ToLookup<T>(this IEnumerable<IDocument> documents, string key)` where `key` is the metadata key that you want to generate a lookup for.

For example, say you have a sequence of documents, some of which contain metadata for the key “Tags”. Also, assume that some of the documents with “Tags” metadata contain a single value some contain arrays. If you simply call `Documents.ToLookup<string>(“Tags”)` you will get back an `ILookup<T, IDocument>` keyed by each possible tag string with a sequence of the documents that contain that tag as the value.