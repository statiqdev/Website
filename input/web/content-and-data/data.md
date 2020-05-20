Order: 2
---
In addition to [content](xref:web-content), Statiq Web has rich support for data files.

Data files should be placed in the `input` folder (or a sub-folder) alongside content and are processed based on their media type (which is determined by file extension). The following extensions are recognized by default:

- `.json` processes the file as JSON.
- `.yaml` and `.yml` processes the file as YAML.

Regardless of format the data files are parsed and the data in them is added as [metadata](xref:documents-and-metadata#about-metadata) to the resulting documents.

The `DataFiles` [setting](xref:web-settings) controls how data files are located and is set to `**/{!_,}*.{json,yaml,yml}` by default. This loads all `.json`, `.yaml`, and `.yml` files in any input directory unless it starts with an underscore `_`.

# Accessing Data Files

Data files are processed by the `Data` pipeline and can be accessed through the `Outputs` property of the [execution context](xref:execution-context) (which is also available directly in some templating languages like [Razor](xref:template-languages#razor) as an `Outputs` property).

Unlike [content files](xref:web-content), the resulting data documents are not output from the pipeline in a [hierarchy](xref:web-content-hierarchy) and all documents regardless of depth are part of the collection obtained from the `Outputs` without requiring the use of `.Flatten()`. You can also use `.FilterSources()` and `.FilterDestinations()` extension methods to filter the collection of output documents using a [globbing pattern](xref:files-and-paths#globbing).

For example, the following code will find all documents containing data from YAML files that describe different fruits stored in a `food/fruit` folder under the default `input` folder:

```csharp
IDocument[] fruits = Outputs["Data"]
  .FilterSources("food/fruit/*")
  .ToArray();
```

# Outputting Data Files

By default data files are not output. You may want to adjust this behavior for individual data files or entire directories. This behavior can be controlled with the `ShouldOutput` metadata and there are a few ways to set it:

- Set `ShouldOutput` to `true` directly in the data file. This will result in the `ShouldOutput` value being present in the output file, but that's not always a problem.
- Set `ShouldOutput` to `true` for an individual file, and entire directory, or an entire directory tree in a [directory metadata](xref:web-directory-metadata) file.
- Set `ShouldOutput` to `true` in a [sidecar](xref:web-sidecar-files) file.
- Set `ShouldOutput` to `true` in [front matter](xref:web-front-matter).
