Order: 4
---
Files with a `.csx` or `.cs` extension are processed as a script. Similar to [computed values](xref:metadata-values#computed-values), full scripts also provide access to a number of predefined global properties (see the `ScriptBase` class in `Statiq.Core` for all script properties):

- `ExecutionState` contains the current `IExecutionState` object.
- `Context` (and the `ctx` shorthand) contain the current [execution context](xref:execution#execution-context).
- `Document` (and the `doc` shorthand) contain the current [document](xref:documents-and-metadata).
- `PipelineName`: Gets the name of the currently executing pipeline.
- `Pipeline`: Gets the currently executing pipeline.
- `Phase`: Gets the currently executing [phase](xref:pipelines-and-modules#phases) of the current pipeline.
- `Parent`: Gets the parent execution context if currently in a nested [module](xref:about-modules).
- `Module`: Gets the current executing [module](xref:about-modules).
- `Inputs`: The collection of input [documents](xref:documents-and-metadata) to the current [module](xref:about-modules).

# Front Matter

Scripts support a special syntax for [front matter](xref:front-matter). In addition to the standard `---` front matter delimiter, a script can also delimit front matter using a comment block starting on the first line:

```csharp
/*
Title: My Page
*/
return "This is my page!";
```

# Return Values

The script will behave differently depending on the value(s) it returns:

- If the return value is `null`, a document representing the current file will be output to the pipeline.
- If the return value is a `IDocument`, `IEnumerable<IDocument>`, or `IAsyncEnumerable<IDocument>` the returned document(s) will be output to the pipeline.
- If the return value is `IEnumerable<KeyValuePair<string, object>>`, the document will be cloned with the returned items as metadata and output to the pipeline.
- If the return value is a `IEnumerable<IModule>` or `IModule`, the module(s) will be executed and the results will be output to the pipeline.
- If the return value is an `IContentProvider`, `IContentProviderFactory`, or `Stream` the document will be output to the pipeline with new content.
- If the return value is anything else, the content of the document will changed to the string value and output to the pipeline.