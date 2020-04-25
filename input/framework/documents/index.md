Title: Documents and Metadata
Order: 2
---
Documents and their metadata are the primary way information is passed though [pipelines and modules](xref:pipelines_and_modules).

A *document* is a combination of *content* and *[metadata](xref:metadata)* as it makes it’s way through the system. Document content is often read from files, though it can also come from a database, a web service, or any other source. Documents are immutable and must be cloned to change their content and add or change their metadata.

Along with their content, every [document](/framework/concepts/documents) also contains *metadata*. As with documents, metadata is immutable and you must clone a document to add additional metadata. Several [modules](/framework/concepts/modules), such as `SetMetadata`, are designed to allow you to manipulate document metadata as part of your [pipeline](/framework/concepts/pipelines).

