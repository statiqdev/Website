Order: 4
---
Analyzers provide a mechansim to run validation checks, inspect documents, and otherwise report on the status of your content and data.

An analyzer implements `IAnalyzer` and is run on the output documents from one (or more) pipeline(s) and phase(s). Analyzers are run after each phase so exceptions propogate immediatly, but analyzer results are reported at the end of execution. An analyzer receives an `IAnalyzerContext` that includes the documents to be analyzed as `IAnalyzerContext.Inputs` an `AddAnalyzerResult(...)` method used to add analyzer messages.

# Writing Analyzers

Some base classes are provided that make writing analyzers easier:

- `Analyzer` is a basic analyzer that analyzes documents from one (or more) pipeline(s) and phase(s).
- `SyncAnalyzer` can be used when the analyze method should be synchronous (I.e. not `async`).

An analyzer defines which pipeline(s) and phase(s) it should be run after using the `PipelinePhases` property. Analyzers also define a default logging level their result messages should be logged as using the `LogLevel` property (which can be overriden, see below).

## Using The Bootstrapper

The [Bootstrapper](xref:bootstrapper) also includes some extension methods like `Analyze(...)` and `AnalyzeDocument(...)` that let you define analyzers as a delegate.

# Registering Analyzers

## Registering With The Bootstrapper

When using the [Bootstrapper](xref:bootstrapper) all analyzer classes in the entry assembly are instantiated and added with their default log level automatically.

The Bootstrapper also has a number of `AddAnalyzer(...)` methods that allow you to manually register analyzers and optionally define their log level.

## Registering In Settings And Metadata

You can also add analyzers by setting an `Analyzers`    [setting](xref:settings). For example, in a [configuration file](xref:settings#configuration-files) you can write:

```txt
Analyzers:
  - ValidateSomeMetadata
```
    
This will add the `ValidateSomeMetadata` analyzer (provided it exists within referenced assemblies). The analyzer will be added with it's default log level, but you can also specify a different log level by using an equal sign:

```txt
Analyzers:
  - ValidateSomeMetadata=Warning
```

This will register the `ValidateSomeMetadata` analyzer with a log level of `Warning`, or change the log level to `Warning` if the analyzer has previously been registered.

You can also change the log level of a registered analyzer on a per-document basis using metadata. For example, to disable the `ValidateSomeMetadata` analyzer for a prtocular document, place the following in the front matter of the document:

```txt
Analyzers:
  - ValidateSomeMetadata=None
```

## Registering On The Command Line

Analyzers can be registered and log levels set using the [command line](xref:command-line-interface) as well using either `-a` or `--analyzer` and the same `name=log-level` syntax. For example:

```
dotnet run -- -a ValidateSomeMetadata=Warning
```


