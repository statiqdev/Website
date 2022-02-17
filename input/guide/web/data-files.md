Order: 3
Badge: Web
---
In addition to [content](xref:content-files), Statiq Web has rich support for data files.

Data files should be placed in the `input` folder (or a sub-folder) alongside content and are processed based on their media type (which is determined by file extension). The following extensions are recognized by default:

- `.json` processes the file as JSON.
- `.yaml` and `.yml` processes the file as YAML.

Regardless of format the data files are parsed and the data in them is added as [metadata](xref:documents-and-metadata#about-metadata) to the resulting documents.

# Accessing Data Files

Data files are processed by the `Data` pipeline and can be accessed through the `Outputs` property of the [execution context](xref:execution-context) (which is also available directly in some templating languages like [Razor](xref:template-languages#razor) as an `Outputs` property).

For example, the following code will find all documents containing data from YAML files that describe different fruits stored in a `food/fruit` folder under the default `input` folder:

```csharp
IDocument[] fruits = Outputs
  .FromPipeline("Data")
  .FilterSources("food/fruit/*")
  .ToArray();
```

# Outputting Data Files

By default data files are not output. You may want to adjust this behavior for individual data files or entire directories. This behavior can be controlled with the `ShouldOutput` metadata and there are a few ways to set it:

- Set `ShouldOutput` to `true` directly in the data file. This will result in the `ShouldOutput` value being present in the output file, but that's not always a problem.
- Set `ShouldOutput` to `true` for an individual file, and entire directory, or an entire directory tree in a [directory metadata](xref:directory-metadata) file.
- Set `ShouldOutput` to `true` in a [sidecar](xref:sidecar-files) file.
- Set `ShouldOutput` to `true` in [front matter](xref:front-matter).