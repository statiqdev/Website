Order: 5
---
By default the [bootstrapper](xref:bootstrapper) will automatically add all pipelines by type in the entry assembly via reflection, but you can also use it to add or define new pipelines.

It contains several methods to add additional pipelines by either type or as an instance:

- `AddPipeline<TPipeline>()` will add a pipeline by type.
- `AddPipeline(IPipeline)` and various overloads will add a pipeline instance.

## Finding Pipelines By Reflection

The bootstrapper also includes the ability to use reflection to find and add pipeline types:

- `AddPipelines()` will add all pipelines defined in the entry assembly.
- `AddPipelines(Assembly)` will add all pipelines defined in the specified assembly.
- `AddPipelines(Type)` will add all pipelines defined as nested classes in the specified parent type.
- `AddPipelines<TParent>()` will add all pipelines defined as nested classes in the specified parent type.

## Adding Directly

You can add pipelines to the `IPipelineCollection` directly using the following extensions:

- `AddPipelines(Action<IPipelineCollection>)`
- `AddPipelines(Action<IReadOnlyConfigurationSettings, IPipelineCollection>)`

## Defining Pipelines

In addition to adding pipelines defined as classes, the bootstrapper also has a robust set of extensions for directly defining pipelines:

- `AddPipeline(...)` overloads will directly define a pipeline.
- `AddSerialPipeline(...)` overloads will directly define a pipeline that has dependencies on all other currently defined pipelines.
- `AddIsolatedPipeline(...)` overloads will directly define an [isolated pipeline](xref:pipelines-and-modules#isolated).
- `AddDeploymentPipeline(...)` overloads will directly define a [deployment pipeline](xref:pipelines-and-modules#deployment).

Each of these methods have overloads that allow you to:

- Specify a collection of [modules](xref:about-modules) to be executed during the [process phase](xref:pipelines-and-modules#process-phase) or other [phases](xref:pipelines-and-modules#phases).
- Specify files to read during the [input phase](xref:pipelines-and-modules#input-phase).
- Specify how to write files during the [output phase](xref:pipelines-and-modules#output-phase).
- Specify pipeline dependencies to other pipelines.

## Pipeline Builder

The bootstrapper also provides a fluent API specifically for defining pipelines using a "builder" style:

- `BuildPipeline(string, Action<PipelineBuilder>)` specifies a pipeline name and a delegate that uses a new `PipelineBuilder`.

The `PipelineBuilder` includes a number of fluent methods for defining different parts of your pipeline:

- `WithInputReadFiles()` overloads define files to read during the [input phase](xref:pipelines-and-modules#input-phase).
- `WithOutputWriteFiles()` overloads define how to write files during the [output phase](xref:pipelines-and-modules#output-phase).
- `AsSerial()` indicates that the pipeline should have dependencies on all other currently defined pipelines.
- `AsIsolated()` indicates that the pipeline is an [isolated pipeline](xref:pipelines-and-modules#isolated).
- `AsDeployment()` indicates that the pipeline is a [deployment pipeline](xref:pipelines-and-modules#deployment).
- `WithInputModules()` adds modules to the [input phase](xref:pipelines-and-modules#input-phase).
- `WithProcessModules()` adds modules to the [process phase](xref:pipelines-and-modules#process-phase).
- `WithPostProcessModules()` adds modules to the [post-process phase](xref:pipelines-and-modules#post-process-phase).
- `WithOutputModules()` adds modules to the [output phase](xref:pipelines-and-modules#output-phase).
- `WithInputConfig()` uses a [configuration delegate](xref:configuration-delegates) to define the output of the [input phase](xref:pipelines-and-modules#input-phase).
- `WithProcessConfig()` uses a [configuration delegate](xref:configuration-delegates) to define the output of the [process phase](xref:pipelines-and-modules#process-phase).
- `WithPostProcessConfig()` uses a [configuration delegate](xref:configuration-delegates) to define the output of the [post-process phase](xref:pipelines-and-modules#post-process-phase).
- `WithOutputConfig()` uses a [configuration delegate](xref:configuration-delegates) to define the output of the [output phase](xref:pipelines-and-modules#output-phase).
- `WithDependencies()` defines the dependencies for the pipeline.
- `WithExecutionPolicy()` indicates which [execution policy](xref:pipelines-and-modules#execution-policy) the pipeline should use.
- `ManuallyExecute()` indicates the pipeline should use the [manual execution policy](xref:pipelines-and-modules#manual).
- `AlwaysExecute()` indicates the pipeline should use the [always execution policy](xref:pipelines-and-modules#always).

To complete building the pipeline call `Build()`:

```csharp
using System;
using Statiq.App;

namespace MyGenerator
{
  public class Program
  {
    public static async Task<int> Main(string[] args) =>
      await Bootstrapper
        .Factory
        .CreateDefault(args)
        .BuildPipeline("Render Markdown", builder => builder
            .WithInputReadFiles("*.md")
            .WithProcessModules(new RenderMarkdown())
            .WithOutputWriteFiles(".html"))
        .RunAsync();
  }
}
```
