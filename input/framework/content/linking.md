Title: Linking To Documents
---
Statiq is generally used to generate HTML and as such, it's often necessary to link from one document to another.

Linking is usually a little more complicated than just adding a relative path to an output file. For example, most static site hosts will hide an `index` file name and/or `.html` extensions from files and you probably want your links to reflect that. In addition, you may or may not want to include the full hostname and virtual path of your site.

There are a few different ways you can generate links:

- The [execution context](xref:execution-context) has a number of `.GetLink()` extension methods, including one that takes a [document](xref:documents-and-metadata).
- The `IDocument` interface also has a `.GetLink()` extension method that can return a link to a particular document.
- For the most control, including over which file names and extensions should be hidden, use the static `LinkGenerator` helper class.

# Link Settings

Several [global settings](xref:settings) help control default link generation:

- `Host`: The host to use when generating links.
- `LinksUseHttps`: Indicates if generated links should use HTTPS instead of HTTP as the scheme.
- `LinkRoot`: The default relative root path to use when generating links (for example "/virtual/directory").
- `LinkHideIndexPages`: Indicates whether to hide index pages by default when generating links.
  - When using the [bootstrapper](xref:bootstrapper) this defaults to `true`.
- `LinkHideExtensions`: Indicates whether to hide ".html" and ".htm" extensions by default when generating links.
  - When using the [bootstrapper](xref:bootstrapper) this defaults to `true`.
- `LinkLowercase`: Indicates that links should always be rendered in lowercase.