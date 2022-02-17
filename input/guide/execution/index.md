Order: 8
---
Execution happens when [pipelines and the modules they contain](xref:pipelines-and-modules) are evaluated.

# Engine

When pipelines in Statiq are executed, a special object called the engine is responsible for coordinating activity. While an engine can be instantiated directly, it's most typically created by using a [bootstrapper](xref:bootstrapper).

The engine contains all the global objects used to perform generation such as the [file system abstraction](xref:files-and-paths). It also creates the [pipeline dependency graph](xref:pipelines-and-modules#concurrency-and-dependencies) and executes the [modules](xref:about-modules) in each pipeline.