Order: 7
---
Archives are one of the most powerful features of Statiq Web and allow you to group and page content.

An archive is defined as a normal [template](xref:web-templates) that contains special metadata. When Statiq Web sees the metadata below it will process the file as an archive and produce the archive outputs. Archives are processed in the `Archive` [pipeline](xref:pipelines-and-modules).

There are two types of archive pages that the pipeline can produce from a single archive file: _indexes_ and _groups_. To illustrate the difference, consider a blog where each post contains one or more tags. You probably want a single _index_ page that lists all the available tags. You probably also want individual _group_ pages for each tag that lists the blog posts containing that tag, and you may want those lists of posts to be paginated and ordered.

A single archive file can produce both types of output documents. The template in the file is used for both, and you can tell whether an index or a group is being rendered by whether or not the document contains the `GroupKey` metadata key which contains the group value. For example, in a Razor archive template you might have the following:

``` html
@if (Document.ContainsKey(Keys.GroupKey))
{
  <div>
    <!-- Render a group page -->
  </div>
}
else
{
  <div>
    <!-- Render an index page -->
  </div>
}
```

In a group page the value of `GroupKey` contains the group value (for example, the tag) and the child documents (accessible with `Document.GetChildren()`) contains each item (for example, each blog post for the tag).

In an index page the child documents contains each of groups (for example, each tag).

The following metadata controls the archive and should be placed in the [front matter](xref:front-matter) of the archive template:

- `ArchivePipelines`: The pipeline(s) to get documents for the archive from. Defaults to the `Content` pipeline if not defined.
- `ArchiveSources`: A globbing pattern to filter documents from the archive pipeline(s) based on source path (or all documents from the pipeline(s) if not defined).
- `ArchiveFilter`: An additional metadata filter for documents from the archive pipeline(s) that should return a `bool`.
- `ArchiveKey`: The key to use for generating archive groups. The source documents will be grouped by the key value(s). If this is not defined, only a single archive index with the source documents will be generated.
- `ArchiveKeyComparer`: Should return a comparer to be used for comparing archive keys. For example, use `ArchiveKeyComparer: => StringComparer.OrdinalIgnoreCase.ToConvertingEqualityComparer()` to convert the value of archive keys to a string and compare them using a case-insensitive ordinal string comparison.
- `ArchivePageSize`: The number of items on each group page (or all group items if not defined). The current page index is stored in the `Index` metadata value.
- `ArchiveTitle`: The title of each group output document. This is usually a [computed value](xref:metadata-values#computed-values) that calculates the title based on the group key. If this value is not specified, the default title will be "[Archive Title] - [Group Key] (Page [Index (If Paged)])".
- `ArchiveDestination`: The destination path of each group output document. This is usually a [computed value](xref:metadata-values#computed-values) that calculates the destination based on the group key. If this value is not specified, the default group destination will be "[Archive File Path]/[Archive File Name]/[Group Key]/[Index (If Paged)].html". The destination path of the archive index follows normal destination calculation and will be placed at the same relative path as the archive file or can be changed with metadata like `DestinationPath`.
- `ArchiveOrderKey`: The metadata key that sorting should be based on.
- `ArchiveOrderDescending`: Indicates the archive should be sorted in descending order.
