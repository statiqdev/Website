Order: 5
Description: Execution is when pipelines and the modules they contain are run.
---
# Engine

When pipelines in Statiq are executed, a special object called the `Engine` is responsible for coordinating activity. The engine contains all the global objects used to perform generation such as the [file system abstraction](/framework/concepts/files). It also creates the [pipeline dependency graph](/framework/concepts/pipelines#concurrency-and-dependencies) and executes the [modules](/framework/concepts/modules) in each pipeline.

# Execution Context

While executing pipelines and modules, the current state and other functionality is passed in an instance of `IExecutionContext`. This object contains lots of information such as the current pipeline, phase, and module, the settings and file system, the input documents to the module, and more. The current execution context is generally accessed through [configuration delegates](/framework/concepts/modules#configuration) or a [module execution method](/framework/extensibility/modules).

The context also implements `IDocumentFactory` so it can be used to create documents, `ILogger` so it can be used for logging, and `IServiceProvider` so it can be used as a dependency injection service provider.
