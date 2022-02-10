Order: 1
---
The bootstrapper is the easiest way to configure an [engine](xref:execution#engine), add [pipelines and modules](xref:pipelines-and-modules) to it, and process [command-line arguments](xref:command-line-interface).

# Creating A Bootstrapper

A bootstrapper can be created using the static `Bootstrapper.Factory` property:

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
        .RunAsync();
  }
}
```

When using Statiq Framework you can create a bootstrapper with the `.CreateDefault(args)` method on the `Factory`. Other Statiq projects like [Statiq Web](xref:web) often have other factory methods like `.CreateWeb(args)`.

The bootstrapper exposes a fluent API very similar to how ASP.NET Core and other modern .NET projects are provisioned.

# Default Behavior

The bootstrapper does a lot by default when you call `.CreateDefault(args)`. However, if you'd rather have more control over any of the default behavior, you can call a different creation method:

- `Create(string[] args)` will create the bootstrapper without any default behavior and it will be up to you to add any default behavior you want using other fluent methods.
- `CreateDefault(string[] args, DefaultFeatures features)` will create a bootstrapper with only the default features you specify.
- `CreateDefaultWithout(string[] args, DefaultFeatures features)` will create a bootstrapper with all the default features _except_ the ones that you specify.

The default behavior of the bootstrapper is controlled by `DefaultFeatures` flags:

- `None`: No default features will be added.
- `BootstrapperConfigurators`: Adds all implementations of `IConfigurator<Bootstrapper>` and `IConfigurator<IBootstrapper>` (see [configurators](xref:configurators)).
- `Logging`: Adds default logging providers such as the console logger.
- `Settings`: Sets default values for certain [settings](xref:settings).
- `EnvironmentVariables`: Adds all environment variables as settings.
- `ConfigurationFiles`: Adds support for [configuration files](xref:settings#configuration-files).
- `BuildCommands`: Adds the default `pipelines` and `deploy` [commands](xref:commands) as well as all `ICommand` implementations.
- `CustomCommands`: Adds all `ICommand` implementations without also adding the `pipelines` and `deploy` [commands](xref:commands).
- `Shortcodes`: Adds all [shortcodes](xref:shortcodes).
- `Namespaces`: Adds all namespaces from all referenced assemblies to the `Namespaces` collection in the [engine](xref:execution#engine).
- `Pipelines`: Adds all [pipelines](xref:pipelines-and-modules) in the entry assembly.
- `All`: Configures all default features.

## Avoiding Default Behavior in Statiq Web <?# WebBadge /?>

When you create a bootstrapper using the `Bootstrapper.Factory.CreateWeb(args)` extension method, it implies that the default bootstrapper behavior should be included and automatically configures all default behavior for Statiq Web. If you'd rather have more control over bootstrapper default behavior, you can use the `AddWeb()` extension method after creating a standard bootstrapper with something like `.CreateDefaultWithout()` instead.

For example, this creates a bootstrapper without adding pipelines from the entry assembly automatically:

```csharp
using System;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Web;

namespace MySite
{
  public class Program
  {
    public static async Task<int> Main(string[] args) =>
      await Bootstrapper
        .Factory
        .CreateDefaultWithout(args, DefaultFeatures.Pipelines)
        .AddWeb()
        .RunAsync();
  }
}
```