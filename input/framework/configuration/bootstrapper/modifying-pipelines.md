Order: 6
---
Sometimes you may want to modify an existing [pipeline](xref:pipelines-and-modules). This is particularly helpful in cases like [Statiq Web](xref:web) where you have an existing set of pipelines that are defined by someone else and want to tweak their behavior by adding, removing, or changing the modules they contain.

The bootstrapper provides a method for this purpose:

- `ModifyPipeline(string, Action<IPipeline>)`

This method take the name of a pipeline and allows you to provide an action that can manipulate the `IPipeline` with that name. The `IPipeline` interface contains `ModuleList` properties (essentially `IList<IModule>`) for each [pipeline phase](xref:pipelines-and-modules#phases).