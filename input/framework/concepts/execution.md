Order: 5
Description: Execution is when pipelines and the modules they contain are run.
---
# Engine

When pipelines in Statiq are executed, a special object called the `Engine` is responsible for coordinating activity. The engine contains all the global objects used to perform generation such as the [file system abstraction](/framework/concepts/files). It also creates the [pipeline dependency graph](/framework/concepts/pipelines#concurrency-and-dependencies) and executes the [modules](/framework/concepts/modules) in each pipeline.

# Execution Context

The engine creates an _execution context_ for pipeline and module. It can be used to access the current input documents to the module, the output documents from other pipelines, global objects from the engine, and a variety of utility functionality. The current execution context is generally accessed through [configuration delegates](/framework/concepts/modules#configuration) and the [module execution method](/framework/extensibility/modules).