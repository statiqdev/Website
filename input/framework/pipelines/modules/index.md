Title: About Modules
Order: 3
---
A module is a component that performs a specific action, usually by creating, processing, or manipulating documents.

A module is a small single-purpose component that takes [documents](xref:documents-and-metadata) as input, does something based on those documents (possibly transforming them), and outputs documents as a result of whatever operation was performed. Modules are typically chained in a sequence called a [pipeline](xref:pipelines-and-modules).

# Concurrency

Some modules process documents in parallel while others process them sequentially. Modules that process documents in parallel can often be forced to process documents sequentially by using the `WithParallelExecution(false)` extension method. This might be useful when the documents being processed or a configuration setting aren't thread-safe.