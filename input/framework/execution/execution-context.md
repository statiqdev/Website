Order: 2
---
While executing [pipelines and modules](xref:pipelines-and-modules), the current state and other functionality is passed in an object called the execution context.

The execution context contains lots of information such as the current pipeline, phase, and module, the settings and file system, the input documents to the module, and more. The current execution context is generally provided by [configuration delegates](xref:configuration-delegates) or a [module execution method](xref:writing-modules).

# Interfaces

The execution context implements a number of interfaces that provide additional capabilities:

- `IMetadata`: All [settings](xref:settings) can be accessed directly through the execution context as metadata.

- `ILogger`: All logging should be performed through the execution context, which tracks the current pipeline and module and logs them out along with your messages.

- `IDocumentFactory`: The execution context can be used to create new documents.

- `IServiceProvider`: allows you to use the execution context as a dependency injection service provider.

# Properties

In addition to directly implementing these interfaces, the execution context also contains a number of useful properties:

- `ExecutionId`: Uniquely identifies the current execution cycle which can be helpful for managing caches or resetting data.

- `CancellationToken`: Should be used whenever by possible by modules and other code to provide cancellation support.

- `ApplicationState`: Contains initial application state data like the original command line arguments.

- `ClassCatalog`: A catalog of all classes in all referenced assemblies.

- `Events`: The collection of registered [events](xref:events).

- `FileSystem`: The current [file system](xref:files-and-paths#virtual-file-system) abstraction.

- `Settings`: The application [settings](xref:settings) as metadata (even though this is also exposed directly by the execution context by implementing `IMetadata`).

- `Shortcodes`: A read-only collection of all available [shortcodes](xref:shortcodes).

- `Namespaces`: Gets a set of namespaces that should be brought into scope for modules that perform dynamic compilation.

- `MemoryStreamFactory`: Provides pooled memory streams (via the `RecyclableMemoryStream` library).

- `Outputs`: Gets the collection of outputs from all previously processed [pipelines](xref:pipelines-and-modules).

- `Services`: Gets the dependency injection service provider (even though this is also exposed directly by the execution context by implementing `IServiceProvider`).

- `ScriptHelper`: Gets a helper that can compile and evaluate C# scripts.

- `Pipelines`: Gets a read-only collection of all defined [pipelines](xref:pipelines-and-modules).

- `PipelineName`: Gets the name of the currently executing pipeline.

- `Pipeline`: Gets the currently executing pipeline.

- `Phase`: Gets the currently executing [phase](xref:pipelines-and-modules#phases) of the current pipeline.

- `Parent`: Gets the parent execution context if currently in a nested [module](xref:about-modules).

- `Module`: Gets the current executing [module](xref:about-modules).

- `Inputs`: The collection of input [documents](xref:documents-and-metadata) to the current [module](xref:about-modules) (probably the most important property of the execution context).

# Current Execution Context

You can get the current execution context at any time by calling `IExecutionContext.Current`. This static default interface property is implemented using an `AsyncLocal<IExecutionContext>` so it always contains the correct execution context even across asynchronous calls.