Order: 1
---
Configurators are classes that can manipulate the [engine](xref:execution#engine), [bootstrapper](xref:bootstrapper), and other items at startup.

Configurators are used internally by the [bootstrapper](xref:bootstrapper) and while you generally won't need to use them directly, you can create your own configurators by implementing `IConfigurator<TConfigurable>` where `TConfigurable` is a type that implements `IConfigurable`. The following types currently implement `IConfigurable`:

- `Bootstrapper`: the configurator can directly configure the current bootstrapper.
- `Engine`: the configurator can directly configure the current engine.
- `ConfigurableCommands`: used to configure the [commands](xref:commands).
- `ConfigurableConfiguration`: used to configure the `IConfigurationBuilder` that maps [configuration files](xref:settings#configuration-files) to settings.
- `ConfigurableServices`: used to configure the `IServiceCollection`.
- `ConfigurableSettings`: used to configure [settings](xref:settings).

The `IConfigurator<TConfigurable>` interface contains a single `void Configure(TConfigurable configurable)` method that provides an instance of the configurable object.

Because the bootstrapper [automatically registers all implementations of `IConfigurator<Bootstrapper>` and `IConfigurator<IBootstrapper>`](xref:bootstrapper#default-behavior), those types of configurators can be particularly helpful in extension scenarios where you have an library that should operate on whatever bootstrapper loads it.
