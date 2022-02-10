Order: 3
---
Documents are the primary unit of information in Statiq and are a combination of content and metadata.

Documents are immutable and once their content is changed or a value is added to their metadata, it can never be removed (though it can be overwritten by a cloned document). Though the documentation often talks about documents being "transformed" or "manipulated" by [modules](xref:pipelines-and-modules), this isn't strictly accurate. Instead modules return a new cloned copy of the document with different content and/or additional metadata, while maintaining all the metadata the original document had.

For example, this visualizes a single document that contains some content as well as two metadata values:

<div class="mermaid">
    graph TD
        subgraph Document
            subgraph Metadata
                Title
                Published
            end
            Content
        end
</div>

It's tempting to think of documents as being one-to-one with files in the filesystem, but they're much more than that. While files are often the primary way documents are created, they can come from other sources too. It's better to think of documents and being part of a database. In this mode of thought Statiq is like a document database, with Statiq documents being analogous to the document concept from other document databases.

# About Metadata

Along with their content, every document also contains metadata. As with documents, metadata is immutable and you must clone a document to add additional metadata. Several [modules](xref:about-modules), such as `SetMetadata`, are designed to allow you to manipulate document metadata as part of your [pipeline](xref:pipelines-and-modules).

There are several ways to add metadata to documents and metadata is merged together from multiple sources to determine the complete set of metadata for a given document. This _data cascade_ (credit to [11ty](https://www.11ty.dev/docs/data-cascade/) for the excellent term) applies metadata from the lowest priority source to the highest. A document will contain metadata from all the sources, but if a higher priority metadata source contains the same key as a lower priority one, the value from the higher priority source will take precedence.

Note that [global settings cascade to documents](xref:settings#cascade-to-documents). This can be helpful because it means you can define metadata that's intended for use by documents as a global setting and it will cascade to every document as a default value.

## Metadata Sources <?# WebBadge /?>

In Statiq Framework, you need to use modules like `ExtractFrontMatter` to add metadata to document, but Statiq Web includes support for many metadata sources out of the box. Here are the default metadata sources in Statiq Web from lowest priority to highest. Think of metadata being applied (or _cascaded_) from the top of this list to the bottom, with duplicate keys overwriting existing values along the way.

- Environment variables.
- [Configuration files](xref:settings#configuration-files).
- [Default Statiq Framework settings](xref:settings).
- [Default Statiq Web settings](xref:web-settings).
- [Settings you define via the Bootstrapper](xref:specifying-settings).
- [Directory metadata](xref:directory-metadata) (closer to the file have higher priority).
- [Sidecar files](xref:sidecar-files).
- [Front matter](xref:front-matter).
- Parsed data content (if a [data file](xref:data-files)).

# Creating and Cloning Documents

There are two ways to get a new document: you can create one from scratch or you can clone an existing one.

To create a document you typically call one of the `CreateDocument()` method overloads on the current [execution context](xref:execution-context). These methods let you provide the initial metadata and/or content that the new document should contain.

To clone an existing document and replace or add new content and/or metadata you can call one of the `Clone()` methods on the document itself. If you're unsure whether you have a null document, the execution context also provides several `CloneOrCreateDocument()` overloads that either clones an existing document or creates a new one depending on if the provided document reference is null or not.

If your module creates or manipulates documents, follow these guidelines and tips on document creation and working with documents:

- Call `Clone()` on existing documents to clone with new properties.
- Call `Engine.SetDefaultDocumentType<TDocument>()` to change the default document type.
- Call `CreateDocument()` (engine or execution context) to create a new document of the default document type.
- Call `CreateDocument<TDocument>()` (engine or execution context) to create a new document of the specified document type.
- Call `CloneOrCreateDocument()` (engine or execution context) to either clone _or_ create a new document of the default document type depending on if a passed-in document exists (is `null`) or not.
- Call `CloneOrCreateDocument<TDocument>()` (engine or execution context) to either clone _or_ create a new document of the specified document type depending on if a passed-in document exists (is `null`) or not.

Statiq is very flexible with what can be considered a document. You may find that a custom document type better represents your data than creating a standard document. If you already have an existing data element (such as the result of an API call), it might also be helpful to wrap that object as a document instead of copying it’s data to a default document object. Follow these guidelines and tips when working with alternate document types:

- Use base classes:
  - Implementing `IDocument` is the minimum requirement, but it’s not recommended to implement this interface directly.
  - Override `Document<TDocument>` to derive a custom document type with built-in metadata support.
  - Override `IDocument.Clone()` in custom document types as needed. The default behavior is to perform a member-wise clone.
- Convert an existing object of any type into a `IDocument` using `.ToDocument()` extensions:
  - This wraps the object in an `ObjectDocument<T>`.

# Document Properties

In addition to metadata, every document has a few core properties.

## Document ID

Every document has an `Id` property. This is a `Guid` that uniquely identifies the document within a given execution. Once a document is created every cloned copy of that document, regardless of whether the content or metadata is changed, will have the same ID. This lets you identify the "same" document even after it's been cloned a number of times.

## Source and Destination

All documents have two properties that relate to file location: `Source` and `Destination`. `Source` is an absolute path and indicates where on disk the document came from (assuming it came from disk). `Destination` is a relative path and indicates where in the output folder the document should be written. Not all documents are intended for output (some are just for conveying data), so not all documents will have a source or destination property.

## Content Provider

The content of a document is accessed through a content provider (an instance of `IContentProvider`). This lets the framework control access to content and ensures a consistent experience regardless of content source. You should provide a content provider when creating or cloning a document if you want to set it's content. The most common way of getting a content provider for a particular type of content (such as a `string` or a `Stream`) is to call one of the `GetContentProvider()` methods from the current [execution context](xref:execution-context).

# Accessing Documents

During execution you can access all the documents generated by each pipeline using the `IPipelineOutputs` interface, which is available via the `Outputs` property of the current [execution context](xref:execution-context). You can also access documents generated by a given pipeline using various modules such as `ConcatDocuments` which are useful when setting up multi-pipeline document flows.