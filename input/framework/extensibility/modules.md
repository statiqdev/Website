Description: Writing your own modules is easy!
---
[Modules](/framework/concepts/modules) are the basic building blocks of Statiq functionality and writing your own is a good way to easily expand the capabilities of your generator application. Modules implement the `IModule` interface which defines a single `Task<IEnumerable<IDocument>> ExecuteAsync(IExecutionContext context)` method. The execution context passed to the `ExecuteAsync` method contains the input documents to the module as well as providing access to output documents from other pipelines and various engine and utility functionality.

While you could implement this interface directly, in practice most modules are derived from a number of different base module classes:

TODO

Note that you should never call `IModule.ExecuteAsync(...)` directly. Instead, if you need to execute a module directly, you should call one of the `IExecutionContext.Execute(...)` overloads to execute the module through an existing execution context and engine.