Order: 3
---
Events let you hook into various stages of the generation process and can be helpful when you need to implement cross-cutting behavior at runtime or when you need to modify the behavior of existing [pipelines](xref:pipelines-and-modules). Statiq has a global event mechanism that makes it easy to subscribe to and handle events.

You can subscribe to an event in an [engine](xref:execution#engine) through the `Events` property:

```csharp
engine.Events.Subscribe<BeforeModuleExecution>(
  evt => evt.Context.LogInformation("I’m in a module!"));
```

You can also subscribe to an event using the [bootstrapper](xref:bootstrapper):

```csharp
await Bootstrapper
  .Factory
  .CreateDefault(args)
  .SubscribeEvent<BeforeModuleExecution>(
    evt => evt.Context.LogInformation("I’m in a module!"))
  // ...
  .RunAsync();
```

Events are represented by an _event object_ which doesn’t have to follow any pattern or derive from any special base class. To expose your own events, create an object that will represent the event and it’s data and then raise subscribers through the [execution context](xref:execution#execution-context):

```csharp
await context.Events.RaiseAsync(new MyEvent("some data"));
```

All subscribers to the `MyEvent` object will be invoked in the order in which they were subscribed.

Some of the events Statiq Framework supports are:

- `BeforeEngineExecution` - raised before the engine executes pipelines.
- `AfterEngineExecution` - raised after the engine executes pipelines.
- `BeforePipelinePhaseExecution` - raised before a pipeline phase is executed.
- `AfterPipelinePhaseExecution` - raised after a pipeline phase is executed.
- `BeforeModuleExecution` - raised before a module is executed and provides an opportunity to "short-circuit" the module and provide alternate output documents.
- `AfterModuleExecution` - raised after a module has executed and provides an opportunity to further operate on output documents or provide alternate output documents.
