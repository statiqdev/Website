Order: 1
---
Content files contain the pages of your site and are processed based on their media type (which is determined by file extension).

The following extensions are recognized by default:

- `.html` processes the file as plain HTML.
- `.md` processes the file as [Markdown](xref:template-languages#markdown).
- `.cshtml` processes the file as [Razor](xref:template-languages#razor).

The `ContentFiles` [setting](xref:web-settings) controls how content files are located and is set to `**/{!_,}*.{html,cshtml,md}` by default. This loads all `.html`, `.cshtml`, and `.md` files in any input directory unless it starts with an underscore `_`.

In all cases, global processing (such as processing [front matter](xref:web-front-matter) and applying [shortcodes](xref:web-shortcodes)) is performed and [layouts](xref:web-templates#layouts) and [themes](xref:web-themes) are applied.

Many [themes](xref:web-themes) treat content differently depending on what sub-folder the files are in. For example, themes for blogging often infer blog posts should go in a "input/posts" sub-folder. In most cases the specific paths used for different types of content are [configurable as settings](xref:web-settings).

# Accessing Content Files

Content files are processed by the `Content` pipeline and can be accessed through the `Outputs` property of the [execution context](xref:execution-context) (which is also available directly in some templating languages like [Razor](xref:template-languages#razor) as an `Outputs` property).

The resulting content documents are output from the pipeline in a [hierarchy](xref:web-content-hierarchy), so only documents representing top-level folders are returned from the `Outputs` collection. To flatten the documents and filter for a set of ones you want you'll need to call `.Flatten()` first. You can also use `.FilterSources()` and `.FilterDestinations()` extension methods to filter the collection of output documents using a [globbing pattern](xref:files-and-paths#globbing).

For example, the following code will find all content documents that describe different fruits stored in a `food/fruit` folder under the default `input` folder:

```csharp
IDocument[] fruits = Outputs["Data"]
  .FilterSources("food/fruit/*")
  .ToArray();
```

# Preventing Content File Output

By default content files are output to their destination path. You may want to adjust this behavior for individual content files or entire directories. This behavior can be controlled with the `ShouldOutput` metadata and there are a few ways to set it:

- Set `ShouldOutput` to `false` directly in the data file which will prevent that file from being output.
- Set `ShouldOutput` to `false` for an individual file, and entire directory, or an entire directory tree in a [directory metadata](xref:web-directory-metadata) file.
- Set `ShouldOutput` to `false` in a [sidecar](xref:web-sidecar-files) file.
- Set `ShouldOutput` to `false` in [front matter](xref:web-front-matter).

Preventing content file output can be helpful when you want to define partial content (like product descriptions, personal biographies, etc.) for inclusion in other documents without resulting in an output file.
