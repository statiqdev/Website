When pipelines in Statiq are executed, a special object called the engine is responsible for coordinating activity.

The engine contains all the global objects used to perform generation such as the [file system abstraction](xref:files_and_paths). It also creates the [pipeline dependency graph](xref:pipelines#concurrency-and-dependencies) and executes the [modules](xref:modules) in each pipeline.