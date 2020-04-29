By default the Bootstrapper will automatically add all pipelines by type in the entry assembly via reflection, but you can also use it to add or define new pipelines.

It contains several methods to add additional pipelines by either type or as an instance:

- `AddPipeline<TPipeline>()` will add a pipeline by type (and enable [service injection](#service-injection)).

- `AddPipeline(IPipeline)` and various overloads will add a pipeline instance.

## Finding Pipelines By Reflection

TODO

## Defining Directly

TODO

## Pipeline Builder

TODO - using the pipeline builder to create pipelines