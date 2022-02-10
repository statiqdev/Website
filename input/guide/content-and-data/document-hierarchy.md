Order: 8
---
The structuring of documents in a hierarchy is an important concept to understand since it helps navigate documents to create components like breadcrumbs or navigation bars.

Documents can be structured as trees in a few different ways:
- Based on destination path by using `Outputs` or `OutputPages` directly (more on this below).
- Based on source path by using `Outputs.AsSourceTree()`.
- Based on metadata stored in a key like `Children` by using `Outputs.AsMetadataTree()`.

It's important to understand when you should use each of these techniques. The different between a tree based on destination paths and one based on document metadata is particularly important. The former should be used when you want to get a full picture of your site and it's content, data, and other documents. The latter should be used when you have one or more documents that have "child" documents which may or may not be related to their output destination (such as a list of blog posts from an [archive](xref:archives)).

# Destination Tree

When you use certain methods of the `Outputs` property of the [execution context](xref:execution-context), the results are based on a tree derived from the destination path of documents. In other words, it's based on what the site will look like once it's output. This includes the following members of `Outputs`:

- `GetParentOf()`: Gets the parent document of a given document.
- `GetChildrenOf()`: Gets the child documents of a given document.
- `GetDescendantsOf()`: Gets all descendant documents of a given document.
- `GetSiblingsOf()`: Gets all siblings of a given document.
- `GetAncestorsOf()`: Gets all ancestors of a given document, the closest being first.
- The indexer (`Outputs[...]`): Searches the tree using [globbing pattern(s)](xref:files-and-paths#globbing).

It's often desirable to filter the outputs and output tree just to "pages" (I.e. documents that output to ".html" or ".htm"). For example, when creating a navigation bar, you only want pages and not data files, resources, images, etc. to appear. In this case you can use the `OutputPages` property which is pre-filtered to those pages and has the same methods as above.

Documents with a name of `index.html` are considered to be the parent of other documents in their directory.

# Source Tree

You may want to find documents and navigate their hierarchy based on source path, in which case you can call `Outputs.AsSourceTree()`. The resulting object contains all of the methods described above, but it operates on document source paths.

# Metadata Tree

Some modules and pipelines (like [archives](xref:archives)) add metadata to documents to represent "children". For example, a blog archive might contain all blog posts as children of the archive document, but those individual posts might be going to entirely different locations on disk (such as when creating date-based URL slugs like "/2019/04/01/my-post"). In this case you want a tree structure that maps to the metadata of the documents and not their destination or source path.

You can create such as tree from all documents using `Outputs.AsMetadataTree()` or any other sequence of documents using `IEnumerable<IDocument>.AsMetadataTree()`. The methods on this tree are the same as those described above. For a single document you can get it's children `IDocument.GetChildren()`.

## Flattening A Metadata Tree

There are many times when instead of a hierarchy you'd rather have a flat collection of documents. Calling `Flatten()` on a collection of documents that contain child documents via metadata will collapse the entire metadata-based tree into a flat sequence. Note that each document will still contain it's children, it's just that they'll all be part of the result sequence.