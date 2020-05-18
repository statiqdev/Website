Documents from the `Content` pipeline are structured in a hierarchy that makes it easy to navigate them similar to their location on disk.

When you get documents from the `Content` pipeline, for example by using `Outputs["Content"]`, the return value is likely just a single document or a small number of root documents. Each of the return documents potentially contains child documents (usually in a `Children` metadata value), and those child document may contain child documents to form a tree.

Documents with a name of `index.*` are considered to be the parent of other documents in their directory. If no index document exists in a directory, a "placeholder" parent document is created which can be identified by having a `TreePlaceholder` metadata value.

# Navigating The Tree

A number of `IDocument` extension methods exist to help navigate the document hierarchy:

- `GetParent()`: Gets the parent document of a given document.
- `HasChildren()`: Returns if the document has any child documents.
- `GetChildren()`: Gets the child documents of a given document.
- `GetDescendants()`: Gets all descendant documents of a given document.
- `GetDescendantsAndSelf()`: Gets all descendant documents of a given document including the given document.

The methods above that return a collection (including the return value of getting documents from a pipeline like `Outputs["Content"]` all return a `DocumentList<IDocument>`. `DocumentList<IDocument>` exposes an indexer that filters documents by a destination pattern:

```csharp
IDocument result = Outputs["Content"]["first/second/third.html"];
```

# Flattening

There are many times when instead of a hierarchy you'd rather have a flat collection of documents. Calling `Flatten()` on a collection of documents will collapse the entire tree into a flat sequence. Note that each document will still contain it's children, it's just that they'll all be part of the result sequence.