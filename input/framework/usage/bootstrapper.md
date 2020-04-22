The bootstrapper helps configure an engine, add modules and pipelines to it, and process command-line arguments.

The `Bootstrapper` is defined in [Statiq.App](https://www.nuget.org/packages/Statiq.App) and helps create an [engine](xref:execution#engine) and has fluent methods to configure it, add modules and pipelines, and process command-line arguments.

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

When using Statiq Framework you can create the boostrapper with the `.CreateDefault(args)` method on the `Factory`. Other Statiq projects like [Statiq Web](/web) often have other factory methods like `.CreateWeb(args)`.

# Command Line and Commands

One of the more important responsibilities of the `Bootstrapper` is to manage the command-line interface by processing arguments and executing commands.

# Defining Pipelines

# Settings