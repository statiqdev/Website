Order: 1
---
Pipelines are classes that implement the `IPipeline` interface, though you typically create one by deriving from the abstract `Pipeline` class which initializes several defaults for you.

Most pipeline configuration happens in the pipeline’s constructor by editing properties of the base `Pipeline`, though you can add whatever extra capabilities or methods you want.

# Specifying Dependencies

Two properties can be used to specify dependency data of the pipeline:

- `Dependencies` is a `HashSet<string>` that contains the names of pipelines that the current one depends on. In other words, any pipeline name in the `Dependencies` collection will be executed before the current pipeline is executed. If the current pipeline is specified as the only executing pipeline (for example, on the [command line](xref:command-line-interface)) then it’s dependencies (and their dependencies) will also be executed.

- `DependencyOf` is a `HashSet<string>` that defines pipelines that this pipeline is a dependent of. In other words, it’s the opposite of the `Dependencies` collection and indicates the current pipeline should be executed before those specified in the `DependencyOf` collection. This property is particularly valuable when you’re trying to add behavior before pre-configured pipelines such as in [Statiq Web](/web).

# Other Settings

In addition to specifying dependencies, other settings can be configured from the pipeline constructor:

- `Isolated` is a `bool` that indicates if the pipeline is an [isolated pipeline](xref:pipelines#isolated). This means that it will be executed independent of any other pipelines.

- `Deployment` is a `bool` that indicates if the pipeline is a [deployment pipeline](xref:pipelines#deployment) and should be executed when using the deployment command.

- `ExecutionPolicy` specifies the particular [execution policy](xref:pipelines#execution-policy) the pipeline should use.

# Adding Modules

Modules can be added to the pipeline through four `ModuleList` properties (which are essentially `IList<IModule>`) corresponding to the different [pipeline phases](xref:pipelines#phases):

- `InputModules`

- `ProcessModules`

- `PostProcessModules`

- `OutputModules`

You can either add modules individually (the properties are initialized with an empty list) or create a new `ModuleList` and set the property to that. Note that modules are added as instances, so adding a `ReadFiles` module to the input phase of a pipeline might look like:

```csharp
public class Content : Pipeline
{
  public Content()
  {
    InputModules = new ModuleList
    {
      new ReadFiles("**/*.md")
    };

    // Other pipeline configuration
  }
}
```

# Service Injection

If the pipeline is added by type (see [Using The Bootstrapper](#using-the-bootstrapper) below), any services that were [registered with the dependency injection container](xref:bootstrapper#registering-services) will be available to the constructor and you can inject them.

# Using The Bootstrapper

The easiest way to add pipelines to your application is through [the bootstrapper](xref:bootstrapper). By default it will automatically add all pipelines by type in the entry assembly via reflection.

It contains several methods to add additional pipelines by either type or as an instance:

- `AddPipeline<TPipeline>()` will add a pipeline by type (and enable [service injection](#service-injection)).

- `AddPipeline(IPipeline)` and various overloads will add a pipeline instance.

## Finding Pipelines By Reflection

TODO

## Defining Directly

TODO

## Pipeline Builder

TODO - using the pipeline builder to create pipelines