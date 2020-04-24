[Modules](xref:modules) are the basic building blocks of Statiq functionality. If the out-of-the-box modules don’t satisfy your use case, it’s easy to customize generation by creating new modules.

Modules implement the `IModule` interface which defines a single `Task<IEnumerable<IDocument>> ExecuteAsync(IExecutionContext context)` method. The [execution context](xref:execution#execution-context) passed to the `ExecuteAsync` method contains the input documents to the module as well as providing access to output documents from other pipelines and various engine and utility functionality.

While you can implement `IModule` easily enough yourself, in practice most modules are derived from a number of different base module classes:

- `Module` is a common module base class that's appropriate for most modules.
  - `ParallelModule` can be used when documents should be processed in parallel.
  - `SyncModule` can be used when the module execution should be synchronous (I.e. not `async`).
  - `ParallelSyncModule` can be used when documents should be processed in parallel but the module execution should be synchronous (I.e. not `async`).
- `ParentModule` can be used to execute child modules.
  - `SyncParentModule` can be used when the module execution should be synchronous (I.e. not `async`).
  - `ForEachDocument` can be used to execute child modules against each input document one at a time.
  - `ForAllDocuments` can be used to execute child modules against all documents and is very useful for grouping a sequence of child modules into a single parent module.
- `ChildDocumentsModule` can be used to execute child modules and then combine or manipulate input documents based on the output documents of the child modules.
  - `SyncChildDocumentsModule` can be used when the module execution should be synchronous (I.e. not `async`).
- `ConfigModule<TValue>` and `MultiConfigModule` can be used to manage `Config<TValue>` delegates for configuring the module (you can also manage configuration delegates yourself, these modules just make it a little easier).
  - `ParallelConfigModule<TValue>` and `ParallelMultiConfigModule` can be used when documents should be processed in parallel.
  - `SyncConfigModule<TValue>` and `SyncMultiConfigModule` can be used when the module execution should be synchronous (I.e. not `async`).
  - `ParallelSyncConfigModule<TValue>` and `ParallelSyncMultiConfigModule` can be used when documents should be processed in parallel but the module execution should be synchronous (I.e. not `async`).
- `ReadDataModule` can be used to convert data from arbitrary data sources into output documents.

When using the base module classes you should never call `IModule.ExecuteAsync(...)` directly. Instead, most of the module base classes above have both an `ExecuteContext` virtual method and/or an `ExecuteInput` virtual method.

- Overload the `ExecuteContext` method to have your code called once for all the input documents (available via `IExecutionContext.Inputs`). This is useful for modules that need to create new documents from scratch or that need to aggregate or operate on the input documents as a set.
- Overload the `ExecuteInput` method to have you code called once per document. This is useful when the module transforms or manipulates documents that are unrelated to each other.

Here are some other guidelines and tips to follow when writing a module:

- Consider using the built-in `ExecuteConfig` module instead of writing your own.
  - You may not even need a new module. The `ExecuteConfig` module lets you specify a delegate that can return documents, content, and other types of data which will be converted to output documents as appropriate.
- Use `Config<T>` [configuration delegates](xref:configuration_delegates).
  - If your module needs to accept user-configurable values, use `Config<T>`.
  - Consider using one of the base module classes that deals with `Config<T>` like `ConfigModule` or `MultiConfigModule`.
- Avoid document-to-document references (especially to/from children):
  - Try to avoid creating documents that reference other documents, especially in the top-level output documents (parent documents that reference children may be okay in some cases). If a document references another document and a following module clones the referenced document, the reference will still point to the old document and not the new clone.
- Preserve input ordering:
  - Many modules output documents in a specific order and following modules should preserve that order whenever possible. The base module classes do this by default, but any explicit parallel operations should preserve ordering as well (I.e., by calling `.AsParallel().AsOrdered()`).
- Only reference `Statiq.Common`:
  - If a module is in a separate assembly from your application you shouldn’t need a reference to `Statiq.Core`, and if you find that you do please open an issue so the appropriate functionality can be moved to `Statiq.Common`.
- Name modules using a VerbNoun convention when possible.
