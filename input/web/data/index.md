Order: 3
---
In addition to [content](xref:web-content), Statiq Web has rich support for data files.

Data files should be placed in the `input` folder (or a sub-folder) alongside content and are processed based on their media type (which is determined by file extension). The following extensions are recognized by default:

- `.json` processes the file as JSON.
- `.yaml` and `.yml` processes the file as YAML.

Regardless of format the data files are parsed and the data in them is added as [metadata](xref:documents-and-metadata#about-metadata) to the resulting documents.

The `DataFiles` [setting](xref:web-settings) controls how data files are located and is set to `**/{!_,}*.{json,yaml,yml}` by default.

# Accessing Data Files

Data files are processed by the `Data` pipeline and can be accessed through the `Outputs` property of the [execution context](xref:execution-context) (which is also available directly in some templating languages like [Razor](xref:template-languages#razor).

The resulting data documents are output from the pipeline in a hierarchy, so only documents representing top-level folders are returned from the `Outputs` collection. To flatten the documents and filter for a set of ones you want you'll need to call `.Flatten()` first.

For example, the following code will find all documents containing data from YAML files that describe different fruits stored in a `food/fruit` folder under the default `input` folder:

```csharp
IDocument[] fruits = Outputs["Data"]
  .Flatten()
  .FilterSources("food/fruit/*")
  .ToArray();
```